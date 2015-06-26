#region

using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Threading;

#endregion

namespace PeekPoker.Interface
{
    /// <summary>
    ///     Real Time Memory Access Class using xbdm
    ///     NB: Large dump speed depends on the version of xbdm you have
    ///     and TCP is closed after each function.
    /// </summary>
    public class RealTimeMemory
    {
        #region Eventhandlers/DelegateHandlers

        /// <summary>
        /// Send the progress bar update to the main
        /// </summary>
        public event UpdateProgressBarHandler ReportProgress;

        #endregion

        private readonly string _ipAddress;
        private bool _connected;
        private bool _memexValidConnection;
        private RwStream _readWriter;
        private uint _startDumpLength;
        private uint _startDumpOffset;
        private bool _stopSearch;
        private TcpClient _tcp;

        #region Constructor

        /// <summary>RealTimeMemory constructor Example: Default start dump = 0xC0000000 and length = 0x1FFFFFFF</summary>
        /// <param name="ipAddress">The IP address</param>
        /// <param name="startDumpOffset">The start dump address</param>
        /// <param name="startDumpLength">The dump length</param>
        public RealTimeMemory(string ipAddress, uint startDumpOffset, uint startDumpLength)
        {
            _ipAddress = ipAddress;
            _connected = false;
            //full memory dump by default
            _startDumpOffset = startDumpOffset;
            _startDumpLength = startDumpLength;
        }

        #endregion

        #region Methods

        /// <summary>Connect to the  using port 730 using the given ip address</summary>
        /// <returns>True if connection was successful and False if not</returns>
        public bool Connect()
        {
            try
            {
                if (_ipAddress.Length < 5)
                    throw new Exception("Invalid IP");
                if (_connected) return true; //If you are already connected then return
                _tcp = new TcpClient(); //New Instance of TCP
                //Connect to the specified host using port 730
                _tcp.Connect(_ipAddress, 730);
                _tcp.ReceiveTimeout = 5000; //1sec
                var response = new byte[1024];
                _tcp.Client.Receive(response);
                string reponseString = Encoding.ASCII.GetString(response).Replace("\0", "");
                //validate connection
                _connected = reponseString.Substring(0, 3) == "201";

                return _connected;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>Poke the Memory</summary>
        /// <param name="memoryAddress">The memory address to Poke Example:0xCEADEADE - Uses *.FindOffset</param>
        /// <param name="value">The value to poke Example:000032FF (hex string)</param>
        public void Poke(string memoryAddress, string value)
        {
            Poke(Convert(memoryAddress), value);
        }

        /// <summary>Poke the Memory</summary>
        /// <param name="memoryAddress">The memory address to Poke Example:0xCEADEADE - Uses *.FindOffset</param>
        /// <param name="value">The value to poke Example:000032FF (hex string)</param>
        public void Poke(uint memoryAddress, string value)
        {
            if (!Functions.IsHex(value))
                throw new Exception("Not a valid Hex String!");
            if (!Connect()) return; //Call function - If not connected return
            try
            {
                WriteMemory(memoryAddress, value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
            }
        }

        /// <summary>Peek into the Memory</summary>
        /// <param name="startDumpAddress">The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength">The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress">The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize">The byte size to peek Example: "0x4" or "4"</param>
        /// <returns>Return the hex string of the value</returns>
        public string Peek(string startDumpAddress, string dumpLength, string memoryAddress, string peekSize)
        {
            return Peek(Convert(startDumpAddress), Convert(dumpLength), Convert(memoryAddress), ConvertSigned(peekSize));
        }

        /// <summary>Peek into the Memory</summary>
        /// <param name="startDumpAddress">The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength">The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress">The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize">The byte size to peek Example: "0x4" or "4"</param>
        /// <returns>Return the hex string of the value</returns>
        private string Peek(uint startDumpAddress, uint dumpLength, uint memoryAddress, int peekSize)
        {
            uint total = (memoryAddress - startDumpAddress);
            if (memoryAddress > (startDumpAddress + dumpLength) || memoryAddress < startDumpAddress)
                throw new Exception("Memory Address Out of Bounds");

            if (!Connect()) return null; //Call function - If not connected return
            if (!GetMeMex(startDumpAddress, dumpLength))
                return null; //call function - If not connected or if somethign wrong return

            var readWriter = new RwStream();
            try
            {
                var data = new byte[1026]; //byte chuncks

                //Writing each byte chuncks========
                for (int i = 0; i < dumpLength/1024; i++)
                {
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, 1024);
                    ReportProgress(0, (int) (dumpLength/1024), (i + 1), "Dumping Memory...");
                }
                //Write whatever is left
                var extra = (int) (dumpLength%1024);
                if (extra > 0)
                {
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, extra);
                }
                readWriter.Flush();
                readWriter.Position = total;
                byte[] value = readWriter.ReadBytes(peekSize);
                return Functions.ToHexString(value);
            }
            catch (SocketException se)
            {
                readWriter.Flush();
                readWriter.Position = total;
                byte[] value = readWriter.ReadBytes(peekSize);
                throw new Exception(se.Message);
                return Functions.ToHexString(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                readWriter.Close(true);
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        /// <summary>
        /// Find pointer offset
        /// </summary>
        /// <param name="pointer"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BindingList<SearchResults> FindHexOffset(string pointer)
        {
            _stopSearch = false;
            if (pointer == null)
                throw new Exception("Empty Search string!");
            if (!Functions.IsHex(pointer))
                throw new Exception(string.Format("{0} is not a valid Hex string.", pointer));
            if (!Connect()) return null; //Call function - If not connected return
            if (!GetMeMex()) return null; //call function - If not connected or if something wrong return
            BindingList<SearchResults> values;
            try
            {
                //LENGTH or Size = Length of the dump
                uint size = _startDumpLength;
                _readWriter = new RwStream();
                _readWriter.ReportProgress += ReportProgress;
                var data = new byte[1026]; //byte chuncks

                //Writing each byte chuncks========
                //No need to mess with it :D
                for (int i = 0; i < size/1024; i++)
                {
                    if (_stopSearch)
                        return new BindingList<SearchResults>();
                    _tcp.Client.Receive(data);
                    _readWriter.WriteBytes(data, 2, 1024);
                    ReportProgress(0, (int) (size/1024), (i + 1), "Dumping Memory...");
                }
                //Write whatever is left
                var extra = (int) (size%1024);
                if (extra > 0)
                {
                    if (_stopSearch)
                        return new BindingList<SearchResults>();
                    _tcp.Client.Receive(data);
                    _readWriter.WriteBytes(data, 2, extra);
                }
                _readWriter.Flush();
                //===================================
                //===================================
                if (_stopSearch)
                    return new BindingList<SearchResults>();
                _readWriter.Position = 0;
                values = _readWriter.SearchHexString(Functions.StringToByteArray(pointer),
                                                     _startDumpOffset);
                return values;
            }
            catch (SocketException)
            {
                _readWriter.Flush();
                //===================================
                //===================================
                if (_stopSearch)
                    return new BindingList<SearchResults>();
                _readWriter.Position = 0;
                values = _readWriter.SearchHexString(Functions.StringToByteArray(pointer),
                                                     _startDumpOffset);

                return values;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _readWriter.Close(true);
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
                ReportProgress(0, 100, 0);
            }
        }

        /// <summary>
        /// Dump the memory
        /// </summary>
        /// <param name="filename">The file to save to</param>
        /// <param name="startDumpAddress">The start dump address</param>
        /// <param name="dumpLength">The dump length</param>
        public void Dump(string filename, string startDumpAddress, string dumpLength)
        {
            Dump(filename, Functions.Convert(startDumpAddress), Functions.Convert(dumpLength));
        }

        /// <summary>
        /// Dump the memory
        /// </summary>
        /// <param name="filename">The file to save to</param>
        /// <param name="startDumpAddress">The start dump address</param>
        /// <param name="dumpLength">The dump length</param>
        public void Dump(string filename, uint startDumpAddress, uint dumpLength)
        {
            if (!Connect()) return; //Call function - If not connected return
            if (!GetMeMex(startDumpAddress, dumpLength))
                return; //call function - If not connected or if something wrong return

            var readWriter = new RwStream(filename);
            try
            {
                var data = new byte[1026]; //byte chuncks
                //Writing each byte chuncks========
                for (int i = 0; i < dumpLength/1024; i++)
                {
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, 1024);
                    ReportProgress(0, (int) (dumpLength/1024), (i + 1), "Dumping Memory...");
                }
                //Write whatever is left
                var extra = (int) (dumpLength%1024);
                if (extra > 0)
                {
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, extra);
                }
                readWriter.Flush();
            }
            catch (SocketException)
            {
                readWriter.Flush();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                readWriter.Close(false);
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        /// <summary>Send a freeze command to the xbox</summary>
        public void StopCommand()
        {
            try
            {
                if (!Connect()) return; //Call function - If not connected return
                var response = new byte[1024];
                //Send a stop command to the xbox - freeze
                _tcp.Client.Send(Encoding.ASCII.GetBytes(string.Format("STOP\r\n")));
                _tcp.Client.Receive(response);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        /// <summary>Send a start command to the xbox</summary>
        public void StartCommand()
        {
            try
            {
                if (!Connect()) return; //Call function - If not connected return
                var response = new byte[1024];
                //Send a start command to the xbox - resume
                _tcp.Client.Send(Encoding.ASCII.GetBytes(string.Format("GO\r\n")));
                _tcp.Client.Receive(response);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        #region Private

        private void WriteMemory(uint address, string data)
        {
            int sent = 0;
            try
            {
                // Send the setmem command
                _tcp.Client.Send(
                    Encoding.ASCII.GetBytes(string.Format("SETMEM ADDR=0x{0} DATA={1}\r\n", address.ToString("X2"), data)));
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock ||
                    ex.SocketErrorCode == SocketError.IOPending ||
                    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                {
                    // socket buffer is probably full, wait and try again
                    Thread.Sleep(30);
                }
                else
                    throw new Exception(ex.Message + " - " + sent); // any serious error occurr
            }
        }

        private bool GetMeMex()
        {
            return GetMeMex(_startDumpOffset, _startDumpLength);
        }

        private bool GetMeMex(uint startDump, uint length)
        {
            if (_memexValidConnection) return true;
            //ADDR=0xDA1D0000 - The start offset in the physical memory I want the dump to start
            //LENGTH = Length of the dump
            _tcp.Client.Send(
                Encoding.ASCII.GetBytes(string.Format("GETMEMEX ADDR={0} LENGTH={1}\r\n", startDump, length)));
            var response = new byte[1024];
            _tcp.Client.Receive(response);
            string reponseString = Encoding.ASCII.GetString(response).Replace("\0", "");
            //validate connection
            _memexValidConnection = reponseString.Substring(0, 3) == "203";
            return _memexValidConnection;
        }

        private static uint Convert(string value)
        {
            if (value.Contains("0x"))
                return System.Convert.ToUInt32(value.Substring(2), 16);
            return System.Convert.ToUInt32(value, 16);
        }

        private static int ConvertSigned(string value)
        {
            if (value.Contains("0x"))
                return System.Convert.ToInt32(value.Substring(2), 16);
            return System.Convert.ToInt32(value, 16);
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>Set or Get the start dump offset</summary>
        public uint DumpOffset
        {
            set { _startDumpOffset = value; }
        }

        /// <summary>Set or Get the dump length</summary>
        public uint DumpLength
        {
            set { _startDumpLength = value; }
            get { return _startDumpLength; }
        }

        /// <summary>
        /// Stop any searching
        /// </summary>
        public bool StopSearch
        {
            get
            {
                if (!_readWriter.Accessed) return false;
                return _readWriter.StopSearch;
            }
            set
            {
                if (!_readWriter.Accessed) return;
                _readWriter.StopSearch = value;
                _stopSearch = value;
            }
        }

        #endregion
    }
}
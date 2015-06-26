using System;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace PeekPoker
{
    /// <summary>Contains function/s that deals with I/O reading and writing of Data</summary>
    public class RWStream
    {
        #region Eventhandlers/DelegateHandlers
        public event UpdateProgressBarHandler ReportProgress;
        #endregion

        private bool _accessed;
        private BinaryReader _bReader;
        private BinaryWriter _bWriter;
        private string _fileName;
        private Stream _fStream;
        private readonly bool _isBigEndian;
        private bool _stopSearch;

        #region RwStream Constructors
        /// <summary>Makes a temporary file Stream</summary>
        public RWStream()
        {
            try
            {
                _fileName = Path.GetTempPath() + Guid.NewGuid() + ".ISOLib";
                _fStream = new FileStream(_fileName,FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                _bReader = new BinaryReader(_fStream);
                _bWriter = new BinaryWriter(_fStream);
                _isBigEndian = true;
                _accessed = true;
                _stopSearch = false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public RWStream(string filename)
        {
            try
            {
                _fileName = filename;
                _fStream = new FileStream(_fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                _bReader = new BinaryReader(_fStream);
                _bWriter = new BinaryWriter(_fStream);
                _isBigEndian = true;
                _accessed = true;
                _stopSearch = false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Methods

        /// <summary>Clears buffer by Flushing and Closes theI/O Stream</summary>
        /// <param name="delete">Delete file </param>
        public void Close(bool delete)
        {
            try
            {
                if (_accessed)
                {
                    Flush();
                    _bWriter.Close();
                    _bWriter = null;
                    _bReader.Close();
                    _bReader = null;
                    _fStream.Close();
                    _fStream = null;
                    _accessed = false;
                    if (_fileName != null)
                    {
                        if(delete)File.Delete(_fileName);
                        _fileName = null;
                    }
                        Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        /// <summary>Release all resources used by reader and writer stream</summary>
        private void Dispose()
        {
            try
            {
                if (_accessed)
                {
                    if (_bReader != null)
                    {
                        _bReader.BaseStream.Dispose();
                    }
                    if (_bWriter != null)
                    {
                        _bWriter.BaseStream.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        /// <summary>Clears buffer for this stream and any unbuffered data will be written</summary>
        public void Flush()
        {
            try
            {
                _bReader.BaseStream.Flush();
                _bWriter.BaseStream.Flush();
                _fStream.Flush();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region Reader
        /// <summary>Reads a set size of bytes</summary>
        /// <param name="length">The byte array length</param>
        /// <returns>Byte Array</returns>
        public byte[] ReadBytes(int length)
        {
            return ReadBytes(length, _isBigEndian);
        }

        /// <summary>Reads a set size of bytes</summary>
        /// <param name="length">The byte array length</param>
        /// <param name="isBigEndien">Specifiy if read is in Big Endien Type</param>
        private byte[] ReadBytes(int length, bool isBigEndien)
        {
            try
            {
                if (Position == Length)
                    throw new Exception("Cannot move position past file size");
                if (length == 0)
                    return new byte[0];
                var buffer = new byte[length];
                _fStream.Read(buffer, 0, length);
                if(!isBigEndien)
                    Array.Reverse(buffer);
                return buffer;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public BindingList<Types.SearchResults> SearchHexString(byte[] pattern, uint startDumpOffset)
        {
            byte[] buffer = ReadBytes((int)Length, _isBigEndian);
            var positions = new BindingList<Types.SearchResults>();
            var i = Array.IndexOf(buffer, pattern[0], 0);
            int x = 1;
            while (i >= 0 && i <= buffer.Length - pattern.Length)
            {
                if (_stopSearch) return positions;
                ReportProgress(0, buffer.Length, i, "Searching...");

                var segment = new byte[pattern.Length];
                Buffer.BlockCopy(buffer, i, segment, 0, pattern.Length);
                if (segment.SequenceEqual(pattern))
                {
                    var results = new Types.SearchResults();
                    results.Offset = Functions.Functions.ToHexString(Functions.Functions.UInt32ToBytes(startDumpOffset + (uint)i));
                    results.Value = Functions.Functions.ByteArrayToString(segment);
                    results.ID = x.ToString();
                    positions.Add(results);
                    if (_stopSearch) return positions;
                    i = Array.IndexOf(buffer, pattern[0], i + pattern.Length);
                    x++;
                }
                else 
                {
                    if (_stopSearch) return positions;
                    i = Array.IndexOf(buffer, pattern[0], i + 1);
                }
                if (_stopSearch) return positions;
            }
            return positions;
        }
        #endregion

        #region Writer
        /// <summary>Writes a region of a byte array to the current stream</summary>
        /// <param name="buffer">A byte array containing the data to write</param>
        /// <param name="index">The starting point to start writing</param>
        /// <param name="count">The amount of bytes to write</param>
        public void WriteBytes(byte[] buffer, int index, int count)
        {
            try
            {
                WriteBytes(buffer, index, count, _isBigEndian);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>Writes a region of a byte array to the current stream</summary>
        /// <param name="buffer">A byte array containing the data to write</param>
        /// <param name="index">The starting point to start writing</param>
        /// <param name="count">The amount of bytes to write</param>
        /// <param name="isBigEndian">If the Endien type is Big</param>
        private void WriteBytes(byte[] buffer, int index, int count, bool isBigEndian)
        {
            try
            {
                if (!isBigEndian)
                    Array.Reverse(buffer);
                _bWriter.Write(buffer, index, count);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion
        
        #region Properties
        /// <summary>Get/Set if current initialization length</summary>
        private long Length
        {
            get
            {
                return _fStream.Length;
            }
        }
        /// <summary>Get/Set if current initialization position</summary>
        public long Position
        {
            private get
            {
                return _fStream.Position;
            }
            set
            {
                try
                {
                    if ((value != _fStream.Position) || (value <= _fStream.Length))
                    {
                        _fStream.Position = value;
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }
        }
        public bool Accessed
        {
            get { return _accessed; }
        }
        public bool StopSearch
        {
            get { return _stopSearch; }
            set { _stopSearch = value; }
        }
        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Be.Windows.Forms;
using PeekPoker.Interface;

namespace PeekPoker.PeekNPoke
{
    public partial class PeekNPoke : Form
    {
        #region variables

        private readonly AutoCompleteStringCollection _data = new AutoCompleteStringCollection();
        private readonly RealTimeMemory _rtm;
        private byte[] _old;

        public event ShowMessageBoxHandler ShowMessageBox;

        public event EnableControlHandler EnableControl;

        public event GetTextBoxTextHandler GetTextBoxText;

        public event SetTextBoxTextDelegateHandler SetTextBoxText;

        public event UpdateProgressBarHandler UpdateProgressbar;

        #endregion variables

        public PeekNPoke(RealTimeMemory rtm)
        {
            InitializeComponent();
            _rtm = rtm;
            ChangeNumericMaxMin();
        }

        #region Handlers

        private void FreezeButtonClick(object sender, EventArgs e)
        {
            try
            {
                _rtm.StopCommand();
                unfreezeButton.Enabled = true;
                freezeButton.Enabled = false;
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                unfreezeButton.Enabled = false;
                freezeButton.Enabled = true;
            }
        }

        private void UnfreezeButtonClick(object sender, EventArgs e)
        {
            try
            {
                _rtm.StartCommand();
                unfreezeButton.Enabled = false;
                freezeButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                unfreezeButton.Enabled = true;
                freezeButton.Enabled = false;
            }
        }

        private void HexBoxSelectionStartChanged(object sender, EventArgs e)
        {
            ChangeNumericValue(); //When you select an offset on the hexbox

            byte[] prev = Functions.HexToBytes(peekPokeAddressTextBox.Text);
            int address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format((address + (int) hexBox.SelectionStart).ToString("X8"));
        }

        private void IsSignedCheckedChanged(object sender, EventArgs e)
        {
            ChangeNumericMaxMin();
            ChangeNumericValue();
        }

        private void NumericIntKeyPress(object sender, KeyPressEventArgs e)
        {
            if (hexBox.ByteProvider != null)
            {
                ChangedNumericValue(sender);
            }
        }

        private void NumericValueChanged(object sender, EventArgs e)
        {
            if (hexBox.ByteProvider != null)
            {
                ChangedNumericValue(sender);
            }
        }

        private void FixTheAddresses(object sender, EventArgs e)
        {
            var Sender = sender as TextBox;
            {
                if (Sender != null)
                    try
                    {
                        if (Sender.Text == "") //If the users wiped the box we fill it with 4(00), an empty box is bad.
                        {
                            Sender.Text = "00000000";
                            return;
                        }

                        if (Sender == peekPokeAddressTextBox) //Address specific formatting. [32 Bit Address, no "0x"]
                        {
                            string math = Sender.Text.Contains("+") ? "+" : "-";
                                //Checks for addition or subtraction symbol, defaults to subtract which is harmless if its not there.
                            Sender.Text = Sender.Text.ToUpper().StartsWith("0X")
                                              ? (Sender.Text.ToUpper().Substring(2).Trim())
                                              : Sender.Text.ToUpper().Trim();
                                //If has 0x remove it, set to upper and traim spaces.
                            string[] adrsample = Sender.Text.Split(Convert.ToChar(math));
                            //Now we check for addition commands
                            if (adrsample.Length >= 2)
                            {
                                var adrhex = ((uint) new UInt32Converter().ConvertFromString("0x" + adrsample[0]));
                                    //Formats address to have 4 bytes and be hex.
                                if (!adrsample[1].Contains("0x"))
                                    adrsample[1] = ("0x" + adrsample[1]); //Preps for conversion.
                                var adrhex2 = ((uint) new UInt32Converter().ConvertFromString(adrsample[1]));
                                    //Formats address to have 4 bytes and be hex.
                                Sender.Text = math == "+"
                                                  ? (adrhex + adrhex2).ToString("X8")
                                                  : (adrhex - adrhex2).ToString("X8");
                            }

                            if (!Functions.IsHex(Sender.Text))
                                //Last check to see if its usable, if not the users an idiot.
                            {
                                while (Sender.Text.Length < 8) //pad out the address
                                {
                                    Sender.Text = ("0" + Sender.Text);
                                }
                                //Sender.Text = (Sender.Text.ToString("X8"));
                                //ShowMessageBox("Input must be hex.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            return; //End of Addressbox Specific code
                        }
                        if (!Functions.IsHex(Sender.Text))
                        {
                            Sender.Text = Sender.Text.ToUpper().StartsWith("0X")
                                              ? (Sender.Text.ToUpper().Substring(2))
                                              : ((uint) new UInt32Converter().ConvertFromString(Sender.Text)).ToString(
                                                  "X");
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessageBox(ex.Message, "PeekNPoke", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
        }

        private void PeekButtonClick(object sender, EventArgs e)
        {
            AutoComplete(); //run function
            ThreadPool.QueueUserWorkItem(Peek);
        }

        private void PokeButtonClick(object sender, EventArgs e)
        {
            AutoComplete(); //run function
            ThreadPool.QueueUserWorkItem(Poke);
        }

        private void NewPeekButtonClick(object sender, EventArgs e)
        {
            NewPeek();
        }

        private void NewPeek()
        {
            //Clean up
            peekPokeAddressTextBox.Text = "C0000000";
            peekLengthTextBox.Text = "FF";
            SelAddress.Clear();
            peekPokeFeedBackTextBox.Clear();
            NumericInt8.Value = 0;
            NumericInt16.Value = 0;
            NumericInt32.Value = 0;
            NumericFloatTextBox.Text = "0";
            hexBox.ByteProvider = null;
            hexBox.Refresh();
        }

        private void PeekNPokeLoad(object sender, EventArgs e)
        {
            ChangeNumericMaxMin();
        }

        private void HexBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                hexBox.CopyHex();
                e.SuppressKeyPress = true;
            }
        }

        private void HexBoxMouseUp(object sender, MouseEventArgs e)
        {
            ChangeNumericValue(); //When you select an offset on the hexbox

            if (hexBox.ByteProvider == null) return;
            byte[] prev = Functions.HexToBytes(peekPokeAddressTextBox.Text);
            int address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format((address + (int) hexBox.SelectionStart).ToString("X8"));
        }

        #endregion Handlers

        #region Functions

        private void ChangeNumericMaxMin()
        {
            if (isSigned.Checked)
            {
                NumericInt8.Maximum = SByte.MaxValue;
                NumericInt8.Minimum = SByte.MinValue;
                NumericInt16.Maximum = Int16.MaxValue;
                NumericInt16.Minimum = Int16.MinValue;
                NumericInt32.Maximum = Int32.MaxValue;
                NumericInt32.Minimum = Int32.MinValue;
            }
            else
            {
                NumericInt8.Maximum = Byte.MaxValue;
                NumericInt8.Minimum = Byte.MinValue;
                NumericInt16.Maximum = UInt16.MaxValue;
                NumericInt16.Minimum = UInt16.MinValue;
                NumericInt32.Maximum = UInt32.MaxValue;
                NumericInt32.Minimum = UInt32.MinValue;
            }
        }

        private void ChangeNumericValue()
        {
            if (hexBox.ByteProvider == null) return;
            List<byte> buffer = hexBox.ByteProvider.Bytes;
            if (isSigned.Checked)
            {
                NumericInt8.Value = (buffer.Count - hexBox.SelectionStart) > 0
                                        ? Functions.ByteToSByte(hexBox.ByteProvider.ReadByte(hexBox.SelectionStart))
                                        : 0;
                NumericInt16.Value = (buffer.Count - hexBox.SelectionStart) > 1
                                         ? Functions.BytesToInt16(
                                             buffer.GetRange((int) hexBox.SelectionStart, 2).ToArray())
                                         : 0;
                NumericInt32.Value = (buffer.Count - hexBox.SelectionStart) > 3
                                         ? Functions.BytesToInt32(
                                             buffer.GetRange((int) hexBox.SelectionStart, 4).ToArray())
                                         : 0;

                NumericFloatTextBox.Clear();
                float f = (buffer.Count - hexBox.SelectionStart) > 3
                              ? Functions.BytesToSingle(buffer.GetRange((int) hexBox.SelectionStart, 4).ToArray())
                              : 0;
                NumericFloatTextBox.Text = f.ToString();
            }
            else
            {
                NumericInt8.Value = (buffer.Count - hexBox.SelectionStart) > 0
                                        ? buffer[(int) hexBox.SelectionStart]
                                        : 0;
                NumericInt16.Value = (buffer.Count - hexBox.SelectionStart) > 1
                                         ? Functions.BytesToUInt16(
                                             buffer.GetRange((int) hexBox.SelectionStart, 2).ToArray())
                                         : 0;
                NumericInt32.Value = (buffer.Count - hexBox.SelectionStart) > 3
                                         ? Functions.BytesToUInt32(
                                             buffer.GetRange((int) hexBox.SelectionStart, 4).ToArray())
                                         : 0;

                NumericFloatTextBox.Clear();
                float f = (buffer.Count - hexBox.SelectionStart) > 3
                              ? Functions.BytesToSingle(buffer.GetRange((int) hexBox.SelectionStart, 4).ToArray())
                              : 0;
                NumericFloatTextBox.Text = f.ToString();
            }
            byte[] prev = Functions.HexToBytes(peekPokeAddressTextBox.Text);
            int address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format((address + (int) hexBox.SelectionStart).ToString("X8"));
        }

        private void ChangedNumericValue(object numfield)
        {
            if (hexBox.SelectionStart >= hexBox.ByteProvider.Bytes.Count) return;
            if (numfield.GetType() == typeof (NumericUpDown))
            {
                var numeric = (NumericUpDown) numfield;
                switch (numeric.Name)
                {
                    case "NumericInt8":
                        if (isSigned.Checked)
                        {
                            Console.WriteLine(((sbyte) numeric.Value).ToString("X2"));
                            hexBox.ByteProvider.WriteByte(hexBox.SelectionStart,
                                                          Functions.HexToBytes(((sbyte) numeric.Value).ToString("X2"))[0
                                                              ]);
                        }
                        else
                        {
                            hexBox.ByteProvider.WriteByte(hexBox.SelectionStart,
                                                          Convert.ToByte((byte) numeric.Value));
                        }
                        break;

                    case "NumericInt16":
                        for (int i = 0; i < 2; i++)
                        {
                            hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i, isSigned.Checked
                                                                                         ? Functions.Int16ToBytes(
                                                                                             (short) numeric.Value)[i]
                                                                                         : Functions.UInt16ToBytes(
                                                                                             (ushort) numeric.Value)[i]);
                        }
                        break;

                    case "NumericInt32":
                        for (int i = 0; i < 4; i++)
                        {
                            hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i, isSigned.Checked
                                                                                         ? Functions.Int32ToBytes(
                                                                                             (int) numeric.Value)[i]
                                                                                         : Functions.UInt32ToBytes(
                                                                                             (uint) numeric.Value)[i]);
                        }
                        break;
                }
            }
            else
            {
                var textbox = (TextBox) numfield;
                for (int i = 0; i < 4; i++)
                {
                    hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i,
                                                  Functions.FloatToByteArray(Convert.ToSingle(textbox.Text))[i]);
                }
            }
            hexBox.Refresh();
        }

        private void AutoComplete()
        {
            peekPokeAddressTextBox.AutoCompleteCustomSource = _data; //put the auto complete data into the textbox
            int count = _data.Count;
            for (int index = 0; index < count; index++)
            {
                string value = _data[index];
                //if the text in peek or poke text box is not in autocomplete data - Add it
                if (!ReferenceEquals(value, peekPokeAddressTextBox.Text))
                    _data.Add(peekPokeAddressTextBox.Text);
            }
        }

        #endregion Functions

        #region Thread Safe

        private void SetHexBoxByteProvider(DynamicByteProvider value)
        {
            if (hexBox.InvokeRequired)
                Invoke((MethodInvoker) (() => SetHexBoxByteProvider(value)));
            else
            {
                hexBox.ByteProvider = value;
            }
        }

        private void SetHexBoxRefresh()
        {
            if (hexBox.InvokeRequired)
                Invoke((MethodInvoker) (SetHexBoxRefresh));
            else
            {
                hexBox.Refresh();
            }
        }

        private DynamicByteProvider GetHexBoxByteProvider()
        {
            //recursion
            var returnVal = new DynamicByteProvider(new byte[] {0, 0, 0, 0});
            if (hexBox.InvokeRequired)
                hexBox.Invoke((MethodInvoker)
                              delegate { returnVal = GetHexBoxByteProvider(); });
            else
                return (DynamicByteProvider) hexBox.ByteProvider;
            return returnVal;
        }

        #endregion Thread Safe

        #region Thread Function

        private void Peek(object a)
        {
            try
            {
                EnableControl(peekButton, false);
                if (string.IsNullOrEmpty(GetTextBoxText(peekLengthTextBox)) ||
                    Convert.ToUInt32(GetTextBoxText(peekLengthTextBox), 16) == 0)
                    throw new Exception("Invalid peek length!");
                if (string.IsNullOrEmpty(GetTextBoxText(peekPokeAddressTextBox)) ||
                    Convert.ToUInt32(GetTextBoxText(peekPokeAddressTextBox), 16) == 0)
                    throw new Exception("Address cannot be 0 or null");
                //convert peek result string values to byte

                byte[] retValue =
                    Functions.StringToByteArray(_rtm.Peek(GetTextBoxText(peekPokeAddressTextBox),
                                                          GetTextBoxText(peekLengthTextBox),
                                                          GetTextBoxText(peekPokeAddressTextBox),
                                                          GetTextBoxText(peekLengthTextBox)));
                var buffer = new DynamicByteProvider(retValue) {IsWriteByte = true}; //object initilizer

                _old = new byte[buffer.Bytes.Count];
                buffer.Bytes.CopyTo(_old);

                SetHexBoxByteProvider(buffer);
                SetHexBoxRefresh();
                EnableControl(peekButton, true);

                SetTextBoxText(peekPokeFeedBackTextBox, "Peek Success!");
                UpdateProgressbar(0, 100, 0);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableControl(peekButton, true);
                UpdateProgressbar(0, 100, 0);
            }
        }

        private void Poke(object a)
        {
            try
            {
                EnableControl(pokeButton, false);
                uint dumplength = (uint) hexBox.ByteProvider.Length/2;
                _rtm.DumpOffset = Functions.Convert(GetTextBoxText(peekPokeAddressTextBox)); //Set the dump offset
                _rtm.DumpLength = dumplength; //The length of data to dump

                DynamicByteProvider buffer = GetHexBoxByteProvider();
                if (fillCheckBox.Checked)
                {
                    for (int i = 0; i < dumplength; i++)
                    {
                        uint value = Convert.ToUInt32(peekPokeAddressTextBox.Text, 16);
                        string address = string.Format((value + i).ToString("X8"));
                        _rtm.Poke(address, String.Format("{0,0:X2}", Convert.ToByte(fillValueTextBox.Text, 16)));
                        UpdateProgressbar(0, (int) (dumplength), (i + 1), "Poking Memory...");
                    }
                    SetTextBoxText(peekPokeFeedBackTextBox, "Poke Success!");
                }
                else
                {
                    for (int i = 0; i < buffer.Bytes.Count; i++)
                    {
                        if (buffer.Bytes[i] == _old[i]) continue;

                        uint value = Convert.ToUInt32(peekPokeAddressTextBox.Text, 16);
                        string address = string.Format((value + i).ToString("X8"));
                        _rtm.Poke(address, String.Format("{0,0:X2}", buffer.Bytes[i]));
                        SetTextBoxText(peekPokeFeedBackTextBox, "Poke Success!");
                    }
                }
                UpdateProgressbar(0, 100, 0);
                EnableControl(pokeButton, true);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableControl(pokeButton, true);
            }
        }

        #endregion Thread Function
    }
}
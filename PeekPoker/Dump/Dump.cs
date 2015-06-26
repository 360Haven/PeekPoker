using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using PeekPoker.Interface;

namespace PeekPoker.Dump
{
    public partial class Dump : Form
    {
        private readonly RealTimeMemory _rtm;
        private string _dumpFilePath;

        public Dump(RealTimeMemory rtm)
        {
            InitializeComponent();
            _rtm = rtm;
        }

        public event ShowMessageBoxHandler ShowMessageBox;

        public event UpdateProgressBarHandler UpdateProgressbar;

        public event EnableControlHandler EnableControl;

        public event GetTextBoxTextHandler GetTextBoxText;

        private void FixTheAddresses(object sender, EventArgs e)
        {
            try
            {
                if (!Functions.IsHex(dumpStartOffsetTextBox.Text))
                {
                    if (!dumpStartOffsetTextBox.Text.Equals(""))
                        dumpStartOffsetTextBox.Text = uint.Parse(dumpStartOffsetTextBox.Text).ToString("X");
                }

                if (Functions.IsHex(dumpLengthTextBox.Text)) return;
                if (!dumpLengthTextBox.Text.Equals(""))
                    dumpLengthTextBox.Text = uint.Parse(dumpLengthTextBox.Text).ToString("X");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, "PeekNPoke", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DumpMemoryButtonClick(object sender, EventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                _dumpFilePath = saveFileDialog.FileName;
                using (FileStream file = File.Create(_dumpFilePath))
                {
                    file.Close();
                }

                var oThread = new Thread(DumpMem);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BaseFileButtonClick(object sender, EventArgs e)
        {
            dumpStartOffsetTextBox.Text = string.Format("82000000");
            dumpLengthTextBox.Text = string.Format("05000000");
        }

        private void AllocatedDataButtonClick(object sender, EventArgs e)
        {
            dumpStartOffsetTextBox.Text = string.Format("40000000");
            dumpLengthTextBox.Text = string.Format("05000000");
        }

        private void PhysicalRamButtonClick(object sender, EventArgs e)
        {
            dumpStartOffsetTextBox.Text = string.Format("C0000000");
            dumpLengthTextBox.Text = string.Format("1FFF0FFF");
        }

        private void DumpMem()
        {
            try
            {
                EnableControl(dumpMemoryButton, false);
                _rtm.Dump(_dumpFilePath, GetTextBoxText(dumpStartOffsetTextBox), GetTextBoxText(dumpLengthTextBox));
                UpdateProgressbar(0, 100, 0);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(dumpMemoryButton, true);
                UpdateProgressbar(0, 100, 0);
                Thread.CurrentThread.Abort();
            }
        }
    }
}
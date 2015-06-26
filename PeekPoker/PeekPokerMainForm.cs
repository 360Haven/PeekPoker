using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using PeekPoker.About;
using PeekPoker.Interface;
using PeekPoker.Plugin;

namespace PeekPoker
{
    public partial class PeekPokerMainForm : Form
    {
        #region global varibales

        private List<ListViewItem> _listviewItem;
        private PluginService _pluginService;
        private RealTimeMemory _rtm; //DLL is now in the Important File Folder

        #endregion global varibales

        public PeekPokerMainForm()
        {
            InitializeComponent();
            LoadPlugins();
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
            Process.GetCurrentProcess().Kill(); //Immediately stop the process
        }

        private void Form1Load(Object sender, EventArgs e)
        {
            try
            {
                string ip = "";

                string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
                if (!(File.Exists(filePath)))
                {
                    using (FileStream str = File.Create(filePath))
                    {
                        str.Close();
                    }
                }
                using (var file = new StreamReader(filePath))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        switch (line)
                        {
                            case "#IP#":
                                ip = file.ReadLine();
                                break;
                        }
                    }
                }

                ipAddressTextBox.Text = ip;
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, "Peek Poker", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region button clicks

        private void showHideOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (optionPanel.Visible)
                optionPanel.Hide();
            else
                optionPanel.Show();
        }

        private void showHidePluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pluginPanel.Visible)
                pluginPanel.Hide();
            else
                pluginPanel.Show();
        }

        private void AboutToolStripMenuItem1Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        //The click handler for the plugins
        private void PluginClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                var item = (Button) sender; // get the menu item
                foreach (AbstractPlugin plugin in _pluginService.PluginDatas)
                {
                    if (plugin.ApplicationName != item.Name) continue;

                    //Setting Values
                    plugin.APRtm = _rtm;
                    plugin.IsMdiChild = !displayOutsideParentBox.Checked;
                    plugin.APShowMessageBox += ShowMessageBox;
                    plugin.APEnableControl += EnableControl;
                    plugin.APUpdateProgressBar += UpdateProgressbar;
                    plugin.APGetTextBoxText += GetTextBoxText;
                    plugin.APSetTextBoxText += SetTextBoxText;
                    plugin.Display(this);
                }
                foreach (AbstractPlugin plugin in _pluginService.OptionPluginDatas)
                {
                    if (plugin.ApplicationName != item.Name) continue;

                    //Setting Values
                    plugin.APRtm = _rtm;
                    plugin.IsMdiChild = !displayOutsideParentBox.Checked;
                    plugin.APShowMessageBox += ShowMessageBox;
                    plugin.APEnableControl += EnableControl;
                    plugin.APUpdateProgressBar += UpdateProgressbar;
                    plugin.APGetTextBoxText += GetTextBoxText;
                    plugin.APSetTextBoxText += SetTextBoxText;
                    plugin.Display(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //When you click on the connect button
        private void ConnectButtonClick(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(Connect);
        }

        private void ipAddressTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
                ThreadPool.QueueUserWorkItem(Connect);
        }

        //When you click on the www.360haven.com
        private void ToolStripStatusLabel2Click(object sender, EventArgs e)
        {
            Process.Start("www.360haven.com");
        }

        private void peekNpokeButton_Click(object sender, EventArgs e)
        {
            PeekNPoke.PeekNPoke form = displayOutsideParentBox.Checked
                                           ? new PeekNPoke.PeekNPoke(_rtm)
                                           : new PeekNPoke.PeekNPoke(_rtm) {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.UpdateProgressbar += UpdateProgressbar;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.SetTextBoxText += SetTextBoxText;
            form.Show();
        }

        private void dumpButton_Click(object sender, EventArgs e)
        {
            Dump.Dump form = displayOutsideParentBox.Checked
                                 ? new Dump.Dump(_rtm)
                                 : new Dump.Dump(_rtm) {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.UpdateProgressbar += UpdateProgressbar;
            form.Show();
        }

        private void SearchButtonClick(object sender, EventArgs e)
        {
            Search.Search form = displayOutsideParentBox.Checked
                                     ? new Search.Search(_rtm)
                                     : new Search.Search(_rtm) {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.UpdateProgressbar += UpdateProgressbar;
            form.Show();
        }

        private void pluginInfoButton_Click(object sender, EventArgs e)
        {
            PluginInfo.PluginInfo form = displayOutsideParentBox.Checked
                                             ? new PluginInfo.PluginInfo(_listviewItem)
                                             : new PluginInfo.PluginInfo(_listviewItem) {MdiParent = this};
            form.Show();
        }

        #endregion button clicks

        #region Functions

        private void LoadPlugins()
        {
            try
            {
                string pathToPlugins = AppDomain.CurrentDomain.BaseDirectory + "Plugins";
                if (!(Directory.Exists(pathToPlugins))) Directory.CreateDirectory(pathToPlugins);

                _pluginService = new PluginService(pathToPlugins);
                _listviewItem = new List<ListViewItem>(_pluginService.PluginDatas.Count);

                foreach (AbstractPlugin pluginData in _pluginService.PluginDatas)
                {
                    EnableControl(pluginInfoButton, true);
                    var item = new Button
                                   {
                                       Name = pluginData.ApplicationName,
                                       Tag = pluginData.ApplicationName,
                                       Text = pluginData.ApplicationName,
                                       Image = pluginData.Icon.ToBitmap(),
                                       Size = new Size(108, 75),
                                       ImageAlign = ContentAlignment.TopCenter,
                                       MaximumSize = new Size(108, 75),
                                       Dock = DockStyle.Left,
                                       TextAlign = ContentAlignment.BottomCenter,
                                   };
                    item.Click += PluginClickEventHandler;
                    pluginPanel.Controls.Add(item);

                    //Plugin Details
                    var listviewItem = new ListViewItem(pluginData.ApplicationName);
                    listviewItem.SubItems.Add(pluginData.Description);
                    listviewItem.SubItems.Add(pluginData.Author);
                    listviewItem.SubItems.Add(pluginData.Version);
                    _listviewItem.Add(listviewItem);
                }

                //Load Options
                foreach (AbstractPlugin pluginData in _pluginService.OptionPluginDatas)
                {
                    var item = new Button
                                   {
                                       Name = pluginData.ApplicationName,
                                       Tag = pluginData.ApplicationName,
                                       Text = pluginData.ApplicationName,
                                       Size = new Size(187, 33),
                                       MaximumSize = new Size(187, 33),
                                       Dock = DockStyle.Top,
                                       TextAlign = ContentAlignment.MiddleCenter,
                                   };
                    item.Click += PluginClickEventHandler;
                    mainGroupBox.Controls.Add(item);
                    MinimumSize = new Size(MinimumSize.Width, MinimumSize.Height + 33);
                    //Plugin Details
                    var listviewItem = new ListViewItem(pluginData.ApplicationName);
                    listviewItem.SubItems.Add(pluginData.Description);
                    listviewItem.SubItems.Add(pluginData.Author);
                    listviewItem.SubItems.Add(pluginData.Version);
                    _listviewItem.Add(listviewItem);
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, "Peek Poker", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Save()
        {
            string ipAddress = GetTextBoxText(ipAddressTextBox);
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
            if (!(File.Exists(filePath)))
            {
                using (FileStream str = File.Create(filePath))
                {
                    str.Close();
                }
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("#License#");
            stringBuilder.AppendLine("Accept");
            stringBuilder.AppendLine("#IP#");
            stringBuilder.AppendLine(ipAddress);

            string line = stringBuilder.ToString();
            using (var file = new StreamWriter(filePath))
            {
                file.Write(line);
            }
        }

        #endregion Functions

        #region Thread Functions

        private void Connect(object a)
        {
            if (ipAddressTextBox.Text.ToUpper() == "DEBUG") //For debugging PP without a connection to xbox
            {
                EnableControl(mainGroupBox, true);
                EnableControl(pluginPanel, true);
                return; //Bypass needing to connect to xbox for debugging purposes.
            }
            try
            {
                if (!Regex.IsMatch(ipAddressTextBox.Text, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"))
                    //Checks if valid IP
                {
                    MessageBox.Show(" IP Address is not valid!");
                    return;
                }

                Save(); //Moved to own function
                _rtm = new RealTimeMemory(ipAddressTextBox.Text, 0, 0); //initialize real time memory
                _rtm.ReportProgress += UpdateProgressbar;

                if (!_rtm.Connect())
                {
                    throw new Exception("Connection Failed!");
                }
                EnableControl(mainGroupBox, true);

                SetToolStripLabel("Connected!");
                //#CODE FOR SAVING IP IN REGISTRY#
                //Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\XenonSDK", "DefaultIP",
                //                  ipAddress.ToString(CultureInfo.InvariantCulture)); //Store IP in registry
                ShowMessageBox("Connected!", "Peek Poker", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SetTextBoxText(connectButton, "Re-Connect");
                EnableControl(pluginPanel, true);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        #endregion Thread Functions

        #region safeThreadingProperties

        //Getter Methods
        private string GetTextBoxText(Control control)
        {
            //recursion
            string returnVal = "";
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)
                               delegate { returnVal = GetTextBoxText(control); });
            else
                return control.Text;
            return returnVal;
        }

        private void EnableControl(Control control, bool value)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker) (() => EnableControl(control, value)));
            else
                control.Enabled = value;
        }

        //Setter Methods
        private void SetToolStripLabel(string value)
        {
            if (InvokeRequired)
                Invoke((MethodInvoker) (() => SetToolStripLabel(value)));
            else
            {
                statusStripLabel.Text = value;
            }
        }

        private void SetTextBoxText(Control control, string value)
        {
            if (control.InvokeRequired)
                Invoke((MethodInvoker) (() => SetTextBoxText(control, value)));
            else
            {
                control.Text = value;
            }
        }

        private void ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //Using lambda express - I believe its slower - Just an example
            if (InvokeRequired)
                Invoke((MethodInvoker) (() => ShowMessageBox(text, caption, buttons, icon)));
            else MessageBox.Show(this, text, caption, buttons, icon);
        }

        //Progress bar Delegates
        private void UpdateProgressbar(int min, int max, int value, string text = "Idle")
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke((MethodInvoker) (() => UpdateProgressbar(min, max, value, text)));
            }
            else
            {
                if (StatusProgressBar.ProgressBar != null)
                {
                    StatusProgressBar.ProgressBar.Maximum = max;
                    StatusProgressBar.ProgressBar.Minimum = min;
                    StatusProgressBar.ProgressBar.Value = value;
                }
                statusStripLabel.Text = text;
            }
        }

        #endregion safeThreadingProperties
    }
}
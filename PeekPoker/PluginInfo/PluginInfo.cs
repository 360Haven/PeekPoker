using System.Collections.Generic;
using System.Windows.Forms;

namespace PeekPoker.PluginInfo
{
    public partial class PluginInfo : Form
    {
        public PluginInfo(List<ListViewItem> items)
        {
            InitializeComponent();
            foreach (ListViewItem item in items)
            {
                pluginListView.Items.Add(item);
            }
        }
    }
}
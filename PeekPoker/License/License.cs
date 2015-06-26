using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PeekPoker.License
{
    public partial class License : Form
    {
        public License()
        {
            InitializeComponent();
        }

        private void agreeButton_Click(object sender, EventArgs e)
        {
            string ip = "";
            string line;

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

            //Save
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("#License#");
            stringBuilder.AppendLine("Accept");
            stringBuilder.AppendLine("#IP#");
            stringBuilder.AppendLine(ip);

            line = stringBuilder.ToString();
            using (var file = new StreamWriter(filePath))
            {
                file.Write(line);
            }
            var form = new PeekPokerMainForm();
            form.Show();
            Hide();
        }

        private void notAgreeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
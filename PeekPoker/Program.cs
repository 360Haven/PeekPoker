using System;
using System.IO;
using System.Windows.Forms;

namespace PeekPoker
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                string license = "";
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
                            case "#License#":
                                license = file.ReadLine();
                                break;
                        }
                    }
                }

                if (license == "Accept")
                    Application.Run(new PeekPokerMainForm());
                else
                    Application.Run(new License.License());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
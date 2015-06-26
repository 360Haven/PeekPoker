using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PeekPoker.About
{
    partial class AboutBox : Form
    {
        //TODO: Image
        public AboutBox()
        {
            InitializeComponent();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("Peek Poker - Open Source Memory Editor"));
            stringBuilder.AppendLine(string.Format("Cybersam, 8Ball, PureIso, Jappi88"));
            stringBuilder.AppendLine(string.Format("Fairchild (Committer)"));
            stringBuilder.AppendLine(string.Format("Mojobojo (Committer)"));
            stringBuilder.AppendLine(string.Format("Feudalnate(Committer)"));
            stringBuilder.AppendLine(string.Format("Natelx (xbdm)"));
            stringBuilder.AppendLine(string.Format("optantic (Tester)"));
            stringBuilder.AppendLine(string.Format("actualmanx (Tester)"));
            stringBuilder.AppendLine(string.Format("ioritree (Tester)"));
            stringBuilder.AppendLine(string.Format("Be.Windows.Forms.HexBox.dll"));
            stringBuilder.AppendLine(string.Format("360Haven"));

            Text = String.Format("About {0}", AssemblyTitle);
            labelProductName.Text = "PeekPoker";
            labelVersion.Text = String.Format("Version {0}", AssemblyVersion + " " + "Revision 8");
            labelCopyright.Text = "GNU GPL v3";
            textBoxDescription.Text = stringBuilder.ToString();
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute) attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute) attributes[0]).Company;
            }
        }

        #endregion Assembly Attribute Accessors
    }
}
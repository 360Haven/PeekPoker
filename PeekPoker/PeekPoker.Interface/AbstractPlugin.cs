using System.Drawing;
using System.Windows.Forms;

namespace PeekPoker.Interface
{
    /// <summary>
    /// The Abstract Plugin Type for Peek Poker
    /// </summary>
    public abstract class AbstractPlugin : IPlugin
    {
        #region Delegate Handlers

        /// <summary>
        /// Show MessageBox - Thread Safe
        /// </summary>
        public ShowMessageBoxHandler APShowMessageBox { get; set; }

        /// <summary>
        /// Work with the progressbar - Thread Safe
        /// </summary>
        public UpdateProgressBarHandler APUpdateProgressBar { get; set; }

        /// <summary>
        /// Enable Controls - Thread Safe
        /// </summary>
        public EnableControlHandler APEnableControl { get; set; }

        /// <summary>
        /// Get a text from a textbox - Thread Safe
        /// </summary>
        public GetTextBoxTextHandler APGetTextBoxText { get; set; }

        /// <summary>
        /// Set a text into a textbox - Thread Safe
        /// </summary>
        public SetTextBoxTextDelegateHandler APSetTextBoxText { get; set; }

        #endregion Delegate Handlers

        /// <summary>
        /// The Abstract Plugin Constructor
        /// </summary>
        protected AbstractPlugin()
        {
            ApplicationName = "Unavailable";
            Description = "Unavailable";
            Author = "Unavailable";
            Version = "Unavailable";
            Icon = null;
            PluginType = PluginType.Game;
        }

        #region Abstract Plugin Methods

        /// <summary>
        /// The real time memory property
        ///
        /// </summary>
        public RealTimeMemory APRtm { get; set; }

        /// <summary>
        /// Set or Get if the plugin loads as a MDI
        /// </summary>
        public bool IsMdiChild { get; set; }

        /// <summary>
        /// The Plugin type
        /// </summary>
        public PluginType PluginType { get; set; }

        /// <summary>
        /// Display Method - Should be overridden
        /// </summary>
        /// <param name="parent">The Peek Poker Application</param>
        public virtual void Display(Form parent)
        {
            Form form = IsMdiChild
                            ? new Form {MdiParent = parent}
                            : new Form();
            form.Show();
        }

        #endregion Abstract Plugin Methods

        #region IPlugin

        /// <summary>
        /// The Application Name
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// The application Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Application Author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The Application Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The Application Icon
        /// </summary>
        public Icon Icon { get; set; }

        #endregion IPlugin

        //Values Set From peek poker Application
    }
}
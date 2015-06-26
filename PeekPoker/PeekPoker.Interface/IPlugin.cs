#region

using System.Drawing;

#endregion

namespace PeekPoker.Interface
{
    //======================================================//
    // Interface that would allow other applications/tools  //
    // to be plugged into the peekpoker application.        //
    // =====================================================//

    /// <summary>
    /// The PeekPoker Interface
    /// </summary>
    public interface IPlugin
    {
        #region App Properties

        /// <summary>Application name</summary>
        string ApplicationName { get; set; }

        /// <summary>Application's Description</summary>
        string Description { get; set; }

        /// <summary>The Author of the application</summary>
        string Author { get; set; }

        /// <summary>The version of the application</summary>
        string Version { get; set; }

        /// <summary>The application's Icon</summary>
        Icon Icon { get; set; }

        #endregion
    }
}
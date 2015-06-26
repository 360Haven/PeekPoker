using System.Windows.Forms;

namespace PeekPoker.Interface
{
    /// <summary>
    /// Update the Main Application's progress bar
    /// </summary>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <param name="value">The current value</param>
    /// <param name="text">The text that should be  displayed in the status bar</param>
    public delegate void UpdateProgressBarHandler(int min, int max, int value, string text = "Idle");

    /// <summary>
    /// Display's a message
    /// </summary>
    /// <param name="text">The text</param>
    /// <param name="caption">The header</param>
    /// <param name="buttons">The buttons</param>
    /// <param name="icon">The Icon</param>
    public delegate void ShowMessageBoxHandler(
        string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);

    /// <summary>
    /// Get a text from a control
    /// </summary>
    /// <param name="control">The control variable name</param>
    /// <returns>The text</returns>
    public delegate string GetTextBoxTextHandler(Control control);

    /// <summary>
    /// Enable or Disable a control
    /// </summary>
    /// <param name="control">The control variable name</param>
    /// <param name="value">True to Enable and False to Disable</param>
    public delegate void EnableControlHandler(Control control, bool value);

    /// <summary>
    /// Set a text to a control
    /// </summary>
    /// <param name="control">The control variable name</param>
    /// <param name="value">The text</param>
    public delegate void SetTextBoxTextDelegateHandler(Control control, string value);
}
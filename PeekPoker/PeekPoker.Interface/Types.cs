namespace PeekPoker.Interface
{
    /// <summary>
    /// The Search Result
    /// </summary>
    public class SearchResults
    {
        /// <summary>
        /// The ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// The Offset
        /// </summary>
        public string Offset { get; set; }

        /// <summary>
        /// The value
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// The type of plugin being added to the application
    /// </summary>
    public enum PluginType
    {
        /// <summary>
        /// A game editor or a realtime editor
        /// </summary>
        Game,

        /// <summary>
        /// A form program located on the selection option
        /// </summary>
        SelectionOption,
    }
}
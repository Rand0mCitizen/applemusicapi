using AppleMusic.Common.Contracts;

namespace AppleMusic.Cli.Helpers
{
    /// <summary>
    /// Interface for search results displays
    /// </summary>
    public interface ISearchResultDisplay
    {
        /// <summary>
        /// Method used to display search result in implementations
        /// </summary>
        /// <param name="request">Request data</param>
        /// <param name="result">Result data</param>
        void Display(string request, SearchResult result);
    }
}

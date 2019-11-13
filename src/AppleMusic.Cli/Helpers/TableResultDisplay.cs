using AppleMusic.Common.Contracts;
using System;
using System.Linq;

namespace AppleMusic.Cli.Helpers
{
    /// <summary>
    /// Default display in table-like format
    /// </summary>
    public class TableResultDisplay : ISearchResultDisplay
    {
        public void Display(string request, SearchResult result)
        {
            if (result.ResultCount == 0)
            {
                Console.WriteLine($"There are no albums for {request} found.");
                return;
            }

            Console.WriteLine($"Albums for {request} ({result.ResultCount}):");
            var items = result.Items.Select(i => $"{FormatItem(i)}").ToArray();
            Console.WriteLine(string.Join(Environment.NewLine, items));
            Console.WriteLine();
        }

        private string FormatItem(SearchResultItem i)
        {
            return $"{i.ReleaseDate?.Year}\t{NormalizeName(i.CollectionName)}\t {i.TrackCount} track(s)";
        }

        private string NormalizeName(string name, int maxLength = 30)
        {
            if (name.Length < maxLength)
            {
                return name.PadRight(maxLength, ' ');
            }
            return name.Substring(0, maxLength - 3) + "...";
        }
    }
}

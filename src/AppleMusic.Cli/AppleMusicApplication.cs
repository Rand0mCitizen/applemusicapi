using System;
using System.Linq;
using AppleMusic.Cli.Helpers;

namespace AppleMusic.Cli
{
    public class AppleMusicApplication
    {
        private readonly ApiClient _apiClient;
        private readonly ISearchResultDisplay _resultDisplay;
        public AppleMusicApplication(ApiClient apiClient, ISearchResultDisplay resultDisplay)
        {
            _apiClient = apiClient;
            _resultDisplay = resultDisplay;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Please input artist's title or `quit`:");
                var line = Console.ReadLine();
                if (line.Contains("quit", StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }

                var artist = line;
                var result = _apiClient.GetAlbumsByArtistAsync(artist).GetAwaiter().GetResult();
                //_resultDisplay.Display(artist, result);
                Console.WriteLine(string.Join(',', result.Select(r => r.Title)));
            }
        }
    }
}

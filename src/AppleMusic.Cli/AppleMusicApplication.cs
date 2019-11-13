using AppleMusic.Cli.Helpers;
using AppleMusic.Common.Helpers;
using System;

namespace AppleMusic.Cli
{
    public class AppleMusicApplication
    {
        private readonly IAppleMusicClient _appleMusicClient;
        private readonly ISearchResultDisplay _resultDisplay;
        public AppleMusicApplication(IAppleMusicClient appleMusicClient, ISearchResultDisplay resultDisplay)
        {
            _appleMusicClient = appleMusicClient;
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
                var result = _appleMusicClient.GetAlbumsByArtistAsync(artist).Result;
                _resultDisplay.Display(artist, result);
            }
        }
    }
}

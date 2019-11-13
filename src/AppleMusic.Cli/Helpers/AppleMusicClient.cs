using AppleMusic.Common;
using AppleMusic.Common.Contracts;
using AppleMusic.Common.Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppleMusic.Cli.Helpers
{
    /// <summary>
    /// IAppleMusicClient client implementation
    /// </summary>
    public class AppleMusicClient : IAppleMusicClient
    {
        private readonly HttpClient _client;
        private readonly AppleMusicConfig _config;

        public AppleMusicClient(HttpClient client, AppleMusicConfig config)
        {
            _client = client;
            _config = config;
            _client.DefaultRequestHeaders.Add("User-Agent", _config.UserAgent);
        }

        /// <summary>
        /// Method to search provided artits's albums
        /// </summary>
        /// <param name="artist">Artist title</param>
        /// <returns><see cref="Task{SearchResult}"/></returns>
        public async Task<SearchResult> GetAlbumsByArtistAsync(string artist)
        {
            artist = WebUtility.UrlEncode(artist);
            var url = $"{_config.BaseUrl}&term={artist}";
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SearchResult>(content);
            return result;
        }
    }
}

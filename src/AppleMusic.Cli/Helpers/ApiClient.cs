using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AppleMusic.Domain.Model;
using Newtonsoft.Json;

namespace AppleMusic.Cli.Helpers
{
    /// <summary>
    /// IAppleMusicClient client implementation
    /// </summary>
    public class ApiClient
    {
        private readonly HttpClient _client;
        private readonly ApiConfig _config;

        public ApiClient(HttpClient client, ApiConfig config)
        {
            _client = client;
            _config = config;
        }

        /// <summary>
        /// Method to search provided artits's albums
        /// </summary>
        /// <param name="artist">Artist title</param>
        /// <returns><see cref="Task{SearchResult}"/></returns>
        public async Task<IEnumerable<Artist>> GetAlbumsByArtistAsync(string artist)
        {
            artist = WebUtility.UrlEncode(artist);
            var url = $"{_config.ApiUrl}/{artist}";
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Artist>>(content);
            return result;
        }
    }
}

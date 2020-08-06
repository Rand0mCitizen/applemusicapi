using System.Threading.Tasks;
using AppleMusic.Common.Contracts;

namespace AppleMusic.Common.Helpers
{
    /// <summary>
    /// AppluMusic service client interface
    /// </summary>
    public interface IAppleMusicClient
    {
        /// <summary>
        /// Method to search provided artits's albums
        /// </summary>
        /// <param name="artist">Artist title</param>
        /// <returns><see cref="Task{SearchResult}"/></returns>
        Task<SearchResult> GetAlbumsByArtistAsync(string artist);
    }
}

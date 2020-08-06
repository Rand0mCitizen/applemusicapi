using System.Collections.Generic;
using System.Threading.Tasks;
using AppleMusic.Domain.Model;
using AppleMusic.Store.App.Helpers.Dto;

namespace AppleMusic.Store.App.Service
{
    public interface IArtistService
    {
        Task AddAsync(string[] titles);

        Task AddAsync(string title);

        Task RemoveAsync(int id);

        Task<Artist> GetAsync(int id);

        Task<ICollection<Artist>> FindByTitleAsync(string title);

        Task<ICollection<Artist>> ListAsync();

        Task AddAlbumAsync(int artistId, AlbumDto album);
    }
}

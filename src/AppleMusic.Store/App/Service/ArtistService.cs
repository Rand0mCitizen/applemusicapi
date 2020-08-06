using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleMusic.Common.Helpers;
using AppleMusic.Domain;
using AppleMusic.Domain.Model;
using AppleMusic.Store.App.DataContext;
using AppleMusic.Store.App.Helpers.Dto;
using Microsoft.EntityFrameworkCore;

namespace AppleMusic.Store.App.Service
{
    public class ArtistService : IArtistService
    {
        private readonly EfContext _dbContext;
        private readonly IArtistRepository _artistRepository;
        private readonly IAppleMusicClient _appleMusicClient;

        public ArtistService(EfContext dataContext, IArtistRepository repository, IAppleMusicClient appleMusicClient)
        {
            _dbContext = dataContext;
            _artistRepository = repository;
            _appleMusicClient = appleMusicClient;
        }

        public async Task AddAsync(string[] titles)
        {
            var newArtists = titles.Select(t => Artist.Create(t)).ToArray();
            await _artistRepository.AddAsync(newArtists);
        }

        public async Task AddAsync(string title)
        {
            var artist = Artist.Create(title);
            await _artistRepository.AddAsync(artist);
        }

        public async Task<Artist> GetAsync(int id)
        {
            return await _artistRepository.GetAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            await _artistRepository.RemoveAsync(id);
        }

        public async Task<ICollection<Artist>> FindByTitleAsync(string title)
        {
            var temp = await GetByTitleAsyncInternal(title);
            if (!temp.Any())
            {
                var apiResponse = await _appleMusicClient.GetAlbumsByArtistAsync(title);
                var artistNames = apiResponse.Items.GroupBy(i => i.ArtistId).Select(i => i.First().ArtistName).ToArray();
                await AddAsync(artistNames);
                return await GetByTitleAsyncInternal(title);
            }
            return temp;
        }

        private async Task<ICollection<Artist>> GetByTitleAsyncInternal(string title)
        {
            return await _artistRepository.ListAsync(a => a.Title.ToLower().StartsWith(title.ToLower()));
        }

        public async Task AddAlbumAsync(int artistId, AlbumDto album)
        {
            var artist = await _artistRepository.GetAsync(artistId);

            var newAlbum = new Album() { Title = album.Title };
            newAlbum.Songs = album.Songs.Select(s => new Song { Title = s }).ToArray();

            artist.AddAlbum(newAlbum);
            await _artistRepository.UpdateAsync(artist);
        }

        public async Task<ICollection<Artist>> ListAsync()
        {
            return await _artistRepository.List().ToArrayAsync();
        }
    }
}

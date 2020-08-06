using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppleMusic.Domain.Model;
using AppleMusic.Store.App.DataContext;
using Microsoft.EntityFrameworkCore;

namespace AppleMusic.Store.App
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly EfContext _dbContext;

        public ArtistRepository(EfContext context)
        {
            _dbContext = context;
        }

        public IQueryable<Artist> List()
        {
            return _dbContext.Artists;
        }

        public async Task AddAsync(Artist artist)
        {
            _dbContext.Artists.Add(artist);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(Artist[] artists)
        {
            _dbContext.Artists.AddRange(artists);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Artist> GetAsync(int id)
        {
            return await List().Where(a => a.Id == id)
                .Include(a => a.Albums).ThenInclude(a => a.Songs)
                .FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _dbContext.Remove(_dbContext.Artists.FirstOrDefaultAsync(a => a.Id == id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Artist artist)
        {
            var entry = await _dbContext.Artists.FirstOrDefaultAsync(a => a.Id == artist.Id);
            entry.Title = artist.Title;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Artist>> ListAsync(Expression<Func<Artist, bool>> filter)
        {
            return await List().Where(filter).ToArrayAsync();
        }
    }
}

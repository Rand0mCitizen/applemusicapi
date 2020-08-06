using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppleMusic.Domain.Model;
using AppleMusic.Store.App.DataContext;
using Microsoft.EntityFrameworkCore;

namespace AppleMusic.Store
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly EfContext _dbContext;

        public ArtistRepository(EfContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Artist>> ListAsync(Expression<Func<Artist, bool>> filter)
        {
            return await _dbContext.Artists.Where(filter).ToArrayAsync();
        }

        public async Task AddAsync(Artist artist)
        {
            _dbContext.Artists.Add(artist);
            await _dbContext.SaveChangesAsync();
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
    }
}

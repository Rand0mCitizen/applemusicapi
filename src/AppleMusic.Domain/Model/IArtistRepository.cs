using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppleMusic.Domain.Model
{
    public interface IArtistRepository
    {
        Task AddAsync(Artist artist);

        Task AddAsync(Artist[] artists);

        Task<Artist> GetAsync(int id);

        Task RemoveAsync(int id);

        Task UpdateAsync(Artist artist);

        IQueryable<Artist> List();

        Task<ICollection<Artist>> ListAsync(Expression<Func<Artist, bool>> filter);
    }
}

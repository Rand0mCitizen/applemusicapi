using AppleMusic.Domain;
using AppleMusic.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace AppleMusic.Store.App.DataContext
{
    public class EfContext : DbContext
    {
        public virtual DbSet<Artist> Artists { get; set; }

        public virtual DbSet<Album> Albums { get; set; }

        public virtual DbSet<Song> Songs { get; set; }
    }
}

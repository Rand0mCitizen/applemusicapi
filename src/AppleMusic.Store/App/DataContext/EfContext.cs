using System.Reflection;
using AppleMusic.Domain;
using AppleMusic.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace AppleMusic.Store.App.DataContext
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Artist> Artists { get; set; }

        public virtual DbSet<Album> Albums { get; set; }

        public virtual DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Test.db", options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            base.OnConfiguring(optionsBuilder);
        }
    }
}

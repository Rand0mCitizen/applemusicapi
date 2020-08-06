using System;
using System.Collections.Generic;
using AppleMusic.Domain.Model;

namespace AppleMusic.Domain
{
    public class Album
    {
        public int Id { get; set; }

        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}

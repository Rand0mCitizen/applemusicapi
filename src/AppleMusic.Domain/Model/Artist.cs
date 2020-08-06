using System;
using System.Collections.Generic;
using System.Linq;

namespace AppleMusic.Domain.Model
{
    public class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
            Songs = new HashSet<Song>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

        public void AddAlbum(Album album)
        {
            Albums.Add(album);
            Array.ForEach(album.Songs.ToArray(), s => AddSong(s));
        }

        public void AddSong(Song song)
        {
            Songs.Add(song);
        }

        public static Artist Create(string title)
        {
            return new Artist
            {
                Title = title
            };
        }
    }
}

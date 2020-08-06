namespace AppleMusic.Domain.Model
{
    public class Song
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public int ArtistId { get; set; }

        public int AlbumId { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Album Album { get; set; }
    }
}

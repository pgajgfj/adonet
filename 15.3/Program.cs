using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MusicApp
{
    public class Artist
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }

    public class Country
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }

    public class Album
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int Year { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        public byte[] CoverImage { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }

    public class Track
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public TimeSpan Duration { get; set; }

        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }

    public class Playlist
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Category { get; set; }

        public int UserId { get; set; }

        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }

    public class PlaylistTrack
    {
        public int Id { get; set; }

        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

        public int TrackId { get; set; }
        public Track Track { get; set; }
    }

    public class MusicAppContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .HasRequired(a => a.Country)
                .WithMany(c => c.Artists)
                .HasForeignKey(a => a.CountryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Album>()
                .HasRequired(a => a.Artist)
                .WithMany(a => a.Albums)
                .HasForeignKey(a => a.ArtistId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Track>()
                .HasRequired(t => t.Album)
                .WithMany(a => a.Tracks)
                .HasForeignKey(t => t.AlbumId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlaylistTrack>()
                .HasKey(pt => new { pt.PlaylistId, pt.TrackId });

            modelBuilder.Entity<PlaylistTrack>()
                .HasRequired(pt => pt.Playlist)
                .WithMany(p => p.PlaylistTracks)
                .HasForeignKey(pt => pt.PlaylistId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlaylistTrack>()
                .HasRequired(pt => pt.Track)
                .WithMany(t => t.PlaylistTracks)
                .HasForeignKey(pt => pt.TrackId)
                .WillCascadeOnDelete(false);
        }
    }
}
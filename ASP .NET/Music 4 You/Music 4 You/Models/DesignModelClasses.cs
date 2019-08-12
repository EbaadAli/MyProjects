using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Assignment8.Models
{
 
    public class Artist
    {
        public Artist()
        {
            Albums = new List<Album>();
            MediaItems = new List<MediaItem>();
            Name = "";
            BirthOrStartDate = DateTime.Now;
        }

        public string BirthName { get; set; }
        public DateTime BirthOrStartDate { get; set; }
        public string Executive { get; set; }
        public string Genre { get; set; }
        public string UrlArtist { get; set; }
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; }
       
        public ICollection<Album> Albums { get; set; }
        public ICollection <MediaItem> MediaItems { get; set; }
    }

    public class UserAccounts
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string Address { get; set; }
        public double TotalSales { get; set; }
        public bool isManager { get; set; }
    }
    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }


    public class Album
    {
        public Album()
        {
            Tracks = new List<Track>();
            Artists = new List<Artist>();
            ReleaseDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Genre { get; set; }
        public string Coordinator { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string UrlAlbum { get; set; }
        
     
        public Artist Artist { get; set; }
        
        public Track Track { get; set; }

        public ICollection<Track> Tracks { get; set; }
        public ICollection<Artist> Artists { get; set; }

    }


    public class Track
    {
        public Track()
        {
            Albums = new List<Album>();
        }

        public string Clerk { get; set; }
        public string Composers { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        [StringLength(200)]
        public string AudioContentType { get; set; }
        public byte[] Audio { get; set; }
 
        public ICollection<Album> Albums { get; set; }
    }


    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MediaItem
    {
        public MediaItem()
        {
            Timestamp = DateTime.Now;

            // StringId generator
            // Code is from Mads Kristensen
            // http://madskristensen.net/post/generate-unique-strings-and-numbers-in-c

            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            StringId = string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        // For the generated identifier
        [Required, StringLength(100)]
        public string StringId { get; set; }

        // Media item
        public byte[] Content { get; set; }
        [StringLength(200)]
        public string ContentType { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        public string Caption { get; set; }
        [Required]
        public Artist Artist { get; set; }
    }
 
}

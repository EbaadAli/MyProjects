using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Assignment8.Models;

namespace Assignment8.Controllers
{
    public class ArtistAdd
    {
        
        public ArtistAdd()
        {
            BirthOrStartDate = new DateTime();
        }
        [Key]
        public int Id { get; set; }
        [Required, StringLength(200)]
        [Display(Name="Artist Birth Name")] 
        public string BirthName { get; set; }
        public string Executive { get; set; }
        [Display(Name="Genre")]
        public string Genre { get; set; }
        [Display(Name="Artist Name")]
        public string Name { get; set; }
        [Display(Name="Artist Picture")]
        public string UrlArtist { get; set; }
        [Display(Name="Artist Birth/Start Date")]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name="Genre")]
        public SelectList GenreList { get; set; }
        public int NumberOfTracks { get; set; }
        [DataType(DataType.MultilineText)]
        public string Profile { get; set; }
    }

    public class ArtistAddForm: ArtistAdd
    {
        [Key]
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
    }

    public class ArtistBase: ArtistAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class ArtistWithDetail : ArtistBase
    {
        public ICollection<AlbumBase> Albums { get; set; }
    }

    public class ArtistEditForm
    {

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string BirthName { get; set; }
        public string Executive { get; set; }
        public string Genre { get; set; }
        public string Name { get; set; }
        public string UrlArtist { get; set; }
        public int BirthOrStartDate { get; set; }

        public ICollection<int> AlbumId { get; set; }
    }

    public class ArtistEdit
    {
        public ArtistEdit()
        {

        }

        public ICollection<int> AlbumId { get; set; }
        public string BirthName { get; set; }
        public string Executive { get; set; }
        public string Genre { get; set; }
        public string Name { get; set; }
        public string UrlArtist { get; set; }
        public int BirthorStartDate { get; set; }
    }

    public class ArtistWithMediaID : ArtistBase
    {
        public ArtistWithMediaID()
        {
            MediaItems = new List<MediaItemBase>();
        }

        public IEnumerable<MediaItemBase> MediaItems { get; set; }
    }
}

 

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment8.Models;

namespace Assignment8.Controllers
{
    public class AlbumAdd
    {
        public AlbumAdd()
        {
     
            ReleaseDate = new DateTime();
        }

        [Display(Name = "Album Genre")]
        public string Genre { get; set; }
        [Required, StringLength(120)]
        public string Coordinator { get; set; }

        [Display(Name = "Album Name")]
        public string Name { get; set; }
  
        [Display(Name = "Album Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Album Cover Art")]
        public string UrlAlbum { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        [StringLength(500)] 
        public string Description { get; set; }
        public string ArtistName { get; set; }
    }   

    public class AlbumAddForm : AlbumAdd
    {
        [Key]
        public int ArtistId { get; set; }

        public SelectList GenreList { get; set; }

       
    }

    public class AlbumBase : AlbumAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class AlbumWithDetail : AlbumBase
    {
        [Display(Name = "Number of Tracks")]
        public int NumOfTracks { get; set; }
    }

    public class AlbumEditForm
    {
        public string Coordinator { get; set; }
        public string Genre { get; set; }
        public string Name { get; set; }
        public int ReleaseDate { get; set; }
        public string UrlAlbum { get; set; }
        public ICollection<int> AlbumId { get; set; }
    }

    public class AlbumEdit
    {
        public AlbumEdit()
        {

        }

        public int Id { get; set; }
        public string Coordinator { get; set; }
        public string Genre { get; set; }
        public string Name { get; set; }
        public int ReleaseDate { get; set; }
        public string UrlAlbum { get; set; }
        public ICollection<int> AlbumId { get; set; }
    }
}
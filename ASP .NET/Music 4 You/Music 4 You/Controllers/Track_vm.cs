using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Assignment8.Models;

namespace Assignment8.Controllers
{
    public class TrackAddForm
    {

        public TrackAddForm()
        {
            Albums = new List<Album>();
            //BirthOrStartDate = new DateTime();
        }

        [Required, StringLength(100)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Track Composers")]

        public String Composers { get; set; }

        [Display(Name = "Track Genre")]
        public String Genre { get; set; }

        [Display(Name = "Clerk")]
        public string Clerk { get; set; }

        //In this "Form" class, the property type is string, and the data type is upload
        [Required]
        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public string AudioUpload { get; set; }

        public int AlbumId { get; set; }
        public String AlbumName { get; set; }
        [Display(Name = "Track Genre")]
        public SelectList GenreList { get; set; }
        public ICollection<Album> Albums { get; set; }

    }
    public class TrackAdd
    {
        public TrackAdd()
        {
            Albums = new List<Album>();
            //BirthOrStartDate = new DateTime();
        }
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public String Composers { get; set; }
        public String Genre { get; set; }
        public string Clerk { get; set; }

        //In this "Add" class, notice the type is HttpPostedFileBase
        [Required]
        public HttpPostedFileBase AudioUpload { get; set; }
        public ICollection<Album> Albums { get; set; }
        public SelectList GenreList { get; set; }


    }
    public class TrackBase
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Track Composers")]

        public String Composers { get; set; }

        [Display(Name = "Track Genre")]
        public String Genre { get; set; }

        [Display(Name = "Clerk")]
        public string Clerk { get; set; }

        [Display(Name = "Sample File")]
        public string AudioUrl
        {
            get
            {
                return $"/audio/{Id}";
            }
        }
    }
    public class TrackAudio
    {
        public int Id { get; set; }
        public string AudioContentType { get; set; }
        public byte[] Audio { get; set; }
    }

    public class TrackWithDetail : TrackBase
    {
        public TrackWithDetail()
        {
            Albums = new List<AlbumBase>();
        }

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Clerk { get; set; }

        [StringLength(200)]
        public string Composers { get; set; }
        public string Genre { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }
        public IEnumerable<AlbumBase> Albums { get; set; }
        public int AlbumCount { get; set; }
    }

 
    public class TrackEditForm
    {
  
        public string Clerk { get; set; }
        public string Composers { get; set; }
        public SelectList Genre { get; set; }
        public int Id { get; set; }

        [Required,StringLength(120)]
        [Display(Name="Track Name")]
        public string Name { get; set; }
    }

    public class TrackEdit
    {
        public int Id { get; set; }
        public ICollection<int> AlbumId { get; set; }
        [StringLength(200)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }
        [Required, StringLength(200)]
        public string Clerk { get; set; }

        [StringLength(220)]
        public string Composers { get; set; }
        public string Genre { get; set; }
    }
}



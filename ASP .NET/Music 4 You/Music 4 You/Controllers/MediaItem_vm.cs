using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class MediaItemAddForm
    {
        public int ArtistId { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        [Display(Name = "Caption")]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "Media Item")]
        [DataType(DataType.Upload)]
        public string MediaItemUpload { get; set; }
        public string ArtistName { get; internal set; }
    }

    public class MediaItemAdd
    {
        public int ArtistId { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        public string Caption { get; set; }

        [Required]
        public HttpPostedFileBase MediaItemUpload { get; set; }
    }

    public class MediaItemContent
    {
        public int Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }

    public class MediaItemBase : MediaItemContent
    {
        public int Id { get; set; }

        [Display(Name = "Added on date/time")]
        public DateTime Timestamp { get; set; }

        // For the generated identifier
        [Required, StringLength(100)]
        [Display(Name = "Identifier")]
        public string StringId { get; set; }

        // Brief descriptive caption
        [Required, StringLength(500)]
        [Display(Name = "Caption")]
        public string Caption { get; set; }
    }
  }

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment8.Controllers
{

    public class GenreAdd
    {
        public GenreAdd()
        {

        }

        public int Id { get; set; }
        [Required,StringLength(120)]
        public string Name { get; set; }

    }
    
    public class GenreBase : GenreAdd
    {
        public GenreBase()
        {

        }

        [Key]
        public int Id { get; set; }
    }

}

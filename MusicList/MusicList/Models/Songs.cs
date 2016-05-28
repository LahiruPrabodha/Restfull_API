using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicList.Models
{
    public class Songs
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }

       
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

       // public virtual Artist Artist { get; set; }

        
    }
    
}
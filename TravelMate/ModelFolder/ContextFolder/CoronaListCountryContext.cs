using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelMate.ModelFolder.ContextFolder
{
    public class CoronaListCountryContext
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int NewConfirmed { get; set; }
        [Required]
        public int TotalConfirmed { get; set; }
        [Required]
        public int NewDeaths { get; set; }
        [Required]
        public int TotalDeaths { get; set; }
        [Required]
        public int NewRecovered { get; set; }
        [Required]
        public int TotalRecovered { get; set; }
        [Required]
        public string Date { get; set; }
    }
}

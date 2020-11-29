using System.ComponentModel.DataAnnotations;

namespace TravelMate.ModelFolder.ContextFolder
{
    public class CoronaListCountryContext
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Country { get; set; }
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
        public string Date { get; set; }
    }
}

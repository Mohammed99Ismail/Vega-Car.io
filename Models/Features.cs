using System.ComponentModel.DataAnnotations;

namespace vega.Models
{
    public class Features
    {
        public int Id { get; set; } 
        [Required]       
        public string Name { get; set; }        
    }
}
using System.ComponentModel.DataAnnotations;

namespace vega.Models.Resources
{
    public class FeatureResource
    {
        public int Id { get; set; } 
        [Required]       
        public string Name { get; set; }        
    }
}
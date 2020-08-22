using System.ComponentModel.DataAnnotations;

namespace vega.Models.Resources
{
    public class PhotoResource
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        public int vehicleId {get;set;}

    }
}
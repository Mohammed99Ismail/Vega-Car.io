using System.ComponentModel.DataAnnotations;

namespace vega.Models.Resources
{
    public class ContactResources
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Phone { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilivaCase
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = string.Empty;
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        
    }
}

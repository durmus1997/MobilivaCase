using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilivaCase
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string CustomerEmail { get; set; } = string.Empty;
        [Required]
        [StringLength(10)]
        public string CustomerGSM { get; set; } = string.Empty;
        public double TotalAmount { get; set; }
        
    }
}

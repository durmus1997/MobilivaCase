using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilivaCase
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }
        
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetBenchmark.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public required Customer Customer { get; set; }

        public required List<OrderItem> OrderItems { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalOrder { get; set; } // sum of sub total
    }
}

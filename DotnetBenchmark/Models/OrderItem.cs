using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetBenchmark.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public required Product Product { get; set; }
        public int Qty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; } // product price * qty
    }
}

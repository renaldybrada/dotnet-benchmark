namespace DotnetBenchmark.Models
{
    public class Customer
    {
        public enum EnumCustomerType
        {
            Regular,
            Premium,
            Pro
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public EnumCustomerType CustomerType { get; set; }

        // Order relations
        public List<Order> OrderList { get; set; } = new ();
    }
}

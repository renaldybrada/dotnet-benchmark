using DotnetBenchmark.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetBenchmark.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            SeedProductData(context);
            SeedCustomerData(context);
            SeedOrderData(context);
        }

        private static void SeedProductData(ApplicationDbContext context)
        {
            if (!context.Products.Any())
            {
                Random random = new Random();

                // seeding products
                List<Product> products = new List<Product>();
                for (int i = 1; i <= 1000; i++)
                {
                    products.Add(new Product
                    {
                        Name = "Product " + i,
                        Price = Math.Round((decimal)random.NextDouble() * 10000)
                    });
                }

                context.Products.AddRange(products);
                context.SaveChanges();
            }            
        }

        private static void SeedCustomerData(ApplicationDbContext context)
        {
            if (!context.Customers.Any())
            {
                Random random = new Random();

                // seeding customers
                List<Customer> customers = new List<Customer>();
                for (int i = 1; i <= 1000; i++)
                {
                    customers.Add(new Customer
                    {
                        Name = "Customer " + i,
                        Address = "Address Customer " + i,
                        CustomerType = (Customer.EnumCustomerType)random.Next(Enum.GetValues<Customer.EnumCustomerType>().Length),
                    });
                }

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        private static void SeedOrderData(ApplicationDbContext context)
        {
            if (!context.Orders.Any())
            {
                Random random = new Random();
                List<Order> OrderList = new List<Order>();

                for(int i = 1; i <= 1000; i++) // loop all customer, make some order for each customer
                {
                    var cust = context.Customers.Find(i);
                    
                    for (int o = 1; o <= random.NextInt64(1,3); o++) // loop random order amount each customer. about 1 - 3 orders per customer
                    {
                        List<OrderItem> OrderItems = new List<OrderItem>();

                        for (int p = 1; p <= random.NextInt64(1,5); p++ ) // loop random product each order. about 1 - 5 products per order
                        {
                            var product = context.Products.Find((int)random.NextInt64(1, 1000));
                            int qty = (int)random.NextInt64(1, 10);

                            OrderItems.Add(new OrderItem
                            {
                                ProductId = product.Id,
                                Product = product,
                                Qty = qty,
                                SubTotal = qty * product.Price
                            });
                        }

                        Order order = new Order
                        {
                            CustomerId = cust.Id,
                            Customer = cust,
                            OrderItems = OrderItems,
                            TotalOrder = OrderItems.Sum(x => x.SubTotal)
                        };

                        OrderList.Add(order);
                    }
                }

                context.Orders.AddRange(OrderList);
                context.SaveChanges();
            }
        }


    }
}

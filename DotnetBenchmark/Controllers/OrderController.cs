using DotnetBenchmark.Data;
using DotnetBenchmark.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetBenchmark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("sync")]
        public ActionResult GetOrder()
        {
            List<Order> orders = _context.Orders
                                        .Include(x => x.Customer)
                                        .Include(x => x.OrderItems)
                                        .ThenInclude(x => x.Product)
                                        .ToList();

            var results = orders.Select(x => new
            {
                Id = x.Id,
                CustomerName = x.Customer.Name,
                Detail = x.OrderItems.Select(y => new
                {
                    ProductName = y.Product.Name,
                    Qty = y.Qty,
                    SubTotal = y.SubTotal
                }),
                Total = x.TotalOrder
            });

            return Ok(results);
        }

        [HttpGet("async")]
        public async Task<ActionResult> GetOrderAsync()
        {
            List<Order> orders = await _context.Orders
                                        .Include(x => x.Customer)
                                        .Include(x => x.OrderItems)
                                        .ThenInclude(x => x.Product)
                                        .ToListAsync();

            var results = orders.Select(x => new
            {
                Id = x.Id,
                CustomerName = x.Customer.Name,
                Detail = x.OrderItems.Select(y => new
                {
                    ProductName = y.Product.Name,
                    Qty = y.Qty,
                    SubTotal = y.SubTotal
                }),
                Total = x.TotalOrder
            });

            return Ok(results);
        }

        [HttpGet("sync-notrack")]
        public ActionResult GetOrderNoTrack()
        {
            List<Order> orders = _context.Orders
                                        .Include(x => x.Customer)
                                        .Include(x => x.OrderItems)
                                        .ThenInclude(x => x.Product)
                                        .AsNoTracking()
                                        .ToList();

            var results = orders.Select(x => new
            {
                Id = x.Id,
                CustomerName = x.Customer.Name,
                Detail = x.OrderItems.Select(y => new
                {
                    ProductName = y.Product.Name,
                    Qty = y.Qty,
                    SubTotal = y.SubTotal
                }),
                Total = x.TotalOrder
            });

            return Ok(results);
        }

        [HttpGet("async-notrack")]
        public async Task<ActionResult> GetOrderAsyncNoTrack()
        {
            List<Order> orders = await _context.Orders
                                        .Include(x => x.Customer)
                                        .Include(x => x.OrderItems)
                                        .ThenInclude(x => x.Product)
                                        .AsNoTracking()
                                        .ToListAsync();

            var results = orders.Select(x => new
            {
                Id = x.Id,
                CustomerName = x.Customer.Name,
                Detail = x.OrderItems.Select(y => new
                {
                    ProductName = y.Product.Name,
                    Qty = y.Qty,
                    SubTotal = y.SubTotal
                }),
                Total = x.TotalOrder
            });

            return Ok(results);
        }
    }
}

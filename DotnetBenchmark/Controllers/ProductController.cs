using BenchmarkDotNet.Running;
using DotnetBenchmark.Data;
using DotnetBenchmark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenchmarkDotNet.Attributes;

namespace DotnetBenchmark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("sync")]
        [Benchmark]
        public ActionResult GetListProduct() {
            List<Product> products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("async")]
        [Benchmark]
        public async Task<ActionResult> GetListProductAsync()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("sync-notrack")]
        public ActionResult GetListProductNoTrack()
        {
            List<Product> products = _context.Products.AsNoTracking().ToList();
            return Ok(products);
        }

        [HttpGet("async-notrack")]
        public async Task<ActionResult> GetListProductAsyncNoTrack()
        {
            List<Product> products = await _context.Products.AsNoTracking().ToListAsync();
            return Ok(products);
        }
    }
}

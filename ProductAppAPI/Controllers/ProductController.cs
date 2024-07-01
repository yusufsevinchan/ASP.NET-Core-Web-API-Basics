using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAppAPI.Core;
using ProductAppAPI.Data;

namespace ProductAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        private List<ProductEntity> _products = new List<ProductEntity>
        {
            new ProductEntity { Id = 1, Name = "Kalem", Price = 5 },
            new ProductEntity { Id = 2, Name = "Defter", Price = 10 },
            new ProductEntity { Id = 3, Name = "Silgi", Price = 3 }
        };

        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> GetProducts()
        {
            if (_context == null)
            {
                return NotFound("Kayitli urun yok !");
            }

            return Ok(await _context.Products.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductEntity>> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Urun bulunamadi !");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductEntity>> AddProduct(ProductEntity product)
        {
            if (product == null)
            {
                return BadRequest("Urun bilgileri bos olamaz !");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());

        }

        [HttpPut]
        public async Task<ActionResult<ProductEntity>> UpdateProduct(ProductEntity reqx)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == reqx.Id);
            if (existingProduct == null)
            {
                return NotFound("Urun bulunamadi !");
            }

            existingProduct.Name = reqx.Name;
            existingProduct.Price = reqx.Price;
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<ProductEntity>> DeleteProduct(int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Silinecek urun bulunamadi !");
            }

            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShoppingCart.Models;

namespace MyShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly myShoppingCartContext _context;

        public ProductsController(myShoppingCartContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {

            return await _context.Product.ToListAsync();          
            //return await _context.Product.Include(cat=>cat.Category).Include(pro=>pro.CategoryId).Where(cat => cat.CategoryId == id )

            //var resProduct = await _context.Product.ToListAsync();
            //var resCategory = await _context.Category.ToListAsync();
            //var resData = (from p in resProduct
            //           join c in resCategory on p.CategoryId equals c.CategoryId
            //           select new Product
            //           {
            //               ProductName = p.ProductName,
            //               Category = c.CategoryName,
            //               Price = p.Price,
            //               Discount = p.Discount,
            //               Quantity = p.Quantity,
            //               Gst = p.Gst
            //           }).ToList();
            
            //return resData;

        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await GetProduct(product.ProductId);
            //return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}

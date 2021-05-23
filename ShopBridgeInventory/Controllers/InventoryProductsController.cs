using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBridgeInventory.Models;

namespace ShopBridgeInventory.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryProductsController : ControllerBase
    {
        private readonly ShopBridgeContext _context;

        public InventoryProductsController(ShopBridgeContext context)
        {
            _context = context;
        }

        // GET: api/InventoryProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryProducts>>> GetInventoryProducts()
        {
            var products = _context.InventoryProducts.AsQueryable();

                products = _context.InventoryProducts.Where(i => i.AvailableQuantity > 0);
            return await _context.InventoryProducts.ToListAsync();
        }

        // GET: api/InventoryProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryProducts>> GetInventoryProducts(int id)
        {
            var inventoryProducts = await _context.InventoryProducts.FindAsync(id);

            if (inventoryProducts == null)
            {
                return NotFound();
            }

            return inventoryProducts;
        }

        // PUT: api/InventoryProducts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryProducts(int id, InventoryProducts inventoryProducts)
        {
            if (id != inventoryProducts.ProductId)
            {
                return BadRequest();
            }
            inventoryProducts.LastUpdated = DateTime.Now;
            _context.Entry(inventoryProducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InventoryProducts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<InventoryProducts>> PostInventoryProducts(InventoryProducts inventoryProducts)
        {
            inventoryProducts.LastUpdated = DateTime.Now;
            _context.InventoryProducts.Add(inventoryProducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventoryProducts", new { id = inventoryProducts.ProductId }, inventoryProducts);
        }

        // DELETE: api/InventoryProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InventoryProducts>> DeleteInventoryProducts(int id)
        {
            var inventoryProducts = await _context.InventoryProducts.FindAsync(id);
            if (inventoryProducts == null)
            {
                return NotFound();
            }

            _context.InventoryProducts.Remove(inventoryProducts);
            await _context.SaveChangesAsync();

            return inventoryProducts;
        }

        private bool InventoryProductsExists(int id)
        {
            return _context.InventoryProducts.Any(e => e.ProductId == id);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using kiwiho.EFcore.MultiTenant.MixMode.DbContext;
using kiwiho.EFcore.MultiTenant.MixMode.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kiwiho.EFcore.MultiTenant.MixMode.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class ProductController : ControllerBase
    {
        private readonly StoreDbContext storeDbContext;

        public ProductController(StoreDbContext storeDbContext)
        {
            this.storeDbContext = storeDbContext;
            this.storeDbContext.Database.EnsureCreated();
            
            // this.storeDbContext.Database.Migrate();
        }

        [HttpPost("")]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            var rct = await this.storeDbContext.Products.AddAsync(product);

            await this.storeDbContext.SaveChangesAsync();

            return rct?.Entity;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get([FromRoute] int id)
        {

            var rct = await this.storeDbContext.Products.FindAsync(id);

            return rct;

        }

        [HttpGet("")]
        public async Task<ActionResult<List<Product>>> Search()
        {
            var rct = await this.storeDbContext.Products.ToListAsync();
            return rct;
        }
    }
}
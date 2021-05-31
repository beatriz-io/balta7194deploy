using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetActionResultAsync([FromServices] DataContext context)
        {
            // se eu quisesse mostrar so o id da categoria, nao iria precisar desse include, mas como a propriedade é da classe Category, precisa.
            // o include gera um join no banco
            // podemos ter varios includes
            try
            {
                var products = await context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
                return Ok(products);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível listar as categorias" });
            }

        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            try
            {
                var product = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return Ok(product);
            }
            catch (System.Exception)
            {

                return BadRequest(new { message = "Não foi possível encontrar a categoria" });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("categories/{id:int}")] // procura todos os produtos da categoria x
        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("v1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> GetTaskAsync([FromServices] DataContext context)
        {
            var employee = new User { Id = 1, Username = "robin", Password = "robin", Role = "employee" };
            var manager = new User { Id = 2, Username = "batman", Password = "batman", Role = "manager" };
            var category = new Category { Id = 1, Title = "escritorio" };
            var product = new Product { Id = 1, Category = category, Title = "mouse", Price = 12, Description = "um mouse bacana" };

            context.Users.Add(employee);
            context.Users.Add(manager);
            context.Categories.Add(category);
            context.Products.Add(product);

            await context.SaveChangesAsync();

            return Ok(new { message = "Dados configurados" });
        }
    }

}
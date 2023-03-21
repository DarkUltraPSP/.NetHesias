using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly DbContext _context;

        private DbSet<Recipe> Recipes => _context.Set<Recipe>();

        public RecipeController(DbContext context)
        {
            _context = context;
        }

        // GET: api/<RecipeController>
        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            return _context.Set<Recipe>().ToList();
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public Recipe? Get(int id)
        {
            return _context.Set<Recipe>()
                .Include(x => x.Parameters)
                .FirstOrDefault(x => x.Id == id);
        }

        // POST api/<RecipeController>
        [HttpPost]
        public void Post([FromBody] Recipe value)
        {
            value.LastModificationDate=DateTime.UtcNow;
            _context.Set<Recipe>().Add(value);
            _context.SaveChanges();
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult  Put(int id, [FromBody] string newName)
        {
            var recipe = Recipes.Find(id);
            if (recipe == null)
                return NotFound($"No recipe found with id: {id}");

            recipe.Name = newName;
            recipe.LastModificationDate=DateTime.UtcNow;
            Recipes.Update(recipe);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Recipes.Remove(new Recipe { Id = id });
        }

        [HttpPost("dupliquer/{id}")]
        public void Dupliquer([FromBody] string newName, int id)
        {
            var previousRecipeParameters = _context.Set<Parameter>()
                .Where(x => x.Recipe.Id == id)
                .ToList();
            var newParameters = previousRecipeParameters
                 .Select(x => new Parameter { Name = x.Name, Value = x.Value })
                 .ToList();
            var newRecipe = new Recipe
            {
                Name = newName,
                LastModificationDate = DateTime.UtcNow,
                Parameters = newParameters
            };
            _context.Set<Recipe>().Add(newRecipe);
            _context.SaveChanges();

        }
    }
}

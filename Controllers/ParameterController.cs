using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApplication7.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private readonly DbContext _context;

        public ParameterController(DbContext context)
        {
            _context = context;
        }
        
        // GET: api/<ParameterController>
        [HttpGet]
        public IEnumerable<Parameter> Get()
        {
            return _context.Set<Parameter>()
                .OrderBy(x=>x.Name)
                .ToList();
        }


        // GET api/<ParameterController>/5
        [HttpGet("{id}")]
        public Parameter? Get(int id)
        {
            return _context.Set<Parameter>().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<ParameterController>
        [HttpPost]
        public void Post([FromBody]Parameter value)
        {
            _context.Set<Parameter>().Add(value);
            _context.SaveChanges();
        }

        // PUT api/<ParameterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] decimal value)
        {
            var dbSet = _context.Set<Parameter>();

            var parameterToUpdate= dbSet.Find(id);

            parameterToUpdate.Value = value;

            dbSet.Update(parameterToUpdate);

            _context.SaveChanges();
        }
        
        // PUT api/<ParameterController>/5
        [HttpPut("rename/{id}")]
        public void Rename(int id, [FromBody] string newName)
        {
            var dbSet = _context.Set<Parameter>();

            var parameterToUpdate= dbSet.Find(id);

            parameterToUpdate.Name = newName;

            dbSet.Update(parameterToUpdate);

            _context.SaveChanges();
        }

        // DELETE api/<ParameterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Set<Parameter>().Remove(new Parameter{ Id = id});
            _context.SaveChanges();
        }

        [HttpGet("count")]
        public int Count()
        {
            return _context.Set<Parameter>().Count();
        }
        
        [HttpGet("contains")]
        public bool Contains(string parameterName)
        {
            return _context.Set<Parameter>().Any(x=>x.Name==parameterName);
        }
        
        [HttpGet("biggestvalue")]
        public decimal BiggestValue()
        {
            return _context.Set<Parameter>()
                .OrderByDescending(x=>(int)x.Value)
                .Select(x=>x.Value)
                .FirstOrDefault();

            _context.Set<Parameter>().Select(x => x.Value).Max();
        }
    }
}

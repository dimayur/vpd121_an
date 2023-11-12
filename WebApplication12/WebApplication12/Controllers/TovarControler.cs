using Microsoft.AspNetCore.Mvc;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    [Route("api/[Controller]")]
    public class TovarControler : Controller
    {
        private static List<Tovars> tovares = new List<Tovars>(new[] {
            new Tovars(){ Id = 1, Name = "Telephone1", Description = "Daun" },
            new Tovars(){ Id = 2, Name = "Telephone2", Description = "Daun" },
            new Tovars(){ Id = 3, Name = "Telephone3", Description = "Daun" },
            new Tovars(){ Id = 4, Name = "Telephone4", Description = "Daun" },
            new Tovars(){ Id = 5, Name = "Telephone5", Description = "Daun" },
            new Tovars(){ Id = 5, Name = "Telephone5", Description = "Daun" },
        });
        private int NextID => tovares.Count() == 0 ? 1 : tovares.Max(x => x.Id) + 1;
        [HttpGet("GetProd")]
        public int GetProd()
        {
            return NextID;
        }
        [HttpGet]
        public IEnumerable<Tovars> Get() => tovares;

        [HttpGet("Id/{id}")]
        public IActionResult Get(int id)
        {
            var Respons = tovares.SingleOrDefault(p => p.Id == id);

            if (Respons == null)
            {
                return NotFound();
            }

            return Ok(Respons);
        }
        [HttpDelete("Remove/{id}")]
        public IActionResult Delete(int id)
        {
            var RemovedString = tovares.SingleOrDefault(p => p.Id == id);
            if (RemovedString == null)
            {
                return NotFound();
            }
            tovares.Remove(RemovedString);
            return Ok();
        }
        [HttpGet("Name/{name}")]
        public IActionResult Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Invalid name parameter");
            }

            var Respons = tovares.SingleOrDefault(p => p.Name == name);

            if (Respons == null)
            {
                return NotFound();
            }

            return Ok(Respons);
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Tovars newTovar)
        {
            if (newTovar == null)
            {
                return BadRequest("Invalid request data");
            }


            newTovar.Id = NextID;


            tovares.Add(newTovar);


            return Ok(newTovar);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        public List<People> peoples = new List<People>()
        {
            new People() {id = 1, name = "Tom", age = 15},
            new People() {id = 2, name = "Bob", age = 25}
        };

        [HttpGet]
        public IActionResult GetAllPeople()
        {
            return Ok(peoples);
        }

        [HttpGet("{id}")]
        public IActionResult GetPeopleId(int id)
        {
            People people = peoples.FirstOrDefault(x => x.id == id);
            if (people == null)
                return NotFound("Увы такого чипиздрика нет");
            return Ok(people);
        }

        [HttpPost]
        public IActionResult AddPerson([FromBody] People newPeople)
        {
            if (peoples.Any(x => x.id == newPeople.id))
                return Conflict("Такой чипиздрик есть");
            peoples.Add(newPeople);
            return Created("Все гуд", newPeople);
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePeople(int id, [FromBody] People people)
        {
            People p = peoples.FirstOrDefault(x => x.id == id);
            if (p == null)
                return NotFound("Такой чипиздрик не найден для изменений");
            p = people;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePeople(int id)
        {
            People p = peoples.FirstOrDefault(x => x.id == id);
            if (p == null)
                return NotFound("Такой чипиздрик не найден для изменений");
            peoples.Remove(p);
            return NoContent();
        }

    }
}

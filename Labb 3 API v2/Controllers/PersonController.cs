using Labb_3_API_v2.Models;
using Labb_3_API_v2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb_3_API_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IRepository<Person> _personRepo;
        private IRepository<Link> _linkRepo;
        private IRepository<Interest> _interestRepo;
        public PersonController(IRepository<Person> personRepo, IRepository<Link> linkRepo, IRepository<Interest> interestRepo)
        {
            _personRepo = personRepo;
            _linkRepo = linkRepo;
            _interestRepo = interestRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            try
            {
                return Ok(await _personRepo.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to get data");
            }
            
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _personRepo.GetById(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to get data");
            }            
        }

        [HttpGet("Links")]
        public async Task<IActionResult> GetLinks(int id)
        {
            try
            {
                var links = await _linkRepo.GetAll();
                var result = new List<Link>();

                foreach (var link in links)
                {
                    if (link.PersonId == id)
                    {
                        result.Add(link);
                    }
                }
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to get data");
            }
            
        }

        [HttpGet("Interests")]
        public async Task<IActionResult> GetInterests(int id)
        {
            try
            {
                var interests = await _interestRepo.GetAll();
                var result = new List<Interest>();

                foreach (var interest in interests)
                {
                    foreach (var person in interest.Persons)
                    {
                        if (person.PersonId == id)
                        {
                            result.Add(interest);
                        }
                    }
                }
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to get data");
            }
            
        }

        //[HttpPut("{id:int}/{name}/{phone}")]
        //public async Task<IActionResult> UpdatePerson(int id, string name, string phone)
        //{
        //    try
        //    {
        //        var result = await _personRepo.GetById(id);
        //        if (result != null)
        //        {
        //            Person updatedPerson = new Person { PersonId = id, Name = name, Phone = phone };
        //            await _personRepo.Update(updatedPerson);
        //            return Ok(updatedPerson);
        //        }
        //        return NotFound();
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error to update data");
        //    }
            
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var result = await _personRepo.GetById(id);
        //        if (result != null)
        //        {
        //            await _personRepo.Delete(result);
        //            return Ok(result);
        //        }
        //        return NotFound();
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error to delete data");
        //    }
            
        //}

        [HttpPost]
        public async Task<IActionResult> CreateNewPerson(Person newPerson)
        {
            try
            {
                if(newPerson != null)
                {
                    var createdPerson = await _personRepo.Add(newPerson);
                    return CreatedAtAction(nameof(GetById), new
                    {
                        id = createdPerson.PersonId
                    }, createdPerson);
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to post data");
            }
        }
    }
}

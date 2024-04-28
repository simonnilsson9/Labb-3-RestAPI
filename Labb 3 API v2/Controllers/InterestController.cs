using Labb_3_API_v2.Models;
using Labb_3_API_v2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb_3_API_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private IRepository<Interest> _interestRepo;
        private IRepository<Person> _personRepo;

        public InterestController(IRepository<Interest> interestRepo, IRepository<Person> personRepo)
        {
            _interestRepo = interestRepo;
            _personRepo = personRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _interestRepo.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to get data");
            }
            
        }

        [HttpPut("UpdatePersonInterest")]
        public async Task<IActionResult> UpdatePersonInterest(int interestId, int personId)
        {
            try
            {
                var interestToUpdate = await _interestRepo.GetById(interestId);
                var personToUpdate = await _personRepo.GetById(personId);
                if (interestToUpdate != null)
                {
                    interestToUpdate.Persons = new List<Person>()
                    {personToUpdate};

                    await _interestRepo.Update(interestToUpdate);
                    return Ok(interestToUpdate);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to update data");
            }
        }
    }
}

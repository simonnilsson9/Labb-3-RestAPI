using Labb_3_API_v2.Models;
using Labb_3_API_v2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb_3_API_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private IRepository<Link> _linkRepo;
        
        public LinkController(IRepository<Link> linkRepo)
        {
            _linkRepo = linkRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLinks()
        {
            try
            {
                return Ok(await _linkRepo.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to get data");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateLink(string title, string url, int personId, int interestId)
        {
            try
            {
                Link linkToCreate = new Link()
                {
                    LinkName = title,
                    LinkUrl = url,
                    InterestId = interestId,
                    PersonId = personId
                };
                await _linkRepo.Add(linkToCreate);
                return Ok(linkToCreate);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}

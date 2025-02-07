using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;


        public SkillsController(DevFreelaDbContext context)
        {
            _context = context;
        }


        // GET / API/ Skills    
        [HttpGet()]
        public IActionResult GetAll()
        {
            var skills = _context.Skills.ToList();

            return Ok(skills);
        }

        //Post api/skills
        [HttpPost()]
        public IActionResult Post(CreateSkillsInputModel model)
        {
            if (model is null)
                return BadRequest();

            var skillInputModel = new Skill(model.Description);

            _context.Skills.Add(skillInputModel);
            _context.SaveChanges();

            return NoContent();
        }

    }
}

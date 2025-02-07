using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;


        public UsersController(DevFreelaDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users
                .Include(u => u.Skills)
                    .ThenInclude(us => us.Skill)
                .SingleOrDefault(u => u.Id == id);

            if (user is null)
                return NotFound();

            var model = UserViewModel.FromEntity(user);

            return Ok(model);
        }

        // POST / API / Users
        [HttpPost()]
        public IActionResult Post(CreateUserInputModel model)
        {
            if (model is null)
                return BadRequest();

            var user = new User(model.FullName, model.Email, model.BirthDate);

            _context.Users.Add(user);
            _context.SaveChanges();

            return NoContent();
        }

        // PUT / API / Users / 1 / profile - picture
        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            // Processar a imagem (guardar no banco de dados, subir pra um diretorio, s3, blob, ou outro server )

            return Ok(description);
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(UserSkillsInputModel model)
        {
            var userSkills = model.SkillsIds.Select(s => new UserSkill(model.Id, s)).ToList();

            _context.UserSkills.AddRange(userSkills);
            _context.SaveChanges();

            return NoContent();
        }

    }
}

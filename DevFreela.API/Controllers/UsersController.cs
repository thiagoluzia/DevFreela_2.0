using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUSerService _service;


        public UsersController(IUSerService service)
        {
            _service = service;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if (result is null)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        // POST / API / Users
        [HttpPost()]
        public IActionResult Post(CreateUserInputModel model)
        {
            if (model is null)
                return BadRequest();

            var result = _service.Post(model);

            if(!result.ISuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
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

            //_context.UserSkills.AddRange(userSkills);
            //_context.SaveChanges();

            return NoContent();
        }

    }
}

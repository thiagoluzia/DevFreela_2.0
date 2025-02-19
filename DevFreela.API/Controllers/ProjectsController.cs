using DevFreela.Application.Models;
using DevFreela.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly FreelanceTotalCostConfig _config;
        private readonly IProjectService _service;

        public ProjectsController(IOptions<FreelanceTotalCostConfig> options, IProjectService service)
        {
            _config = options.Value;
            _service = service;
        }


        // GET api/projects?searcha=crm
        [HttpGet()]
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var result = _service.GetAll(search, page, size);
            return Ok(result);
        }

        #region Testantdo Exceptionhandler
        // GET api/projects/1
        [HttpGet("error/{erro}")]
        public IActionResult GetByIdError()
        {
            throw new Exception();
        }
        #endregion

        // GET api/projects/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if(!result.ISuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // POST api/projects
        [HttpPost()]
        public IActionResult Post(CreateProjectInputModel model)
        {
            if (model.TotalCost < _config.Minimum || model.TotalCost > _config.Maximum)
                return BadRequest("Total cost must be between 1000 and 999999");

            var result = _service.Insert(model);

            if(!result.ISuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { Id = result.Data }, model);
        }

        // PUT apt/projects/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModels model)
        {
            var result = _service.Update(model);

            if (!result.ISuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // DELETE api/projects/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);

            if (!result.ISuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // PUT api/projects/1/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var result = _service.Start(id);

            if (!result.ISuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // PUT api/projects/1/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var result = _service.Complete(id);

            if (!result.ISuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // POST api/projects/1/comments
        [HttpPost("{id}/coments")]
        public IActionResult PostComents(int id, CreateProjectComentInputModel model)
        {
            var result =  _service.InsertComment(id, model);

            if (!result.ISuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}

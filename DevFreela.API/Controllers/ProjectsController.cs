using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly FreelanceTotalCostConfig _config;
        private readonly DevFreelaDbContext _context;

        public ProjectsController(IOptions<FreelanceTotalCostConfig> options, DevFreelaDbContext context)
        {
            _config = options.Value;
            _context = context;
        }


        // GET api/projects?searcha=crm
        [HttpGet()]
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search)))
                //.OrderByDescending(p => p.Id)
                .Skip(page * size)
                .Take(size)
                .ToList();

            var model = projects.Select(p => ProjectViewModel.FromEntity(p)).ToList();

            return Ok(model);
        }

        //// GET api/projects/1
        //[HttpGet("{erro}")]
        //public IActionResult GetByIdError()
        //{
        //    throw new Exception();
        //}

        // GET api/projects/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            var model = ProjectViewModel.FromEntity(project);

            return Ok(model);
        }

        // POST api/projects
        [HttpPost()]
        public IActionResult Post(CreateProjectInputModel model)
        {
            if (model.TotalCost < _config.Minimum || model.TotalCost > _config.Maximum)
                return BadRequest("Total cost must be between 1000 and 999999");

            var project = model.ToEntity();
            _context.Projects.Add(project);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        // PUT apt/projects/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModels model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/projects/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(where => where.Id == id);

            if (project is null)
                return NotFound();

            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        // PUT api/projects/1/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(where => where.Id == id);

            if (project is null)
                return NotFound();

            project.Start();
            _context.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        // PUT api/projects/1/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(where => where.Id == id);
            if (project is null)
                return NotFound();

            project.Complete();
            _context.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        // POST api/projects/1/comments
        [HttpPost("{id}/coments")]
        public IActionResult PostComents(int id, CreateProjectComentInputModel model)
        {
            if (model is null)
                return BadRequest();

            var project = _context.Projects.SingleOrDefault(where => where.Id == id);

            if (project is null)
                return NotFound();

            var comment = model.ToEntity();
            _context.ProjectComments.Add(comment);
            _context.SaveChanges();

            return Ok();
        }
    }
}

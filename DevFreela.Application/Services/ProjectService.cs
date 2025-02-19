using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly FreelanceTotalCostConfig _config;
        private readonly DevFreelaDbContext _context;


        public ProjectService(IOptions<FreelanceTotalCostConfig> options, DevFreelaDbContext context)
        {
            _config = options.Value;
            _context = context;
        }


        public ResultViewModel Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(where => where.Id == id);

            if (project is null)
                return ResultViewModel.Error("Projeto Não encontrado");

            project.Complete();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(where => where.Id == id);

            if (project is null)
                return ResultViewModel.Error("Projeto Não encontrado");

            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<List<ProjectViewModel>> GetAll(string search = "", int page = 0, int size = 3)
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search)))
                .OrderByDescending(p => p.Id)
                .Skip(page * size)
                .Take(size)
                .ToList();

            var model = projects.Select(p => ProjectViewModel.FromEntity(p)).ToList();

            return ResultViewModel<List<ProjectViewModel>>.Success(model);
        }

        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            var project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            if (project is null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateProjectInputModel model)
        {
            var project = model.ToEntity();

            _context.Projects.Add(project);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(project.Id);
        }

        public ResultViewModel InsertComment(int id, CreateProjectComentInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(where => where.Id == id);

            if (project is null)
                return ResultViewModel.Error("Projeto não existe");

            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user is null)
                return ResultViewModel.Error("Usuário não existe");

            var comment = model.ToEntity();

            _context.ProjectComments.Add(comment);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(where => where.Id == id);

            if (project is null)
                return ResultViewModel.Error("Projeto Não encontrado");

            project.Start();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Update(UpdateProjectInputModels model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == model.IdProject);

            if (project is null)
                return ResultViewModel.Error("Projeto não existe");

            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}

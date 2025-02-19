using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class USerService : IUSerService
    {
        private readonly DevFreelaDbContext _context;
        public USerService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var result = _context.Users
                .Include(u => u.Skills)
                    .ThenInclude(us => us.Skill)
                .SingleOrDefault(u => u.Id == id);

            if(result is null)
                return ResultViewModel<UserViewModel>.Error("Usuário não encontrado");

            var model = UserViewModel.FromEntity(result);

            return ResultViewModel<UserViewModel>.Success(model);
        }

        public ResultViewModel<int> Post(CreateUserInputModel model)
        {
            var user = model.ToEntity();

            _context.Users.Add(user);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(user.Id);
        }

        public ResultViewModel PostSkills(UserSkillsInputModel model)
        {
            throw new NotImplementedException();
        }
    }

}

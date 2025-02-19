using DevFreela.Application.Models;

namespace DevFreela.Application.Services
{
    public interface IUSerService
    {
        ResultViewModel<UserViewModel> GetById(int id);
        ResultViewModel<int> Post(CreateUserInputModel model);
        ResultViewModel PostSkills(UserSkillsInputModel model);
    }

}

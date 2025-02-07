namespace DevFreela.API.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate)
            : base()
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            
            Active = true;
            Skills = [];
            Coments = [];
            OwnerProjects = [];
            FreelancerProject = []; 
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool Active { get; private set; }
        public List<UserSkill> Skills { get; private set; }
        public List<ProjectComment> Coments { get; private set; }
        public List<Project> OwnerProjects { get; private set; }
        public List<Project> FreelancerProject { get; private set; }
    }
}

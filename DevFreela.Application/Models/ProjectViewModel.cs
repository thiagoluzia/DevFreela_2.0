using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel(
            int id, 
            string? title, 
            string? description, 
            int idClient, 
            int idFreelancer, 
            string clienteName, 
            string freelancerName, 
            decimal totalCost, 
            List<ProjectComment> coments)
        {
            Id = id;
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            ClienteName = clienteName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;

            Coments = coments.Select(c => c.Content).ToList();
        }


        public int Id { get; private set; }
        public string? Title { get; private set; }
        public string? Description { get; private set; }
        public int IdClient { get; private set; }
        public int IdFreelancer { get; private set; }
        public string ClienteName { get; private set; }
        public string FreelancerName { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<string> Coments { get; private set; }


        public static ProjectViewModel FromEntity(Project entity)
            => new ProjectViewModel(
                entity.Id, 
                entity.Title, 
                entity.Description, 
                entity.IdClient, 
                entity.IdFreelancer, 
                entity.Client.FullName, 
                entity.Freelancer.FullName, 
                entity.TotalCost, 
                entity.Comments);
    }
}

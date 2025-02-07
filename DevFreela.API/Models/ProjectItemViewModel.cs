using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class ProjectItemViewModel
    {
        public ProjectItemViewModel(int id, string title, string clienteName, string freelancerName, decimal totalCost)
        {
            Id = id;
            Title = title;
            ClienteName = clienteName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
        }


        public int Id { get; private set; }
        public string Title { get; private set; }
        public string ClienteName { get; private set; }
        public string FreelancerName { get; private set; }
        public decimal TotalCost { get; private set; }
    }
}

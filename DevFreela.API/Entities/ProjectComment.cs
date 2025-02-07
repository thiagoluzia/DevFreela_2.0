namespace DevFreela.API.Entities
{
    public class ProjectComment : BaseEntity
    {
        public ProjectComment(string content, int idProject, int idUSer)
            : base()
        {
            Content = content;
            IdProject = idProject;
            IdUSer = idUSer;
        }


        public string Content { get; private set; }
        public int IdProject { get; private set; }
        public Project Project { get; private set; }
        public int IdUSer { get; private set; }
        public User USer { get; private set; }
    }
}

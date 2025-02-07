using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class CreateProjectComentInputModel
    {
        public string? Comment { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }

        public ProjectComment ToEntity()
            => new(Comment, IdProject, IdUser);

    }
}

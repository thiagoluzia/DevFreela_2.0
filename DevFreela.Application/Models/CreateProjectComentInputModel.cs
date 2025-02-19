using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
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

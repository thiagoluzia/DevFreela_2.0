﻿using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class Project : BaseEntity
    {
        protected Project() { }

        public Project(
            string title,
            string description,
            int idCliente,
            //User client,
            int idFreelancer,
            //User freelancer,
            decimal totalCost
            //DateTime? startedAt,
            //DateTime? completedAt,
            //ProjectStatusEnum status,
            //List<ProjectComment> coments
            ) 
            : base()
        {
            Title = title;
            Description = description;
            IdClient = idCliente;
            IdFreelancer = idFreelancer;
            TotalCost = totalCost;
            Status = ProjectStatusEnum.CREATED;
            Comments = [];
        }


        public string? Title { get; private set; }
        public string? Description { get; private set; }
        public int IdClient { get; private set; }
        public User Client { get; private set; }
        public int IdFreelancer { get; private set; }
        public User Freelancer { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; private set; }
        public ProjectStatusEnum Status { get; private set; }
        public List<ProjectComment> Comments { get; private set; }


        public void Cancel()
        {
            if (Status == ProjectStatusEnum.INRPOGRESS || Status == ProjectStatusEnum.SUSPENDED)
                Status = ProjectStatusEnum.CALCELLED;
        }

        public void Start()
        {
            if (Status == ProjectStatusEnum.CREATED)
            {
                Status = ProjectStatusEnum.INRPOGRESS;
                StartedAt = DateTime.Now;
            }
        }

        public void Complete()
        {
            if (Status == ProjectStatusEnum.PAYMENTPENDING || Status == ProjectStatusEnum.INRPOGRESS)
            {
                Status = ProjectStatusEnum.COMPLETED;
                CompletedAt = DateTime.Now;
            }
        }

        public void SetPaymentPending()
        {
            if (Status == ProjectStatusEnum.INRPOGRESS)
                Status = ProjectStatusEnum.PAYMENTPENDING;
        }

        public void Update(string title, string description, decimal totalCost)
        {
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }
    }
}

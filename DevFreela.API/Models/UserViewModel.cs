﻿using DevFreela.API.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace DevFreela.API.Models
{
    public class UserViewModel
    {
        public UserViewModel(
            string fullName, 
            string email, 
            DateTime birthDate, 
            List<string> skills)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Skills = skills;
        }


        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get;  private set; }
        public List<string> Skills { get; private set; }

        public static UserViewModel FromEntity(User user)
        {
            var skills = user.Skills.Select(u => u.Skill.Description).ToList();

            var userVieModel = new UserViewModel(
                user.FullName,
                user.Email,
                user.BirthDate,
                skills
                );

            return userVieModel;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string ProjectDescription { get; set; }
        public List<TeamProject> TeamProjects { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

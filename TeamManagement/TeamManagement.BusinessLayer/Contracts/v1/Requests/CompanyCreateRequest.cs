using System;
using System.ComponentModel.DataAnnotations;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class CompanyCreateRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string CeoUserName { get; set; }
        [Required]
        public string CeoEmail { get; set; }
        [Required]
        public string CeoPassword { get; set; }
    }
}

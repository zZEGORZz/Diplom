using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace POCHTI_KURSACH.Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Operation> Operations { get; set; }
        public User() 
        { 
            Operations = new List<Operation>();
        }
    }
}

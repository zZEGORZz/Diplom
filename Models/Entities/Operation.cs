using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace POCHTI_KURSACH.Models.Entities
{
    public class Operation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

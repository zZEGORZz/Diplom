using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace POCHTI_KURSACH.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Garant { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string image { get; set; }
        public List<Operation> Operations { get; set; }
        public Product()
        {
            Operations = new List<Operation>();
        }
    }
}

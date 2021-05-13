using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCHTI_KURSACH.Models
{
    public class ProductsInBagViewModel
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
        public string image { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}

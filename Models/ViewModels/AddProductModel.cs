using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace POCHTI_KURSACH.Models.ViewModels
{
    public class AddProductModel
    {

        public string Name { get; set; }

        public float Price { get; set; }
        public int Garant { get; set; }
        public int Amount { get; set; }

        public int CategoryId { get; set; }

        public string image { get; set; }
    }
}
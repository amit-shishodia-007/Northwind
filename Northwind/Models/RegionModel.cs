using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models
{
    public class RegionModel
    {
        [Key]
        public int RegionId { get; set; }
        public string RegionDescription { get; set; }
    }
}

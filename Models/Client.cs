using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    /// <summary>
    /// Basic user/client for stationary orders
    /// </summary>
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [Range(0, int.MaxValue)]
        public int OrderedPriceSum { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}

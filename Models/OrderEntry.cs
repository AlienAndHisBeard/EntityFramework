using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{

    /// <summary>
    /// Amount of certain item ordered by the client in the specified order
    /// </summary>
    public class OrderEntry
    {

        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
    }
}

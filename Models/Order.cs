using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    /// <summary>
    /// Order made by the user
    /// </summary>
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public List<OrderEntry> OrderEntries { get; } = new();

        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public bool IsCompleted { get; set; }

        public long TotalPrice()
        {
            long total = 0;
            foreach (var orderEntry in OrderEntries)
            {
                total += orderEntry.Item.Price * orderEntry.Amount;
            }
            return total;
        }

        public long AmountOfItems()
        {
            long total = 0;
            foreach (var orderEntry in OrderEntries)
            {
                total += orderEntry.Amount;
            }
            return total;
        }
    }
}

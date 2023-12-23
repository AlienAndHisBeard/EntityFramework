using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models
{

    /// <summary>
    /// Item to sell with available amount in the warehouse
    /// </summary>
    public class Item
    {
        [Key]
        public int Id { get; init; }

        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Cake_Shop.Models
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }


    }
}

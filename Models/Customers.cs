namespace Cake_Shop.Models
{
    public class Customers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }

        public string Address { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}

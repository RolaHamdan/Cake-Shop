using System.ComponentModel.DataAnnotations;

namespace Cake_Shop.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }


        public int Products_id { get; set; }//foreign key  
       


        public int CustomerId { get; set; }
     

        public int StatusId { get; set; }
       


        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }

        public Customers Customer { get; set; }

    }
}

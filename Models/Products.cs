using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cake_Shop.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Please Enter ProductId")]
        public int ProductId { get; set; }

       
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Enter Price")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Please Enter Category")]
        public string Category { get; set; }

      
        public string Image { get; set; }
    }
}

using CoffeeShopApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Coffee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [ForeignKey("Id")]
        public int BeanVarietyId { get; set; }

        public BeanVariety BeanVariety { get; set; }

    }
}
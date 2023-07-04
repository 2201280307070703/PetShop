using System.ComponentModel;

namespace PetShop.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public decimal Quanity { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int AnimalTypeId { get; set; }

        public AnimalType AnimalType { get; set; }

        public Guid SellerId { get; set; }

        public Seller Seller { get; set; }
    }
}

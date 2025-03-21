using System.ComponentModel.DataAnnotations;

namespace HotelGestionale.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public string? Category { get; set; }
    }
}

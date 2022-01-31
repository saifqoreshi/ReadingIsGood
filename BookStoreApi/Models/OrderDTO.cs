using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models
{
    public enum OrderStatus: byte
    {
        Initiated = 1,
        Processing = 2,
        Shipped = 3,
        Received = 4
    }

    public class OrderDTO
    {
        [Required(ErrorMessage = "Customer Id is a required field.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid CustomerId")]
        public int CustomerId { get; set; }
        public List<OrderDetailDTO> Details { get; set; }
    }

    public class OrderDetailDTO
    {
        [Required(ErrorMessage = "Book Id is a required field.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid BookId number")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Quantity is a required field.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid Quantity")]
        public int Quantity { get; set; }
    }
}

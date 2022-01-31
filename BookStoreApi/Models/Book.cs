using System;
using System.Collections.Generic;

#nullable disable

namespace BookStoreApi.Models
{
    public partial class Book
    {
        public Book()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int Quantity { get; set; }
        public DateTime PublishedDate { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

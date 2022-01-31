using System;
using System.Collections.Generic;

#nullable disable

namespace BookStoreApi.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public byte Status { get; set; }
        public int CustomerId { get; set; }
        public decimal? Amount { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

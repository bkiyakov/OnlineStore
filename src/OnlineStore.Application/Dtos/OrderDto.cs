using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Application.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public int OrderNumber { get; set; }
        public string Status { get; set; }
        public IList<OrderElementDto> Items { get; set; }
    }
}

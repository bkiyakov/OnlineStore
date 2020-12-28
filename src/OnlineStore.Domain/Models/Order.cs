using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineStore.Domain.Models
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Column("Customer_id")]
        public Guid CustomerId { get; set; }
        //Customer
        [Required]
        [Column("Order_Date")]
        public DateTime OrderDate { get; set; }
        [Column("Shipment_Date")]
        public DateTime? ShipmentDate { get; set; }
        [Column("Order_Number")]
        public int OrderNumber { get; set; }
        public string Status { get; set; }
        public virtual IEnumerable<OrderElement> Items { get; set; }


        public Order()
        {
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.New;
        }
    }

    public static class OrderStatus
    {
        public const string New = "Новый";
        public const string InProgress = "Выполняется";
        public const string Done = "Выполнен";
    }
}

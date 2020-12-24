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
        public string Status { get; private set; }
        public virtual IEnumerable<OrderElement> Items { get; set; }

        private const string StatusNew = "Новый";
        private const string StatusInProgress = "Выполняется";
        private const string StatusDone = "Выполнен";

        public Order()
        {
            OrderDate = DateTime.UtcNow;
            Status = StatusNew;
        }

        public void SetInProgress()
        {
            // TODO добавить проверку на статус
            Status = StatusInProgress;
        }

        public void SetInDone()
        {
            // TODO добавить проверку на статус
            Status = StatusDone;
        }
    }
}

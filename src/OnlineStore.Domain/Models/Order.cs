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

        public const string StatusNew = "Новый";
        public const string StatusInProgress = "Выполняется";
        public const string StatusDone = "Выполнен";

        public Order()
        {
            OrderDate = DateTime.UtcNow;
            Status = StatusNew;
        }

        public void SetStatus(string status)
        {
            switch (status)
            {
                case StatusInProgress:
                    SetInProgress();
                    break;
                case StatusDone:
                    SetInDone();
                    break;
                default:
                    throw new ApplicationException($"Заказ не может быть установлен в статус {status}");
            }
        }

        private void SetInProgress()
        {
            if (Status != StatusNew && Status != StatusInProgress)
                throw new ApplicationException($"Товар не находится в статусе {StatusNew}");

            Status = StatusInProgress;
        }

        private void SetInDone()
        {
            if (Status != StatusInProgress && Status != StatusDone)
                throw new ApplicationException($"Товар не находится в статусе {StatusInProgress}");

            Status = StatusDone;
        }
    }
}

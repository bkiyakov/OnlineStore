using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineStore.Domain.Models
{
    /// <summary>
    /// Элемент заказа
    /// </summary>
    public class OrderElement
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Column("Order_Id")]
        public Guid OrderId { get; set; }
        [Required]
        [Column("Item_Id")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        [Column("Items_Count")]
        public int ItemsCount { get; set; }
        [Required]
        [Column("Item_Price")]
        public decimal ItemPrice { get; set; }
    }
}

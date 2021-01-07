using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Models
{
    public class OrderGetAllPagedInputModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Номер страницы должен быть больше нуля")]
        public int PageNumber { get; set; }
        [Required]
        [Range(1, 50, ErrorMessage = "Кол-во заказов на странице должно быть от 1 до 50")]
        public int PageSize { get; set; }
        public string Status { get; set; } = "";
    }
}

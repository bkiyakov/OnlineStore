using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Application.Dtos
{
    public class OrderListPagingDto
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasNext { get; set; }
        public IList<OrderDto> OrderList { get; set; }
    }
}

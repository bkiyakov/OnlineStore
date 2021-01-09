using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Application.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public int Discount { get; set; }
    }
}

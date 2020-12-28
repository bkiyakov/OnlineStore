using System;
using System.Collections.Generic;
using System.Text;
using OnlineStore.Domain.Models;

namespace OnlineStore.Application.Dtos
{
    /// <summary>
    /// Data transfer object for <see cref="Product"/>
    /// </summary>
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}

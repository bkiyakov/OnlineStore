﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Models
{
    public class EditUserInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"\d{4}-\d{4}",
            ErrorMessage = "Код заказчика не соответствует шаблону")]
        public string Code { get; set; }
        public string Address { get; set; }
        public int Discount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Models
{
    public class UserRegistrationInputModel
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
        [Required(ErrorMessage = "Эл. почта обязательна")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmPassword { get; set; }
    }
}

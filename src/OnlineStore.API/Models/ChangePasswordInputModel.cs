using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Models
{
    public class ChangePasswordInputModel
    {
        [Required(ErrorMessage = "Старый пароль обязателен")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Новый пароль обязателен")]
        public string NewPassword { get; set; }
        [Required]
        public string Role { get; set; }
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmPassword { get; set; }
    }
}

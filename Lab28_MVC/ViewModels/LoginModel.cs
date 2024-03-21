using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Lab28_MVC.ViewModels
{
    [AllowAnonymous]
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Nickname")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

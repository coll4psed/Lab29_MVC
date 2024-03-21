using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Lab28_MVC.ViewModels
{
    [AllowAnonymous]
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Nickname")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль введён неверно")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не указан телефон")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не выбран вопрос")]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Не введён ответ на вопрос")]
        public string Answer { get; set; }
    }
}

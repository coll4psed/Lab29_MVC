using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab28_MVC.ViewModels
{
    public class EditModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Дата больше текущей")]
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int IdGender { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Worky.Models.Account
{
    public class ChangePassModel
    {
        [Required(ErrorMessage = "Не указан пароль")]
        public string OldPass { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string NewPass { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [Compare("NewPass", ErrorMessage = "Пароили не совпадают")]
        public string ConfirmPass { get; set; }
    }
}

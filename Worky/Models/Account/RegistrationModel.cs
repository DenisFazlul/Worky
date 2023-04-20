using System.ComponentModel.DataAnnotations;

namespace Worky.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Не указан имейл")]
        [RegularExpression(@"[\w\.-]*[a-zA-Z0-9_]@[\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]",ErrorMessage ="Не является имейлом ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "укажите пароль")]
        public string Pass { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [Compare("Pass",ErrorMessage ="Пароили не совпадают")]
        public string ConfirmPass { get; set; }

        internal string GenerateCode()
        {
            Random r = new Random();
           double val= r.Next(1000, 9999);
            return val.ToString();
        }

        internal string GeneratePass()
        {
            Random r = new Random();
            double val = r.Next(1000, 99999);
            return val.ToString();
        }
    }
}

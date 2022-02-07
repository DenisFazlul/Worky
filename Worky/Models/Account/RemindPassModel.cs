using System.ComponentModel.DataAnnotations;

namespace Worky.Models.Account
{
    public class RemindPassModel
    {
        [Required(ErrorMessage = "Не указан имейл")]
        [RegularExpression(@"[\w\.-]*[a-zA-Z0-9_]@[\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]", ErrorMessage = "Не является имейлом ")]
        public string Email { get; set; }
    }
}

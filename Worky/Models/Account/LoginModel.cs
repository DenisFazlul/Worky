using System.ComponentModel.DataAnnotations;
using Worky.Users;

namespace Worky.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан имейл")]
        [RegularExpression(@"[\w\.-]*[a-zA-Z0-9_]@[\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]", ErrorMessage = "Не является имейлом ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }

        internal bool IsUserAcsepted(User user )    
        {
            if(user==null)
            {
                return false;
            }
            if(user.Pass==this.Password&&user.Email==user.Email)
            {
                return true;
            }
            else
            {
                return false;   
            }
        }
    }
}

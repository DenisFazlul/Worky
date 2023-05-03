using System.ComponentModel.DataAnnotations.Schema;
using Worky.Models.Account;

namespace Worky.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string ConfirmCode { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public string UserName { get; set; } = "Новый";
        public bool IsBlock { get; set; } = false;

        
       
      

        internal void SetDataFromModel(ChangeUserDataModel model)
        {
            this.UserName = model.UserName;
        }

        internal string GetName()
        {
            if (UserName == "Новый")
            {
                int end = this.Email.IndexOf("@");
                return  this.Email.Substring(0, end);
               
            }
            else
            {
                return UserName;
            }
        }

        
        
        internal void GenerateCode()
        {
            Random r = new Random();
            double val = r.Next(1000, 9999);
            this.ConfirmCode = val.ToString();
        }
        internal void GeneratePass()
        {
            Random r = new Random();
            double val = r.Next(1000, 99999);
           this.Pass= val.ToString();
        }
    }
}

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
    }
}

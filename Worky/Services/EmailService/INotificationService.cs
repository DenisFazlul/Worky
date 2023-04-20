namespace Worky.Services.EmailService
{
    public interface INotificationService
    {
        public void Send(string toEmail, string header, string content);
        public void SentConvfirmCode(Users.User user);
        public void SentPass(Users.User user);
        public void SentInviteToUser(Users.User user, int ProjectId);
        public void SentPassWasChanged(Users.User user);
    }
}

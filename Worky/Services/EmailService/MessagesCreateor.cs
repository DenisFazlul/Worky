namespace Worky.Services.EmailService
{
    public class MessagesCreateor
    {
        public static string ConfrimMessage(Users.User user)
        {
            return $"Код поддтверждения {user.ConfirmCode}";
        }
        public static string PassMessage(Users.User user)
        {
           return $"Ваш пароль {user.Pass}";
        }
        public static string InviteMessage(Users.User user, int ProjectId)
        {
            string Message = $"Вас пригласили к проекту \n" +
                $" Ссылка на проект https://workyy.ru/Project/Index?ProjectId={ProjectId} " +
                $"\n" +
                $"Пароль для входа-{user.Pass}" +
                $"\n" +
                $"Логин-{user.Email}" +
                $"\n" +
                $"Код подтверждения-{user.ConfirmCode}";

            return Message;
        }
        public static string PassWasChangeMessage()
        {
            return $"Ваш пароль был измнен \n" +
                $" Еслии вы не совершали этого действия сообщение в техподдержку ";
        }
    }
}

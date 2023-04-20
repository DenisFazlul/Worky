using System.Net.Mail;
using System.Net;
using Worky.Users;
using System.Text.Json;
using Newtonsoft.Json;

namespace Worky.Services.EmailService

{
    public static class JsonFileReader
    {
        public static async Task<T> ReadAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
    public class Root
    {
        public EmailConfig emailConfig { get; set; }
    }
    public class EmailConfig
    {
        public string Elogin { get; set; }
        public string Eemail { get; set; }
        public string Epass { get; set; }
        public string Esmtp { get; set; }
        public string Eport { get; set; }
    }
    public class EmailNotifyService : INotificationService
    {
       
        EmailConfig config;
        public EmailNotifyService()
        {
            config = Configs.GetEmailConfig();
        }
        private async void LoadConfigs()
        {
            string priv = @"C:\Users\denis\source\repos\EmailConfigPrivate.json";
            string json = "";
            if(File.Exists(priv))
            {
                config=  JsonFileReader.ReadAsync<EmailConfig>(priv).Result;
                using (StreamReader r = new StreamReader(priv))
                {
                   json= r.ReadToEnd();
                }
                   
            }
            else
            {
                using (StreamReader r = new StreamReader("EmailConfig.json"))
                {
                    json = r.ReadToEnd();
                }
                
            }
            json=  json.Replace("\n", "");
            json = json.Replace("\r", "");
            config=JsonConvert.DeserializeObject<EmailConfig>(json);
            
        }
         
        public void Send(string toEmail, string header, string content)
        {
             sentMessage(toEmail,header,content);
        }

        public void SentConvfirmCode(User user)
        {
            string message=MessagesCreateor.ConfrimMessage(user);
            Send(user.Email, message, "Завершение регистрации");
        }

        public void SentInviteToUser(User user, int ProjectId)
        {
            string message = MessagesCreateor.InviteMessage(user, ProjectId);
            Send(user.Email, message, "Приглашение к проекту");
        }
        public void SentPassWasChanged(Users.User user)
        {
            string Message = $"Ваш пароль был измнен \n" +
              $" Еслии вы не совершали этого действия сообщение в техподдержку ";
            Send(user.Email, Message, "Пароль изменен");
        }
        public void SentPass(User user)
        {
            string message = MessagesCreateor.PassMessage(user);
            Send(user.Email, message, "Востановление пароя");
        }

        private async void sentMessage(string to, string content, string header)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(config.Eemail); // Адрес отправителя
            mail.To.Add(new MailAddress(to)); // Адрес получателя
            mail.Subject = header;
            mail.Body = content;

            SmtpClient client = new SmtpClient (config.Esmtp,Convert.ToInt32(config.Eport));

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(config.Elogin, config.Epass);
            client.SendAsync(mail, null);// Ваши логин и пароль

        }
    }
}

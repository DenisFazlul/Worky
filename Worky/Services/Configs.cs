
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Worky.Services
{
   
    public class Configs
    {
        public static EmailService.EmailConfig GetEmailConfig()
        {
            EmailService.EmailConfig config = null;
            string priv = @"C:\Users\denis\source\repos\EmailConfigPrivate.json";
            string json = "";
            if (File.Exists(priv))
            {
                json= File.ReadAllText(priv);
                
              

            }
            else
            {
                json = File.ReadAllText("EmailConfig.json");

            }
            config = JsonConvert.DeserializeObject<EmailService.Root>(json).emailConfig;
             
            return config;
        }
    }
}

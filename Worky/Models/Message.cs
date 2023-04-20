namespace Worky.Models
{
    public class Message
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public string BackUrl { get; set; }
        public string UrlName { get; set; }
        public Message()
        {

        }
        public Message(string header, string conntetn,string backUrl,string urlName)
        {
            this.Header = header;
            this.Content = conntetn;
            this.BackUrl = backUrl;
            this.UrlName = urlName;
        }
    }
}

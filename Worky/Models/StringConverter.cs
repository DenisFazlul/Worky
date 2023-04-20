namespace Worky.Models
{
    public class StringConverter
    {
        string s { get; set; }
        public StringConverter(string val)
        {
            this.s = val;


        }
        public  string ToUp()
        {

            this.s = char.ToUpper(this.s[0]) + this.s.Substring(1);
            return s;
        }
    }
}

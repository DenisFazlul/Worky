using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
namespace Worky.Models.Calendar
{
    public class CallendarModel
    {
        public int ProjectId { get; set; }
        public List<int> Years { get; set; }
        public int CurYear { get; set; }
        public List<Month> months { get; set; }
        public Month CurMonth { get; set; }

        internal void GetCurMonth()
        {
            int mo = DateTime.Now.Month;
            SetMonth(mo);
        }

        internal void GetCurYear()
        {
            this.CurYear= DateTime.Now.Year;
        }

        internal void SetYear(int year)
        {
            this.CurYear = year;
        }

        public List<Day> Days { get; set; }
        public CallendarModel()
        {
            months=new List<Month>();
            Years = new List<int>();
            this.Days = new List<Day>();
            GetYears();
            GetMon();
            

        }

        public void GetDays()
        {
            int days= DateTime.DaysInMonth(this.CurYear, this.CurMonth.Number+1);
            AddEmptyDays();
            for(int i=0; i<days; i++)
            {
                Day day = new Day(this.CurYear,this.CurMonth.Number+1,i+1,this.ProjectId);
               
                day.GetDayEvents();
                this.Days.Add(day);
            }
        }

        private void AddEmptyDays()
        {
            DateTime FirsDatMonth = new DateTime(this.CurYear, this.CurMonth.Number+1, 1);

            DayOfWeek dayOfWeek= FirsDatMonth.DayOfWeek;
            if(dayOfWeek==DayOfWeek.Tuesday)
            {
                AddDays(1);
            }
            else if(dayOfWeek==DayOfWeek.Wednesday)
            {
                AddDays(2);
            }
            else if (dayOfWeek == DayOfWeek.Thursday)
            {
                AddDays(3);
            }
            else if (dayOfWeek == DayOfWeek.Friday)
            {
                AddDays(4);
            }
            else if (dayOfWeek == DayOfWeek.Saturday)
            {
                AddDays(5);
            }
            else if (dayOfWeek == DayOfWeek.Sunday)
            {
                AddDays(6);
            }


        }
        private void AddDays(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Day d = new Day();
                d.IsValid = false;
                this.Days.Add(d);
            }
        }

        public SelectList GetListmonth()
        {
            return new SelectList(this.months, "Number", "Name",this.CurMonth);
        }
        public SelectList GetListyear()
        {
            return new SelectList(this.Years, this.CurYear);
        }


        internal void SetMonth(int month)
        {
            this.CurMonth = this.months.Where(i => i.Number == month-1).FirstOrDefault();
        }

        internal void GetMonthDayes()
        {
          
        }

        public void GetYears()
        {
            Years.Add(2022);
        }
        public void GetMon()
        {
            for (int i = 0; i < DateTimeFormatInfo.CurrentInfo.MonthNames.Count()-1; i++)
            {
                Month m = new Month();
                StringConverter conv = new StringConverter(DateTimeFormatInfo.CurrentInfo.MonthNames[i]);

                m.Name = conv.ToUp();

                m.Number = i;
                this.months.Add(m);
            } 
        }
     
    }
}

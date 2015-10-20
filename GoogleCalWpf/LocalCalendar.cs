using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace Schuss.GoogleCalWpf
{
    public class LocalCalendar : CalendarListEntry
    {
        int _calendarNumber;
        public LocalCalendar(CalendarListEntry googleCalendar) : base()
        {
            this.Description = googleCalendar.Description;
            this.Id = googleCalendar.Id;
            this.Summary = googleCalendar.Summary;
            this.TimeZone = googleCalendar.TimeZone;
            this.AccessRole = googleCalendar.AccessRole;
            this.BackgroundColor = googleCalendar.BackgroundColor;
            this.ColorId = googleCalendar.ColorId;
            this.DefaultReminders = googleCalendar.DefaultReminders;
            this.ETag = googleCalendar.ETag;
            this.ForegroundColor = googleCalendar.ForegroundColor;
            this.Hidden = googleCalendar.Hidden;
            this.Kind = googleCalendar.Kind;
            this.Location = googleCalendar.Location;
            this.Primary = googleCalendar.Primary;
            this.Selected = googleCalendar.Selected;
            this.SummaryOverride = googleCalendar.SummaryOverride;
           
        }

        public int CalendarNumber
        {
            get { return _calendarNumber; }
            set { _calendarNumber = value; }
        }
    }
}

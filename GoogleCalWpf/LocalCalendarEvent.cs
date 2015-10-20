using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace Schuss.GoogleCalWpf
{
    public class LocalCalendarEvent : DispatchNotifyPropertyChanged
    {
        string _description;
        DateTime _startDateTime;
        DateTime _endDateTime;
        bool _allDay;
        string _id;

        public LocalCalendarEvent()
        {
            _allDay = true;
        }


        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public bool AllDay
        {
            get { return _allDay; }
            set
            {
                if (_allDay != value)
                {
                    _allDay = value;
                    OnPropertyChanged("AllDay");
                }
            }
        }
        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set
            {
                if (_startDateTime != value)
                {
                    _startDateTime = value;
                    OnPropertyChanged("StartDateTime");
                }
            }
        }

        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set
            {
                if (_endDateTime != value)
                {
                    _endDateTime = value;
                    OnPropertyChanged("EndDateTime");
                }
            }
        }

        public string ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public string StartDate
        {
            get { return StartDateTime.ToShortDateString(); }
        }
        public string StartTime
        {
            get { return StartDateTime.ToShortTimeString(); }
        }
        public string EndDate
        {
            get { return EndDateTime.ToShortDateString(); }
        }
        public string EndTime
        {
            get { return EndDateTime.ToShortTimeString(); }
        }

        public void AddGoogleEvent(Event googleEvent)
        {
            StartDateTime = GetDateTime(googleEvent.Start);
            EndDateTime = GetDateTime(googleEvent.End);
            Description = googleEvent.Summary;
            ID = googleEvent.Id;
        }
        private DateTime GetDateTime(EventDateTime eventDateTime)
        {
            DateTime gDateTime;

            if (eventDateTime.DateTime != null)
            {
                int year = Convert.ToInt32(eventDateTime.DateTime.Value.Year);
                int month = Convert.ToInt32(eventDateTime.DateTime.Value.Month);
                int day = Convert.ToInt32(eventDateTime.DateTime.Value.Day);
                int hour = Convert.ToInt32(eventDateTime.DateTime.Value.Hour);
                int minute = Convert.ToInt32(eventDateTime.DateTime.Value.Minute);

                gDateTime = new DateTime(year, month, day, hour, minute, 0);
                AllDay = false;
            }
            else if (eventDateTime.Date != null)
            {
                int year = Convert.ToInt32(eventDateTime.Date.Substring(0, 4));
                int month = Convert.ToInt32(eventDateTime.Date.Substring(5, 2));
                int day = Convert.ToInt32(eventDateTime.Date.Substring(8, 2));

                gDateTime = new DateTime(year, month, day);
                AllDay = true;
            }
            else
                gDateTime = new DateTime();

            return gDateTime;
        }
    }
}

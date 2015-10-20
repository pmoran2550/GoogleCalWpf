using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2;

namespace Schuss.GoogleCalWpf.Models
{
    public class MainViewModel : DispatchNotifyPropertyChanged
    {
        private DateTime _startDate;
        private CalendarService service;
        private IList<LocalCalendar> _calendarList;
        private ObservableCollection<LocalCalendarEvent> eventList1;
        private ObservableCollection<LocalCalendarEvent> eventList2;
        private ObservableCollection<LocalCalendarEvent> currentEventList;
        private LocalCalendar _sourceCalendar;
        private LocalCalendar _destinationCalendar;
        private bool _connected;
        private Window _windowControl;
        private LocalCalendarEvent _selectedDestinationEvent;
        private bool _destinationEventSelected;

        public MainViewModel(Window windowControl)
        {
            _startDate = DateTime.Now;
            eventList1 = new ObservableCollection<LocalCalendarEvent>();
            eventList2 = new ObservableCollection<LocalCalendarEvent>();
            currentEventList = eventList1;
            _windowControl = windowControl;
            DestinationEventSelected = false;
        }


        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public IList<LocalCalendar> CalendarList
        {
            get { return _calendarList; }
            set
            {
                if (_calendarList != value)
                {
                    _calendarList = value;
                    OnPropertyChanged("CalendarList");
                }
            }
        }

        public ObservableCollection<LocalCalendarEvent> EventList1
        {
            get { return eventList1; }
            set
            {
                if (eventList1 != value)
                {
                    eventList1 = value;
                    OnPropertyChanged("EventList1");
                }
            }
        }
        public ObservableCollection<LocalCalendarEvent> EventList2
        {
            get { return eventList2; }
            set
            {
                if (eventList2 != value)
                {
                    eventList2 = value;
                    OnPropertyChanged("EventList2");
                }
            }
        }

        public LocalCalendar SourceCalendar
        {
            get { return _sourceCalendar; }
            set
            {
                if (_sourceCalendar != value)
                {
                    _sourceCalendar = value;
                    OnPropertyChanged("SourceCalendar");
                }
            }
        }

        public LocalCalendar DestinationCalendar
        {
            get { return _destinationCalendar; }
            set
            {
                if (_destinationCalendar != value)
                {
                    _destinationCalendar = value;
                    OnPropertyChanged("DestinationCalendar");
                }
            }
        }

        public LocalCalendarEvent SelectedDestinationEvent
        {
            get { return _selectedDestinationEvent; }
            set
            {
                if (_selectedDestinationEvent != value)
                {
                    _selectedDestinationEvent = value;
                    OnPropertyChanged("SelectedDestinationEvent");
                    DestinationEventSelected = (_selectedDestinationEvent == null) ? false : true;
                }
            }
        }

        public bool DestinationEventSelected
        {
            get { return _destinationEventSelected; }
            set
            {
                if (_destinationEventSelected != value)
                {
                    _destinationEventSelected = value;
                    OnPropertyChanged("DestinationEventSelected");
                }
            }
        }

        public bool Connected
        {
            get { return _connected; }
            set
            {
                if (_connected != value)
                {
                    _connected = value;
                    OnPropertyChanged("Connected");
                }
            }
        }
         public void SetCurrentList(int listID)
        {
            if (listID == 2)
                currentEventList = eventList2;
            else
                currentEventList = eventList1;
        }

        private void ConnectToCalendarService()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secret_480898442601-8v9dcls55b2jdggaa4k5jqq2rp7to68a.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    "user", CancellationToken.None, new FileDataStore("Calendar.MyCalendar")).Result;
            }

            // Create the service.
            service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleCalWpf",
            });
        }

        private void LoadCalendars()
        {
            //Fetch the list of calendar list 
            CalendarList = new List<LocalCalendar>();
            IList<CalendarListEntry> googleCalendarList = service.CalendarList.List().Execute().Items;
            for (int i = 0; i < googleCalendarList.Count; i++)
            {
                LocalCalendar newCal = new LocalCalendar(googleCalendarList[i]);
                newCal.CalendarNumber = i;
                CalendarList.Add(newCal);
            }
            if (CalendarList != null)
                Connected = true;
        }
        private void UpdateEvents(LocalCalendar calendarToUpdate)
        {
            CalendarListEntry calendarSelected = new CalendarListEntry();

            if (calendarToUpdate != null && calendarToUpdate is CalendarListEntry)
            {

                currentEventList.Clear();  // remove any existing events
                EventsResource.ListRequest listRequest = service.Events.List(calendarToUpdate.Id);
                listRequest.TimeMin = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartDate.Hour, StartDate.Minute, StartDate.Second);
                DateTime StopDate = StartDate.AddMonths(4);
                listRequest.TimeMax = new DateTime(StopDate.Year, StopDate.Month, StopDate.Day, StopDate.Hour, StopDate.Minute, StopDate.Second);
                listRequest.SingleEvents = true;
                listRequest.MaxResults = 500;
                IList<Event> calEvents = listRequest.Execute().Items;
                foreach (Event calEvent in calEvents)
                {
                    LocalCalendarEvent newEvent = new LocalCalendarEvent();
                    newEvent.AddGoogleEvent(calEvent);
                    currentEventList.Add(newEvent);  // add to collection
                }
            }
        }
        private void TransferCalEvents(List<LocalCalendarEvent> sourceEvents)
        {
            if (sourceEvents != null)
            {
                // Create a list of description, start and end times from second (destination) calendar
                IList<string> existingDescription = EventList2.Select(x => x.Description).ToList<string>();
                IList<DateTime> existingStart = EventList2.Select(x => x.StartDateTime).ToList<DateTime>();
                IList<DateTime> existingEnd = EventList2.Select(x => x.EndDateTime).ToList<DateTime>();
                foreach (LocalCalendarEvent sourceEvent in sourceEvents)
                {
                    // Check that the event from the source calendar isn't already in the destination calendar
                    if (!(existingDescription.Contains(sourceEvent.Description) &&
                        existingStart.Contains(sourceEvent.StartDateTime) &&
                        existingEnd.Contains(sourceEvent.EndDateTime)))
                    {
                        // Add event to kid's calendar
                        Event newEvent = new Event();
                        newEvent.Summary = sourceEvent.Description;
                        EventDateTime startTime = new EventDateTime();
                        EventDateTime endTime = new EventDateTime();
                        if (sourceEvent.AllDay)
                        {
                            startTime.Date = sourceEvent.StartDateTime.ToString("yyyy-MM-dd");
                            newEvent.Start = startTime;
                            endTime.Date = sourceEvent.EndDateTime.ToString("yyyy-MM-dd");
                            newEvent.End = endTime;
                        }
                        else
                        {
                            DateTime StartTime = sourceEvent.StartDateTime;
                            startTime.DateTime = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day, StartTime.Hour, StartTime.Minute, StartTime.Second);
                            newEvent.Start = startTime;
                            DateTime EndTime = sourceEvent.EndDateTime;
                            endTime.DateTime = new DateTime(EndTime.Year, EndTime.Month, EndTime.Day, EndTime.Hour, EndTime.Minute, EndTime.Second);
                            newEvent.End = endTime;
                        }
                        service.Events.Insert(newEvent, DestinationCalendar.Id).Execute();
                    }
                }
                SetCurrentList(2);  // force update of destination event list
            }
        }

        private void DeleteCalEvents(List<LocalCalendarEvent> sourceEvents)
        {
            if (sourceEvents != null)
            {
                foreach (LocalCalendarEvent sourceEvent in sourceEvents)
                {
                    service.Events.Delete(DestinationCalendar.Id, sourceEvent.ID).Execute();
                }
                UpdateEvents(DestinationCalendar);  // force update of destination event list
            }
        }

        void ConnectCalendarServiceExecute(object param)
        {
            ConnectToCalendarService();
            LoadCalendars();
        }
        public ICommand ConnectCalendarService { get { return new RelayCommand(ConnectCalendarServiceExecute, null); } }
        void ComboSelectionChangedExecute(object param)
        {
            UpdateEvents((LocalCalendar)param);
        }
        public ICommand ComboSelectionChanged { get { return new RelayCommand(ComboSelectionChangedExecute, null); } }
        void TransferEventsExecute(object param)
        {
            TransferCalEvents((List<LocalCalendarEvent>)param);
            UpdateEvents(DestinationCalendar);
        }
        public ICommand TransferEvents { get { return new RelayCommand(TransferEventsExecute, null); } }
        void ExitAppExecute(object param)
        {
            System.Windows.Application.Current.Shutdown();
        }
        public ICommand ExitApp { get { return new RelayCommand(ExitAppExecute, null); } }
        void DeleteAppt2Execute(object param)
        {
            // Delete entry from second calendar selection
            DeleteCalEvents((List<LocalCalendarEvent>)param);            
        }
        public ICommand DeleteAppt2 { get { return new RelayCommand(DeleteAppt2Execute, null); } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Schuss.GoogleCalWpf.Models;
using Google.Apis.Calendar.v3.Data;
using System.ComponentModel;

namespace Schuss.GoogleCalWpf
{
    /// <summary>
    /// Interaction logic for GCalMainWindow.xaml
    /// </summary>
    public partial class GCalMainWindow : Window
    {
        MainViewModel _viewModel;

        public GCalMainWindow()
        {
            InitializeComponent();

            _viewModel = new MainViewModel(this);

            DataContext = _viewModel;
        }

        private void Calendar1ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SetCurrentList(1);
            ComboBox entry = (ComboBox)sender;
            LocalCalendar calendar = (LocalCalendar)entry.SelectedItem;
            _viewModel.ComboSelectionChanged.Execute(calendar);
            Calendar1Events.Items.SortDescriptions.Add(new SortDescription("StartDateTime", ListSortDirection.Ascending));  // order the list
        }

        private void Calendar2ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SetCurrentList(2);
            ComboBox entry = (ComboBox)sender;
            LocalCalendar calendar = (LocalCalendar)entry.SelectedItem;
            _viewModel.ComboSelectionChanged.Execute(calendar);
            Calendar2Events.Items.SortDescriptions.Add(new SortDescription("StartDateTime", ListSortDirection.Ascending));  // order the list
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // selecteditems cannot be bound, so get selection here instead
            List<LocalCalendarEvent> selectedEvents = new List<LocalCalendarEvent>();
            foreach (LocalCalendarEvent gridEvent in Calendar1Events.SelectedItems)
            {
                LocalCalendarEvent newEvent = new LocalCalendarEvent();
                newEvent = (LocalCalendarEvent)gridEvent;
                if (newEvent != null && !selectedEvents.Contains(newEvent))
                    selectedEvents.Add(newEvent);
            }
            if (selectedEvents.Count > 0)
                _viewModel.TransferEvents.Execute(selectedEvents);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (clickedButton != null)
            {
                List<LocalCalendarEvent> selectedEvents = new List<LocalCalendarEvent>();
                if (clickedButton.Name == "DeleteButton2")
                {
                    foreach (LocalCalendarEvent gridEvent in Calendar2Events.SelectedItems)
                    {
                        LocalCalendarEvent newEvent = new LocalCalendarEvent();
                        newEvent = (LocalCalendarEvent)gridEvent;
                        if (newEvent != null && !selectedEvents.Contains(newEvent))
                            selectedEvents.Add(newEvent);
                    }
                    if (selectedEvents.Count > 0)
                        _viewModel.DeleteAppt2.Execute(selectedEvents);
                }
            }
        }
    }
}

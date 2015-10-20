using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows;
using System.Threading;

namespace Schuss.GoogleCalWpf
{
    public class DispatchNotifyPropertyChanged : INotifyPropertyChanged
    {

        protected Dispatcher UiDispatcher
        {
            get
            {
                return Application.Current != null ? Application.Current.Dispatcher : null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler cachedHandler = this.PropertyChanged;
            if (cachedHandler != null)
            {
                Dispatcher uiDispatcher = UiDispatcher;
                if (uiDispatcher == null || uiDispatcher.Thread == Thread.CurrentThread)
                {
                    cachedHandler(this, new PropertyChangedEventArgs(propertyName));
                }
                else
                {
                    uiDispatcher.BeginInvoke(DispatcherPriority.DataBind,
                       (ThreadStart)delegate()
                       {
                           cachedHandler(this, new PropertyChangedEventArgs(propertyName));
                       });
                }
            }
        }

        protected void QueueOnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler cachedHandler = this.PropertyChanged;
            if (cachedHandler != null)
            {
                Dispatcher uiDispatcher = Application.Current != null ? Application.Current.Dispatcher : null;
                if (uiDispatcher != null)
                {
                    uiDispatcher.BeginInvoke(DispatcherPriority.DataBind,
                       (ThreadStart)delegate()
                       {
                           cachedHandler(this, new PropertyChangedEventArgs(propertyName));
                       });
                }
            }
        }


    }
}

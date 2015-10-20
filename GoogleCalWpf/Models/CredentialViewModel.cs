using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schuss.GoogleCalWpf
{
    public class CredentialViewModel : DispatchNotifyPropertyChanged
    {
        string _clientID;
        string _clientSecret;
        string _apiID;

        public CredentialViewModel()
        {

        }


        public string ClientID
        {
            get { return _clientID; }
            set
            {
                if (_clientID != value)
                {
                    _clientID = value;
                    OnPropertyChanged("ClientID");
                }
            }
        }

        public string ClientSecret
        {
            get { return _clientSecret; }
            set
            {
                if (_clientSecret != value)
                {
                    _clientSecret = value;
                    OnPropertyChanged("ClientSecret");
                }
            }
        }


        public string APIID
        {
            get { return _apiID; }
            set
            {
                if (_apiID != value)
                {
                    _apiID = value;
                    OnPropertyChanged("APIID");
                }
            }
        }
    }
}

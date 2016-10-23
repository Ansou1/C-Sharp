using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MSW_10_UWA.Common;
using Music_Sheet_Writer.Models;
using MusicSheetWriter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace MSW_10_UWA.ViewModel
{
    class AccountViewModel : INotifyPropertyChanged
    {
        private string _loginResponse;
        private string _userID;
        private User _user;

        public ICommand SearchCommandAccountViewModel { get; private set; }
        public ICommand LogOutCommandAccountViewModel { get; private set; }
        public ICommand EditeProfileCommandAccountViewModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private String username;
        public String UserName
        {
            get { return username; }
            set { username = value; OnPropertyChanged("UserName"); }
        }

        private String firstname;
        public String FirstName
        {
            get { return firstname; }
            set { firstname = value; OnPropertyChanged("FirstName"); }
        }

        private String lastname;
        public String LastName
        {
            get { return lastname; }
            set { lastname = value; OnPropertyChanged("LastName"); }
        }

        private String eMail;
        public String EMail
        {
            get { return eMail; }
            set { eMail = value; OnPropertyChanged("EMail"); }
        }

        private BitmapImage pictureurl;
        public BitmapImage PictureURL
        {
            get { return pictureurl; }
            set { pictureurl = value; OnPropertyChanged("PictureURL"); }
        }

        private String message;
        public String Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        private void doLogout(object sender, EventArgs e)
        {
            INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
            navigationService.NavigateTo(ViewModelLocator.NavigationMainPage);
        }

        public AccountViewModel()
        {
            Messenger.Default.Register<String>(this, loginResponse =>
            {
                _loginResponse = loginResponse;
                System.Diagnostics.Debug.WriteLine("###### recieved AccountViewModel ######");
                System.Diagnostics.Debug.WriteLine(_loginResponse);
                JsonTools test = new JsonTools();
                _userID = test.JsonGetIdUser(_loginResponse);
                AccountInformationDisplay();
            });

            SearchCommandAccountViewModel = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("SearchCommandAccountViewModel click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationSearchView);
            });

            LogOutCommandAccountViewModel = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("LogOutCommandAccountViewModel click :)");
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doLogout);
                Globals test_global = null;
                test_global = Globals.getInstance();
                connect.HttpPostRequest(test_global.IpAPI + "api/logout", "");
            });

            EditeProfileCommandAccountViewModel = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("AccountUpdateCommandAccountViewModel click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                Messenger.Default.Send(_loginResponse);
                navigationService.NavigateTo(ViewModelLocator.NavigationAccountUpdateView);
            });
        }

        private void AccountInformationDisplay()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(AccountInformationDisplay2);
            Globals test_global = null;
            test_global = Globals.getInstance();
            connect.HttpGetRequest(test_global.IpAPI + "api/users/" + _userID + "/personal_data");
        }

        private void AccountInformationDisplay2(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            JsonTools tmp = new JsonTools();

            this._user = tmp.getUserFromJson(connect.Response);

            UserName = _user.UserName;
            FirstName = _user.FirstName;
            LastName = _user.LastName;
            EMail = _user.Email;

            //this.UserImage.ImageSource = new BitmapImage(new Uri(_user.PictureURL, UriKind.Absolute));

            Globals test_global = null;
            test_global = Globals.getInstance();

            if (_user.PictureURL.Equals(""))
            {
                _user.PictureURL = test_global.IpAPI + "bundles/msw/images/default_avatar.png";
            }
                //_user.PictureURL = "https://musicsheetwriter.tk/bundles/msw/images/default_avatar.png";
            PictureURL = new BitmapImage(new Uri(_user.PictureURL, UriKind.Absolute));
            Message = _user.Message;
        }
    }
}

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MSW_10_UWA.Common;
using MusicSheetWriter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Data.Json;

namespace MSW_10_UWA.ViewModel
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        public ICommand SignUpCommandSignUpPage { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private String userName;
        public String UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private String userNameEmail;
        public String UserNameEmail
        {
            get { return userNameEmail; }
            set
            {
                userNameEmail = value;
                OnPropertyChanged("UserNameEmail");
            }
        }

        private String userPassword;
        public String UserPassword
        {
            get { return userPassword; }
            set
            {
                userPassword = value;
                OnPropertyChanged("UserPassword");
            }
        }

        private void doConnection(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine(connect.getValue());
            if (connect.getValue() == true)
            {
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.GoBack();
            }
        }

        public SignUpViewModel()
        {
            SignUpCommandSignUpPage = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("SignUpCommandSignUpPage click :)");
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doConnection);
                JsonObject json = new JsonObject();
                json.SetNamedValue("username", JsonValue.CreateStringValue(UserName));
                json.SetNamedValue("email", JsonValue.CreateStringValue(UserNameEmail));
                json.SetNamedValue("password", JsonValue.CreateStringValue(UserPassword));
                System.Diagnostics.Debug.WriteLine(json.Stringify());
                Globals test_global = null;
                test_global = Globals.getInstance();
                connect.HttpPostRequest(test_global.IpAPI + "api/users", json.Stringify());
                UserNameEmail = "";
                UserPassword = "";
            });
        }
    }
}

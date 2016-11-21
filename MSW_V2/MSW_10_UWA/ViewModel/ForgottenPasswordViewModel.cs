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

namespace MSW_10_UWA.ViewModel
{
    class ForgottenPasswordViewModel : INotifyPropertyChanged
    {
        public ICommand ForgottenPasswordCommandForgottenPasswordPage { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        private void doForgotPass(Object sender, EventArgs e)
        {
            HTTPRequest connect = new HTTPRequest();
            connect = (HTTPRequest)sender;
            if (connect.getValue() == true)
            {
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.GoBack();
            }
        }

        public ForgottenPasswordViewModel()
        {
            ForgottenPasswordCommandForgottenPasswordPage = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("ForgottenPasswordCommandForgottenPasswordPage click :)");
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doForgotPass);
                Globals test_global = null;
                test_global = Globals.getInstance();
                connect.HttpPostRequest(test_global.IpAPI + "api/forgotten_password", "{ \"email\":\"" + UserNameEmail + "\" }");
            });
        }
    }
}

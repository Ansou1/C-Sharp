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

namespace MSW_10_UWA.ViewModel
{
    class ChangePasswordViewModel : INotifyPropertyChanged
    {
        private string _loginResponse;
        private string _userID;

        public ICommand SubmitCommandChangePasswordViewModel { get; private set; }
        public ICommand CancelCommandAccountUpdateViewModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private String currentPassword;
        public String CurrentPassword
        {
            get { return currentPassword; }
            set
            {
                currentPassword = value;
                OnPropertyChanged("CurrentPassword");
            }
        }

        private String newPassword;
        public String NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }

        public ChangePasswordViewModel()
        {
            Messenger.Default.Register<String>(this, loginResponse =>
            {
                _loginResponse = loginResponse;
                System.Diagnostics.Debug.WriteLine("###### recieved AccountUpdateViewModel ######");
                System.Diagnostics.Debug.WriteLine(_loginResponse);
                JsonTools test = new JsonTools();
                _userID = test.JsonGetIdUser(_loginResponse);
            });

            SubmitCommandChangePasswordViewModel = new RelayCommand(() => 
            {
                Globals test_global = null;
                test_global = Globals.getInstance();
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doChangePassword);
                connect.HttpPutRequest(test_global.IpAPI + "api/users/" + _userID + "/password", "{ \"current_password\":\"" + this.CurrentPassword + "\",\n\"new_password\":\"" + this.NewPassword + "\" }");

                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.GoBack();
            });

            CancelCommandAccountUpdateViewModel = new RelayCommand(() => 
            {
                NewPassword = "";
                CurrentPassword = "";
            });

        }

        private void doChangePassword(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("wait de l'élement en envoie :)");
        }

    }
}

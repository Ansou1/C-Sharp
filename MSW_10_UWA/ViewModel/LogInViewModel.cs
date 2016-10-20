using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System.ComponentModel;
using System;
using System.Diagnostics;
using MusicSheetWriter;
using GalaSoft.MvvmLight.Messaging;
using MSW_10_UWA.Common;
using Windows.Security.Credentials;
using System.Collections.Generic;

namespace MSW_10_UWA.ViewModel
{
    class LogInViewModel : INotifyPropertyChanged
    {
        public ICommand SignInCommandLoginPage { get; private set; }
        public ICommand CreateAccountCommandLoginPage { get; private set; }
        public ICommand ForgotPasswordCommandLoginPage { get; private set; }
        public ICommand RememberMeCommandLoginPage { get; private set; }

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

        private Boolean rememberMe;
        public Boolean RememberMe
        {
            get { return rememberMe; }
            set
            {
                rememberMe = value;
                OnPropertyChanged("RememberMe");
            }
        }

        private void doConnection(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            string response = connect.Response;
            System.Diagnostics.Debug.WriteLine(response);
            if (connect.getValue() == true)
            {

                if (RememberMe == true)
                    saveCredential();

                UserNameEmail = "";
                UserPassword = "";
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                Messenger.Default.Send(response);
                navigationService.NavigateTo(ViewModelLocator.NavigationPivotView); //ici j'envoie mon objet JSON response
            }
        }

        public LogInViewModel()
        {
            RememberMe = false;

            IReadOnlyList<PasswordCredential> data = RetriveCredential();
            if (data != null)
            {
                foreach (var item in data)
                {
                    item.RetrievePassword();
                    UserNameEmail = item.UserName;
                    UserPassword = item.Password;
                }
            }

            SignInCommandLoginPage = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("SignInCommandLoginPage click :)");
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doConnection);
                connect.HttpPostRequest("http://localhost/trunk/web/app_dev.php/api/login", "{ \"login\":\"" + this.UserNameEmail + "\",\n\"password\":\"" + this.UserPassword + "\" }");
            });

            CreateAccountCommandLoginPage = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("CreateAccountCommandLoginPage click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationSignUpView);
            });


            ForgotPasswordCommandLoginPage = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("ForgotPasswordCommandLoginPage click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationForgottenPasswordView);
            });

        }

        void saveCredential()
        {
            PasswordVault vault = new PasswordVault();
            PasswordCredential cred = new PasswordCredential("MSW", UserNameEmail, UserPassword);
            vault.Add(cred);
        }

        IReadOnlyList<PasswordCredential> RetriveCredential()
        {
            PasswordVault vault = new PasswordVault();
            try
            {
                return vault.FindAllByResource("MSW");
            }
            catch (Exception e)
            {
                return null;
            }

        }

    }
}

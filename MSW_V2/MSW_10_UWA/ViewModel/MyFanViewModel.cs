using GalaSoft.MvvmLight.Messaging;
using Music_Sheet_Writer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using MSW_10_UWA.Common;
using MusicSheetWriter;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;

namespace MSW_10_UWA.ViewModel
{
    class MyFanViewModel : INotifyPropertyChanged
    {
        private String _loginResponse;
        private String _userID;

        public ICommand SearchCommandMyFanViewModel { get; private set; }
        public ICommand LogOutCommandMyFanViewModel { get; private set; }
        public ICommand SelectedUserCommandMyFanViewModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<User> userList;
        public ObservableCollection<User> UserList
        {
            get { return userList; }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }

        private User userClick;
        public User UserClick
        {
            get { return userClick; }
            set
            {
                userClick = value;
                OnPropertyChanged("UserClick");
            }
        }

        private void doLogout(object sender, EventArgs e)
        {
            INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
            navigationService.NavigateTo(ViewModelLocator.NavigationMainPage);
        }

        public MyFanViewModel()
        {
            Messenger.Default.Register<String>(this, loginResponse =>
            {
                _loginResponse = loginResponse;
                System.Diagnostics.Debug.WriteLine("###### recieved MyFan ######");
                System.Diagnostics.Debug.WriteLine(_loginResponse);
                JsonTools test = new JsonTools();
                _userID = test.JsonGetIdUser(_loginResponse);
                getListeFanUser();
            });

            SearchCommandMyFanViewModel = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("SearchCommandMyFanViewModel click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationSearchView);
            });

            LogOutCommandMyFanViewModel = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("LogOutCommandMyFanViewModel click :)");
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doLogout);
                Globals test_global = null;
                test_global = Globals.getInstance();
                connect.HttpPostRequest(test_global.IpAPI + "api/logout", "");
            });

            SelectedUserCommandMyFanViewModel = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("SelectedUserCommandMyFanViewModel click :)");
                Messenger.Default.Send(UserClick);
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationAccountSelectedView);
            });
        }

        private void getListeFanUser2(object sender, EventArgs e)
        {
            ObservableCollection<User> UserListTmp = new ObservableCollection<User>();
            JsonTools tmp = new JsonTools();
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Liste des fan de user #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);

            JsonArray jsonArray;
            if (JsonArray.TryParse(connect.Response, out jsonArray))
            {
                foreach (var item in jsonArray)
                {
                    UserListTmp.Add(tmp.getUserFromJson(item.Stringify()));
                }
                UserList = UserListTmp;
            }
        }

        private void getListeFanUser()
        {
            Globals test_global = null;
            test_global = Globals.getInstance();
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.getListeFanUser2);
            System.Diagnostics.Debug.WriteLine(test_global.IpAPI + "api/users/" + this._userID + "/subscribers");
            connect.HttpGetRequest(test_global.IpAPI + "api/users/" + this._userID + "/subscribers");
        }

    }
}

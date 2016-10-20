using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MSW_10_UWA.Common;
using Music_Sheet_Writer.Models;
using MusicSheetWriter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Data.Json;

namespace MSW_10_UWA.ViewModel
{
    class MyMuseSelectedViewModel : INotifyPropertyChanged
    {
        private String _userID;

        public ICommand SearchCommandMyMuseSelectedViewModel { get; private set; }
        public ICommand LogOutCommandMyMuseSelectedViewModel { get; private set; }
        public ICommand SelectedUserCommandMyMuseSelectedViewModel { get; private set; }


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

        public MyMuseSelectedViewModel()
        {
            Messenger.Default.Register<User>(this, userselected =>
            {
                JsonTools test = new JsonTools();
                _userID = userselected.Id.ToString();
                getListMuseUser();
            });

            SearchCommandMyMuseSelectedViewModel = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("SearchCommandMyMuseViewModel click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationSearchView);
            });

            LogOutCommandMyMuseSelectedViewModel = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("LogOutCommandMyMuseViewModel click :)");
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doLogout);
                Globals test_global = null;
                test_global = Globals.getInstance();
                connect.HttpPostRequest(test_global.IpAPI + "api/logout", "");
            });

            SelectedUserCommandMyMuseSelectedViewModel = new RelayCommand(() =>
            {
                Messenger.Default.Send(UserClick);
                System.Diagnostics.Debug.WriteLine("SelectedUserCommandMyMuseViewModel click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationAccountSelectedView);
            });
        }

        private void getListMuseUser2(object sender, EventArgs e)
        {
            ObservableCollection<User> UserListTmp = new ObservableCollection<User>();
            JsonTools tmp = new JsonTools();
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Liste des muses à user #######################");
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

        private void getListMuseUser()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.getListMuseUser2);
            Globals test_global = null;
            test_global = Globals.getInstance();
            connect.HttpGetRequest(test_global.IpAPI + "api/users/" + this._userID + "/subscriptions");
        }
    }
}

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MSW_10_UWA.Common;
using MSW_10_UWA.Models;
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MSW_10_UWA.ViewModel
{
    class SearchViewModel : INotifyPropertyChanged
    {
        public ICommand LogOutCommandSearchViewModel { get; private set; }
        public ICommand SearchIconCommandSearchViewModel { get; private set; }
        public ICommand SelectedScoreCommandSearchViewModel { get; private set; }
        public ICommand SelectedUserCommandSearchViewModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private String searchString;
        public String SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                OnPropertyChanged("SearchString");
            }
        }

        private ObservableCollection<Score> scoreList;
        public ObservableCollection<Score> ScoreList
        {
            get { return scoreList; }
            set
            {
                scoreList = value;
                OnPropertyChanged("ScoreList");
            }
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

        private Score scoreClick;
        public Score ScoreClick
        {
            get { return scoreClick; }
            set
            {
                scoreClick = value;
                OnPropertyChanged("ScoreClick");
            }
        }

        private void doLogout(object sender, EventArgs e)
        {
            INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
            navigationService.NavigateTo(ViewModelLocator.NavigationMainPage);
        }

        public SearchViewModel()
        {
            LogOutCommandSearchViewModel = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("LogOutCommandMyScoreViewModel click :)");
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doLogout);
                Globals test_global = null;
                test_global = Globals.getInstance();
                connect.HttpPostRequest(test_global.IpAPI + "api/logout", "");
            });

            SearchIconCommandSearchViewModel = new RelayCommand(() =>
            {
                getListSearchScore();
                getListSearchUser();
            });

            SelectedScoreCommandSearchViewModel = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("Oui Search Score click :)");

                Messenger.Default.Send(ScoreClick);
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationScoreSelectedView);
            });

            SelectedUserCommandSearchViewModel = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("Oui Search User click :)");

                Messenger.Default.Send(UserClick);
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationAccountSelectedView);
            });
        }

        #region search score
        private void getListSearchScore2(object sender, EventArgs e)
        {
            ObservableCollection<Score> ScoreListTmp = new ObservableCollection<Score>();
            JsonTools tmp = new JsonTools();
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Get all scores of user #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
            JsonArray jsonArray;
            if (JsonArray.TryParse(connect.Response, out jsonArray))
            {
                foreach (var item in jsonArray)
                {
                    ScoreListTmp.Add(tmp.getScoreFromJson(item.Stringify()));
                }
                ScoreList = ScoreListTmp;
            }
        }

        private void getListSearchScore()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.getListSearchScore2);
            Globals test_global = null;
            test_global = Globals.getInstance();
            connect.HttpGetRequest(test_global.IpAPI + "api/scores?title=" + SearchString);
        }
        #endregion

        #region Search User
        private void getListSearchUser2(object sender, EventArgs e)
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

        private void getListSearchUser()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.getListSearchUser2);
            Globals test_global = null;
            test_global = Globals.getInstance();
            connect.HttpGetRequest(test_global.IpAPI + "api/users?uname=" + SearchString);
        }
        #endregion
    }
}

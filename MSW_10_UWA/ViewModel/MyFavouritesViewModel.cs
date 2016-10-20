using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MSW_10_UWA.Common;
using MSW_10_UWA.Models;
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
    class MyFavouritesViewModel : INotifyPropertyChanged
    {
        private String _loginResponse;
        private String _userID;

        public ICommand SearchCommandMyFavouritesViewModel { get; private set; }
        public ICommand LogOutCommandMyFavouritesViewModel { get; private set; }
        public ICommand SelectedScoreCommandMyFavouritesViewModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        public MyFavouritesViewModel()
        {
            Messenger.Default.Register<String>(this, loginResponse =>
            {
                _loginResponse = loginResponse;
                System.Diagnostics.Debug.WriteLine("###### recieved MyFavourite ######");
                System.Diagnostics.Debug.WriteLine(_loginResponse);
                JsonTools test = new JsonTools();
                _userID = test.JsonGetIdUser(_loginResponse);
                getListFavouritesUser();
            });

            SearchCommandMyFavouritesViewModel = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("SearchCommandMyScoreViewModel click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationSearchView);
            });

            LogOutCommandMyFavouritesViewModel = new RelayCommand(() =>
            {
                Globals test_global = null;
                test_global = Globals.getInstance();
                System.Diagnostics.Debug.WriteLine("LogOutCommandMyScoreViewModel click :)");
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doLogout);
                connect.HttpPostRequest(test_global.IpAPI + "api/logout", "");
            });

            SelectedScoreCommandMyFavouritesViewModel = new RelayCommand(() =>
             {
                 System.Diagnostics.Debug.WriteLine("Oui Search Score click :)");

                 Messenger.Default.Send(ScoreClick);
                 INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                 navigationService.NavigateTo(ViewModelLocator.NavigationScoreSelectedView);
             });
        }

        private void getListFavouritesUser2(object sender, EventArgs e)
        {
            ObservableCollection<Score> ScoreListTmp = new ObservableCollection<Score>();
            JsonTools tmp = new JsonTools();
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Get favourite scores of user #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
            JsonArray jsonArray;
            if (JsonArray.TryParse(connect.Response, out jsonArray))
            {
                foreach (var item in jsonArray)
                {
                    ScoreList.Add(tmp.getScoreFromJson(item.Stringify()));
                }
                ScoreList = ScoreListTmp;
            }
        }

        private void getListFavouritesUser()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.getListFavouritesUser2);
            Globals test_global = null;
            test_global = Globals.getInstance();
            connect.HttpGetRequest(test_global.IpAPI + "api/users/" + _userID + "/scores/favourites");
        }
    }
}

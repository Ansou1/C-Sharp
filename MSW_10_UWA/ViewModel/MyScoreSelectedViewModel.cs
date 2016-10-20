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

namespace MSW_10_UWA.ViewModel
{
    class MyScoreSelectedViewModel : INotifyPropertyChanged
    {
        private String _userID;

        public ICommand SearchCommandMyScoreSelectedViewModel { get; private set; }
        public ICommand LogOutCommandMyScoreSelectedViewModel { get; private set; }
        public ICommand SelectedScoreCommandMyScoreSelectedViewModel { get; private set; }

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

        public MyScoreSelectedViewModel()
        {
            Messenger.Default.Register<User>(this, userselected =>
            {
                System.Diagnostics.Debug.WriteLine("###### recieved MyScore ######");
                JsonTools test = new JsonTools();
                _userID = userselected.Id.ToString();
                getListScoreUser();
            });


            SearchCommandMyScoreSelectedViewModel = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("SearchCommandMyScoreViewModel click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationSearchView);
            });

            LogOutCommandMyScoreSelectedViewModel = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("LogOutCommandMyScoreViewModel click :)");
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doLogout);
                Globals test_global = null;
                test_global = Globals.getInstance();
                connect.HttpPostRequest(test_global.IpAPI + "api/logout", "");
            });

            SelectedScoreCommandMyScoreSelectedViewModel = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("Oui Search Score click :)");

                Messenger.Default.Send(ScoreClick);
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationScoreSelectedView);
            });
        }

        private void getListScoreUser2(object sender, EventArgs e)
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
                    ScoreList.Add(tmp.getScoreFromJson(item.Stringify()));
                }
                ScoreList = ScoreListTmp;
            }
        }

        private void getListScoreUser()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.getListScoreUser2);
            Globals test_global = null;
            test_global = Globals.getInstance();
            connect.HttpGetRequest(test_global.IpAPI + "api/users/" + _userID + "/scores/own");
        }
    }
}

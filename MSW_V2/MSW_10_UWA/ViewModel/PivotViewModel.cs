using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MSW_10_UWA.Common;
using Music_Sheet_Writer.Models;
using MusicSheetWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Data.Json;
using Windows.UI.Xaml.Navigation;

namespace MSW_10_UWA.ViewModel
{
    class PivotViewModel
    { 
        private string _loginResponse;
        private string _userID;

        public ICommand MyScoreCommandPivotPage { get; private set; }
        public ICommand MyFavouritesCommandPivotPage { get; private set; }
        public ICommand MyMuseCommandPivotPage { get; private set; }
        public ICommand MyFanCommandPivotPage { get; private set; }
        public ICommand SearchCommandSearchPage { get; private set; }
        public ICommand AccountCommandAccountPage { get; private set; }
        public ICommand LogOutCommandPivotPage { get; private set; }

        private void doLogout(object sender, EventArgs e)
        {
            INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
            navigationService.NavigateTo(ViewModelLocator.NavigationMainPage);
        }

        public PivotViewModel()
        {
            Messenger.Default.Register<String>(this, loginResponse =>
            {
                _loginResponse = loginResponse;
                System.Diagnostics.Debug.WriteLine("###### recieved Pivot ######");
                System.Diagnostics.Debug.WriteLine(_loginResponse);
                JsonTools test = new JsonTools();
                _userID = test.JsonGetIdUser(_loginResponse);
                Globals test_global = null;
                test_global = Globals.getInstance();
                test_global.UserConnected = _userID;
            });

            MyScoreCommandPivotPage = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("MyScoreCommandPivotPage click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                Messenger.Default.Send(_loginResponse);
                navigationService.NavigateTo(ViewModelLocator.NavigationMyScoreView);
            });

            MyFavouritesCommandPivotPage = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("MyFavouritesCommandPivotPage click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                Messenger.Default.Send(_loginResponse);
                navigationService.NavigateTo(ViewModelLocator.NavigationMyFavouritesView);
            });

            MyMuseCommandPivotPage = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("MyMuseCommandPivotPage click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                Messenger.Default.Send(_loginResponse);
                navigationService.NavigateTo(ViewModelLocator.NavigationMyMuseView);
            });

            MyFanCommandPivotPage = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("MyFanCommandPivotPage click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                Messenger.Default.Send(_loginResponse);
                navigationService.NavigateTo(ViewModelLocator.NavigationMyFanView);
            });




            SearchCommandSearchPage = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("SearchCommandSearchPage click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationSearchView);
            });

            AccountCommandAccountPage = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("AccountCommandAccountPage click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.NavigateTo(ViewModelLocator.NavigationAccountView);
            });

            LogOutCommandPivotPage = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("LogOutCommandPivotPage click :)");
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doLogout);
                Globals test_global = null;
                test_global = Globals.getInstance();
                connect.HttpPostRequest(test_global.IpAPI + "api/logout", "");
            });
        }
    }
}


//class PersonHubViewModel
//{
//    public ICommand NavigateBackCommand { get; private set; }

//    public PersonHubViewModel()
//    {
//        NavigateBackCommand = new RelayCommand(() =>
//        {
//            INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
//            navigationService.GoBack();
//        });
//    }
//}
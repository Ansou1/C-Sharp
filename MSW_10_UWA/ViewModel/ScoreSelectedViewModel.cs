using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
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
    class ScoreSelectedViewModel : INotifyPropertyChanged
    {
        public ICommand LogOutCommandScoreSelectedViewModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private String locationPreview;
        public String LocationPreview
        {
            get { return locationPreview; }
            set
            {
                locationPreview = value;
                OnPropertyChanged("LocationPreview");
            }
        }

        private void doLogout(object sender, EventArgs e)
        {
            INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
            navigationService.NavigateTo(ViewModelLocator.NavigationMainPage);
        }

        public ScoreSelectedViewModel()
        {
            Messenger.Default.Register<String>(this, loginResponse =>
            {
                LocationPreview = loginResponse;
            });

            LogOutCommandScoreSelectedViewModel = new RelayCommand(() => 
            {
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doLogout);
                Globals test_global = null;
                test_global = Globals.getInstance();
                connect.HttpPostRequest(test_global.IpAPI + "api/logout", "");
            });
        }
    }
}

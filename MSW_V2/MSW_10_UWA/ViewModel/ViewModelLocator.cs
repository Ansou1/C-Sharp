using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using MSW_10_UWA.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSW_10_UWA.ViewModel
{
    class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<INavigationService>(() => CreateNavigationService());
            SimpleIoc.Default.Register<LogInViewModel>(true);
            SimpleIoc.Default.Register<PivotViewModel>(true);
            SimpleIoc.Default.Register<SignUpViewModel>(true);
            SimpleIoc.Default.Register<ForgottenPasswordViewModel>(true);
            SimpleIoc.Default.Register<AccountViewModel>(true);
            SimpleIoc.Default.Register<SearchViewModel>(true);
            SimpleIoc.Default.Register<MyFanViewModel>(true);
            SimpleIoc.Default.Register<MyFavouritesViewModel>(true);
            SimpleIoc.Default.Register<MyMuseViewModel>(true);
            SimpleIoc.Default.Register<MyScoreViewModel>(true);
            SimpleIoc.Default.Register<AccountUpdateViewModel>(true);
            SimpleIoc.Default.Register<ChangePasswordViewModel>(true);
            SimpleIoc.Default.Register<AccountSelectedViewModel>(true);
            SimpleIoc.Default.Register<ScoreSelectedViewModel>(true);
            SimpleIoc.Default.Register<MyFanSelectedViewModel>(true);
            SimpleIoc.Default.Register<MyFavouritesSelectedViewModel>(true);
            SimpleIoc.Default.Register<MyMuseSelectedViewModel>(true);
            SimpleIoc.Default.Register<MyScoreSelectedViewModel>(true);
        }

        public MyFavouritesSelectedViewModel MyFavouritesSelectedViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MyFavouritesSelectedViewModel>(); }
        }

        public MyMuseSelectedViewModel MyMuseSelectedViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MyMuseSelectedViewModel>(); }
        }

        public MyScoreSelectedViewModel MyScoreSelectedViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MyScoreSelectedViewModel>(); }
        }

        public MyFanSelectedViewModel MyFanSelectedViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MyFanSelectedViewModel>(); }
        }

        public LogInViewModel LogInViewModel
        {
            get { return ServiceLocator.Current.GetInstance<LogInViewModel>(); }
        }

        public PivotViewModel PivotViewModel
        {
            get { return ServiceLocator.Current.GetInstance<PivotViewModel>(); }
        }

        public SignUpViewModel SignUpViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SignUpViewModel>(); }
        }

        public ForgottenPasswordViewModel ForgottenPasswordViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ForgottenPasswordViewModel>(); }
        }

        public AccountViewModel AccountViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AccountViewModel>(); }
        }

        public SearchViewModel SearchViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SearchViewModel>(); }
        }

        public MyFanViewModel MyFanViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MyFanViewModel>(); }
        }

        public MyFavouritesViewModel MyFavouritesViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MyFavouritesViewModel>(); }
        }

        public MyMuseViewModel MyMuseViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MyMuseViewModel>(); }
        }

        public MyScoreViewModel MyScoreViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MyScoreViewModel>(); }
        }


        public AccountUpdateViewModel AccountUpdateViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AccountUpdateViewModel>(); }
        }

        public ChangePasswordViewModel ChangePasswordViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ChangePasswordViewModel>(); }
        }

        public AccountSelectedViewModel AccountSelectedViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AccountSelectedViewModel>(); }
        }

        public ScoreSelectedViewModel ScoreSelectedViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ScoreSelectedViewModel>(); }
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(NavigationMainPage, typeof(MainPage));
            navigationService.Configure(NavigationPivotView, typeof(PivotView));
            navigationService.Configure(NavigationSignUpView, typeof(SignUpView));
            navigationService.Configure(NavigationForgottenPasswordView, typeof(ForgottenPasswordView));
            navigationService.Configure(NavigationAccountView, typeof(AccountView));
            navigationService.Configure(NavigationSearchView, typeof(SearchView));
            navigationService.Configure(NavigationMyFanView, typeof(MyFanView));
            navigationService.Configure(NavigationMyFavouritesView, typeof(MyFavouritesView));
            navigationService.Configure(NavigationMyMuseView, typeof(MyMuseView));
            navigationService.Configure(NavigationMyScoreView, typeof(MyScoreView));
            navigationService.Configure(NavigationAccountUpdateView, typeof(AccountUpdateView));
            navigationService.Configure(NavigationChangePasswordView, typeof(ChangePasswordView));
            navigationService.Configure(NavigationAccountSelectedView, typeof(AccountSelectedView));
            navigationService.Configure(NavigationScoreSelectedView, typeof(ScoreSelectedView));
            navigationService.Configure(NavigationMyScoreSelectedView, typeof(MyScoreSelectedView));
            navigationService.Configure(NavigationMyFanSelectedView, typeof(MyFanSelectedView));
            navigationService.Configure(NavigationMyFavouritesSelectedView, typeof(MyFavouritesSelectedView));
            navigationService.Configure(NavigationMyMuseSelectedView, typeof(MyMuseSelectedView));

            return navigationService;
        }

        public const string NavigationMainPage = "MainPage";
        public const string NavigationPivotView = "PivotView";
        public const string NavigationSignUpView = "SignUpView";
        public const string NavigationForgottenPasswordView = "ForgottenPasswordView";
        public const string NavigationAccountView = "AccountPageView";
        public const string NavigationSearchView = "SearchView";
        public const string NavigationMyFanView = "MyFanView";
        public const string NavigationMyFavouritesView = "MyFavouritesView";
        public const string NavigationMyMuseView = "MyMuseView";
        public const string NavigationMyScoreView = "MyScoreView";
        public const string NavigationAccountUpdateView = "AccountUpdateView";
        public const string NavigationChangePasswordView = "ChangePasswordView";
        public const string NavigationAccountSelectedView = "AccountSelectedView";
        public const string NavigationScoreSelectedView = "ScoreSelectedView";
        public const string NavigationMyScoreSelectedView = "MyScoreSelectedView";
        public const string NavigationMyFanSelectedView = "MyFanSelectedView";
        public const string NavigationMyFavouritesSelectedView = "MyFavouritesSelectedView";
        public const string NavigationMyMuseSelectedView = "MyMuseSelectedView";

        //public const string Navigation="";
    }
}

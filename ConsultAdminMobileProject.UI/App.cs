using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdminMobileProject.UI.View;
using ConsultAdminMobileProject.ViewModel;
using Xamarin.Forms;

namespace ConsultAdminMobileProject.UI
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    };

    public class App : Application
    {
        public static MasterDetailPage MasterDetailPage;

        public App()
        {
            var loginViewModel = new LoginViewModel();
            loginViewModel.PropertyChanged += LoginViewModel_PropertyChanged;

            MainPage = new LoginPage(loginViewModel);
        }

        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        async void LoginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var vm = sender as LoginViewModel;
            switch (e.PropertyName)
            {
                case "LoginRequired":
                    {
                        if (vm != null && vm.LoginRequired)
                        {
                            if (!(MainPage is LoginPage))
                            {
                                MainPage = new LoginPage(vm);
                            }
                        }
                        else
                        {
                            //TODO: Ska avkommenteras när API-anropet går snabbare sen (Gör att news list vyn populeras)
                            NewsListViewModel newsListModel = new NewsListViewModel();
                            await newsListModel.FillNewsList();

                            MasterDetailPage = new MasterDetailPage
                            {
                                Master = new HomeScreenMenuPage(),
                                Detail = new NavigationPage(new HomeScreenPage(newsListModel))  //TODO: Ska ändras till: Detail = new NavigationPage(new HomeScreenPage(news)) sen, se ovan
                            };
                            MainPage = MasterDetailPage;
                        }
                        break;
                    }
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

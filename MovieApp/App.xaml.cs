using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            
        }

        protected override async void OnStart()
        {
            MainPage = new MainPage();
            await Task.Delay(3000);


            MainPage = new NavigationPage(new FilmPage());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

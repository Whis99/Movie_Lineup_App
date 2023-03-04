using System;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace MovieApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            runDatabase();
            
        }

        private void runDatabase()
        {
            //Create database table
            Database.createDbTable();

            // While screen is loading update
            // the database if connected to internet
            if (Utilities.IsConnectedToInternet())
            {
                foreach (Film film in Utilities.getMovieDbList())
                {
                    Database.updateDatabase(film);

                }
            }
        }
    }

}

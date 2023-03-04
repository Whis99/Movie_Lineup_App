using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmPage : ContentPage
    {
        static List<Film> films = Database.readDbTable();
        static Film film;
        static int index = 0;
        bool internet = Utilities.IsConnectedToInternet();

        public FilmPage()
        {
            Thread.Sleep(10000);
            InitializeComponent();

            

            if (index >= 0 && index <= films.Count)
            {
                dataBiding();
            }
            else
            {
                index = 0;
                dataBiding();
            }
        }

        

        void dataBiding()
        {
            try
            {
                film = films.ElementAt(index);

                poster.Source = ImageSource.FromUri(new Uri("https://image.tmdb.org/t/p/w342" + film.poster_path));
                title.Text = film.title;
                overview.Text = film.overview;
                date.Text = film.release_date;
                if (internet)
                {
                    internetIcon.Source = "internet.png";
                }
                else
                {
                    internetIcon.Source = "noInternet.png";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private async void poster_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FilmDetail(films, index));
        }

        private void previousBtn_Clicked(object sender, EventArgs e)
        {
            index--;
            dataBiding();
        }

        private void nextBtn_Clicked(object sender, EventArgs e)
        {
            index++;
            dataBiding();
        }
    }
}
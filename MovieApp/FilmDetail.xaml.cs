using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmDetail : ContentPage
    {
        static Film film;
        public const String VIDEO_URL = "https://api.themoviedb.org/3/movie/{0}/videos?api_key=a07e22bc18f5cb106bfe4cc1f83ad8ed";

        public FilmDetail(List<Film> films, int index)
        {
            InitializeComponent();
            loadMovie(films, index);
        }

        void loadMovie(List<Film> films, int index)
        {
            try
            {
                film = films.ElementAt(index);

                title.Text =  film.title;
                overview.Text = film.overview;
                language.Text = "Language: " + film.original_language;
                voteAv.Text = "Average: " + film.vote_average.ToString();
                vote.Text = "Votes: " + film.vote_count.ToString();
                date.Text = "Date: " + film.release_date;

                bool internet = Utilities.IsConnectedToInternet();

                if (internet)
                {
                    internetIcon2.Source = "internet.png";
                    videoPlay.Source = String.Format("https://www.youtube.com/watch?v={0}", youtubeKey());
                }
                else
                {
                    internetIcon2.Source = "noInternet.png";
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public String youtubeKey()

        {

            String reponse = "";
            String youtubeKey = "";

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    reponse = webClient.DownloadString(String.Format(VIDEO_URL, film.id));
                }

                using (JsonDocument document = JsonDocument.Parse(reponse))
                {
                    JsonElement root = document.RootElement;
                    JsonElement resultsList = root.GetProperty("results");

                    int i = 0;
                    while (true)
                    {
                        String type = resultsList[i].GetProperty("type").ToString();
                        youtubeKey = resultsList[i].GetProperty("key").ToString();
                        if (type.Equals("Clip"))
                        {
                            break;
                        }
                        i++;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return youtubeKey;
        }
    }
}
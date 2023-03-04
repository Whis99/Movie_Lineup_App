using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp
{
    public class Film
    {
        private string adult { get; set; }
        public String backdrop_path { get; set; }

        private List<int> _genre_ids;

        public List<int> genre_ids()
        {
            return _genre_ids;
        }

        public void Setgenre_ids(List<int> value)
        {
            _genre_ids = value;
        }

        public int id { get; set; }
        public String original_title { get; set; }
        public String overview { get; set; }
        public String poster_path { get; set; }
        public String release_date { get; set; }
        public String original_language { get; set; }
        public float popularity { get; set; }
        public String title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }
}

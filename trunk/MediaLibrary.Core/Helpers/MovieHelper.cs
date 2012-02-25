using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace MediaLibrary.Core
{
    public class MovieHelper
    {
        public static void FindRelatedMovies(HashSet<KeyValuePair<int, int>> relatedMovies, Movie source, ObservableCollection<Movie> related)
        {
            foreach (Movie item in related)
            {
                if (!relatedMovies.Contains(new KeyValuePair<int, int>(source.ID, item.ID)) && !relatedMovies.Contains(new KeyValuePair<int, int>(item.ID, source.ID)))
                {
                    relatedMovies.Add(new KeyValuePair<int, int>(source.ID, item.ID));
                }
            }
        }
    }
}

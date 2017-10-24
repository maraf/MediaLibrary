using Neptuo;
using Neptuo.Models.Keys;
using Neptuo.Observables;
using Neptuo.Observables.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    /// <summary>
    /// A single movie.
    /// </summary>
    public class Movie : ObservableObject
    {
        /// <summary>
        /// Gets a library where movie belongs.
        /// </summary>
        public Library Library { get; private set; }

        /// <summary>
        /// Gets an unique movie key.
        /// </summary>
        public IKey Key { get; private set; }

        private string name;

        /// <summary>
        /// Gets a name of the movie.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets a collection of keys of related movies.
        /// </summary>
        public RelatedMovieObservableCollection RelatedMovieKeys { get; private set; }

        /// <summary>
        /// Gets a collection of additional field values.
        /// </summary>
        public MovieFieldValueCollection FieldValues { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="key">An unique movie key.</param>
        /// <param name="library">A library where movie belongs.</param>
        public Movie(IKey key, Library library)
        {
            Ensure.Condition.NotEmptyKey(key);
            Ensure.NotNull(library, "library");
            Key = key;
            Library = library;
            RelatedMovieKeys = new RelatedMovieObservableCollection(Key, library.Movies);
            FieldValues = new MovieFieldValueCollection(library.MovieFields, RaisePropertyChanged);
        }
    }
}

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
        public ObservableCollection<IKey> RelatedMovieKeys { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="key">An unique movie key.</param>
        public Movie(IKey key)
        {
            Ensure.Condition.NotEmptyKey(key);
            Key = key;
            RelatedMovieKeys = new ObservableCollection<IKey>();
        }
    }
}

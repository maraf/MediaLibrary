using Neptuo;
using Neptuo.Models.Keys;
using Neptuo.Observables.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    /// <summary>
    /// A collection of movies.
    /// </summary>
    public class MovieCollection : ObservableCollection<Movie>
    {
        /// <summary>
        /// Gets a movie by <paramref name="key"/>.
        /// </summary>
        /// <param name="key">A key of the movie to find.</param>
        /// <returns>A movie with <paramref name="key"/> or <c>null</c>.</returns>
        public Movie FindByKey(IKey key)
        {
            Ensure.Condition.NotEmptyKey(key);
            return this.FirstOrDefault(m => m.Key.Equals(key));
        }


        /// <summary>
        /// Tries to remove movie with <paramref name="key"/>.
        /// Returns <c>true</c> if removal was successful.
        /// </summary>
        /// <param name="key">A key of the movie to remove.</param>
        /// <returns><c>true</c> if removal was successful; <c>false</c> otherwise.</returns>
        public bool Remove(IKey key)
        {
            Movie model = FindByKey(key);
            if (model == null)
                return false;

            return Remove(model);
        }
    }
}

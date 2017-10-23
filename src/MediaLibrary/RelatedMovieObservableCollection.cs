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
    /// A collection of unique related movies.
    /// </summary>
    public class RelatedMovieObservableCollection : ObservableCollection<IKey>
    {
        private readonly IKey key;
        private readonly MovieCollection movies;

        /// <summary>
        /// Creates a new instance with movie <paramref name="key"/> and collection of <paramref name="movies"/>.
        /// </summary>
        /// <param name="key">A key of the movie where relations belong.</param>
        /// <param name="movies">A collection of all movies to validate keys.</param>
        public RelatedMovieObservableCollection(IKey key, MovieCollection movies)
        {
            Ensure.Condition.NotEmptyKey(key);
            Ensure.NotNull(movies, "movies");
            this.key = key;
            this.movies = movies;
        }

        private Movie GetMovie(IKey otherKey)
        {
            Movie other = movies.FindByKey(otherKey);
            if (other == null)
                throw Ensure.Exception.ArgumentOutOfRange("otherKey", "Related movie was not found.");

            return other;
        }

        protected override void InsertItem(int index, IKey otherKey)
        {
            if (!isInternal)
            {
                if (key.Equals(otherKey))
                    throw Ensure.Exception.ArgumentOutOfRange("otherKey", "Relation to itself is not supported.");

                if (Contains(otherKey))
                    return;

                Movie other = GetMovie(otherKey);
                other.RelatedMovieKeys.AddInternal(key);
            }

            base.InsertItem(index, otherKey);
        }

        protected override void RemoveItem(int index)
        {
            if (!isInternal)
            {
                IKey otherKey = this[index];
                Movie other = GetMovie(otherKey);
                other.RelatedMovieKeys.RemoveInternal(key);
            }

            base.RemoveItem(index);
        }


        private bool isInternal = false;

        internal void AddInternal(IKey otherKey)
        {
            try
            {
                isInternal = true;
                Add(otherKey);
            }
            finally
            {
                isInternal = false;
            }
        }

        private void RemoveInternal(IKey otherKey)
        {
            try
            {
                isInternal = true;
                Remove(otherKey);
            }
            finally
            {
                isInternal = false;
            }
        }
    }
}

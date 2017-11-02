using Neptuo;
using Neptuo.Models.Keys;
using Neptuo.Observables;
using Neptuo.Observables.Collections;
using Neptuo.PresentationModels;
using Neptuo.PresentationModels.Observables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    /// <summary>
    /// A single movie.
    /// </summary>
    public class Movie : ObservableModel
    {
        /// <summary>
        /// Gets a library where movie belongs.
        /// </summary>
        public Library Library { get; private set; }

        /// <summary>
        /// Gets an unique movie key.
        /// </summary>
        public IKey Key { get; private set; }

        /// <summary>
        /// Gets or sets a name of the movie.
        /// </summary>
        public string Name
        {
            get { return this.GetValueOrDefault(nameof(Name), (string)null); }
            set { this.TrySetValue(nameof(Name), value); }
        }

        /// <summary>
        /// Gets or sets a date time stamp when the movies was added.
        /// </summary>
        public DateTime Added
        {
            get { return this.GetValueOrDefault(nameof(Added), DateTime.Now); }
            set { this.TrySetValue(nameof(Added), value); }
        }

        /// <summary>
        /// Gets a collection of keys of related movies.
        /// </summary>
        public RelatedMovieObservableCollection RelatedMovieKeys { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="key">An unique movie key.</param>
        /// <param name="library">A library where movie belongs.</param>
        public Movie(IKey key, Library library)
            : base(library.MovieDefinition)
        {
            Ensure.Condition.NotEmptyKey(key);
            Ensure.NotNull(library, "library");
            Key = key;
            Library = library;
            RelatedMovieKeys = new RelatedMovieObservableCollection(Key, library.Movies);
        }

        /// <summary>
        /// Returns <c>true</c> if any value contains <paramref name="text"/>.
        /// </summary>
        /// <param name="text">A search phrase.</param>
        /// <returns><c>true</c> if any value contains <paramref name="text"/>; <c>false</c> otherwise.</returns>
        public bool IsMatched(string text)
        {
            if (String.IsNullOrEmpty(text))
                return true;

            foreach (IFieldDefinition fieldDefinition in ModelDefinition.Fields)
            {
                if (fieldDefinition.Metadata.TryGet("IsSearchable", out bool isSearchable) && !isSearchable)
                    continue;

                if (TryGetValue(fieldDefinition.Identifier, out object value))
                {
                    if (value == null)
                        continue;

                    if (value.ToString().ToLowerInvariant().Contains(text))
                        return true;
                }
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            Movie other = obj as Movie;
            if (other == null)
                return false;

            return Key.Equals(other.Key);
        }
    }
}

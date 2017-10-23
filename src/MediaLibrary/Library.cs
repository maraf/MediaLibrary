using Neptuo.Activators;
using Neptuo.Models.Keys;
using Neptuo.Observables.Collections;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    /// <summary>
    /// A collection of movies saved at a location.
    /// </summary>
    public class Library : IFactory<Movie>
    {
        /// <summary>
        /// Gets a collection of movies.
        /// </summary>
        public ObservableCollection<Movie> Movies { get; private set; }

        /// <summary>
        /// Gets a library configuration.
        /// </summary>
        public LibraryConfiguration Configuration { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public Library()
        {
            Movies = new ObservableCollection<Movie>();
            Movies.CollectionChanged += OnMoviesChanged;

            Configuration = new LibraryConfiguration();
            Configuration.PropertyChanged += OnConfigurationChanged;
        }

        private void OnMoviesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void OnConfigurationChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        public Movie Create()
        {
            return new Movie(GuidKey.Create(Guid.NewGuid(), "Movie"));
        }
    }
}

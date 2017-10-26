using Neptuo;
using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using Neptuo.Models.Keys;
using Neptuo.Observables.Collections;
using Neptuo.PresentationModels;
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
    public class Library : IFactory<Movie>, IFactory<Movie, string>
    {
        /// <summary>
        /// Gets a collection of movies.
        /// </summary>
        public MovieCollection Movies { get; private set; }

        /// <summary>
        /// Gets a movie presentation model definition.
        /// </summary>
        public IModelDefinition MovieDefinition { get; private set; }

        /// <summary>
        /// Gets a library configuration.
        /// </summary>
        public LibraryConfiguration Configuration { get; private set; }

        /// <summary>
        /// Gets a configuration presentation model definition.
        /// </summary>
        public IModelDefinition ConfigurationDefinition { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public Library()
        {
            ConfigurationDefinition = new ModelDefinition(
                "LibraryConfiguration",
                new List<IFieldDefinition>()
                {
                    new FieldDefinition(nameof(LibraryConfiguration.Name), typeof(string), new KeyValueCollection().Add("Label", "Name").Add("AutoFocus", true)),
                    new FieldDefinition(nameof(LibraryConfiguration.FilePath), typeof(string), new KeyValueCollection().Add("Label", "File Path").Add("IsPersistent", false)),
                },
                new KeyValueCollection()
            );

            MovieDefinition = new ModelDefinition(
                "Movie",
                new List<IFieldDefinition>()
                {
                    new FieldDefinition(nameof(Movie.Name), typeof(string), new KeyValueCollection().Add("Label", "Name").Add("AutoFocus", true)),
                    new FieldDefinition("OriginalName", typeof(string), new KeyValueCollection().Add("Label", "Original Name")),
                    new FieldDefinition("Storage", typeof(string), new KeyValueCollection().Add("Label", "Storage")),
                    new FieldDefinition("Added", typeof(DateTime), new KeyValueCollection().Add("Label", "Added").Add("IsReadOnly", true))
                },
                new KeyValueCollection()
            );

            Configuration = new LibraryConfiguration(this);
            Configuration.PropertyChanged += OnConfigurationChanged;

            Movies = new MovieCollection();
            Movies.CollectionChanged += OnMoviesChanged;
        }

        private void OnMoviesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Movie model in e.OldItems)
                    model.RelatedMovieKeys.Clear();
            }
        }

        private void OnConfigurationChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        public IKey CreateMovieKey()
        {
            return GuidKey.Create(Guid.NewGuid(), "Movie");
        }

        public Movie Create()
        {
            Movie movie = new Movie(CreateMovieKey(), this);
            Movies.Add(movie);
            return movie;
        }

        public Movie Create(string name)
        {
            Movie movie = new Movie(GuidKey.Create(Guid.NewGuid(), "Movie"), this)
            {
                Name = name
            };
            Movies.Add(movie);
            return movie;
        }
    }
}

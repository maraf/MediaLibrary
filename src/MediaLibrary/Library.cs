using Neptuo;
using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using Neptuo.Models.Keys;
using Neptuo.Observables.Collections;
using Neptuo.PresentationModels;
using Neptuo.PresentationModels.UI;
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
                    new FieldDefinition(
                        nameof(LibraryConfiguration.Name), 
                        typeof(string), 
                        (MetadataCollection)new MetadataCollection()
                            .AddLabel("Name")
                            .AddIsAutoFocus(true)
                    ),
                    new FieldDefinition(
                        nameof(LibraryConfiguration.FilePath), 
                        typeof(string),
                        (MetadataCollection)new MetadataCollection()
                            .AddLabel("File Path")
                            .Add("IsPersistent", false)
                    ),
                },
                new MetadataCollection()
            );

            MovieDefinition = new ModelDefinition(
                "Movie",
                new List<IFieldDefinition>()
                {
                    new FieldDefinition(
                        nameof(Movie.Name),
                        typeof(string),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(0, 0)
                            .AddGridColumnSpan(2)
                            .AddLabel("Name")
                            .AddIsAutoFocus(true)
                            .Add("Main.Left", true)
                    ),
                    new FieldDefinition(
                        "OriginalName",
                        typeof(string),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(0, 1)
                            .AddGridColumnSpan(2)
                            .AddLabel("Original Name")
                            .Add("Additional.Left", true)
                    ),
                    new FieldDefinition(
                        "Storage",
                        typeof(string),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(0, 2)
                            .AddGridColumnSpan(2)
                            .AddLabel("Storage")
                            .Add("Main.Right", true)
                    ),
                    new FieldDefinition(
                        "Year",
                        typeof(int?),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(0, 3)
                            .AddLabel("Year")
                            .Add("Additional.Right", true)
                    ),
                    new FieldDefinition(
                        "Country",
                        typeof(string),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(1, 3)
                            .AddLabel("Country")
                            .Add("Additional.Right", true)
                    ),
                    new FieldDefinition(
                        "Category",
                        typeof(string),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(0, 4)
                            .AddGridColumnSpan(2)
                            .AddLabel("Category")
                    ),
                    new FieldDefinition(
                        nameof(Movie.RelatedMovieKeys),
                        typeof(IEnumerable<IKey>),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(0, 5)
                            .AddGridColumnSpan(2)
                            .AddLabel("Related Movies")
                            .Add("IsPersistent", false)
                            .Add("IsSearchable", false)
                            .Add("IsSortable", false)
                    ),
                    new FieldDefinition(
                        "Language",
                        typeof(int?),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(0, 6)
                            .AddLabel("Language")
                            .Add("IsSortable", false)
                            .Add("IsSortable", false)
                    ),
                    new FieldDefinition(
                        "Actors",
                        typeof(string),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(1, 6)
                            .AddLabel("Actors")
                            .Add("IsSortable", false)
                            .Add("Additional.Left", true)
                    ),
                    new FieldDefinition(
                        "Description",
                        typeof(string),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(0, 7)
                            .AddGridColumnSpan(2)
                            .AddLabel("Description")
                            .Add("IsSortable", false)
                            .Add("IsXmlElementContent", true)
                    ),
                    new FieldDefinition(
                        nameof(Movie.Added),
                        typeof(DateTime),
                        (MetadataCollection)new MetadataCollection()
                            .AddGrid(1, 8)
                            .AddGridColumnSpan(2)
                            .AddLabel("Added")
                            .AddIsReadOnly(true)
                            .Add("IsSearchable", false)
                    )
                },
                new MetadataCollection()
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

using Neptuo;
using Neptuo.Observables;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels
{
    public class MovieEditViewModel : ObservableObject
    {
        private readonly Movie movie;

        public MovieCollection Collection { get; }

        private string name;
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

        public MovieEditViewModel(MovieCollection collection, IEnumerable<IFieldDefinition> fields)
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(fields, "fields");
            Collection = collection;
        }

        public MovieEditViewModel(MovieCollection collection, IEnumerable<IFieldDefinition> fields, Movie movie)
            : this(collection, fields)
        {
            Ensure.NotNull(movie, "movie");
            this.movie = movie;

            Name = movie.Name;
        }
    }
}

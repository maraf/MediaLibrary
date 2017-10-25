using MediaLibrary.ViewModels.Commands;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Activators;
using Neptuo.Observables;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaLibrary.ViewModels
{
    public class MovieEditViewModel : ObservableObject, IModelValueProvider
    {
        private readonly Movie movie;

        public MovieCollection Collection { get; }
        public bool IsNewRecord { get; }
        public IReadOnlyCollection<FieldViewModel> Fields { get; private set; }
        public ICommand Save { get; }

        public string Name
        {
            get
            {
                FieldViewModel fieldDefinition = Fields.First(f => f.Definition.Identifier == nameof(Movie.Name));
                return fieldDefinition.Value as string;
            }
            set
            {
                FieldViewModel fieldDefinition = Fields.First(f => f.Definition.Identifier == nameof(Movie.Name));
                fieldDefinition.Value = value;
            }
        }

        public MovieEditViewModel(Library library, INavigatorContext navigator)
        {
            Ensure.NotNull(library, "library");

            Collection = library.Movies;
            Fields = new List<FieldViewModel>(library.MovieDefinition.Fields.Select(f => new FieldViewModel(f, RaisePropertyChanged)));
            IsNewRecord = true;

            Save = new SaveMovieCommand(this, library, navigator, null);
        }

        public MovieEditViewModel(Library library, INavigatorContext navigator, Movie movie)
        {
            Ensure.NotNull(library, "library");
            Ensure.NotNull(movie, "movie");
            this.movie = movie;

            Collection = library.Movies;
            Fields = new List<FieldViewModel>(library.MovieDefinition.Fields.Select(f => new FieldViewModel(f, RaisePropertyChanged)));

            foreach (FieldViewModel fieldViewModel in Fields)
            {
                if (movie.FieldValues.TryGetValue(fieldViewModel.Definition.Identifier, out object value))
                    fieldViewModel.Value = value;
            }

            Save = new SaveMovieCommand(this, library, navigator, movie);
        }

        public bool TryGetValue(string identifier, out object value)
        {
            Ensure.NotNullOrEmpty(identifier, "identifier");
            FieldViewModel field = Fields.FirstOrDefault(f => f.Definition.Identifier == identifier);
            if(field != null)
            {
                value = field.Value;
                return true;
            }

            value = null;
            return false;
        }

        public bool TrySetValue(string identifier, object value)
        {
            Ensure.NotNullOrEmpty(identifier, "identifier");
            FieldViewModel field = Fields.FirstOrDefault(f => f.Definition.Identifier == identifier);
            if (field != null)
            {
                field.Value = value;
                return true;
            }

            return false;
        }

        public void Dispose()
        { }
    }
}

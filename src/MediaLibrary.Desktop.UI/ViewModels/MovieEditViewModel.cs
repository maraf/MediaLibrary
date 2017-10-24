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
    public class MovieEditViewModel : ObservableObject
    {
        private readonly Movie movie;

        public MovieCollection Collection { get; }
        public bool IsNewRecord { get; }

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

        public IReadOnlyCollection<FieldViewModel> Fields { get; private set; }

        public ICommand Save { get; }

        public MovieEditViewModel(Library library, INavigatorContext navigator)
        {
            Ensure.NotNull(library, "library");

            Collection = library.Movies;
            Fields = new List<FieldViewModel>(library.MovieFields.Select(f => new FieldViewModel(f)));
            IsNewRecord = true;

            Save = new SaveMovieCommand(this, library, navigator, null);
        }

        public MovieEditViewModel(Library library, INavigatorContext navigator, Movie movie)
        {
            Ensure.NotNull(library, "library");
            Ensure.NotNull(movie, "movie");
            this.movie = movie;

            Collection = library.Movies;
            Name = movie.Name;
            Fields = new List<FieldViewModel>(library.MovieFields.Select(f => new FieldViewModel(f)));

            Save = new SaveMovieCommand(this, library, navigator, movie);
        }
    }
}

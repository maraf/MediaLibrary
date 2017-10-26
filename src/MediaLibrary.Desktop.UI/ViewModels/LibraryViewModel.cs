using MediaLibrary.ViewModels.Commands;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Models.Keys;
using Neptuo.Observables;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaLibrary.ViewModels
{
    public class LibraryViewModel : ObservableObject
    {
        private readonly Library library;
        private readonly INavigator navigator;

        public string Name => library.Configuration.Name;
        public IEnumerable<Movie> Movies => library.Movies;
        public IEnumerable<SortViewModel> Sorts { get; }

        public ICommand Create { get; }
        public Command<IKey> Edit { get; }
        public Command<IKey> Delete { get; }
        public ICommand Save { get; }
        public ICommand OpenConfiguration { get; }

        public LibraryViewModel(Library library, INavigator navigator, XmlStore store)
        {
            Ensure.NotNull(library, "library");
            Ensure.NotNull(navigator, "navigator");
            this.library = library;
            this.navigator = navigator;

            Sorts = new List<SortViewModel>(library.MovieDefinition.Fields.Select(f => new SortViewModel(f)));
            SortViewModel firstSort = Sorts.FirstOrDefault();
            if (firstSort != null)
                firstSort.IsActive = true;

            Create = new DelegateCommand(() => navigator.CreateMovieAsync(library));
            Edit = new EditMovieCommand(library, navigator);
            Delete = new DeleteMovieCommand(library.Movies, navigator);
            Save = new SaveCommand(library, store);
            OpenConfiguration = new DelegateCommand(() => navigator.LibraryConfigurationAsync(library));

            library.Configuration.PropertyChanged += OnConfigurationPropertyChanged;
        }

        private void OnConfigurationPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(library.Configuration.Name))
                RaisePropertyChanged(nameof(Name));
        }
        
        public void SelectedMovieChanged()
        {
            ((EditMovieCommand)Edit).RaiseCanExecuteChanged();
            ((DeleteMovieCommand)Delete).RaiseCanExecuteChanged();
        }
    }
}

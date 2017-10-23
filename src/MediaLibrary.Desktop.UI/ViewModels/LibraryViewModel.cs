using MediaLibrary.ViewModels.Services;
using Neptuo;
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

        public ICommand Create { get; }

        public LibraryViewModel(Library library, INavigator navigator)
        {
            Ensure.NotNull(library, "library");
            Ensure.NotNull(navigator, "navigator");
            this.library = library;
            this.navigator = navigator;

            Create = new DelegateCommand(() => navigator.CreateMovieAsync(library));

            library.Configuration.PropertyChanged += OnConfigurationPropertyChanged;
        }

        private void OnConfigurationPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(library.Configuration.Name))
                RaisePropertyChanged(nameof(Name));
        }

        public Task FilterAsync(string text)
        {
            return Task.CompletedTask;
        }
    }
}

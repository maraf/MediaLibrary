using MediaLibrary.ViewModels;
using MediaLibrary.ViewModels.Services;
using MediaLibrary.Views;
using Neptuo;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaLibrary
{
    public class AppNavigator : INavigator
    {
        private readonly App application;
        private readonly ILibraryStore store;

        private MainWindow main;
        private LibraryConfigurationWindow libraryConfiguration;
        private MovieEditWindow movieEdit;

        public AppNavigator(App application, ILibraryStore store)
        {
            Ensure.NotNull(application, "application");
            Ensure.NotNull(store, "store");
            this.application = application;
            this.store = store;
        }

        public async Task CreateMovieAsync(Library library)
        {
            Ensure.NotNull(library, "library");
            if (movieEdit == null)
            {
                movieEdit = new MovieEditWindow(this, library, null);
                movieEdit.Closed += OnMovieEditClosed;

                if (main != null)
                {
                    movieEdit.Owner = main;
                    movieEdit.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                }
                else
                {
                    movieEdit.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }

                movieEdit.Show();
            }

            movieEdit.Activate();
        }

        private void OnMovieEditClosed(object sender, EventArgs e)
        {
            movieEdit.Closed -= OnMovieEditClosed;
            movieEdit = null;
        }

        public async Task EditMovieAsync(Library library, IKey movieKey)
        {
            Ensure.NotNull(library, "library");
            Ensure.Condition.NotEmptyKey(movieKey);

            if (movieEdit == null)
            {
                movieEdit = new MovieEditWindow(this, library, library.Movies.FindByKey(movieKey));
                movieEdit.Closed += OnMovieEditClosed;

                if (main != null)
                {
                    movieEdit.Owner = main;
                    movieEdit.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                }
                else
                {
                    movieEdit.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }

                movieEdit.Show();
            }

            movieEdit.Activate();
        }

        public async Task LibraryAsync(Library library)
        {
            Ensure.NotNull(library, "library");
            if (main == null)
            {
                main = new MainWindow();
                main.Closed += OnMainClosed;
                main.DataContext = new MainViewModel(library, this, store);

                main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                main.Show();

                application.MainWindow = main;
            }

            main.Activate();
        }

        private void OnMainClosed(object sender, EventArgs e)
        {
            main.Closed -= OnMainClosed;
            main = null;
            application.MainWindow = null;
        }

        public async Task LibraryConfigurationAsync(Library library)
        {
            Ensure.NotNull(library, "library");
            if (libraryConfiguration == null)
            {
                libraryConfiguration = new LibraryConfigurationWindow(this, library);
                libraryConfiguration.Closed += OnLibraryConfigurationClosed;

                if (main != null)
                {
                    libraryConfiguration.Owner = main;
                    libraryConfiguration.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                }
                else
                {
                    libraryConfiguration.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }

                libraryConfiguration.Show();
            }

            libraryConfiguration.Activate();
        }

        private void OnLibraryConfigurationClosed(object sender, EventArgs e)
        {
            libraryConfiguration.Closed -= OnLibraryConfigurationClosed;
            libraryConfiguration = null;
        }

        public Task<bool> ConfirmAsync(string message)
        {
            return Task.FromResult(MessageBox.Show(message, "Media Library", MessageBoxButton.YesNo) == MessageBoxResult.Yes);
        }

        public Task<IEnumerable<IKey>> SelectMoviesAsync()
        {
            return Task.FromResult(Enumerable.Empty<IKey>());
        }
    }
}

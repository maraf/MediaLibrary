using MediaLibrary.ViewModels;
using MediaLibrary.ViewModels.Services;
using MediaLibrary.Views;
using Microsoft.Win32;
using Neptuo;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaLibrary
{
    public partial class AppNavigator : INavigator
    {
        private readonly App application;
        private readonly ILibraryStore store;
        private readonly IChangeTracker changeTracker;

        private MainWindow main;
        private LibraryConfigurationWindow libraryConfiguration;
        private MovieEditWindow movieEdit;
        private MovieSelectWindow movieSelect;

        public AppNavigator(App application, ILibraryStore store, IChangeTracker changeTracker)
        {
            Ensure.NotNull(application, "application");
            Ensure.NotNull(store, "store");
            Ensure.NotNull(changeTracker, "changeTracker");
            this.application = application;
            this.store = store;
            this.changeTracker = changeTracker;
        }

        public async Task CreateMovieAsync(Library library)
        {
            Ensure.NotNull(library, "library");
            if (movieEdit == null)
            {
                movieEdit = new MovieEditWindow(this, changeTracker, library, null);
                movieEdit.Closed += OnMovieEditClosed;
                StartupLocation(movieEdit);
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
                movieEdit = new MovieEditWindow(this, changeTracker, library, library.Movies.FindByKey(movieKey));
                movieEdit.Closed += OnMovieEditClosed;
                StartupLocation(movieEdit);
                movieEdit.Show();
            }

            movieEdit.Activate();
        }

        public async Task LibraryAsync(Library library)
        {
            Ensure.NotNull(library, "library");
            if (main == null)
            {
                main = new MainWindow(this);
                main.Closed += OnMainClosed;
            }

            main.DataContext = new MainViewModel(library, this, store, changeTracker);

            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            main.Show();

            application.MainWindow = main;

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
                libraryConfiguration = new LibraryConfigurationWindow(this, changeTracker, library);
                libraryConfiguration.Closed += OnLibraryConfigurationClosed;
                StartupLocation(libraryConfiguration);
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
            bool result = MessageBox.Show(message, "Media Library", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
            return Task.Run(() => result);
        }

        public Task<IEnumerable<IKey>> SelectMoviesAsync(Library library)
        {
            Ensure.NotNull(library, "library");
            TaskCompletionSource<IEnumerable<IKey>> result = new TaskCompletionSource<IEnumerable<IKey>>();

            if (movieSelect == null)
            {
                Context<IEnumerable<IKey>> navigator = new Context<IEnumerable<IKey>>(result);

                movieSelect = new MovieSelectWindow(navigator);
                navigator.SetWindow(movieSelect);
                StartupLocation(movieSelect, (Window)movieEdit ?? main);

                movieSelect.Closed += OnMovieSelectClosed;
                movieSelect.DataContext = new LibraryViewModel(library);
                movieSelect.Show();
            }

            movieSelect.Activate();
            return result.Task;
        }

        private void OnMovieSelectClosed(object sender, EventArgs e)
        {
            movieSelect.Closed -= OnMovieSelectClosed;
            movieSelect = null;
        }

        public async Task SelectLibraryAsync()
        {
            string filePath = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = ".xml";
            bool? result = dialog.ShowDialog();
            if (result ?? false)
                filePath = dialog.FileName;

            if (filePath != null)
            {
                Library library = new Library();
                library.Configuration.FilePath = filePath;
                library.Configuration.Name = Path.GetFileNameWithoutExtension(filePath);
                await store.LoadAsync(library);
                await LibraryAsync(library);
            }
        }

        private void StartupLocation(Window wnd, Window owner = null)
        {
            if (owner == null)
                owner = main;

            if (owner != null)
            {
                wnd.Owner = owner;
                wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
        }
    }
}

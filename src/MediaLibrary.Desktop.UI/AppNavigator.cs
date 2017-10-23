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
        private MainWindow main;
        private MovieEditWindow movieEdit;

        public async Task CreateMovieAsync(Library library)
        {
            Ensure.NotNull(library, "library");
            if (movieEdit == null)
            {
                movieEdit = new MovieEditWindow();
                movieEdit.Closed += OnMovieEditClosed;
                movieEdit.DataContext = new MovieEditViewModel(library, movieEdit);

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

        public Task EditMovieAsync(Library library, IKey movieKey)
        {
            throw new NotImplementedException();
        }

        public async Task Library(Library library)
        {
            Ensure.NotNull(library, "library");
            if (main == null)
            {
                main = new MainWindow();
                main.Closed += OnMainClosed;
                main.DataContext = new LibraryViewModel(library, this);

                main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                main.Show();
            }

            main.Activate();
        }

        private void OnMainClosed(object sender, EventArgs e)
        {
            main.Closed -= OnMainClosed;
            main = null;
        }
    }
}

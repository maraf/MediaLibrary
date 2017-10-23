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
                MovieEditViewModel viewModel = new MovieEditViewModel(library.Movies, library.MovieFields);
                movieEdit = new MovieEditWindow();
                movieEdit.DataContext = viewModel;

                if (main != null)
                    movieEdit.Owner = main;

                movieEdit.Show();
            }

            movieEdit.Activate();
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
                main.DataContext = new LibraryViewModel(library, this);

                main.Show();
            }

            main.Activate();
        }
    }
}

using MediaLibrary.ViewModels;
using MediaLibrary.ViewModels.Services;
using MediaLibrary.Views.Controls;
using Neptuo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaLibrary.Views
{
    public partial class MainWindow : Window
    {
        private readonly INavigator navigator;

        public MainViewModel ViewModel => (MainViewModel)DataContext;
        public LibraryView View => (LibraryView)Content;

        public MainWindow(INavigator navigator)
        {
            Ensure.NotNull(navigator, "navigator");
            this.navigator = navigator;

            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            View.Focus();
        }

        private void lvwMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedMovieChanged();
        }

        private void lvwMovies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Movie movie = View.SelectedItem;
            if (movie != null && ViewModel.Edit.CanExecute(movie.Key))
                ViewModel.Edit.Execute(movie.Key);
        }

        private bool isConfirmed;

        private async void OnClosing(object sender, CancelEventArgs e)
        {
            if (isConfirmed)
            {
                isConfirmed = false;
                return;
            }

            if (ViewModel.HasChange)
            {
                e.Cancel = true;

                if (await navigator.ConfirmAsync("You have unsaved changes. Do you want to save them now?"))
                    ViewModel.Save.Execute(null);

                isConfirmed = true;
                Close();
            }
        }
    }
}

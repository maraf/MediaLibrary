using MediaLibrary.ViewModels;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
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

namespace MediaLibrary.Views.Controls
{
    public partial class LibraryView : UserControl
    {
        public LibraryViewModel ViewModel
        {
            get { return (LibraryViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", 
            typeof(LibraryViewModel), 
            typeof(LibraryView), 
            new PropertyMetadata(null, OnViewModelChanged)
        );

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LibraryView control = (LibraryView)d;
            control.DataContext = e.NewValue;
        }

        public LibraryView()
        {
            InitializeComponent();
            Background = null;

            kebFind.Command = new DelegateCommand(() => tbxFilter.Focus());

            //brdTop.Background = new SolidColorBrush(SystemColorProvider.ColorizationColor());
        }

        public new void Focus()
        {
            lvwMovies.Focus();
        }

        private async void tbxFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                await ViewModel.FilterAsync(tbxFilter.Text);
        }

        private void lvwMovies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Movie movie = (Movie)lvwMovies.SelectedItem;
            if (movie != null && ViewModel.Edit.CanExecute(movie.Key))
                ViewModel.Edit.Execute(movie.Key);
        }

        private void lvwMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedMovieChanged();
        }

        private void tbxFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}

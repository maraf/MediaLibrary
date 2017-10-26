using MediaLibrary.ViewModels;
using Neptuo.Observables.Commands;
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

namespace MediaLibrary.Views.Controls
{
    public partial class LibraryView : UserControl
    {
        private readonly CollectionViewSource movies;

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
            DataContextChanged += OnDataContextChanged;
            Loaded += OnLoaded;

            kebFind.Command = new DelegateCommand(() => tbxFilter.Focus());

            //brdTop.Background = new SolidColorBrush(SystemColorProvider.ColorizationColor());

            movies = (CollectionViewSource)Resources["MoviesCollectionView"];
            movies.Filter += OnMoviesFilter;
        }

        private void OnLoaded(object sender, RoutedEventArgs e) => UpdateDefaultSorting();
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) => UpdateDefaultSorting();

        private void UpdateDefaultSorting()
        {
            if (ViewModel != null)
            {
                SortViewModel sortViewModel = ViewModel.Sorts.FirstOrDefault(s => s.IsActive);
                if (sortViewModel != null)
                    UpdateSorting(sortViewModel);
            }
        }

        private void OnMoviesFilter(object sender, FilterEventArgs e)
        {
            Movie model = (Movie)e.Item;
            e.Accepted = model.IsMatched(serachPhrase);
        }

        public new void Focus()
        {
            lvwMovies.Focus();
        }

        private string serachPhrase;

        private void tbxFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                serachPhrase = tbxFilter.Text.ToLowerInvariant();
                movies.View.Refresh();
                lvwMovies.SelectedIndex = 0;
            }
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

        private void srvSorts_SelectionChanged(object sender, SortViewModelChangedEventArgs e) => UpdateSorting(e.ViewModel);

        private void UpdateSorting(SortViewModel newViewModel)
        {
            SortDescription description = movies.SortDescriptions.FirstOrDefault();
            SortViewModel oldViewModel = description != null ? ViewModel.Sorts.FirstOrDefault(s => s.FieldDefinition.Identifier == description.PropertyName) : null;

            if (oldViewModel != null)
            {
                if (newViewModel == oldViewModel)
                {
                    oldViewModel.Direction = oldViewModel.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                }
                else
                {
                    oldViewModel.IsActive = false;
                    newViewModel.Direction = newViewModel.DefaultDirection;
                }
            }
            else
            {
                newViewModel.Direction = newViewModel.DefaultDirection;
            }

            newViewModel.IsActive = true;

            movies.SortDescriptions.Clear();
            movies.SortDescriptions.Add(new SortDescription(newViewModel.FieldDefinition.Identifier, newViewModel.Direction));
            movies.View.Refresh();
        }
    }
}

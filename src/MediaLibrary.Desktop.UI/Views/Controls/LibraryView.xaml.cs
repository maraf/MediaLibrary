using MediaLibrary.ViewModels;
using Neptuo.Observables.Collections;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            control.Content.DataContext = e.NewValue;
        }

        public ObservableCollection<UiCommand> TopCommands
        {
            get { return (ObservableCollection<UiCommand>)GetValue(TopCommandsProperty); }
            set { SetValue(TopCommandsProperty, value); }
        }

        public static readonly DependencyProperty TopCommandsProperty = DependencyProperty.Register(
            "TopCommands",
            typeof(ObservableCollection<UiCommand>),
            typeof(LibraryView),
            new PropertyMetadata(new ObservableCollection<UiCommand>())
        );

        public ObservableCollection<UiCommand> BottomLeftCommands
        {
            get { return (ObservableCollection<UiCommand>)GetValue(BottomLeftCommandsProperty); }
            set { SetValue(BottomLeftCommandsProperty, value); }
        }

        public static readonly DependencyProperty BottomLeftCommandsProperty = DependencyProperty.Register(
            "BottomLeftCommands",
            typeof(ObservableCollection<UiCommand>),
            typeof(LibraryView),
            new PropertyMetadata(new ObservableCollection<UiCommand>())
        );

        public ObservableCollection<UiCommand> BottomRightCommands
        {
            get { return (ObservableCollection<UiCommand>)GetValue(BottomRightCommandsProperty); }
            set { SetValue(BottomRightCommandsProperty, value); }
        }

        public static readonly DependencyProperty BottomRightCommandsProperty = DependencyProperty.Register(
            "BottomRightCommands",
            typeof(ObservableCollection<UiCommand>),
            typeof(LibraryView),
            new PropertyMetadata(new ObservableCollection<UiCommand>())
        );

        public event EventHandler<MouseButtonEventArgs> ListViewMouseDoubleClick;
        public event EventHandler<SelectionChangedEventArgs> ListViewMouseSelectionChanged;

        public Movie SelectedItem
        {
            get { return (Movie)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public IEnumerable<Movie> SelectedItems => lvwMovies.SelectedItems.OfType<Movie>();

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(Movie),
            typeof(LibraryView),
            new PropertyMetadata(null)
        );

        public LibraryView()
        {
            InitializeComponent();
            SetValue(TopCommandsProperty, new ObservableCollection<UiCommand>());
            SetValue(BottomLeftCommandsProperty, new ObservableCollection<UiCommand>());
            SetValue(BottomRightCommandsProperty, new ObservableCollection<UiCommand>());

            Background = null;
            DataContextChanged += OnDataContextChanged;
            Loaded += OnLoaded;

            kebFind.Command = new DelegateCommand(() => tbxFilter.Focus());

            movies = (CollectionViewSource)Resources["MoviesCollectionView"];
            movies.Filter += OnMoviesFilter;

            ListViewMouseSelectionChanged += (sender, e) => SelectedItem = lvwMovies.SelectedItem as Movie;
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

        public new void Focus()
        {
            lvwMovies.Focus();
        }

        #region Filtering

        private string serachPhrase;

        private void tbxFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Filter();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void OnMoviesFilter(object sender, FilterEventArgs e)
        {
            Movie model = (Movie)e.Item;
            e.Accepted = model.IsMatched(serachPhrase);
        }

        private void Filter()
        {
            serachPhrase = tbxFilter.Text.ToLowerInvariant();
            movies.View.Refresh();
            lvwMovies.SelectedIndex = 0;
        }

        private void tbxFilter_GotFocus(object sender, RoutedEventArgs e) => ((TextBox)sender).SelectAll();

        #endregion

        #region Sorting

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

        #endregion

        private void lvwMovies_SelectionChanged(object sender, SelectionChangedEventArgs e) => ListViewMouseSelectionChanged?.Invoke(this, e);
        private void lvwMovies_MouseDoubleClick(object sender, MouseButtonEventArgs e) => ListViewMouseDoubleClick?.Invoke(this, e);
    }
}

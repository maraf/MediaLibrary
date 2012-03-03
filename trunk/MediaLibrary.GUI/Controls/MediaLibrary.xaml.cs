using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaLibrary.Core;
using MediaLibrary.GUI;
using System.ComponentModel;
using System.Collections.Specialized;
using DesktopCore;
using System.IO;
using DesktopCore.Data;

namespace MediaLibrary.Controls
{
    public enum MediaLibraryMode { Display, Selector }

    public delegate void CommandHandler(object sender, CommandEventArgs e);

    /// <summary>
    /// Interaction logic for MediaLibrary.xaml
    /// </summary>
    public partial class MediaLibrary : UserControl
    {
        public Database Database
        {
            get { return (Database)GetValue(DatabaseProperty); }
            set { SetValue(DatabaseProperty, value); }
        }

        public MediaLibraryMode Mode
        {
            get { return (MediaLibraryMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public HashSet<Movie> SelectedItems { get; protected set; }

        public event RoutedEventHandler CloseClick
        {
            add { AddHandler(MediaLibrary.CloseClickEvent, value); }
            remove { RemoveHandler(MediaLibrary.CloseClickEvent, value); }
        }

        public event RoutedEventHandler SelectClick
        {
            add { AddHandler(MediaLibrary.SelectClickEvent, value); }
            remove { RemoveHandler(MediaLibrary.SelectClickEvent, value); }
        }

        public event CommandHandler Command
        {
            add { AddHandler(MediaLibrary.CommandEvent, value); }
            remove { RemoveHandler(MediaLibrary.CommandEvent, value); }
        }

        public MediaLibrary()
        {
            InitializeComponent();
        }

        private ICollectionView GetMoviesCollectionView()
        {
            return (FindResource("movies") as CollectionViewSource).View;
        }

        public void ApplyFilter()
        {
            ICollectionView view = GetMoviesCollectionView();
            if (view != null)
            {

                view.Filter = delegate(object item)
                {
                    Movie movie = (Movie)item;
                    SearchPosition position = SearchHelper.FindSearchPosition(cbxFilterPosition.SelectedIndex);

                    if (!SearchHelper.ContainsText(Database.Filter.Name, movie.Name, position)
                        || !SearchHelper.ContainsText(Database.Filter.Storage, movie.Storage, position)
                        || !SearchHelper.ContainsItems(Database.Filter.Category, movie.Category, ',')
                        || !SearchHelper.ContainsText(Database.Filter.Year, movie.Year.ToString(), position)
                        || !SearchHelper.ContainsText(Database.Filter.Country, movie.Country, position)
                        || !SearchHelper.ContainsItems(Database.Filter.Actors, movie.Actors, ',')
                        || !SearchHelper.IsNewerThen(Database.Filter.Added, movie.Added)
                        || !SearchHelper.TestThreeStatesTrue(Database.Filter.Starred, movie.Starred)
                    )
                        return false;

                    return true;
                };
            }
        }

        public void ClearFilter()
        {
            ICollectionView view = GetMoviesCollectionView();
            if (view != null)
            {
                view.Filter = null;
            }
        }

        private void ExportLibrary(object sender, ExportEventArgs e)
        {
            Database.ExportToExcel(e.Data);
            if(e.Data.OpenAfterExport)
                System.Diagnostics.Process.Start(e.Data.FileName);
        }

        private void btnEditMovie_Click(object sender, RoutedEventArgs e)
        {
            Movie edit = lvwMovies.SelectedItem as Movie;
            if (edit != null)
            {
                EditMovieWindow window = new EditMovieWindow(Database, lvwMovies.SelectedItem as Movie);
                window.ShowDialog();
            }
        }

        private void btnCreateMovie_Click(object sender, RoutedEventArgs e)
        {
            Movie newOne = new Movie();
            EditMovieWindow window = new EditMovieWindow(Database, newOne);
            window.Closing += new System.ComponentModel.CancelEventHandler(delegate
            {
                if (!String.IsNullOrEmpty(newOne.Name))
                {
                    Database.Movies.Add(newOne);
                }
            });
            window.ShowDialog();
        }

        private void btnDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            Movie delete = lvwMovies.SelectedItem as Movie;
            if (delete != null)
            {
                if (lvwMovies.SelectedItems.Count > 1)
                {
                    if (MessageBox.Show(String.Format(Resource.Get("DeleteMoviesConfirm"), delete.Name), Resource.Get("DeleteMoviesTitle"), MessageBoxButton.YesNo) 
                        == MessageBoxResult.Yes)
                    {
                        List<Movie> toDelete = new List<Movie>();
                        foreach (Movie item in lvwMovies.SelectedItems)
                        {
                            toDelete.Add(item);
                        }
                        foreach (Movie item in toDelete)
                        {
                            Database.Movies.Remove(item);
                        }
                    }
                } else if (MessageBox.Show(String.Format(Resource.Get("DeleteMovieConfirm"), delete.Name), Resource.Get("DeleteMovieTitle"), MessageBoxButton.YesNo) 
                    == MessageBoxResult.Yes)
                {
                    Database.Movies.Remove(delete);
                }
            }
            lvwMovies.Focus();
        }

        private void btnCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoriesWindow window = new CategoriesWindow(Database);
            window.ShowDialog();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter();
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            Database.Filter.Clear();
            ClearFilter();
        }

        private void lvwMovies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnEditMovie_Click(sender, e);
        }

        private void lvwMovies_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnEditMovie_Click(sender, e);
            else if (e.Key == Key.Delete)
                btnDeleteMovie_Click(sender, e);
        }

        private void btnSortName_Click(object sender, RoutedEventArgs e)
        {
            CollectionHelper.SortCollection(GetMoviesCollectionView(), "Name");
        }

        private void btnSortCategory_Click(object sender, RoutedEventArgs e)
        {
            CollectionHelper.SortCollection(GetMoviesCollectionView(), "Category");
        }

        private void btnSortStorage_Click(object sender, RoutedEventArgs e)
        {
            CollectionHelper.SortCollection(GetMoviesCollectionView(), "Storage");
        }

        private void btnSortYear_Click(object sender, RoutedEventArgs e)
        {
            CollectionHelper.SortCollection(GetMoviesCollectionView(), "Year");
        }

        private void btnSortCountry_Click(object sender, RoutedEventArgs e)
        {
            CollectionHelper.SortCollection(GetMoviesCollectionView(), "Country");
        }

        private void btnSortAdded_Click(object sender, RoutedEventArgs e)
        {
            CollectionHelper.SortCollection(GetMoviesCollectionView(), "Added");
        }

        private void expFilter_Expanded(object sender, RoutedEventArgs e)
        {
            //FocusManager.SetFocusedElement(expFilter, tbxNameFilter);
            tbxNameFilter.Focus();
        }

        private void expFilter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                expFilter.Focus();
                ApplyFilter();
                if (lvwMovies.Items.Count > 0)
                {
                    lvwMovies.Focus();
                    lvwMovies.SelectedIndex = 0;
                }
            }
        }

        private void btnCopyLibrary_Click(object sender, RoutedEventArgs e)
        {
            StringCollection collection = new StringCollection();
            collection.Add(System.IO.Path.GetFullPath(Database.Location));
            Clipboard.SetFileDropList(collection);
        }

        private void btnPasteLibrary_Click(object sender, RoutedEventArgs e)
        {
            StringCollection collection = Clipboard.GetFileDropList();
            if (collection.Count == 1)
            {
                string path = collection[0];
                if (System.IO.File.Exists(path))
                {
                    Database.Location = path;
                    Database.Load();
                }
            }
        }

        private void btnSaveLibrary_Click(object sender, RoutedEventArgs e)
        {
            Database.Save();
        }

        private void btnExportLibrary_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            //dialog.CheckPathExists = true;
            //dialog.OverwritePrompt = true;
            //dialog.AutoUpgradeEnabled = true;
            //dialog.DefaultExt = ".xls";
            //dialog.Title = Resource.Get("ExportTitle");
            //if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    Database.ExportToExcel(dialog.FileName);

            ExportDialog dialog = new ExportDialog();
            dialog.ExportClick += new ExportHandler(ExportLibrary);
            dialog.ShowDialog();
        }

        private void btnCloseLibrary_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(MediaLibrary.CloseClickEvent, this));
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectedItems = new HashSet<Movie>();
            foreach (Movie item in lvwMovies.SelectedItems)
            {
                SelectedItems.Add(item);
            }

            RaiseEvent(new RoutedEventArgs(MediaLibrary.SelectClickEvent, this));
        }

        private void btnRenameLibrary_Click(object sender, RoutedEventArgs e)
        {
            DatabaseConfiguration dialog = new DatabaseConfiguration(Database);
            dialog.ShowDialog();
        }

        private void mniCopyMovie_Click(object sender, RoutedEventArgs e)
        {
            if (lvwMovies.SelectedItem != null)
            {
                SelectedItems = new HashSet<Movie>();
                foreach (Movie item in lvwMovies.SelectedItems)
                {
                    SelectedItems.Add(item);
                }

                CommandEventArgs ea = new CommandEventArgs(MediaLibrary.CommandEvent, this);
                ea.MediaLibrary = this;
                ea.Database = Database;
                ea.Command = Commands.CopyMovie;

                RaiseEvent(ea);
            }
        }

        private void mniPasteMovie_Click(object sender, RoutedEventArgs e)
        {
            CommandEventArgs ea = new CommandEventArgs(MediaLibrary.CommandEvent, this);
            ea.MediaLibrary = this;
            ea.Database = Database;
            ea.Command = Commands.PasteMovie;

            RaiseEvent(ea);
        }

        private void btnToggleStarredFilter_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilter();
        }

        private void keys_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item.ToString().Length == 1;
        }

        private void tbxPasswordRequired_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            string password = tbxPasswordRequired.Password;
            if (!String.IsNullOrEmpty(password) && !String.IsNullOrWhiteSpace(password))
            {
                Database.Password = password;
                Database.Load();
            }
        }

        private void coxFirstLetterFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (Database != null)
            //{
            //    Database.Filter.Name = coxFirstLetterFilter.Text;
            //    ApplyFilter();
            //}
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //if(e.Command == ApplicationCommands.Copy)
            //    e.CanExecute = lvwMovies.SelectedItem != null;
            //else
            //    e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //if (e.Command == ApplicationCommands.Copy)
            //    mniCopyMovie_Click(sender, e);
            //else if (e.Command == ApplicationCommands.Paste)
            //    mniPasteMovie_Click(sender, e);
        }

        public static readonly RoutedEvent CloseClickEvent = EventManager.RegisterRoutedEvent("CloseClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MediaLibrary));

        public static readonly RoutedEvent SelectClickEvent = EventManager.RegisterRoutedEvent("SelectClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MediaLibrary));

        public static readonly RoutedEvent CommandEvent = EventManager.RegisterRoutedEvent("Command", RoutingStrategy.Bubble, typeof(CommandHandler), typeof(MediaLibrary));

        public static readonly DependencyProperty DatabaseProperty = DependencyProperty.Register("Database", typeof(Database), typeof(MediaLibrary));

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(MediaLibraryMode), typeof(MediaLibrary), new PropertyMetadata(MediaLibraryMode.Display, new PropertyChangedCallback(OnModeChanged)));

        //Decprecated
        private static void OnDatabaseChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MediaLibrary ml = (o as MediaLibrary);
            if (ml != null)
            {
                if (e.Property == DatabaseProperty)
                {
                    if (e.NewValue == null)
                    {
                        ml.Database = e.OldValue as Database;
                    }
                    ml.DataContext = ml.Database;
                }
            }
        }

        private static void OnModeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MediaLibrary ml = (o as MediaLibrary);
            if (ml != null)
            {
                if (ml.Mode == MediaLibraryMode.Display)
                {
                    ml.lvwMovies.SelectionMode = SelectionMode.Single;
                    ml.brdSelect.Visibility = Visibility.Collapsed;
                    ml.dplControls.Visibility = Visibility.Visible;
                    ml.grdMovieControls.Visibility = Visibility.Visible;
                    ml.lvwMovies.MouseDoubleClick -= new MouseButtonEventHandler(ml.lvwMovies_MouseDoubleClick);
                }
                else
                {
                    ml.lvwMovies.SelectionMode = SelectionMode.Multiple;
                    ml.brdSelect.Visibility = Visibility.Visible;
                    ml.dplControls.Visibility = Visibility.Collapsed;
                    ml.grdMovieControls.Visibility = Visibility.Collapsed;
                    ml.lvwMovies.MouseDoubleClick += new MouseButtonEventHandler(ml.lvwMovies_MouseDoubleClick);
                }
            }
        }

        private void dpcFilterAdded_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //Database.Filter.Added = dpcFilterAdded.SelectedDate;
        }
    }
}

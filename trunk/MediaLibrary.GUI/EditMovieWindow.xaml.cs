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
using System.Windows.Shapes;
using MediaLibrary.Core;
using DesktopCore;

namespace MediaLibrary.GUI
{
    /// <summary>
    /// Interaction logic for EditMovieWindow.xaml
    /// </summary>
    public partial class EditMovieWindow : Window
    {
        private Database database;
        private Movie movie;

        public Movie Movie { get { return movie; } protected set { movie = value; } }

        public Database Database { get { return database; } protected set { database = value; } }

        public EditMovieWindow(Database db, Movie movie)
        {
            InitializeComponent();

            Movie = movie;
            Database = db;

            DataContext = movie;
            foreach (string item in Database.Movies.Categories)
            {
                cbxCategory.Items.Add(item);
            }
            foreach (string item in Database.Movies.Countries)
            {
                cbxCountry.Items.Add(item);
            }

            tbxName.Focus();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (tbxDescription.Equals(FocusManager.GetFocusedElement(this)))
                return;

            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                tbxDescription.Focus();
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!String.IsNullOrEmpty(Movie.Category) && !Database.Movies.Categories.Contains(Movie.Category))
                Database.Movies.Categories.Add(Movie.Category);

            if (!String.IsNullOrEmpty(Movie.Country) && !Database.Movies.Countries.Contains(Movie.Country))
                Database.Movies.Countries.Add(Movie.Country);

            if (Movie.Actors != null)
            {
                foreach (string actor in Movie.Actors)
                {
                    if (!Database.Movies.Actors.Contains(actor))
                        Database.Movies.Actors.Add(actor);
                }
            }
        }

        private void btnRelatedAdd_Click(object sender, RoutedEventArgs e)
        {
            QuickFindMovieWindow window = new QuickFindMovieWindow(Database);
            window.Closing += new System.ComponentModel.CancelEventHandler(delegate
            {
                foreach (Movie item in window.SelectedItems)
                {
                    if (!Movie.Related.Contains(item))
                    {
                        Movie.Related.Add(item);
                        item.Related.Add(Movie);
                    }
                }
            });
            window.ShowDialog();
        }

        private void btnRelatedRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lvwRelated.SelectedItem != null)
            {
                Movie related = lvwRelated.SelectedItem as Movie;
                related.Related.Remove(Movie);
                Movie.Related.Remove(related);
            }
        }

        private void lvwRelated_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Movie = lvwRelated.SelectedItem as Movie;
            DataContext = Movie;
        }

        private void btnBrowseWeb_Click(object sender, RoutedEventArgs e)
        {
            string url = String.Format(Database.OnlineFormat, Movie.OnlineIdentifier);
            System.Diagnostics.Process.Start(url);
        }
    }
}

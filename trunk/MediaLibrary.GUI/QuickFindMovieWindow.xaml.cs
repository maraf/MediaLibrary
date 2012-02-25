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
using System.ComponentModel;
using DesktopCore;

namespace MediaLibrary.GUI
{
    /// <summary>
    /// Interaction logic for QuickFindMovieWindow.xaml
    /// </summary>
    public partial class QuickFindMovieWindow : Window
    {
        public Database Database { get; protected set; }

        public HashSet<Movie> SelectedItems { get; protected set; }

        public QuickFindMovieWindow(Database db)
        {
            InitializeComponent();

            Database = db;
            SelectedItems = new HashSet<Movie>();

            DataContext = Database;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void mlbSelector_SelectClick(object sender, RoutedEventArgs e)
        {
            SelectedItems = mlbSelector.SelectedItems;
            Close();
        }
    }
}

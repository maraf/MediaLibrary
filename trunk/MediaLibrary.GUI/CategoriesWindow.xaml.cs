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
    /// Interaction logic for CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        public Database Database { get; set; }

        public CategoriesWindow(Database db)
        {
            InitializeComponent();

            Database = db;

            DataContext = Database.Movies.Categories;

            tbxName.Focus();
        }

        private void btnCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxName.Text) && !Database.Movies.Categories.Contains(tbxName.Text))
            {
                Database.Movies.Categories.Add(tbxName.Text);
                tbxName.Text = "";
            }
            tbxName.Focus();
        }
    }
}

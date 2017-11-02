using MediaLibrary.ViewModels.Services;
using Neptuo;
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
using System.Windows.Shapes;

namespace MediaLibrary.Views
{
    public partial class MovieSelectWindow : Window
    {
        private readonly INavigatorContext navigator;

        public MovieSelectWindow(INavigatorContext navigator)
        {
            Ensure.NotNull(navigator, "navigator");
            this.navigator = navigator;

            InitializeComponent();
        }

        private void OnSelectExecuted()
        {
            navigator.Close(Library.SelectedItems.Select(m => m.Key));
        }
    }
}

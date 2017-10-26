using MediaLibrary.ViewModels.Services;
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
using System.Windows.Shapes;

namespace MediaLibrary.Views
{
    /// <summary>
    /// Interaction logic for LibraryConfigurationWindow.xaml
    /// </summary>
    public partial class LibraryConfigurationWindow : Window, INavigatorContext
    {
        public LibraryConfigurationWindow()
        {
            InitializeComponent();

            kebClose.Command = new DelegateCommand(Close);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            tbxName.Focus();
        }
    }
}

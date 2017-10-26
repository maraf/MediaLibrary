using MediaLibrary.ViewModels;
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
    public partial class MovieEditWindow : Window, INavigatorContext
    {
        public MovieEditViewModel ViewModel
        {
            get { return (MovieEditViewModel)DataContext; }
        }

        public MovieEditWindow()
        {
            InitializeComponent();

            kebClose.Command = new DelegateCommand(Close);
        }
    }
}

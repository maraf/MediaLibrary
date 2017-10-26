using MediaLibrary.ViewModels;
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
    public partial class SortView : ItemsControl
    {
        public IEnumerable<SortViewModel> ViewModel
        {
            get { return (IEnumerable<SortViewModel>)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", 
            typeof(IEnumerable<SortViewModel>), 
            typeof(SortView), 
            new PropertyMetadata(OnViewModelChanged)
        );

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SortView control = (SortView)d;
            control.OnViewModelChanged();
        }

        public event EventHandler<SortViewModelChangedEventArgs> SelectionChanged;

        public SortView()
        {
            InitializeComponent();
            Background = null;
        }

        private void OnViewModelChanged()
        {
            ItemsSource = ViewModel;
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SortViewModel newViewModel = (SortViewModel)button.Tag;

            SelectionChanged?.Invoke(this, new SortViewModelChangedEventArgs(newViewModel));
        }
    }
}

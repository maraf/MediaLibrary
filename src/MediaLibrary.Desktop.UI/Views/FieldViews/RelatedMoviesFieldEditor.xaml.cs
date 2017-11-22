using MediaLibrary.ViewModels;
using Neptuo;
using Neptuo.Models.Keys;
using Neptuo.PresentationModels;
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

namespace MediaLibrary.Views.FieldViews
{
    public partial class RelatedMoviesFieldEditor : UserControl, IFieldValueProvider
    {
        public RelatedMoviesViewModel ViewModel
        {
            get => (RelatedMoviesViewModel)DataContext;
            protected set => DataContext = value;
        }

        public RelatedMoviesFieldEditor(RelatedMoviesViewModel viewModel)
        {
            Ensure.NotNull(viewModel, "viewModel");
            InitializeComponent();

            Background = null;
            ViewModel = viewModel;
        }

        public bool TryGetValue(out object value)
        {
            value = ViewModel.Items.Select(m => m.Key);
            return true;
        }

        public bool TrySetValue(object value)
        {
            IEnumerable<IKey> keys = (IEnumerable<IKey>)value;
            ViewModel.Items.Clear();

            foreach (IKey key in keys)
                ViewModel.Add.AddKey(key);

            return true;
        }
    }
}

using MediaLibrary.ViewModels;
using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Views.Controls
{
    public class SortViewModelChangedEventArgs : EventArgs
    {
        public SortViewModel ViewModel { get; private set; }

        public SortViewModelChangedEventArgs(SortViewModel viewModel)
        {
            Ensure.NotNull(viewModel, "viewModel");
            ViewModel = viewModel;
        }
    }
}

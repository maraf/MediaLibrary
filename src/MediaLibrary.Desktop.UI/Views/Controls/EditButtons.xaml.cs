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
    public partial class EditButtons : UserControl
    {
        public event Action SaveClick;
        public event Action CloseClick;

        public EditButtons()
        {
            InitializeComponent();
        }

        private void btnSave_Click() => SaveClick?.Invoke();
        private void btnClose_Click() => CloseClick?.Invoke();
    }
}

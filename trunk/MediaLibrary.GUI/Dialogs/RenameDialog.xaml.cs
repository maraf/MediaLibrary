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
    /// Interaction logic for RenameDialog.xaml
    /// </summary>
    public partial class RenameDialog : Window
    {
        public RenameFields Fields { get; set; }

        public bool Selected { get; set; }

        public RenameDialog(RenameFields fields)
        {
            InitializeComponent();

            Fields = fields;
            DataContext = fields;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void btnRemane_Click(object sender, RoutedEventArgs e)
        {
            Selected = true;
            Fields.Value = tbxValue.Text;
            Close();
        }

        private void tbxValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnRemane_Click(sender, e);
                e.Handled = true;
            }
        }
    }

    public class RenameFields
    {
        public string Title { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string Rename { get; set; }
    }
}

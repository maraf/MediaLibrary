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
using MediaLibrary.Export;
using DesktopCore;
using MediaLibrary.Core;

namespace MediaLibrary.GUI
{
    /// <summary>
    /// Interaction logic for ExportDialog.xaml
    /// </summary>
    public partial class ExportDialog : Window
    {
        public event ExportHandler ExportClick
        {
            add { AddHandler(ExportDialog.ExportClickEvent, value); }
            remove { RemoveHandler(ExportDialog.ExportClickEvent, value); }
        }

        public ExportDialog()
        {
            InitializeComponent();

            coxColumnsCount.Items.Add("1");
            coxColumnsCount.Items.Add("2");
            coxColumnsCount.Items.Add("3");
            coxColumnsCount.Items.Add("4");

            foreach (string item in Movie.Fields)
            {
                lbxColumns.Items.Add(new KeyValuePair<string, string>(Resource.Get(item), item));
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxFileName.Text) && !String.IsNullOrWhiteSpace(tbxFileName.Text) && lbxColumns.SelectedItems.Count > 0)
            {
                ExportData data = new ExportData();
                data.OpenAfterExport = cbxOpen.IsChecked.Value;
                data.FileName = tbxFileName.Text;
                data.ColumnsCount = Int32.Parse(coxColumnsCount.Text);
                data.Columns = new List<string>();
                foreach (KeyValuePair<string,string> item in lbxColumns.SelectedItems)
                {
                    data.Columns.Add(item.Value);
                }
                RaiseEvent(new ExportEventArgs(ExportDialog.ExportClickEvent) { Data = data });
                Close();
            }
        }

        public static readonly RoutedEvent ExportClickEvent = EventManager.RegisterRoutedEvent("ExportClick", RoutingStrategy.Bubble, typeof(ExportHandler), typeof(ExportDialog));

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbxFileName.Text = dialog.FileName;
        }
    }
}

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
using System.Net;
using System.IO;

namespace MediaLibrary.GUI
{
    /// <summary>
    /// Interaction logic for DatabaseConfiguration.xaml
    /// </summary>
    public partial class DatabaseConfiguration : Window
    {
        public Database Database { get; set; }

        public DatabaseConfiguration(Database database)
        {
            InitializeComponent();

            Database = database;
            DataContext = Database;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            WindowHelper.ExtendGlassFrame(this, new Thickness(-1));
        }

        protected void CollapseExpander(Expander source, Expander eventTarget)
        {
            if (source != null && eventTarget != source)
                source.IsExpanded = false;
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander expander = sender as Expander;
            if (expander != null)
            {
                CollapseExpander(expCommon, expander);
                CollapseExpander(expPassword, expander);
                CollapseExpander(expOnline, expander);
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.FileName = Database.Location;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Database.Location = dialog.FileName;
        }

        private void tbxPassword_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(tbxPassword.IsEnabled)
                tbxPassword.Focus();
        }

        private void btnDownloadNow_Click(object sender, RoutedEventArgs e)
        {
            tblOnlineMessage.Text = Resource.Get("DownloadingMessage");
            Database.Download(delegate
            {
                Database.Load(true);
                tblOnlineMessage.Text = Resource.Get("DownloadedMessage");
            }, delegate(string error)
            {
                tblOnlineMessage.Text = Resource.Get("ErrorDownloadingMessage");
            });
        }

        private void btnPublishNow_Click(object sender, RoutedEventArgs e)
        {
            Database.Save(true);
            tblOnlineMessage.Text = Resource.Get("UploadingMessage");
            Database.Upload(delegate {
                tblOnlineMessage.Text = Resource.Get("UploadedMessage");
            }, delegate(string error) {
                tblOnlineMessage.Text = Resource.Get("ErrorUploadingMessage");
            });
        }
    }
}

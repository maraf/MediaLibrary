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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using MediaLibrary.Core;
using MediaLibrary.Controls;
using DesktopCore;

namespace MediaLibrary.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IManager Manager { get; set; }

        public HashSet<Movie> SelectedMovies { get; protected set; }

        public MainWindow()
        {
            InitializeComponent();

            Manager = ManagerFactory.Create();
            Manager.Load(ManagerFactory.DefaultLocation);

            DataContext = Manager;
        }

        protected bool AskForSave()
        {
            MessageBoxResult result = MessageBox.Show(Resource.Get("ManagerAskForSave"), Resource.Get("AskForSaveTitle"), MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
                return false;

            if (result == MessageBoxResult.Yes)
                Manager.Save(ManagerFactory.DefaultLocation, false);

            foreach (Database item in Manager.Databases)
            {
                result = MessageBox.Show(String.Format(Resource.Get("DatabaseAskForSave"), item.Name), Resource.Get("AskForSaveTitle"), MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    item.Save();
            }

            return true;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (Manager.ExitMode)
            {
                case ExitMode.Autosave:
                    Manager.Save(ManagerFactory.DefaultLocation);
                    break;
                case ExitMode.AskForSave:
                    if (!AskForSave())
                        e.Cancel = true;
                    break;
                case ExitMode.Exit:
                    return;
            }
        }

        private void btnCreateLibrary_Click(object sender, RoutedEventArgs e)
        {
            RenameDialog dialog = new RenameDialog(new RenameFields() { Title = Resource.Get("LibraryRename"), Label = Resource.Get("LibraryName"), Rename = Resource.Get("CreateLibrary") });
            dialog.Closing += new CancelEventHandler(delegate {
                if (dialog.Selected)
                {
                    // Select location
                    System.Windows.Forms.SaveFileDialog fsdialog = new System.Windows.Forms.SaveFileDialog();
                    if (fsdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Database db = new Database();
                        db.Location = fsdialog.FileName;
                        db.Name = dialog.Fields.Value;
                        Manager.Databases.Add(db);
                    }
                }
            });
            dialog.ShowDialog();
        }

        private void btnOpenLibrary_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Database db = new Database();
                    db.Location = dialog.FileName;
                    db.Load();

                    Manager.Databases.Add(db);
                }
                catch (Exception)
                {
                    MessageBox.Show(Resource.Get("ErrorLoadingLibrary"), Resource.Get("ErrorLoadingLibraryTitle"), MessageBoxButton.OK);
                }
            }
        }

        private void MediaLibrary_CloseClick(object sender, RoutedEventArgs e)
        {
            MediaLibrary.Controls.MediaLibrary ml = sender as MediaLibrary.Controls.MediaLibrary;
            if (Manager.Databases.Count > 1)
                Manager.Databases.Remove(ml.Database);
        }

        private void btnConfiguration_Click(object sender, RoutedEventArgs e)
        {
            Configuration dialog = new Configuration(Manager);
            dialog.ShowDialog();
        }

        private void btnExportLibraries_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.CheckPathExists = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.DefaultExt = ".xls";
            dialog.Title = Resource.Get("ExportTitle");
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string pathFormat = dialog.FileName.Replace(".xls", "-{0}.xls");
                foreach (Database item in Manager.Databases)
                {
                    item.ExportToExcel(String.Format(pathFormat, item.Name));
                }
            }
        }

        private void btnCopyLibraries_Click(object sender, RoutedEventArgs e)
        {
            StringCollection paths = new StringCollection();
            foreach (Database item in Manager.Databases)
            {
                paths.Add(System.IO.Path.GetFullPath(item.Location));
            }
            Clipboard.SetFileDropList(paths);
        }

        private void MediaLibrary_Command(object sender, CommandEventArgs e)
        {
            switch (e.Command)
            {
                case Commands.CopyMovie:
                    SelectedMovies = e.MediaLibrary.SelectedItems;
                    break;
                case Commands.PasteMovie:
                    if (SelectedMovies != null)
                    {
                        foreach (Movie item in SelectedMovies)
                        {
                            e.Database.Movies.Add(item);
                        }
                        SelectedMovies = null;
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnSaveLibraries_Click(object sender, RoutedEventArgs e)
        {
            foreach (Database item in Manager.Databases)
            {
                item.Save();
            }
        }

        private void btnCompareLibraries_Click(object sender, RoutedEventArgs e)
        {
            CompareData data = new CompareData(Manager.Databases);
            CompareDialog dialog = new CompareDialog(data);
            dialog.CompareClick += new RoutedEventHandler(delegate {
                dialog.Close();

                switch (data.CompareType)
                {
                    case CompareType.CompareMovies:
                        Database result = CompareHelper.Compare(data.Source, data.Target);
                        if (result.Movies.Count > 0)
                            Manager.Databases.Add(result);
                        break;
                    case CompareType.FillProperties:
                        CompareHelper.FillProperties(data.Source, data.Target);
                        break;
                }
            });
            dialog.ShowDialog();
        }
    }
}

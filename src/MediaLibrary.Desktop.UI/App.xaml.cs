using MediaLibrary.Properties;
using MediaLibrary.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MediaLibrary
{
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppNavigator navigator = new AppNavigator(this);

            string filePath = null;
            if (String.IsNullOrEmpty(Settings.Default.DefaultFilePath) || !File.Exists(Settings.Default.DefaultFilePath))
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.DefaultExt = ".xml";
                bool? result = dialog.ShowDialog();
                if (result ?? false)
                    filePath = dialog.FileName;
            }
            else
            {
                filePath = Settings.Default.DefaultFilePath;
            }

            if (filePath != null)
            {
                XmlStore store = new XmlStore();
                Library library = new Library();
                library.Configuration.FilePath = filePath;
                library.Configuration.Name = Path.GetFileNameWithoutExtension(filePath);
                await store.LoadAsync(library);

                Settings.Default.DefaultFilePath = filePath;
                Settings.Default.Save();

                await navigator.LibraryAsync(library);
            }
            else
            {
                Shutdown();
            }
        }
    }
}

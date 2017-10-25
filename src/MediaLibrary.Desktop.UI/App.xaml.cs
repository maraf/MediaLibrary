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

            AppNavigator navigator = new AppNavigator();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = ".xml";
            bool? result = dialog.ShowDialog();
            if (result ?? false)
            {
                XmlStore store = new XmlStore();
                Library library = new Library();
                library.Configuration.FilePath = dialog.FileName;
                library.Configuration.Name = Path.GetFileNameWithoutExtension(dialog.FileName);
                await store.LoadAsync(library);
                await navigator.LibraryAsync(library);
            }
            else
            {
                Shutdown();
            }
        }
    }
}

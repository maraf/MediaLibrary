using MediaLibrary.Properties;
using MediaLibrary.Views;
using Microsoft.Win32;
using Neptuo;
using Neptuo.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
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
            Converts.Repository
                .AddStringTo<bool>(Boolean.TryParse)
                .AddStringTo<int>(Int32.TryParse)
                .AddStringTo<DateTime>(TryParseDateTime)
                .AddToStringSearchHandler();

            base.OnStartup(e);

            AppChangeTracker changeTracker = new AppChangeTracker();
            AppLibraryStore store = new AppLibraryStore(Settings.Default, new XmlStore(), changeTracker);
            AppNavigator navigator = new AppNavigator(this, store, changeTracker);

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
                Library library = new Library();
                library.Configuration.FilePath = filePath;
                library.Configuration.Name = Path.GetFileNameWithoutExtension(filePath);
                await store.LoadAsync(library);
                await navigator.LibraryAsync(library);
            }
            else
            {
                Library library = new Library();
                await navigator.LibraryAsync(library);
            }
        }

        private bool TryParseDateTime(string input, out DateTime output)
        {
            if (DateTime.TryParse(input, out output))
                return true;

            if (DateTime.TryParseExact(input, "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None, out output))
                return true;

            if (DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None, out output))
                return true;

            return false;
        }
    }
}

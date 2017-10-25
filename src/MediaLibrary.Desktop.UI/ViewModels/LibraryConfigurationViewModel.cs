using MediaLibrary.ViewModels.Commands;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Observables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaLibrary.ViewModels
{
    public class LibraryConfigurationViewModel : ObservableObject
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set
            {
                if (filePath != value)
                {
                    filePath = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string onlineDatabaseName;
        public string OnlineDatabaseName
        {
            get { return onlineDatabaseName; }
            set
            {
                if (onlineDatabaseName != value)
                {
                    onlineDatabaseName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string onlineDatabaseUrlFormat;
        public string OnlineDatabaseUrlFormat
        {
            get { return onlineDatabaseUrlFormat; }
            set
            {
                if (onlineDatabaseUrlFormat != value)
                {
                    onlineDatabaseUrlFormat = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand Save { get; }
        public ICommand Close { get; }

        public LibraryConfigurationViewModel(LibraryConfiguration model, INavigatorContext navigator)
        {
            Ensure.NotNull(model, "model");
            Name = model.Name;
            FilePath = model.FilePath;
            OnlineDatabaseName = model.OnlineDatabaseName;
            OnlineDatabaseUrlFormat = model.OnlineDatabaseUrlFormat;

            Save = new SaveLibraryConfigurationCommand(this, model, navigator);
            Close = new CloseCommand(navigator);
        }
    }
}

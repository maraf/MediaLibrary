using Neptuo.Observables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    public class LibraryConfiguration : ObservableObject
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

        private bool isEncrypted;
        public bool IsEncrypted
        {
            get { return isEncrypted; }
            set
            {
                if (isEncrypted != value)
                {
                    isEncrypted = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}

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

        private string onlineName;
        public string OnlineName
        {
            get { return onlineName; }
            set
            {
                if (onlineName != value)
                {
                    onlineName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string onlineFormat;
        public string OnlineFormat
        {
            get { return onlineFormat; }
            set
            {
                if (onlineFormat != value)
                {
                    onlineFormat = value;
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

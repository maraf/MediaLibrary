using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DesktopCore;

namespace MediaLibrary.Core
{
    public class OnlineDatabases : DesktopCore.ObservableCollection<OnlineDatabase> { }

    public class OnlineDatabase : NotifyPropertyChanged
    {
        private string name;
        private string format;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                FirePropertyChanged("Name");
            }
        }

        public string Format
        {
            get { return format; }
            set
            {
                format = value;
                FirePropertyChanged("Format");
            }
        }
    }

    public class OnlineLinks : DesktopCore.ObservableCollection<OnlineLink> { }

    public class OnlineLink : NotifyPropertyChanged
    {
        private string name;
        private string url;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                FirePropertyChanged("Name");
            }
        }

        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                FirePropertyChanged("Url");
            }
        }
    }
}

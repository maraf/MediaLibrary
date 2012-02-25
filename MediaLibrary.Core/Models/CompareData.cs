using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopCore;
using MediaLibrary.Core;
using System.Windows.Input;

namespace MediaLibrary.Core
{
    public class CompareData : NotifyPropertyChanged
    {
        private Databases databases;
        private Database source;
        private Database target;
        private CompareType compareType;

        public CompareData() { }

        public CompareData(Databases databases)
        {
            Databases = databases;
        }

        public CompareData(Databases databases, Database source, Database target)
            : this(databases)
        {
            Source = source;
            Target = target;
        }

        public Databases Databases
        {
            get { return databases; }
            set
            {
                databases = value;
                FirePropertyChanged("Databases");
            }
        }

        public Database Source
        {
            get { return source; }
            set
            {
                source = value;
                FirePropertyChanged("Source");
            }
        }

        public Database Target
        {
            get { return target; }
            set
            {
                target = value;
                FirePropertyChanged("Target");
            }
        }

        public CompareType CompareType
        {
            get { return compareType; }
            set
            {
                compareType = value;
                FirePropertyChanged("CompareType");
            }
        }
    }

    public enum CompareType
    {
        CompareMovies, FillProperties
    }
}

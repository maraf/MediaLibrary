using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Data;
using DesktopCore;
using System.Threading;

namespace MediaLibrary.Core
{
    public class Movies : DesktopCore.ObservableCollection<Movie>
    {
        private int idCounter = 1;
        private HashSet<int> usedIds = new HashSet<int>();

        public Categories Categories { get; set; }
        public Countries Countries { get; set; }
        public Actors Actors { get; set; }

        public Movies()
        {
            Categories = new Categories();
            Countries = new Countries();
            Actors = new Actors();
        }

        public int NextID
        {
            get
            {
                int val = idCounter;
                idCounter++;
                return val;
            }
        }

        public Movie FindByID(int id)
        {
            foreach (var item in Items)
            {
                if (item.ID == id)
                    return item;
            }
            return null;
        }

        public Movie FindByName(string name)
        {
            foreach (var item in Items)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }

        public new void Add(Movie item)
        {
            if (!String.IsNullOrEmpty(item.Category) && !Categories.Contains(item.Category))
                Categories.Add(item.Category);

            if (!String.IsNullOrEmpty(item.Country) && !Countries.Contains(item.Country))
                Countries.Add(item.Country);

            if (item.Actors != null)
            {
                foreach (string actor in item.Actors)
                {
                    if (!Actors.Contains(actor.Trim()))
                        Actors.Add(actor.Trim());
                }
            }

            if (item.ID == 0 || usedIds.Contains(item.ID))
            {
                item.ID = idCounter;
                idCounter++;
            }
            else
            {
                usedIds.Add(item.ID);
                idCounter = item.ID + 1;
            }

            base.Add(item);
        }

        public new void Remove(Movie item)
        {
            usedIds.Remove(item.ID);
            base.Remove(item);
        }

        public new void RemoveAt(int index)
        {
            Movie item = Items[index];
            Remove(item);
        }

        public new void Clear()
        {
            base.Clear();
            Categories.Clear();
            Countries.Clear();
            Actors.Clear();
        }
    }

    public class Categories : DesktopCore.ObservableCollection<string> { }

    public class Countries : DesktopCore.ObservableCollection<string> { }

    public class Actors : DesktopCore.ObservableCollection<string> { }

    public class Movie : NotifyPropertyChanged
    {
        private int id;
        private string name;
        private string originalName;
        private string storage;
        private int year;
        private string country;
        private string genre;
        private string category;
        private DateTime added;
        private string language;
        private string description;
        private DesktopCore.ObservableCollection<Movie> related = new DesktopCore.ObservableCollection<Movie>();
        private string[] actors;
        private string onlineIdentifier;
        private bool starred;

        public Movie()
        {
            Added = DateTime.Now;
            Year = DateTime.Now.Year;
        }

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                FirePropertyChanged("ID");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                FirePropertyChanged("Name");
            }
        }

        public string OriginalName
        {
            get { return originalName; }
            set
            {
                originalName = value;
                FirePropertyChanged("OriginalName");
            }
        }

        public string Storage
        {
            get { return storage; }
            set
            {
                storage = value;
                FirePropertyChanged("Storage");
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                year = value;
                FirePropertyChanged("Year");
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                FirePropertyChanged("Country");
            }
        }

        public string Genre
        {
            get { return genre; }
            set
            {
                genre = value;
                FirePropertyChanged("Genre");
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                FirePropertyChanged("Category");
            }
        }

        public DateTime Added
        {
            get { return added; }
            set
            {
                added = value;
                FirePropertyChanged("Added");
            }
        }

        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                FirePropertyChanged("Language");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                FirePropertyChanged("Description");
            }
        }

        public DesktopCore.ObservableCollection<Movie> Related
        {
            get { return related; }
            set
            {
                related = value;
                FirePropertyChanged("Related");
            }
        }

        public string[] Actors
        {
            get { return actors; }
            set
            {
                actors = value;
                FirePropertyChanged("Actors");
            }
        }

        public string OnlineIdentifier
        {
            get { return onlineIdentifier; }
            set
            {
                onlineIdentifier = value;
                FirePropertyChanged("OnlineIdentifier");
            }
        }

        public bool Starred
        {
            get { return starred; }
            set
            {
                starred = value;
                FirePropertyChanged("Starred");
            }
        }

        public static readonly string[] Fields = { "ID", "Name", "OriginalName", "Storage", "Year", "Country", /*"Genre", */"Category", "Added", "Language", "Description", "Actors", "OnlineIdentifier", "Starred" };

        public string this[string field]
        {
            get
            {
                switch (field)
                {
                    case "ID": return ID.ToString();
                    case "Name": return Name;
                    case "OriginalName": return OriginalName;
                    case "Storage": return Storage;
                    case "Year": return Year.ToString();
                    case "Country": return Country;
                    //case "Genre": return Genre;
                    case "Category": return Category;
                    case "Added": return Added.ToString(Thread.CurrentThread.CurrentCulture.DateTimeFormat);
                    case "Language": return Language;
                    case "Description": return Description;
                    case "Actors": return Actors.ConcatWith(',');
                    case "OnlineIdentifier": return OnlineIdentifier;
                    case "Starred": return Starred.ToString();
                    default: return null;
                }
            }
        }

        public void FillProperties(Movie source)
        {
            OriginalName = source.OriginalName;
            Storage = source.Storage;
            Year = source.Year;
            Country = source.Country;
            Genre = source.Genre;
            Category = source.Category;
            Added = source.Added;
            Language = source.Language;
            Description = source.Description;
            Actors = source.Actors;
            OnlineIdentifier = source.OnlineIdentifier;
            Starred = source.Starred;
        }
    }

    public class MovieFilter : NotifyPropertyChanged, IFilter
    {
        private string name;
        private string storage;
        private string category;
        private string year;
        private string country;
        private string actors;
        private DateTime? added;
        private bool? starred = false;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                FirePropertyChanged("Name");
            }
        }

        public string Storage
        {
            get { return storage; }
            set
            {
                storage = value;
                FirePropertyChanged("Storage");
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                FirePropertyChanged("Category");
            }
        }

        public string Year
        {
            get { return year; }
            set
            {
                year = value;
                FirePropertyChanged("Year");
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                FirePropertyChanged("Country");
            }
        }

        public string Actors
        {
            get { return actors; }
            set
            {
                actors = value;
                FirePropertyChanged("Actors");
            }
        }

        public DateTime? Added
        {
            get { return added; }
            set
            {
                added = value;
                FirePropertyChanged("Added");
            }
        }

        public bool? Starred
        {
            get { return starred; }
            set
            {
                starred = value;
                FirePropertyChanged("Starred");
            }
        }

        public void Clear()
        {
            Name = null;
            Storage = null;
            Category = null;
            Year = null;
            Country = null;
            Actors = null;
            Added = null;
            Starred = false;
        }
    }
}

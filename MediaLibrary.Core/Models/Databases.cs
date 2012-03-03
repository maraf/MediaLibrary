using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using DesktopCore;
using MediaLibrary.Export;

namespace MediaLibrary.Core
{
    public class Databases : DesktopCore.ObservableCollection<Database>
    { }

    public class Database : NotifyPropertyChanged
    {
        private string name;
        private string location;

        private string onlineName;
        private string onlineFormat;

        private Movies movies;
        private MovieFilter filter;

        private bool encrypted;
        private bool requiresPassword;
        private bool savePassword;
        private string password;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                FirePropertyChanged("Name");
            }
        }

        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                FirePropertyChanged("Location");
            }
        }

        public string OnlineName
        {
            get { return onlineName; }
            set
            {
                onlineName = value;
                FirePropertyChanged("OnlineName");
            }
        }

        public string OnlineFormat
        {
            get { return onlineFormat; }
            set {
                onlineFormat = value;
                FirePropertyChanged("OnlineFormat");
            }
        }

        public Movies Movies
        {
            get { return movies; }
            set
            {
                movies = value;
                FirePropertyChanged("Movies");
            }
        }

        public MovieFilter Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                FirePropertyChanged("Filter");
            }
        }

        public bool Encrypted
        {
            get { return encrypted; }
            set
            {
                encrypted = value;
                FirePropertyChanged("Encrypted");
            }
        }

        public bool RequiresPassword
        {
            get { return requiresPassword; }
            set
            {
                requiresPassword = value;
                FirePropertyChanged("RequiresPassword");
            }
        }

        public bool SavePassword
        {
            get { return savePassword; }
            set
            {
                savePassword = value;
                FirePropertyChanged("SavePassword");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                FirePropertyChanged("Password");
            }
        }

        public Database()
        {
            Movies = new Movies();
            Filter = new MovieFilter();
            Filter.Clear();
        }

        public Database(string location)
            : this()
        {
            Location = location;
            Load();
        }

        public Database(string location, string password)
        {
            Location = location;
            if (password != null)
            {
                Password = password;
                RequiresPassword = true;
            }
            Load();
        }

        public void Save()
        {
            if (Encrypted && RequiresPassword)
                return;

            if (location == null)
                return;

            string backupLocation = location.Replace(".xml", "1.xml");
            try
            {
                if (File.Exists(location))
                {
                    File.Copy(location, backupLocation);
                }

                HashSet<KeyValuePair<int, int>> relatedMovies = new HashSet<KeyValuePair<int, int>>();

                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(dec);

                XmlElement media = doc.CreateElement("MediaLibrary");
                doc.AppendChild(media);

                XmlAttribute name = doc.CreateAttribute("Name");
                name.Value = Name;
                media.Attributes.Append(name);

                XmlAttribute enc = doc.CreateAttribute("Encrypted");
                enc.Value = (Encrypted && Password != null).ToString();
                media.Attributes.Append(enc);

                XmlElement movies = doc.CreateElement("Movies");
                media.AppendChild(movies);

                foreach (Movie m in Movies)
                {
                    XmlElement movie = doc.CreateElement("Movie");
                    XmlAttribute attr;

                    if (m.Name != null)
                    {
                        attr = doc.CreateAttribute("Name");
                        attr.Value = m.Name;
                        movie.Attributes.Append(attr);
                    }
                    if (m.OriginalName != null)
                    {
                        attr = doc.CreateAttribute("OriginalName");
                        attr.Value = m.OriginalName;
                        movie.Attributes.Append(attr);
                    }
                    if (m.ID != null)
                    {
                        attr = doc.CreateAttribute("ID");
                        attr.Value = m.ID.ToString();
                        movie.Attributes.Append(attr);
                    }
                    if (m.Storage != null)
                    {
                        attr = doc.CreateAttribute("Storage");
                        attr.Value = m.Storage;
                        movie.Attributes.Append(attr);
                    }
                    if (m.Year != null)
                    {
                        attr = doc.CreateAttribute("Year");
                        attr.Value = m.Year.ToString();
                        movie.Attributes.Append(attr);
                    }
                    if (m.Country != null)
                    {
                        attr = doc.CreateAttribute("Country");
                        attr.Value = m.Country;
                        movie.Attributes.Append(attr);
                    }
                    if (m.Genre != null)
                    {
                        attr = doc.CreateAttribute("Genre");
                        attr.Value = m.Genre;
                        movie.Attributes.Append(attr);
                    }
                    if (m.Category != null)
                    {
                        attr = doc.CreateAttribute("Category");
                        attr.Value = m.Category;
                        movie.Attributes.Append(attr);
                    }
                    if (m.Added != null)
                    {
                        attr = doc.CreateAttribute("Added");
                        attr.Value = m.Added.ToString();
                        movie.Attributes.Append(attr);
                    }
                    if (m.Language != null)
                    {
                        attr = doc.CreateAttribute("Language");
                        attr.Value = m.Language;
                        movie.Attributes.Append(attr);
                    }
                    if (!String.IsNullOrEmpty(m.Description))
                    {
                        movie.InnerText = m.Description;
                    }
                    if (m.Actors != null && m.Actors.Count() > 0)
                    {
                        attr = doc.CreateAttribute("Actors");
                        attr.Value = m.Actors.ConcatWith(',');
                        movie.Attributes.Append(attr);
                    }
                    if (m.OnlineIdentifier != null)
                    {
                        attr = doc.CreateAttribute("OnlineIdentifier");
                        attr.Value = m.OnlineIdentifier;
                        movie.Attributes.Append(attr);
                    }
                    if (m.Starred)
                    {
                        attr = doc.CreateAttribute("Starred");
                        attr.Value = m.Starred.ToString();
                        movie.Attributes.Append(attr);
                    }

                    movies.AppendChild(movie);

                    MovieHelper.FindRelatedMovies(relatedMovies, m, m.Related);
                }

                XmlElement related = doc.CreateElement("RelatedMovies");
                media.AppendChild(related);

                foreach (KeyValuePair<int, int> item in relatedMovies)
                {
                    XmlElement single = doc.CreateElement("Related");

                    XmlAttribute sourceId = doc.CreateAttribute("SourceID");
                    sourceId.Value = item.Key.ToString();
                    single.Attributes.Append(sourceId);

                    XmlAttribute targetId = doc.CreateAttribute("TargetID");
                    targetId.Value = item.Value.ToString();
                    single.Attributes.Append(targetId);

                    related.AppendChild(single);
                }

                if (Encrypted && Password != null)
                {
                    string content;
                    if (CryptoHelper.Encrypt(media.InnerXml, Password, out content))
                        media.InnerXml = content;
                    else
                        enc.Value = false.ToString();
                }

                doc.Save(location);

            }
            catch (Exception e)
            {
                //TODO: Rename backup back
                if (File.Exists(backupLocation))
                {
                    File.Delete(location);
                    File.Copy(backupLocation, location);
                }
            }
            finally
            {
                if (File.Exists(backupLocation))
                {
                    File.Delete(backupLocation);
                }
            }
        }

        public void Load()
        {
            if (Movies == null)
                Movies = new Movies();
            else
                Movies.Clear();

            if (File.Exists(location))
            {
                XmlDocument doc = new XmlDocument();
                StreamReader sr = new StreamReader(location);
                doc.LoadXml(sr.ReadToEnd());
                sr.Close();

                Name = XmlHelper.GetAttributeValue(doc.DocumentElement, "Name");
                Encrypted = XmlHelper.GetAttributeBool(doc.DocumentElement, "Encrypted");
                OnlineName = XmlHelper.GetAttributeValue(doc.DocumentElement, "OnlineName", "ČSFD");
                OnlineFormat = XmlHelper.GetAttributeValue(doc.DocumentElement, "OnlineFormat", "http://www.csfd.cz/film/{0}");

                if (Encrypted && !RequiresPassword)
                {
                    RequiresPassword = true;
                    return;
                } 
                else if (Encrypted)
                {
                    string content;
                    if (CryptoHelper.Decrypt(doc.DocumentElement.InnerXml, Password, out content))
                    {
                        RequiresPassword = false;
                        doc.DocumentElement.InnerXml = content;
                    }
                }

                foreach (XmlElement m in doc.GetElementsByTagName("Movie"))
                {
                    Movies.Add(new Movie()
                    {
                        ID = XmlHelper.GetAttributeInt(m, "ID", Movies.NextID),
                        Name = XmlHelper.GetAttributeValue(m, "Name"),
                        OriginalName = XmlHelper.GetAttributeValue(m, "OriginalName"),
                        Country = XmlHelper.GetAttributeValue(m, "Country"),
                        Genre = XmlHelper.GetAttributeValue(m, "Genre"),
                        Storage = XmlHelper.GetAttributeValue(m, "Storage"),
                        Year = XmlHelper.GetAttributeInt(m, "Year", DateTime.Now.Year),
                        Category = XmlHelper.GetAttributeValue(m, "Category"),
                        Added = XmlHelper.GetAttributeDateTime(m, "Added"),
                        Language = XmlHelper.GetAttributeValue(m, "Language"),
                        Actors = XmlHelper.GetAttributeValue(m, "Actors", ','),
                        OnlineIdentifier = XmlHelper.GetAttributeValue(m, "OnlineIdentifier"),
                        Starred = XmlHelper.GetAttributeBool(m, "Starred", false),
                        Description = m.InnerText
                    });
                }

                Dictionary<int, Movie> cache = new Dictionary<int, Movie>();
                foreach (XmlElement r in doc.GetElementsByTagName("Related"))
                {
                    int sourceId = Int32.Parse(XmlHelper.GetAttributeValue(r, "SourceID"));
                    int targetId = Int32.Parse(XmlHelper.GetAttributeValue(r, "TargetID"));

                    Movie source;
                    Movie target;

                    if (cache.ContainsKey(sourceId))
                    {
                        source = cache[sourceId];
                    }
                    else
                    {
                        source = Movies.FindByID(sourceId);
                        cache.Add(sourceId, source);
                    }

                    if (cache.ContainsKey(targetId))
                    {
                        target = cache[targetId];
                    }
                    else
                    {
                        target = Movies.FindByID(targetId);
                        cache.Add(targetId, target);
                    }

                    if (source != null && target != null)
                    {
                        source.Related.Add(target);
                        target.Related.Add(source);
                    }
                }
            }
            else
            {
                File.Create(location);
            }
        }

        public void ExportToExcel(string location)
        {
            string format = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>";
            StringBuilder output = new StringBuilder();
            output.Append("<table>");

            output.AppendFormat(format,
                String.Format("<b>{0}</b>", Resource.Get("Name")),
                String.Format("<b>{0}</b>", Resource.Get("OriginalName")),
                String.Format("<b>{0}</b>", Resource.Get("Category")),
                String.Format("<b>{0}</b>", Resource.Get("Year")),
                String.Format("<b>{0}</b>", Resource.Get("Country")),
                String.Format("<b>{0}</b>", Resource.Get("Language")),
                String.Format("<b>{0}</b>", Resource.Get("Storage"))
            );

            foreach (Movie item in Movies)
            {
                output.AppendFormat(format, item.Name, item.OriginalName, item.Category, item.Year, item.Country, item.Language, item.Storage);
            }
            output.Append("</table>");

            File.WriteAllText(location, output.ToString());
        }

        public void ExportToExcel(ExportData data)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<table>");

            output.Append("<tr>");
            for (int i = 0; i < data.ColumnsCount; i++)
            {
                foreach (string item in data.Columns)
                {
                    output.AppendFormat("<td><b>{0}</b></td>", Resource.Get(item));
                }
            }
            output.Append("</tr>");

            int count = 0;
            foreach (Movie movie in Movies)
            {
                if (count % data.ColumnsCount == 0)
                {
                    if (count == 0)
                        output.Append("<tr>");
                    else 
                        output.Append("</tr><tr>");
                }

                foreach (string item in data.Columns)
                {
                    output.AppendFormat("<td>{0}</td>", movie[item]);
                }

                count++;
            }
            output.Append("</tr>");

            output.Append("</table>");
            File.WriteAllText(data.FileName, output.ToString());
        }
    }
}

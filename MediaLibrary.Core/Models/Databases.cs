using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using DesktopCore;
using MediaLibrary.Export;
using System.Net;
using MediaLibrary.Core.Helpers;

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

        private string publicIdentifier;
        private string publicUsername;
        private string publicPassword;
        private bool publishOnSave = false;
        private bool downloadOnLoad = false;
        private bool savePublicPassword = true;
        private string publicDownloadUrlPattern = "http://medialibrary.neptuo.com/api/database/{0}";
        private string publicUploadUrlPattern = "http://medialibrary.neptuo.com/api/database/{0}/update";

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
            set
            {
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

        public string PublicIdentifier
        {
            get { return publicIdentifier; }
            set
            {
                publicIdentifier = value;
                FirePropertyChanged("PublicIdentifier");
            }
        }

        public string PublicUsername
        {
            get { return publicUsername; }
            set
            {
                publicUsername = value;
                FirePropertyChanged("PublicUsername");
            }
        }

        public string PublicPassword
        {
            get { return publicPassword; }
            set
            {
                publicPassword = value;
                FirePropertyChanged("PublicPassword");
            }
        }

        public bool PublishOnSave
        {
            get { return publishOnSave; }
            set
            {
                publishOnSave = value;
                FirePropertyChanged("PublishOnSave");
            }
        }

        public bool DownloadOnLoad
        {
            get { return downloadOnLoad; }
            set
            {
                downloadOnLoad = value;
                FirePropertyChanged("DownloadOnLoad");
            }
        }

        public bool SavePublicPassword
        {
            get { return savePublicPassword; }
            set
            {
                savePublicPassword = value;
                FirePropertyChanged("SavePublicPassword");
            }
        }

        public string PublicDownloadUrlPattern
        {
            get { return publicDownloadUrlPattern; }
            set
            {
                publicDownloadUrlPattern = value;
                FirePropertyChanged("PublicDownloadUrlPattern");
            }
        }

        public string PublicUploadUrlPattern
        {
            get { return publicUploadUrlPattern; }
            set
            {
                publicUploadUrlPattern = value;
                FirePropertyChanged("PublicUploadUrlPattern");
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

        public void Save(bool ignorePublic = false)
        {
            if (Encrypted && RequiresPassword)
                return;

            if (location == null)
                return;

            string backupLocation = location.Replace(".xml", "1.xml");
            try
            {
                if (File.Exists(location))
                    File.Copy(location, backupLocation);

                XmlDocument doc = DatabaseHelper.Serialize(this);

                if (Encrypted && Password != null)
                {
                    string content;
                    if (CryptoHelper.Encrypt(doc.DocumentElement.InnerXml, Password, out content))
                        doc.DocumentElement.InnerXml = content;
                    else
                        doc.DocumentElement.Attributes["Encrypted"].Value = false.ToString();
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
                    File.Delete(backupLocation);
            }

            if (!ignorePublic && PublishOnSave)
                Upload();
        }

        public void Load(bool ignorePublic = false)
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

                DatabaseHelper.Deserialize(doc, this);

                if (!ignorePublic && DownloadOnLoad)
                {
                    Download(delegate
                    {
                        Movies.Clear();

                        doc = new XmlDocument();
                        sr = new StreamReader(location);
                        doc.LoadXml(sr.ReadToEnd());
                        sr.Close();

                        DatabaseHelper.Deserialize(doc, this);
                    });
                }
            }
            else
            {
                File.Create(location);
            }
        }

        public void Download(Action onCompleted = null, Action<string> onError = null)
        {
            try
            {
                WebClient client = new WebClient(); //TODO: Encoding problem!!
                client.Encoding = Encoding.UTF8;
                client.DownloadStringAsync(new Uri(String.Format(PublicDownloadUrlPattern, PublicIdentifier)));
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(delegate(object sender, DownloadStringCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        File.WriteAllText(Location, e.Result);

                        if (onCompleted != null)
                            onCompleted();
                    }
                    else
                    {
                        if (onError != null)
                            onError(e.Error.Message);
                    }
                });
            }
            catch (Exception e)
            {
                if (onError != null)
                    onError(e.Message);
            }
        }

        public void Upload(Action onCompleted = null, Action<string> onError = null)
        {
            try
            {
                WebClient client = new WebClient(); //TODO: Encoding problem!!
                client.Encoding = Encoding.UTF8;
                client.UploadStringAsync(new Uri(String.Format(PublicUploadUrlPattern, PublicIdentifier)), "POST", File.ReadAllText(Location));
                client.UploadStringCompleted += new UploadStringCompletedEventHandler(delegate(object sender, UploadStringCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        if (onCompleted != null)
                            onCompleted();
                    }
                    else
                    {
                        if (onError != null)
                            onError(e.Error.Message);
                    }
                });
            }
            catch (Exception e)
            {
                if (onError != null)
                    onError(e.Message);
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

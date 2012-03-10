using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DesktopCore;

namespace MediaLibrary.Core.Helpers
{
    public static class DatabaseHelper
    {
        public static XmlDocument Serialize(Database database)
        {
            HashSet<KeyValuePair<int, int>> relatedMovies = new HashSet<KeyValuePair<int, int>>();

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(dec);

            XmlElement media = doc.CreateElement("MediaLibrary");
            doc.AppendChild(media);

            XmlAttribute name = doc.CreateAttribute("Name");
            name.Value = database.Name;
            media.Attributes.Append(name);

            XmlHelper.SetAttribute(doc, media, "PublicIdentifier", database.PublicIdentifier);
            XmlHelper.SetAttribute(doc, media, "PublicUsername", database.PublicUsername);

            if (database.SavePublicPassword)
                XmlHelper.SetAttribute(doc, media, "PublicPassword", database.PublicPassword);

            XmlHelper.SetAttribute(doc, media, "SavePublicPassword", database.SavePublicPassword);
            XmlHelper.SetAttribute(doc, media, "PublishOnSave", database.PublishOnSave);
            XmlHelper.SetAttribute(doc, media, "DownloadOnLoad", database.DownloadOnLoad);
            XmlHelper.SetAttribute(doc, media, "PublicDownloadUrlPattern", database.PublicDownloadUrlPattern);
            XmlHelper.SetAttribute(doc, media, "PublicUploadUrlPattern", database.PublicUploadUrlPattern);

            XmlAttribute enc = doc.CreateAttribute("Encrypted");
            enc.Value = (database.Encrypted && database.Password != null).ToString();
            media.Attributes.Append(enc);

            XmlElement movies = doc.CreateElement("Movies");
            media.AppendChild(movies);

            foreach (Movie m in database.Movies)
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

            return doc;
        }

        public static void Deserialize(XmlDocument doc, Database database)
        {
            database.Name = XmlHelper.GetAttributeValue(doc.DocumentElement, "Name");
            database.Encrypted = XmlHelper.GetAttributeBool(doc.DocumentElement, "Encrypted");
            database.OnlineName = XmlHelper.GetAttributeValue(doc.DocumentElement, "OnlineName", "ČSFD");
            database.OnlineFormat = XmlHelper.GetAttributeValue(doc.DocumentElement, "OnlineFormat", "http://www.csfd.cz/film/{0}");

            database.PublicIdentifier = XmlHelper.GetAttributeValue(doc.DocumentElement, "PublicIdentifier");
            database.PublicUsername = XmlHelper.GetAttributeValue(doc.DocumentElement, "PublicUsername");
            database.SavePublicPassword = XmlHelper.GetAttributeBool(doc.DocumentElement, "SavePublicPassword");
            if (database.SavePublicPassword)
                database.PublicPassword = XmlHelper.GetAttributeValue(doc.DocumentElement, "PublicPassword");
            database.PublishOnSave = XmlHelper.GetAttributeBool(doc.DocumentElement, "PublishOnSave");
            database.DownloadOnLoad = XmlHelper.GetAttributeBool(doc.DocumentElement, "DownloadOnLoad");
            database.PublicDownloadUrlPattern = XmlHelper.GetAttributeValue(doc.DocumentElement, "PublicDownloadUrlPattern", database.PublicDownloadUrlPattern);
            database.PublicUploadUrlPattern = XmlHelper.GetAttributeValue(doc.DocumentElement, "PublicUploadUrlPattern", database.PublicUploadUrlPattern);

            if (database.Encrypted && !database.RequiresPassword)
            {
                database.RequiresPassword = true;
                return;
            }
            else if (database.Encrypted)
            {
                string content;
                if (CryptoHelper.Decrypt(doc.DocumentElement.InnerXml, database.Password, out content))
                {
                    database.RequiresPassword = false;
                    doc.DocumentElement.InnerXml = content;
                }
            }

            foreach (XmlElement m in doc.GetElementsByTagName("Movie"))
            {
                database.Movies.Add(new Movie()
                {
                    ID = XmlHelper.GetAttributeInt(m, "ID", database.Movies.NextID),
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
                    source = database.Movies.FindByID(sourceId);
                    cache.Add(sourceId, source);
                }

                if (cache.ContainsKey(targetId))
                {
                    target = cache[targetId];
                }
                else
                {
                    target = database.Movies.FindByID(targetId);
                    cache.Add(targetId, target);
                }

                if (source != null && target != null)
                {
                    source.Related.Add(target);
                    target.Related.Add(source);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;
using DesktopCore;
using System.Threading;
using System.Globalization;

namespace MediaLibrary.Core
{
    public interface IManager
    {
        Databases Databases { get; set; }

        List<Language> Languages { get; set; }

        int WindowHeight { get; set; }

        ExitMode ExitMode { get; set; }

        void Save(string location, bool saveDatabases = true);

        void Load(string location);
    }

    public class ManagerFactory
    {
        public static readonly string DefaultLocation = "Configuration.xml";
        public static readonly string PasswordCrypto = "MediaLibrary_2011-07-27";
        public static readonly string DefaultLocale = "cs-CZ";
        public static readonly string DefaultDatabaseName = "MediaLibrary";
        public static readonly string DatabaseExtension = ".xml";

        public static IManager Create()
        {
            return new DefaultManager();
        }
    }

    public class DefaultManager : NotifyPropertyChanged, IManager
    {
        private Databases databases;
        private int windowHeight = 750;
        private ExitMode exitMode = ExitMode.Autosave;

        public Databases Databases
        {
            get { return databases; }
            set
            {
                databases = value;
                FirePropertyChanged("Databases");
            }
        }

        public List<Language> Languages { get; set; }

        public int WindowHeight
        {
            get { return windowHeight; }
            set
            {
                windowHeight = value;
                FirePropertyChanged("WindowHeight");
            }
        }

        public ExitMode ExitMode
        {
            get { return exitMode; }
            set
            {
                exitMode = value;
                FirePropertyChanged("ExitMode");
            }
        }

        public DefaultManager()
        {
            Databases = new Databases();
        }

        public void Save(string location, bool saveDatabases = true)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(dec);

            XmlElement conf = doc.CreateElement("Configuration");
            doc.AppendChild(conf);

            XmlAttribute wh = doc.CreateAttribute("WindowHeight");
            wh.Value = WindowHeight.ToString();
            conf.Attributes.Append(wh);

            XmlAttribute em = doc.CreateAttribute("ExitMode");
            em.Value = ExitMode.ToString();
            conf.Attributes.Append(em);

            XmlAttribute cul = doc.CreateAttribute("Culture");
            cul.Value = Thread.CurrentThread.CurrentCulture.Name;
            conf.Attributes.Append(cul);


            foreach (Database item in Databases)
            {
                if(saveDatabases)
                    item.Save();

                if (item.Location == null)
                    continue;

                XmlElement db = doc.CreateElement("Database");
                conf.AppendChild(db);

                XmlAttribute loc = doc.CreateAttribute("Location");
                loc.Value = Path.GetFullPath(item.Location);
                db.Attributes.Append(loc);

                if (item.SavePassword)
                {
                    XmlAttribute pass = doc.CreateAttribute("Password");
                    string password;
                    if (CryptoHelper.Encrypt(item.Password, ManagerFactory.PasswordCrypto, out password))
                    {
                        pass.Value = password;
                        db.Attributes.Append(pass);
                    }
                }
            }

            doc.Save(location);
        }

        public void Load(string location)
        {
            if (File.Exists(location))
            {
                XmlDocument doc = new XmlDocument();
                StreamReader sr = new StreamReader(location);
                doc.LoadXml(sr.ReadToEnd());
                sr.Close();

                XmlElement conf = doc.GetElementsByTagName("Configuration")[0] as XmlElement;
                if (conf != null)
                {
                    WindowHeight = XmlHelper.GetAttributeInt(conf, "WindowHeight", WindowHeight);

                    CultureInfo culture = XmlHelper.GetAttributeCulture(conf, "Culture", Thread.CurrentThread.CurrentCulture);
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;

                    ExitMode = XmlHelper.GetAttributeEnum<ExitMode>(conf, "ExitMode", ExitMode);
                }

                foreach (XmlElement db in doc.GetElementsByTagName("Database"))
                {
                    Database item = new Database(XmlHelper.GetAttributeValue(db, "Location"));
                    string passwordEncrypted = XmlHelper.GetAttributeValue(db, "Password");
                    if (passwordEncrypted != null)
                    {
                        item.SavePassword = true;
                        string password;
                        if (CryptoHelper.Decrypt(passwordEncrypted, ManagerFactory.PasswordCrypto, out password))
                        {
                            item.RequiresPassword = true;
                            item.Password = password;
                            item.Load();
                        }
                    }
                    Databases.Add(item);
                }

                Resource.Load("Resources/Resources");

                Languages = new List<Language>();
                foreach (CultureInfo item in Resources.GetSupportedLocales("Resources/Resources", ManagerFactory.DefaultLocale))
                {
                    Languages.Add(new Language() { Name = item.NativeName, Code = item.Name });
                }
            }
            else
            {
                Database database = new Database();
                database.Name = ManagerFactory.DefaultDatabaseName;
                database.Location = Path.Combine(Environment.CurrentDirectory, ManagerFactory.DefaultDatabaseName + ManagerFactory.DatabaseExtension);
                Databases.Add(database);
            }
        }
    }

    public class Language
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

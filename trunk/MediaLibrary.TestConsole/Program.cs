using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MediaLibrary.Core;
using DesktopCore;
using System.Globalization;
using System.Security.Cryptography;
using MediaLibrary.Export;

namespace MediaLibrary.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TestImport();
            //TransformSource();
            //TestCryptography();
            //TestExport();

            //ReadKey
            Console.ReadKey(true);
        }

        static void TestImport()
        {
            string sourceFile = null;
            string destFile;
            while (sourceFile == null || !File.Exists(sourceFile))
            {
                Console.WriteLine("Source file:");
                sourceFile = Console.ReadLine();
            }

            Console.WriteLine("Destionation file:");
            destFile = Console.ReadLine();

            Database db = new Database();
            db.Location = destFile;

            int id = 1;
            foreach (string line in File.ReadAllLines(sourceFile))
            {
                string[] movie = line.Split(',');
                Movie m = new Movie();
                m.ID = id;
                m.Name = movie[0];
                m.Storage = movie[1];
                db.Movies.Add(m);

                id++;
            }
            Console.WriteLine("Loaded {0} movies", id);
            db.Save();
            Console.WriteLine("Saved");
        }

        static void TestCulture()
        {
            foreach (CultureInfo item in Resources.GetSupportedLocales("Resources/Resources", "cs-CZ"))
            {
                Console.WriteLine(item.DisplayName);
            }
        }

        static void TestCryptography()
        {
            Aes aes = Aes.Create();
            byte[] input = { (byte)'H', (byte)'E', (byte)'L', (byte)'L', (byte)'O' };
            byte[] output;

            //Add password
            ICryptoTransform ct = aes.CreateEncryptor();
            output = ct.TransformFinalBlock(input, 0, 5);

            Console.WriteLine("InputBlockLength: {0}, OutputBlockLength: {1}", ct.InputBlockSize, ct.OutputBlockSize);

            foreach (byte item in output)
            {
                Console.Write((char)item);
            }
            Console.WriteLine();

            //Add password
            ct = aes.CreateDecryptor();
            output = ct.TransformFinalBlock(output, 0, output.Length);

            foreach (byte item in output)
            {
                Console.Write((char)item);
            }
            Console.WriteLine();
        }

        static void TestExport()
        {
            Resource.Load("Resources/Resources");
            Database db = new Database(@"D:\Projects\VS10\Projects\MediaLibrary\MediaLibrary.GUI\bin\Release\MediaLibrary.xml");

            ExportData data = new ExportData();
            data.FileName = "Export.xls";
            data.ColumnsCount = 1;
            data.Columns = new List<string>() {"ID", "Name", "Year", "Storage"};

            db.ExportToExcel(data);
        }
    }
}

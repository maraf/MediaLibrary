using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Test
{
    public class Test
    {
        protected XmlStore Store { get; } = new XmlStore();

        protected Library CreateEmpty()
        {
            Library library = new Library();
            library.Configuration.Name = "XmlStoreTest";
            library.Configuration.FilePath = "XmlStoreTest.xml";
            return library;
        }
    }
}

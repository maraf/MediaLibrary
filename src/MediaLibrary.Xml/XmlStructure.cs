using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MediaLibrary
{
    public class XmlStructure
    {
        private readonly bool isNamespaceUsed;

        public static string Namespace => "http://schemas.neptuo.com/xsd/MediaLibrary/v1.xsd";

        public XName Root => GetName("MediaLibrary", Namespace);
        public XName Movies => GetName("Movies", Namespace);
        public XName Movie => GetName("Movie", Namespace);
        public XName MovieId => GetName("ID");

        public XName RelatedMovies => GetName("RelatedMovies", Namespace);
        public XName RelatedMovie => GetName("Related", Namespace);
        public XName RelatedMovieSourceId => GetName("SourceID");
        public XName RelatedMovieTargetId => GetName("TargetID");

        public XmlStructure(bool isNamespaceUsed = true)
        {
            this.isNamespaceUsed = isNamespaceUsed;
        }

        private XName GetName(string name, string namespaceName = null)
        {
            if (isNamespaceUsed && namespaceName != null)
                return XName.Get(name, namespaceName);

            return XName.Get(name);
        }
    }
}

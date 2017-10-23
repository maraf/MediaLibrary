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
        public const string Namespace = "http://schemas.neptuo.com/xsd/MediaLibrary/v1.xsd";

        public static readonly XName Root = XName.Get("MediaLibrary", Namespace);
        public static readonly XName Movies = XName.Get("Movies", Namespace);
        public static readonly XName Movie = XName.Get("Movie", Namespace);
        public static readonly XName MovieId = XName.Get("ID", Namespace);
        public static readonly XName MovieName = XName.Get("Name", Namespace);

        public static readonly XName RelatedMovies = XName.Get("RelatedMovies", Namespace);
        public static readonly XName RelatedMovie = XName.Get("RelatedMovie", Namespace);
        public static readonly XName RelatedMovieSourceId = XName.Get("SourceID", Namespace);
        public static readonly XName RelatedMovieTargetId = XName.Get("TargetID", Namespace);
    }
}

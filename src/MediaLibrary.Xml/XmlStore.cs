using Neptuo;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MediaLibrary
{
    /// <summary>
    /// A serializer and deserializer for <see cref="Library"/>.
    /// </summary>
    public class XmlStore
    {
        /// <summary>
        /// Saves content from <paramref name="model"/> to its storage file as XML.
        /// </summary>
        /// <param name="model">A library to save.</param>
        /// <returns>Continuation task.</returns>
        public Task SaveAsync(Library model)
        {
            Ensure.NotNull(model, "model");

            if (!string.IsNullOrEmpty(model.Configuration.FilePath))
            {
                XDocument document = new XDocument();

                XElement rootElement = new XElement(XmlStructure.Root);
                document.Add(rootElement);

                XElement moviesElement = new XElement(XmlStructure.Movies);
                rootElement.Add(moviesElement);

                XElement relatedMoviesElement = new XElement(XmlStructure.RelatedMovies);
                rootElement.Add(relatedMoviesElement);

                foreach (Movie movie in model.Movies)
                {
                    XElement movieElement = new XElement(XmlStructure.Movie);
                    moviesElement.Add(movieElement);
                    SaveMovie(movieElement, movie);

                    SaveRelated(relatedMoviesElement, movie);
                }

                document.Save(model.Configuration.FilePath);
            }

            return Task.CompletedTask;
        }

        private string GetMovieId(IKey key)
        {
            string id = null;
            if (key is GuidKey guidKey)
                id = guidKey.Guid.ToString();
            else if (key is StringKey stringKey)
                id = stringKey.Identifier;
            else
                throw Ensure.Exception.NotSupported($"Not supported key of class '{key.GetType()}'.");

            return id;
        }

        private void SaveMovieKey(XElement element, XName attributeName, IKey key)
        {
            element.SetAttributeValue(attributeName, GetMovieId(key));
        }

        private void SaveMovie(XElement element, Movie model)
        {
            SaveMovieKey(element, XmlStructure.MovieId, model.Key);
            element.SetAttributeValue(XmlStructure.MovieName, model.Name);
        }

        private void SaveRelated(XElement parentElement, Movie model)
        {
            string sourceId = GetMovieId(model.Key);

            foreach (IKey relatedKey in model.RelatedMovieKeys)
            {
                string targetId = GetMovieId(relatedKey);
                bool isContained = parentElement
                    .Descendants(XmlStructure.RelatedMovie)
                    .Any(e =>
                    {
                        string sId = e.Attribute(XmlStructure.RelatedMovieSourceId)?.Value;
                        string tId = e.Attribute(XmlStructure.RelatedMovieTargetId)?.Value;

                        return sId == sourceId && tId == targetId || sId == targetId && tId == sourceId;
                    });

                if (!isContained)
                {
                    XElement element = new XElement(XmlStructure.RelatedMovie);
                    parentElement.Add(element);

                    element.SetAttributeValue(XmlStructure.RelatedMovieSourceId, sourceId);
                    element.SetAttributeValue(XmlStructure.RelatedMovieTargetId, targetId);
                }
            }
        }

        /// <summary>
        /// Loads (adds) movies from file storage of <paramref name="model"/>.
        /// </summary>
        /// <param name="model">A library to load to.</param>
        /// <returns>Continuation task.</returns>
        public Task LoadAsync(Library model)
        {
            Ensure.NotNull(model, "model");

            if (!string.IsNullOrEmpty(model.Configuration.FilePath) && File.Exists(model.Configuration.FilePath))
            {
                XDocument document = XDocument.Load(model.Configuration.FilePath);
                foreach (XElement movieElement in document.Descendants(XmlStructure.Movies).Descendants(XmlStructure.Movie))
                    model.Movies.Add(LoadMovie(movieElement));

                foreach (XElement relatedElement in document.Descendants(XmlStructure.RelatedMovies).Descendants(XmlStructure.RelatedMovie))
                {
                    var keys = LoadRelatedMovieKeys(relatedElement);
                    Movie movie1 = model.FindByKey(keys.key1);
                    if (movie1 != null)
                        movie1.RelatedMovieKeys.Add(keys.key2);

                    Movie movie2 = model.FindByKey(keys.key2);
                    if (movie2 != null)
                        movie2.RelatedMovieKeys.Add(keys.key1);
                }
            }

            return Task.CompletedTask;
        }

        private Movie LoadMovie(XElement element)
        {
            IKey key = LoadMovieKey(element, XmlStructure.MovieId);
            Movie model = new Movie(key);
            model.Name = element.Attribute(XmlStructure.MovieName)?.Value;

            return model;
        }

        private IKey LoadMovieKey(XElement element, XName attributeName)
        {
            string id = element.Attribute(attributeName)?.Value;
            if (id == null)
                throw Ensure.Exception.InvalidOperation($"Missing attribute '{XmlStructure.MovieId}' on element '{element}'.");

            IKey key = null;
            if (Guid.TryParse(id, out Guid guid))
                key = GuidKey.Create(guid, "Movie");
            else
                key = StringKey.Create(id, "Movie");

            return key;
        }

        private (IKey key1, IKey key2) LoadRelatedMovieKeys(XElement element)
        {
            IKey sourceKey = LoadMovieKey(element, XmlStructure.RelatedMovieSourceId);
            IKey targetKey = LoadMovieKey(element, XmlStructure.RelatedMovieTargetId);

            return (sourceKey, targetKey);
        }
    }
}

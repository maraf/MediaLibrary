using Neptuo;
using Neptuo.Converters;
using Neptuo.Models.Keys;
using Neptuo.PresentationModels;
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
        private readonly IConverterRepository converters;

        /// <summary>
        /// Creates a new instance with default collection of converters (<see cref="Converts.Repository"/>).
        /// </summary>
        public XmlStore()
            : this(Converts.Repository)
        { }

        /// <summary>
        /// Creates a new instance with <paramref name="converters"/> for serializing and deserializing values.
        /// </summary>
        /// <param name="converters">A collection of converters.</param>
        public XmlStore(IConverterRepository converters)
        {
            Ensure.NotNull(converters, "converters");
            this.converters = converters;
        }

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
                XmlStructure structure = new XmlStructure();
                XDocument document = new XDocument();

                XElement rootElement = new XElement(structure.Root);
                document.Add(rootElement);

                XElement moviesElement = new XElement(structure.Movies);
                rootElement.Add(moviesElement);

                XElement relatedMoviesElement = new XElement(structure.RelatedMovies);
                rootElement.Add(relatedMoviesElement);

                foreach (Movie movie in model.Movies)
                {
                    XElement movieElement = new XElement(structure.Movie);
                    moviesElement.Add(movieElement);
                    SaveMovie(structure, movieElement, movie);

                    SaveRelated(structure, relatedMoviesElement, movie);
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

        private void SaveMovie(XmlStructure structure, XElement element, Movie model)
        {
            SaveMovieKey(element, structure.MovieId, model.Key);

            foreach (IFieldDefinition fieldDefinition in model.Library.MovieDefinition.Fields)
            {
                if(model.TryGetValue(fieldDefinition.Identifier, out object value))
                {
                    if (value != null && converters.TryConvert(value.GetType(), typeof(string), value, out object stringValue))
                        element.SetAttributeValue(fieldDefinition.Identifier, (string)stringValue);
                }
            }
        }

        private void SaveRelated(XmlStructure structure, XElement parentElement, Movie model)
        {
            string sourceId = GetMovieId(model.Key);

            foreach (IKey relatedKey in model.RelatedMovieKeys)
            {
                string targetId = GetMovieId(relatedKey);
                bool isContained = parentElement
                    .Descendants(structure.RelatedMovie)
                    .Any(e =>
                    {
                        string sId = e.Attribute(structure.RelatedMovieSourceId)?.Value;
                        string tId = e.Attribute(structure.RelatedMovieTargetId)?.Value;

                        return sId == sourceId && tId == targetId || sId == targetId && tId == sourceId;
                    });

                if (!isContained)
                {
                    XElement element = new XElement(structure.RelatedMovie);
                    parentElement.Add(element);

                    element.SetAttributeValue(structure.RelatedMovieSourceId, sourceId);
                    element.SetAttributeValue(structure.RelatedMovieTargetId, targetId);
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
                XmlStructure structure = new XmlStructure();

                XDocument document = XDocument.Load(model.Configuration.FilePath);

                XElement moviesRootElement = document.Descendants(structure.Movies).FirstOrDefault();
                if (moviesRootElement == null)
                    structure = new XmlStructure(false);

                moviesRootElement = document.Descendants(structure.Movies).FirstOrDefault();
                if (moviesRootElement != null)
                {
                    foreach (XElement movieElement in document.Descendants(structure.Movies).Descendants(structure.Movie))
                        model.Movies.Add(LoadMovie(model, structure, movieElement));

                    foreach (XElement relatedElement in document.Descendants(structure.RelatedMovies).Descendants(structure.RelatedMovie))
                    {
                        var keys = LoadRelatedMovieKeys(structure, relatedElement);
                        Movie movie1 = model.Movies.FindByKey(keys.key1);
                        if (movie1 != null)
                            movie1.RelatedMovieKeys.Add(keys.key2);
                    }
                }
            }

            return Task.CompletedTask;
        }

        private Movie LoadMovie(Library library, XmlStructure structure, XElement element)
        {
            IKey key = LoadMovieKey(structure, element, structure.MovieId);
            Movie model = new Movie(key, library);

            foreach (IFieldDefinition fieldDefinition in library.MovieDefinition.Fields)
            {
                XAttribute attribute = element.Attribute(fieldDefinition.Identifier);
                if (attribute != null && converters.TryConvert(typeof(string), fieldDefinition.FieldType, attribute.Value, out object value))
                    model.TrySetValue(fieldDefinition.Identifier, value);
            }

            return model;
        }

        private IKey LoadMovieKey(XmlStructure structure, XElement element, XName attributeName)
        {
            string id = element.Attribute(attributeName)?.Value;
            if (id == null)
                throw Ensure.Exception.InvalidOperation($"Missing attribute '{structure.MovieId}' on element '{element}'.");

            IKey key = null;
            if (Guid.TryParse(id, out Guid guid))
                key = GuidKey.Create(guid, "Movie");
            else
                key = StringKey.Create(id, "Movie");

            return key;
        }

        private (IKey key1, IKey key2) LoadRelatedMovieKeys(XmlStructure structure, XElement element)
        {
            IKey sourceKey = LoadMovieKey(structure, element, structure.RelatedMovieSourceId);
            IKey targetKey = LoadMovieKey(structure, element, structure.RelatedMovieTargetId);

            return (sourceKey, targetKey);
        }
    }
}

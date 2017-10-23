using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Models.Keys;
using System.IO;

namespace MediaLibrary.Test
{
    [TestClass]
    public class XmlStoreTest : Test
    {
        [TestMethod]
        public void EnsureStorage()
        {
            Assert.IsTrue(File.Exists(CreateEmpty().Configuration.FilePath));
        }

        [TestMethod]
        public void Load()
        {
            Library library = CreateEmpty();
            Store.LoadAsync(library).GetAwaiter().GetResult();

            Assert.AreEqual(2, library.Movies.Count);

            IKey movieKey1 = StringKey.Create("1", "Movie");
            Movie movie1 = library.Movies.FindByKey(movieKey1);
            Assert.IsNotNull(movie1);

            IKey movieKey2 = StringKey.Create("2", "Movie");
            Movie movie2 = library.Movies.FindByKey(movieKey2);
            Assert.IsNotNull(movie2);

            Assert.AreEqual("Movie 1", movie1.Name);
            Assert.AreEqual(1, movie1.RelatedMovieKeys.Count);
            Assert.IsTrue(movie1.RelatedMovieKeys.Contains(movieKey2));

            Assert.AreEqual("Movie 2", movie2.Name);
            Assert.AreEqual(1, movie2.RelatedMovieKeys.Count);
            Assert.IsTrue(movie2.RelatedMovieKeys.Contains(movieKey1));
        }

        [TestMethod]
        public void Save()
        {
            Library library = CreateEmpty();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Models.Keys;
using System.IO;

namespace MediaLibrary.Test
{
    [TestClass]
    public class XmlStoreTest : Test
    {
        protected IKey MovieKey1 { get; } = StringKey.Create("1", "Movie");
        protected IKey MovieKey2 { get; } = StringKey.Create("2", "Movie");

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

            Movie movie1 = library.Movies.FindByKey(MovieKey1);
            Assert.IsNotNull(movie1);

            Movie movie2 = library.Movies.FindByKey(MovieKey2);
            Assert.IsNotNull(movie2);

            Assert.AreEqual("Movie 1", movie1.Name);
            Assert.AreEqual(1, movie1.RelatedMovieKeys.Count);
            Assert.IsTrue(movie1.RelatedMovieKeys.Contains(MovieKey2));

            Assert.AreEqual("Movie 2", movie2.Name);
            Assert.AreEqual(1, movie2.RelatedMovieKeys.Count);
            Assert.IsTrue(movie2.RelatedMovieKeys.Contains(MovieKey1));
        }

        [TestMethod]
        public void Save()
        {
            Library library = CreateEmpty();

            Movie movie1 = new Movie(MovieKey1, library);
            movie1.Name = "Movie 1";
            library.Movies.Add(movie1);

            Movie movie2 = new Movie(MovieKey2, library);
            movie2.Name = "Movie 2";
            library.Movies.Add(movie2);

            movie1.RelatedMovieKeys.Add(movie2.Key);

            Store.SaveAsync(library).GetAwaiter().GetResult();

            Load();
        }
    }
}

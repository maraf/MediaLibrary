using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Test
{
    [TestClass]
    public class RelatedMovieTest : Test
    {
        [TestMethod]
        public void Propagation()
        {
            Library library = CreateEmpty();
            Movie movie1 = library.Create();
            Movie movie2 = library.Create();

            movie1.RelatedMovieKeys.Add(movie2.Key);

            Assert.AreEqual(1, movie1.RelatedMovieKeys.Count);
            Assert.AreEqual(1, movie2.RelatedMovieKeys.Count);
        }

        [TestMethod]
        public void Duplicities()
        {
            Library library = CreateEmpty();
            Movie movie1 = library.Create();
            Movie movie2 = library.Create();

            movie1.RelatedMovieKeys.Add(movie2.Key);
            movie1.RelatedMovieKeys.Add(movie2.Key);
            movie2.RelatedMovieKeys.Add(movie1.Key);
            movie2.RelatedMovieKeys.Add(movie1.Key);

            Assert.AreEqual(1, movie1.RelatedMovieKeys.Count);
            Assert.AreEqual(1, movie2.RelatedMovieKeys.Count);
        }

        [TestMethod]
        public void Remove()
        {
            Library library = CreateEmpty();
            Movie movie1 = library.Create();
            Movie movie2 = library.Create();
            movie1.RelatedMovieKeys.Add(movie2.Key);

            Assert.AreEqual(1, movie1.RelatedMovieKeys.Count);
            Assert.AreEqual(1, movie2.RelatedMovieKeys.Count);

            library.Movies.Remove(movie1);

            Assert.AreEqual(0, movie1.RelatedMovieKeys.Count);
            Assert.AreEqual(0, movie2.RelatedMovieKeys.Count);
        }
    }
}

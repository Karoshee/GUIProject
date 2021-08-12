using NUnit.Framework;
using GUIProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUIProject.Cars;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace GUIProject.Tests
{
    [TestFixture()]
    public class PathsTests
    {
        [Test()]
        public void GetDirectoryTest()
        {
            // arrange
            var paths = new Paths(new FakeDirectory());

            // act
            var result = paths.GetDirectory<Car>();

            // assert
            Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GUIProject\Cars", result, "Неправильный адрес папки для машин");
        }
    }
}
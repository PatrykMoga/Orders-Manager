using Autofac.Extras.Moq;
using NUnit.Framework;
using OrdersManager.Core.FilesProcessing;
using System.IO;
using System.Linq;

namespace OrdersManager.Tests.FilesProcessingTests
{
    [TestFixture]
    public class FilesReaderTests
    {
        [Test]
        public void ReadFiles_WhenCalled_ReadFilesInDirectory()
        {
            using (var mock = AutoMock.GetLoose())
            {
                const string path = @"..\..\..\TestingFiles";
                var sut = mock.Create<FilesReader>();
                sut.ReadFiles(path, SearchOption.AllDirectories);

                var actual = sut.Files.Count();
                Assert.That(actual, Is.EqualTo(6));
            }
        }
    }
}

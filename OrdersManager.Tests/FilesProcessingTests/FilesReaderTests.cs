using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Autofac.Extras.Moq;
using System.IO;
using System.Reflection;
using OrdersManager.Core.FilesProcessing;
using System.Linq;

namespace OrdersManager.Tests.FilesProcessingTests
{
    [TestFixture]
    public class FilesReaderTests
    {

        [Test]
        public void Method_Scenario_ExpectedBehavior()
        {
            var path = @"..\..\..\TestingFiles";
            var reader = new FilesReader();
            reader.ReadFiles(path, SearchOption.AllDirectories);
            
            Assert.That(reader.Files.ToList().Count, Is.EqualTo(3));

        }
    }
}

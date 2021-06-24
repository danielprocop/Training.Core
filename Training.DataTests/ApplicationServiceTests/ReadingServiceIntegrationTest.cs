using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Training.DataTests.RepositoryTest;

namespace Training.DataTests.ApplicationServiceTests
{
    [TestClass]
    public class ReadingServiceIntegrationTest : DataBaseHelper
    {
        public ReadingServiceIntegrationTest()
        {

            CleanDatabase();
        }
      

    }
}

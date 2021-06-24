using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using Training.Data;

namespace Training.DataTests
{
    [TestClass]
    public class DapperContextTest
    {
       private string _connectionString;
        [TestInitialize]
        public void Init()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

            _connectionString = configuration.GetConnectionString("Default");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            IContextFactory contextFactory = new DapperContextFactory(_connectionString);
            using (IContext context = contextFactory.Create())
            {
                context.Execute("delete from dbo.Basis");
                context.Execute("delete from dbo.InputFile");
                context.Commit();
            }
        }

        [TestMethod]
        public void Query_OneItemInserted_OneItemRetrieved()
        {
            IContextFactory contextFactory = new DapperContextFactory(_connectionString);
            using (IContext context = contextFactory.Create())
            {
                const string basisCode = "TestCode1";
                const string basisDescription = "TestDescription1";
                const int id = 1;
                context.Execute(
                    "insert into dbo.Basis(Id, BasisCode,BasisDescription) values (@id, @basisCode, @basisDescription)",
                    new Basis(id, basisCode, basisDescription));
                context.Commit();
                
                // act
                IList<Basis> basisList =
                    context.Query<Basis>("select Id ,BasisCode,BasisDescription from dbo.Basis where Id = @id",
                        new { id });

                // assert
                Assert.AreEqual(1, basisList.Count);
                var retrievedBasis = basisList[0];
                Assert.AreEqual(id, retrievedBasis.Id);
                Assert.AreEqual(basisCode, retrievedBasis.BasisCode);
                Assert.AreEqual(basisDescription, retrievedBasis.BasisDescription);
            }
        }

    }
}

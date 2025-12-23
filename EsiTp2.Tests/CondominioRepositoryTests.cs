using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EsiTp2.Data.Config;
using EsiTp2.Data.Repositories;

namespace EsiTp2.Tests
{
    [TestClass]
    public class CondominioRepositoryTests
    {
        private DbConfig CriarDbConfig()
        {
            var cs = ConfigurationManager
                .ConnectionStrings["SmartCondoDb"]
                .ConnectionString;

            return new DbConfig(cs);
        }

        [TestMethod]
        public void GetAll_DeveDevolver_ListaNaoNula()
        {
            // Arrange
            var repo = new CondominioRepository(CriarDbConfig());

            // Act
            var lista = repo.GetAll();

            // Assert
            Assert.IsNotNull(lista);
        }

        [TestMethod]
        public void GetById_QuandoIdNaoExiste_DeveDevolverNull()
        {
            // Arrange
            var repo = new CondominioRepository(CriarDbConfig());

            // Act
            var cond = repo.GetById(-999);

            // Assert
            Assert.IsNull(cond);
        }
    }
}

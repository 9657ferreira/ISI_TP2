using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EsiTp2.Tests
{
    [TestClass]
    public class DatabaseSmokeTests
    {
        private string GetConnectionString()
        {
            var cs = ConfigurationManager
                        .ConnectionStrings["SmartCondoDb"]
                        .ConnectionString;

            if (string.IsNullOrWhiteSpace(cs))
                Assert.Fail("ConnectionString 'SmartCondoDb' não encontrada no App.config dos testes.");

            return cs;
        }

        [TestMethod]
        public void Deve_Conseguir_Ligar_ABaseDeDados()
        {
            // Arrange
            var cs = GetConnectionString();

            // Act
            using (var conn = new SqlConnection(cs))
            {
                conn.Open();

                // Assert
                Assert.AreEqual(ConnectionState.Open, conn.State,
                    "A ligação à base de dados não ficou em estado Open.");
            }
        }

        [TestMethod]
        public void Tabela_Condominios_Deve_Existir()
        {
            var cs = GetConnectionString();

            using (var conn = new SqlConnection(cs))
            using (var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM sys.tables WHERE name = 'Condominios';", conn))
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                var count = Convert.ToInt32(result);

                Assert.IsTrue(count > 0,
                    "A tabela 'Condominios' não existe na base de dados SmartCondoDb.");
            }
        }

        [TestMethod]
        public void Tabela_Condominios_Deve_Ter_Pelo_Menos_Uma_Linha()
        {
            var cs = GetConnectionString();

            using (var conn = new SqlConnection(cs))
            using (var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM Condominios;", conn))
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                var count = Convert.ToInt32(result);

                Assert.IsTrue(count > 0,
                    "A tabela 'Condominios' existe mas não tem qualquer registo. Insere dados de teste.");
            }
        }
    }
}

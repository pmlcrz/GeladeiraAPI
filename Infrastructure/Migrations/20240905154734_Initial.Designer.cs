using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Xunit;
using System.Threading.Tasks;

namespace GeladeiraAPI.Tests.IntegrationTests
{
    public class MigrationIntegrationTests
    {
        [Fact]
        public async Task Database_ShouldContainItensTableWithExpectedColumns()
        {
            var options = new DbContextOptionsBuilder<GeladeiraContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new GeladeiraContext(options))
            {
                await context.Database.EnsureCreatedAsync();

                var tableExists = await context.Database.ExecuteSqlRawAsync("SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Itens'");
                Assert.Equal(1, tableExists);

                var columnExists = await context.Database.ExecuteSqlRawAsync("SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Itens' AND COLUMN_NAME = 'Nome'");
                Assert.Equal(1, columnExists);
            }
        }
    }
}

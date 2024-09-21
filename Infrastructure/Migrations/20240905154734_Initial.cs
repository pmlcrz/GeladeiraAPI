using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Xunit;
using System.Threading.Tasks;

namespace GeladeiraAPI.Tests.IntegrationTests
{
    public class MigrationTests
    {
        [Fact]
        public async Task Database_ShouldHaveItensTable()
        {
            var options = new DbContextOptionsBuilder<GeladeiraContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new GeladeiraContext(options))
            {
                await context.Database.EnsureCreatedAsync();  // Certifica-se de que o banco está criado

                var tableExists = await context.Database.ExecuteSqlRawAsync("SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Itens'");

                Assert.Equal(1, tableExists);
            }
        }
    }
}

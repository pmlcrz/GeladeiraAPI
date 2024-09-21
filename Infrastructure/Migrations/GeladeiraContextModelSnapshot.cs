using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Domain;
using System.Threading.Tasks;
using Xunit;

namespace GeladeiraAPI.Tests.IntegrationTests
{
    public class GeladeiraContextIntegrationTests
    {
        private GeladeiraContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<GeladeiraContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new GeladeiraContext(options);
        }

        [Fact]
        public async Task CanAddAndRetrieveItemFromDatabase()
        {
            var context = GetInMemoryContext();

            var newItem = new ItemModel
            {
                Nome = "Item Teste",
                Posicao = 2,
                Andar = 3,
                Container = 1
            };

            context.Itens.Add(newItem);
            await context.SaveChangesAsync();

            var itemFromDb = await context.Itens.FirstOrDefaultAsync(i => i.Nome == "Item Teste");

            Assert.NotNull(itemFromDb);
            Assert.Equal("Item Teste", itemFromDb.Nome);
        }
    }
}

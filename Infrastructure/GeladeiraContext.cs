using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Domain;
using System.Threading.Tasks;
using Xunit;

namespace GeladeiraAPI.Tests.IntegrationTests
{
    public class GeladeiraContextTests
    {
        private GeladeiraContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<GeladeiraContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new GeladeiraContext(options);
        }

        [Fact]
        public async Task CanAddNewItemToDatabase()
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

        [Fact]
        public async Task CanRetrieveAllItemsFromDatabase()
        {
            var context = GetInMemoryContext();

            context.Itens.AddRange(
                new ItemModel { Nome = "Item 1", Posicao = 2, Andar = 3, Container = 1 },
                new ItemModel { Nome = "Item 2", Posicao = 3, Andar = 2, Container = 1 }
            );
            await context.SaveChangesAsync();

            var items = await context.Itens.ToListAsync();

            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task CanUpdateItemInDatabase()
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
            itemFromDb.Nome = "Item Atualizado";

            context.Itens.Update(itemFromDb);
            await context.SaveChangesAsync();

            var updatedItem = await context.Itens.FirstOrDefaultAsync(i => i.Nome == "Item Atualizado");

            Assert.NotNull(updatedItem);
            Assert.Equal("Item Atualizado", updatedItem.Nome);
        }

        [Fact]
        public async Task CanDeleteItemFromDatabase()
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
            context.Itens.Remove(itemFromDb);
            await context.SaveChangesAsync();

            var deletedItem = await context.Itens.FirstOrDefaultAsync(i => i.Nome == "Item Teste");

            Assert.Null(deletedItem);
        }
    }
}

using Domain;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GeladeiraAPI.Tests
{
    public class GeladeiraRepositoryTests
    {
        private readonly Mock<GeladeiraContext> _mockContext;
        private readonly Mock<DbSet<ItemModel>> _mockDbSet;
        private readonly GeladeiraRepository.Repository<ItemModel> _repository;

        public GeladeiraRepositoryTests()
        {
            _mockContext = new Mock<GeladeiraContext>();
            _mockDbSet = new Mock<DbSet<ItemModel>>();

            _mockContext.Setup(m => m.Set<ItemModel>()).Returns(_mockDbSet.Object);
            _repository = new GeladeiraRepository.Repository<ItemModel>(_mockContext.Object);
        }

        [Fact]
        public async Task ObterTodosAsync_ShouldReturnAllItems()
        {
            var data = new List<ItemModel>
            {
                new ItemModel { Id = 1, Nome = "Item 1", Posicao = 2, Andar = 1, Container = 1 },
                new ItemModel { Id = 2, Nome = "Item 2", Posicao = 3, Andar = 2, Container = 2 }
            };

            _mockDbSet.Setup(m => m.ToListAsync()).ReturnsAsync(data);

            var result = await _repository.ObterTodosAsync();

            Assert.Equal(2, result.Count());
            Assert.Equal("Item 1", result.First().Nome);
        }

        [Fact]
        public async Task ObterPorIdAsync_ShouldReturnItemById()
        {
            var item = new ItemModel { Id = 1, Nome = "Item 1", Posicao = 2, Andar = 1, Container = 1 };

            _mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(item);

            var result = await _repository.ObterPorIdAsync(1);

            Assert.Equal("Item 1", result.Nome);
        }

        [Fact]
        public async Task AdicionarAsync_ShouldAddNewItem()
        {
            var item = new ItemModel { Nome = "New Item", Posicao = 2, Andar = 1, Container = 1 };

            await _repository.AdicionarAsync(item);

            _mockDbSet.Verify(m => m.AddAsync(item, default), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task AtualizarAsync_ShouldUpdateExistingItem()
        {
            var item = new ItemModel { Id = 1, Nome = "Updated Item", Posicao = 2, Andar = 1, Container = 1 };

            await _repository.AtualizarAsync(item);

            _mockDbSet.Verify(m => m.Update(item), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task RemoverAsync_ShouldRemoveItemById()
        {
            var item = new ItemModel { Id = 1, Nome = "Item to Remove", Posicao = 2, Andar = 1, Container = 1 };

            _mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(item);

            await _repository.RemoverAsync(1);

            _mockDbSet.Verify(m => m.Remove(item), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }
    }
}

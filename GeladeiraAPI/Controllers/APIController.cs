using Application.DTOs;
using Domain;
using Geladeira.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GeladeiraAPI.Tests
{
    public class ItemControllerTests
    {
        private readonly Mock<IService<ItemModel>> _mockService;
        private readonly ItemController _controller;

        public ItemControllerTests()
        {
            _mockService = new Mock<IService<ItemModel>>();
            _controller = new ItemController(_mockService.Object, null, null); 
        }

        [Fact]
        public async Task ObterTodos_ShouldReturnOkResult_WithListOfItems()
        {
            var items = new List<ItemModel>
            {
                new ItemModel { Id = 1, Nome = "Item 1", Posicao = 2, Andar = 3, Container = 1 },
                new ItemModel { Id = 2, Nome = "Item 2", Posicao = 3, Andar = 2, Container = 2 }
            };
            _mockService.Setup(s => s.ObterTodosAsync()).ReturnsAsync(items);

            var result = await _controller.ObterTodos();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnItems = Assert.IsType<List<ItemModel>>(okResult.Value);
            Assert.Equal(2, returnItems.Count);
        }

        [Fact]
        public async Task ObterTodos_ShouldReturnNoContent_WhenNoItems()
        {
            _mockService.Setup(s => s.ObterTodosAsync()).ReturnsAsync(new List<ItemModel>());

            var result = await _controller.ObterTodos();

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ObterPorId_ShouldReturnOk_WhenItemExists()
        {
            var item = new ItemModel { Id = 1, Nome = "Item Teste", Posicao = 2, Andar = 3, Container = 1 };
            _mockService.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync(item);

            var result = await _controller.ObterPorId(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnItem = Assert.IsType<ItemModel>(okResult.Value);
            Assert.Equal(1, returnItem.Id);
        }

        [Fact]
        public async Task ObterPorId_ShouldReturnNotFound_WhenItemDoesNotExist()
        {
            _mockService.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync((ItemModel)null);

            var result = await _controller.ObterPorId(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Adicionar_ShouldReturnCreatedAtAction_WhenItemIsValid()
        {
            var item = new ItemModel { Id = 1, Nome = "Novo Item", Posicao = 2, Andar = 3, Container = 1 };
            _mockService.Setup(s => s.AdicionarAsync(item)).ReturnsAsync(item);

            
            var result = await _controller.Adicionar(item);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnItem = Assert.IsType<ItemModel>(createdResult.Value);
            Assert.Equal(1, returnItem.Id);
        }

        [Fact]
        public async Task Adicionar_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Nome", "O campo Nome é obrigatório.");

            var result = await _controller.Adicionar(new ItemModel());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Atualizar_ShouldReturnOk_WhenItemIsUpdated()
        {
            var item = new ItemModel { Id = 1, Nome = "Item Atualizado", Posicao = 2, Andar = 3, Container = 1 };
            _mockService.Setup(s => s.AtualizarAsync(item)).ReturnsAsync(item);

            var result = await _controller.Atualizar(1, item);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnItem = Assert.IsType<ItemModel>(okResult.Value);
            Assert.Equal("Item Atualizado", returnItem.Nome);
        }

        [Fact]
        public async Task Atualizar_ShouldReturnBadRequest_WhenIdDoesNotMatch()
        {
            var item = new ItemModel { Id = 1, Nome = "Item Atualizado", Posicao = 2, Andar = 3, Container = 1 };

            var result = await _controller.Atualizar(2, item);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Remover_ShouldReturnOk_WhenItemIsRemoved()
        {
            _mockService.Setup(s => s.RemoverAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.Remover(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Remover_ShouldReturnStatusCode500_WhenExceptionOccurs()
        {
            
            _mockService.Setup(s => s.RemoverAsync(1)).ThrowsAsync(new Exception("Erro ao remover item"));

            var result = await _controller.Remover(1);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}

using Application.DTOs;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using static Infrastructure.GeladeiraRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Geladeira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IService<ItemModel> _itemService;
        private readonly GeladeiraRepository _repositorio;
        private readonly IMapper _mapper;



        public ItemController(IService<ItemModel> itemService, GeladeiraRepository repositorio, IMapper mapper)
        {
            _itemService = itemService;
            _repositorio = repositorio;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try {
                var itens = await _itemService.ObterTodosAsync();
                if (!itens.Any())
                {
                    return NoContent();
                }
                return Ok(itens);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



        [HttpGet("Buscar/{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            try
            {
                var item = await _itemService.ObterPorIdAsync(id);

                if (item == null)
                {
                    return NotFound($"O item: {id} nao foi encontrado.");
                }

                return Ok(item);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro: {e.Message}");
            }
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] ItemModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var novoItem = await _itemService.AdicionarAsync(item);
                if (novoItem == null)
                {
                    return BadRequest("Erro ao adicionar item.");
                }
                return CreatedAtAction(nameof(ObterPorId), new { id = novoItem.Id }, novoItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro no servidor: {ex.Message}");
            }
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ItemModel item)
        {
            if (id != item.Id) return BadRequest("Erro ao atualizar item.");
            var itemAtualizado = await _itemService.AtualizarAsync(item);
            if (itemAtualizado == null) return NotFound();
            return Ok(itemAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {

            try
            {
                await _itemService.RemoverAsync(id);
                return Ok("Item removid.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}



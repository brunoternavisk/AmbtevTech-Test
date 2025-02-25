using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Carts.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.Queries;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna todas as vendas (paginadas)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllSales([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string orderBy = "id asc")
        {
            var query = new GetAllCartsQuery(page, size, orderBy);
            var sales = await _mediator.Send(query);
            return Ok(sales);
        }

        /// <summary>
        /// Retorna uma venda específica pelo ID
        /// </summary>
        [HttpGet("{id:int}")] // ✅ Define que o ID é um inteiro
        public async Task<IActionResult> GetSaleById(int id)
        {
            var sale = await _mediator.Send(new GetCartByIdQuery(id));
            if (sale == null)
                return NotFound(new { message = $"Sale with ID {id} not found" });

            return Ok(sale);
        }

        /// <summary>
        /// Cria uma nova venda
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateCartCommand command)
        {
            if (command == null)
                return BadRequest(new { error = "Invalid sale data" });

            var newSaleId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetSaleById), new { id = newSaleId }, new { message = "Sale created successfully", id = newSaleId });
        }

        /// <summary>
        /// Atualiza uma venda existente
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] UpdateCartCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { error = "Sale ID mismatch" });

            await _mediator.Send(command);
            return Ok(new { message = "Cart updated successfully" });
        }

        /// <summary>
        /// Exclui uma venda
        /// </summary>
        [HttpDelete("{id:int}")] // ✅ Define que o ID é um inteiro
        public async Task<IActionResult> DeleteSale(int id)
        {
            await _mediator.Send(new DeleteCartCommand(id));
            return NoContent();
        }
    }
}

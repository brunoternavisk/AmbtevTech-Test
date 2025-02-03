using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepository _saleRepository;

        public SaleController(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGetAllSalesAll()
        {
            var sales = await _saleRepository.GetAllAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
                return NotFound($"Sale with ID {id} not found");
            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] Sale sale)
        {
            if (sale == null)
                return BadRequest(new { error = "Invalid sale data" });

            var newSale = new Sale(sale.SaleNumber, sale.CustomerId, sale.BranchId, sale.Items);
    
            await _saleRepository.CreateAsync(newSale);
    
            return CreatedAtAction(nameof(GetSaleById), new { id = newSale.Id }, newSale);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] Sale sale)
        {
            if (id != sale.Id)
                return BadRequest("Sale ID mismatch");

            var existingSale = await _saleRepository.GetByIdAsync(id);
            if (existingSale == null)
                return NotFound($"Sale with ID {id} not found");

            await _saleRepository.UpdateAsync(sale);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(Guid id)
        {
            var existingSale = await _saleRepository.GetByIdAsync(id);
            if (existingSale == null)
                return NotFound($"Sale with ID {id} not found");

            await _saleRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

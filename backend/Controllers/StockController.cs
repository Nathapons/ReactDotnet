using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly IStockReprository _stockRepo;

        public StockController(ApplicationDBContext context, IStockReprository stockReprository)
        {
            _stockRepo = stockReprository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }  
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStock();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {

            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
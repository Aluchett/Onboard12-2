using Microsoft.AspNetCore.Mvc;
using Onboard12.Server.Services;
using Onboard12.Server.ViewModels;

namespace Onboard12.Server.Controllers;

[ApiController]
[Route("api/sales")]
public class SalesController : Controller
{
    private readonly ISalesService _saleService;
    private readonly ILogger<SalesController> _logger;

    private const string _logInvalidSaleId = "Invalid sale id sent from client.";
    private const string _logInvalidSaleObject = "Invalid sale object sent from client.";

    public SalesController(ISalesService saleService, ILogger<SalesController> logger)
    {
        _saleService = saleService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SalesViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSalesAsync()
    {
        try
        {
            var sales = await _saleService.GetSales();

            return Ok(sales);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetSalesAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SalesViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSaleAsync([FromRoute] int id)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError($"Invalid sale id: {id}");

                return BadRequest("Sale id must be greater than zero.");
            }

            var sale = await _saleService.GetSale(id);

            if (sale == null)
            {
                _logger.LogError($"Sale with id {id} not found.");

                return NotFound();
            }

            return Ok(sale);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetSaleAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateSalesRequest), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSaleAsync([FromBody] CreateSalesRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(_logInvalidSaleObject);

                return BadRequest(ModelState);
            }

            var newSale = await _saleService.CreateSale(request);

            if (newSale == null)
            {
                _logger.LogError(_logInvalidSaleObject);

                return BadRequest(_logInvalidSaleObject);
            }

            return CreatedAtAction("GetSale", new { id = newSale.Id }, newSale);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside CreateSaleAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CreateSalesRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSaleAsync([FromRoute] int id, [FromBody] CreateSalesRequest request)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidSaleId);

                return BadRequest("Sale id must be greater than zero.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError(_logInvalidSaleObject);

                return BadRequest(ModelState);
            }

            var updatedSale = await _saleService.UpdateSale(request);

            if (updatedSale != null)
            {
                _logger.LogError($"Sale with id {id} not found.");

                return NotFound();
            }

            return Ok(updatedSale);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside UpdateSaleAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteSaleAsync([FromRoute] int id)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidSaleId);

                return BadRequest("Sale id must be greater than zero.");
            }

            await _saleService.DeleteSale(id);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError($"Something went wrong inside DeleteSaleAsync action: {ex.Message}");

            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside DeleteSaleAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }
}


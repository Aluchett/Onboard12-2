using Microsoft.AspNetCore.Mvc;
using Onboard12.Server.Services;
using Onboard12.Server.ViewModels;
using StoreReact.ViewModels;
using System.Data.Common; 


namespace Onboard12.Server.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomerController> _logger;

    private const string _logInvalidCustomerId = "Invalid customer id sent from client.";
    private const string _logInvalidCustomerObject = "Invalid customer object sent from client.";

    public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
    {
        _customerService = customerService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomersAsync()
    {
        try
        {
            var customers = await _customerService.GetCustomers();

            return Ok(customers);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetCustomersAsync action: {ex.Message}");

            return Problem(ex.Message);
        }

    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomerAsync([FromRoute] int id)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidCustomerId);

                return BadRequest("Customer id must be greater than zero.");
            }

            var customer = await _customerService.GetCustomer(id);

            if (customer == null)
            {
                _logger.LogInformation($"Customer with id {id} not found.");

                return NotFound();
            }

            return Ok(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetCustomerAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(_logInvalidCustomerObject);

                return BadRequest(ModelState);
            }

            var customer = await _customerService.CreateCustomer(request);

            if (customer == null)
            {
                _logger.LogError(_logInvalidCustomerObject);

                return BadRequest(_logInvalidCustomerObject);
            }

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside CreateCustomerAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCustomerAsync([FromRoute] int id, [FromBody] CreateCustomerRequest request)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidCustomerId);

                return BadRequest(_logInvalidCustomerId);
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError(_logInvalidCustomerObject);

                return BadRequest(ModelState);
            }

            var customer = await _customerService.UpdateCustomer(id, request);

            if (customer == null)
            {
                _logger.LogError(_logInvalidCustomerObject);

                return NotFound(_logInvalidCustomerObject);
            }

            return Ok(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside UpdateCustomerAsync action: {ex.Message}");

            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCustomerAsync([FromRoute] int id)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidCustomerId);

                return BadRequest(_logInvalidCustomerId);
            }

            await _customerService.DeleteCustomer(id);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError($"Something went wrong inside DeleteCustomerAsync action: {ex.Message}");

            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside DeleteCustomerAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }
}

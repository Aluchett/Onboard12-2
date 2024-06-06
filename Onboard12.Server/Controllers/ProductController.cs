using Microsoft.AspNetCore.Mvc;
using Onboard12.Server.Services;
using Onboard12.Server.ViewModels;

namespace Onboard12.Server.Controllers;


[ApiController]
[Route("api/products")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    private const string _logInvalidProductId = "Invalid product id sent from client.";
    private const string _logInvalidProductObject = "Invalid product object sent from client.";

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductsAsync()
    {
        try
        {
            var products = await _productService.GetProducts();

            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetProductsAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductAsync([FromRoute] int id)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidProductId);

                return BadRequest("Product id must be greater than zero.");
            }

            var product = await _productService.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id {id} not found.");

                return NotFound();
            }

            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetProductAsync action: {ex.Message}");

            return Problem(ex.Message);
        }

    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(_logInvalidProductObject);

                return BadRequest(ModelState);
            }

            var product = await _productService.CreateProduct(request);

            if (product == null)
            {
                _logger.LogError(_logInvalidProductObject);

                return BadRequest(_logInvalidProductObject);
            }

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside CreateProductAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProductAsync([FromRoute] int id, [FromBody] CreateProductRequest request)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidProductId);

                return BadRequest("Product id must be greater than zero.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError(_logInvalidProductObject);

                return BadRequest(ModelState);
            }

            var product = await _productService.UpdateProduct(id, request);

            if (product == null)
            {
                _logger.LogError(_logInvalidProductObject);

                return BadRequest(_logInvalidProductObject);
            }

            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside UpdateProductAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidProductId);

                return BadRequest("Product id must be greater than zero.");
            }

            await _productService.DeleteProduct(id);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError($"Something went wrong inside DeleteProductAsync action: {ex.Message}");

            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside DeleteProductAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Onboard12.Server.Services;
using Onboard12.Server.ViewModels; 

namespace Onboard12.Server.Controllers;


[ApiController]
[Route("api/stores")]
public class StoreController : Controller
{
    private readonly IStoreService _storeService;
    private readonly ILogger<StoreController> _logger;

    private const string _logInvalidStoreId = "Invalid store id sent from client.";
    private const string _logInvalidStoreObject = "Invalid store object sent from client.";

    public StoreController(IStoreService storeService, ILogger<StoreController> logger)
    {
        _storeService = storeService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StoreViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStoresAsync()
    {
        try
        {
            var stores = await _storeService.GetStores();

            return Ok(stores);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetStoresAsync action: {ex.Message}");

            return Problem(ex.Message);
        }

    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StoreViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStoreAsync([FromRoute] int id)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidStoreId);

                return BadRequest("Store id must be greater than zero.");
            }

            var store = await _storeService.GetStore(id);

            if (store == null)
            {
                _logger.LogError($"Store with id {id} not found.");

                return NotFound();
            }

            return Ok(store);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetStoreAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(StoreViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateStoreAsync([FromBody] CreateStoreRequest store)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(_logInvalidStoreObject);

                return BadRequest(ModelState);
            }

            var newStore = await _storeService.CreateStore(store);

            if (newStore == null)
            {
                _logger.LogError(_logInvalidStoreObject);

                return BadRequest(_logInvalidStoreObject);
            }

            return CreatedAtAction("GetStore", new { id = newStore.Id }, newStore);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside CreateStoreAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(StoreViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateStoreAsync([FromRoute] int id, [FromBody] CreateStoreRequest store)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidStoreId);

                return BadRequest("Store id must be greater than zero.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid store object sent from client.");

                return BadRequest(ModelState);
            }

            var updatedStore = await _storeService.UpdateStore(id, store);

            if (updatedStore == null)
            {
                _logger.LogError($"Store with id {id} not found.");

                return NotFound();
            }

            return Ok(updatedStore);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside UpdateStoreAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(StoreViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteStoreAsync([FromRoute] int id)
    {
        try
        {
            if (id.Equals(null) || id <= 0)
            {
                _logger.LogError(_logInvalidStoreId);

                return BadRequest("Store id must be greater than zero.");
            }

            await _storeService.DeleteStore(id);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError($"Something went wrong inside DeleteStoreAsync action: {ex.Message}");

            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside DeleteStoreAsync action: {ex.Message}");

            return Problem(ex.Message);
        }
    }
}


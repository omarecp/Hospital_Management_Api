using hma_api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hma_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CrudController<T> : ControllerBase where T : class
  {
    private readonly ICrudService<T> _service;

    public CrudController(ICrudService<T> service)
    {
      _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var result = await _service.GetAll();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var result = await _service.GetById(id);
      if (result == null)
      {
        return NotFound();
      }
      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] T entity)
    {
      var result = await _service.Add(entity);
      return CreatedAtAction(nameof(GetById), new { id = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] T entity)
    {
      var existing = await _service.GetById(id);
      if (existing == null)
      {
        return NotFound();
      }

      var result = await _service.Update(entity);
      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var success = await _service.Delete(id);
      if (!success)
      {
        return NotFound();
      }
      return NoContent();
    }
  }
}

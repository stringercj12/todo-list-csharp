using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo.Data;
using todo.Models;
using todo.ViewModels;

namespace todo.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class TodoController : ControllerBase
  {

    private readonly DataDbContext _context;
    public TodoController(DataDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var todos = await _context.Todos.AsNoTracking().ToListAsync();
      return Ok(todos);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var todo = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
      return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateTodoViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest();

      var todo = new Todo
      {
        Date = DateTime.Now,
        Done = false,
        Title = model.Title
      };

      try
      {
        await _context.Todos.AddAsync(todo);
        await _context.SaveChangesAsync();
        return Created($"{todo.Id}", todo);
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> PutAsync([FromBody] CreateTodoViewModel model, Guid id)
    {
      if (!ModelState.IsValid) return BadRequest();

      var todo = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

      if (todo == null) return NotFound();

      try
      {
        todo.Title = model.Title;
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
        return Ok(todo);
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      var todo = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

      if (todo == null) return NotFound();

      try
      {
        _context.Todos.Remove(todo);

        await _context.SaveChangesAsync();
        return Ok();
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }
  }
}
using System;

namespace todo.Models
{
  public class Todo
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Done { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
  }
}
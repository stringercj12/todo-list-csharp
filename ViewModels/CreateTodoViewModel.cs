using System.ComponentModel.DataAnnotations;

namespace todo.ViewModels{
  public class CreateTodoViewModel{
    [Required]
    public string Title { get; set; }
  }
}
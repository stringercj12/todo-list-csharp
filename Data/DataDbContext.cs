using Microsoft.EntityFrameworkCore;
using todo.Models;

namespace todo.Data
{
  public class DataDbContext : DbContext
  {
    public DbSet<Todo> Todos { get; set; }
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
  }
}
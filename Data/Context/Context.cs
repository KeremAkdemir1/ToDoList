using Microsoft.EntityFrameworkCore;
using PersonalToDoList.Data.Entities;

namespace PersonalToDoList.Data.Context
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<List> Lists { get; set; }
    }
}

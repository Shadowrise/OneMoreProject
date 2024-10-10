using Microsoft.EntityFrameworkCore;

namespace omp.Auth.DB;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Slava", Email = "slava@mail.com" },
            new User { Id = 2, Name = "Ira", Email = "ira@mail.com" },
            new User { Id = 3, Name = "Mark", Email = "mark@mail.com" }
        );
    }
}
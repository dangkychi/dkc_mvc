using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DuHoc.Data;
using System;
using System.Linq;

namespace DuHoc.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DuHocContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DuHocContext>>()))
            {
                if (context.User.Any())
                {
                    return;   // DB has been seeded
                }
                var user = new User
                {
                    user_name = "Lakaka",
                    password = "password",
                    user_role = "normal"
                };

                context.SaveChanges();
            }
        }
    }
}
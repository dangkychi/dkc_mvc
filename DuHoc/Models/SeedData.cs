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
                    user_role = 1
                };

                /*var profile = new Profile
                {
                    full_name = "R.Lakaka",
                    birthdate = DateTime.Parse("1989-2-12"),
                    phone_number = "07548383",
                    email = "lakaka.com",
                    address = "Romantic Comedy",
                    User = user // Set the User navigation property
                };

                // Add the user and profile to the context
                context.User.Add(user);
                context.Profile.Add(profile);
                /*
                // Look for any country.
                if (context.Country.Any())
                {
                    return;   // DB has been seeded
                }

                context.Country.AddRange(
                    new Country
                    {
                        Name = "Australia",
                        Continent = "Asia",
                        Images = "D:\\forproject\\DAPM\\LyThuyet\\TUVANDUHOC\\DuHoc\\DuHoc\\wwwroot\\images\\Aus_flag.png",
                    },

                    new Country
                    {
                        Name = "New Zealand",
                        Continent = "Oceania",
                        Images = "D:\\forproject\\DAPM\\LyThuyet\\TUVANDUHOC\\DuHoc\\DuHoc\\wwwroot\\images\\NewZ_flag.pngy",
                    },

                    new Country
                    {
                        Name = "USA",
                        Continent = "Americas",
                        Images = "D:\\forproject\\DAPM\\LyThuyet\\TUVANDUHOC\\DuHoc\\DuHoc\\wwwroot\\images\\USA_flag.png",
                    },

                    new Country
                    {
                        Name = "Canada",
                        Continent = "Americas",
                        Images = "D:\\forproject\\DAPM\\LyThuyet\\TUVANDUHOC\\DuHoc\\DuHoc\\wwwroot\\images\\Can_flag.png",
                    },

                     new Country
                     {
                         Name = "UK",
                         Continent = "Europe",
                         Images = "D:\\forproject\\DAPM\\LyThuyet\\TUVANDUHOC\\DuHoc\\DuHoc\\wwwroot\\images\\UK_flag.png",
                     }
                );*/

                context.SaveChanges();
            }
        }
    }
}
using LocalServicePlatform.Application.ApplicationConstants;
using LocalServicePlatform.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LocalServicePlatform.Infrastructure.Common
{
    public class SeedData
        
    {
        public static async Task SeedRole (IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new List<IdentityRole>
        {
            new IdentityRole{ Name = CustomRole.MasterAdmin, NormalizedName = CustomRole.MasterAdmin },
            new IdentityRole { Name = CustomRole.Tasker, NormalizedName = CustomRole.Tasker },
            new IdentityRole { Name = CustomRole.Customer, NormalizedName = CustomRole.Customer }
        };
                foreach (var role in roles)
                {
                  if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }
            }
        }
        public static async Task SeedDataAsync(ApplicationDbContext _dbContext) 
        {
            if (!_dbContext.ServiceCategories.Any())
            {
                await _dbContext.ServiceCategories.AddRangeAsync(
                    new ServiceCategories
                    {
                        Name = "Refrigerator Repair"
                        
                    },
                      new ServiceCategories
                      {
                          Name = "Tv Mounting"
                      },
                        new ServiceCategories
                        {
                            Name = "Plumbing Help"
                        },
                          new ServiceCategories
                          {
                              Name = "AC repairs"
                          },
                            new ServiceCategories
                            {
                                Name = "Wiring Help"
                            },
                              new ServiceCategories
                              {
                                  Name = "Book shelf Assembly"
                              });


                await _dbContext.SaveChangesAsync();
            }
                   
        if (!_dbContext.Services.Any())
            {
                await _dbContext.Services.AddRangeAsync(
                    new Services
                    {
                        Name = "Electrical Help",
                        Description="Repairing Fridge,washing machine,AC repair,other electrical appliances etc"
                    },
                      new Services
                      {
                          Name = "Mounting",
                          Description=" "
                      },
                        new Services
                        {
                            Name = "Home Repairs",
                            Description = "j"
                        },
                      new Services
                      {
                          Name = "Painting",
                          Description = "ghj"
                      },
                        new Services
                        {
                            Name = "Assembly",
                            Description = "ghj"
                        },
                      new Services
                      {
                          Name = "Trending",
                          Description = "ghj"
                      });


                              await _dbContext.SaveChangesAsync();
                     

               
            }


           

        }



        }
        

        }

        
    

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public static class SeedData
    {
        public static void Seed(UserManager<Employee> userMan, RoleManager<IdentityRole> roleMan)
        {
            SeedRoles(roleMan);
            SeedUsers(userMan);
        }
        private static void SeedUsers(UserManager<Employee> userMan)
        {

           
            if(userMan.FindByNameAsync("admin@localhost.com").Result ==null)
            {
                var user = new Employee
                {
                    UserName = "admin@localhost.com",
                    Email = "admin@localhost.com"
                };
               var result= userMan.CreateAsync(user, "Becky_2207").Result;

                if (result.Succeeded)
                {
                    userMan.AddToRoleAsync(user, "Administrator").Wait();

                }
            }

        }

        private static void SeedRoles(RoleManager<IdentityRole> roleMan)
        {
            if(!roleMan.RoleExistsAsync("Administrator").Result)
            {
                var result = roleMan.CreateAsync(new IdentityRole { Name = "Administrator" }).Result;
            }

            if (!roleMan.RoleExistsAsync("Employee").Result)
            {
                var result =  roleMan.CreateAsync(new IdentityRole { Name = "Employee" }).Result;
            }

        }
    }
}

using ApiExamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExamples.Data
{
    public static class SeedData
    {
        public static void Initialize(ApiExamplesContext context)
        {
            if (!context.Users.Any())
            {
                var user = new User();

                var userProfile = new OrganizationalProfile();
                userProfile.Department = "Co Lab";
                userProfile.Name = "Allan";
                userProfile.Surname = "Pead";

                user.Password = "^&%&^%^&%";
                user.UserName = "apead";
                user.Profile = userProfile;


                context.Users.Add(user);

                context.SaveChanges();
            }
        }
    }
}

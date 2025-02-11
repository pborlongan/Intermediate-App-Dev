﻿using Demo.BLL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web;
using WebApp.Models;
using static WebApp.Admin.Security.Settings;

namespace WebApp.Admin.Security
{
    public class SecurityDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            // To "seed" a database is to provide it with some initial data
            // when the database is created.

            #region Seed the security roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            // The RoleManager<T> and RoleStore<T> are BLL classes that give flexibility
            // to the design/structure of how we're using Asp.Net Identity.
            // The IdentityRole is an Entity class that represents a security role.

            // Hard-coded security roles (move later on)
            foreach (var role in DefaultSecurityRoles)
                roleManager.Create(new IdentityRole { Name = role });
            #endregion

            #region Seed the users
            //Create user
            var adminUser = new ApplicationUser
            {
                UserName = AdminUserName,
                Email = AdminEmail,
                EmailConfirmed = true
            };

            // Get the BLL user manager
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            // - The ApplicationUserManager is a BLL class in the IdentityConfig.cs file
            var result = userManager.Create(adminUser, AdminPassword);

            if(result.Succeeded)
            {
                // Get the Id that was generated for the user we created/added
                var found = userManager.FindByName(AdminUserName).Id;
                // Add the user to the Administrators role
                userManager.AddToRole(found, AdminRole);
            }

            // Create the other user accounts for all the poeple in my Demo database
            var demoManager = new DemoController();
            var people = demoManager.ListImportantPeople();
            foreach(var person in people)
            {
                var user = new ApplicationUser
                {
                    UserName = $"{person.FirstName}.{person.LastName}",
                    Email = $"{person.FirstName}.{person.LastName}@DemoIsland.com",
                    EmailConfirmed = true,
                    PersonId = person.PersonID
                };

                result = userManager.Create(user, TempPassword);
                if(result.Succeeded)
                {
                    var userId = userManager.FindByName(AdminUserName).Id;
                    userManager.AddToRole(userId, UserRole);
                }
            }
            #endregion


            // Keep the call to the base class to do its seeding work
            base.Seed(context);
        }
    }
}
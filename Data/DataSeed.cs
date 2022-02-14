using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VSApi.Models;

namespace VSApi.Data
{
    public class DataSeed : ControllerBase
    {
        private readonly ApiContext _ctx;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataSeed(
            ApiContext ctx, 
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> CreateDefaultUsers()
        {
            // setup the default role names
            string role_RegisteredUser = "RegisteredUser";
            string role_Administrator = "Administrator";

            // create the default roles (if they doesn't exist yet)
            if (await _roleManager.FindByNameAsync(role_RegisteredUser) == null)
                await _roleManager.CreateAsync(new IdentityRole(role_RegisteredUser));

            if (await _roleManager.FindByNameAsync(role_Administrator) == null)
                await _roleManager.CreateAsync(new IdentityRole(role_Administrator));

            // create a list to track the newly added users
            var addedUserList = new List<ApplicationUser>();

            // check if the admin user already exist
            var email_Admin = "admin@email.com";
            if (await _userManager.FindByNameAsync(email_Admin) == null)
            {
                // create a new admin ApplicationUser account
                var user_Admin = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_Admin,
                    Email = email_Admin,
                };

                // insert the admin user into the DB
                await _userManager.CreateAsync(user_Admin, "MySecr3t$");

                // assign the "RegisteredUser" and "Administrator" roles
                await _userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
                await _userManager.AddToRoleAsync(user_Admin, role_Administrator);

                // confirm the e-mail and remove lockout
                user_Admin.EmailConfirmed = true;
                user_Admin.LockoutEnabled = false;

                // add the admin user to the added users list
                addedUserList.Add(user_Admin);
            }

            // check if the standard user already exist
            var email_User = "user@email.com";
            if (await _userManager.FindByNameAsync(email_User) == null)
            {
                // create a new standard ApplicationUser account
                var user_User = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_User,
                    Email = email_User
                };

                // insert the standard user into the DB
                await _userManager.CreateAsync(user_User, "MySecr3t$");

                // assign the "RegisteredUser" role
                await _userManager.AddToRoleAsync(user_User, role_RegisteredUser);

                // confirm the e-mail and remove lockout
                user_User.EmailConfirmed = true;
                user_User.LockoutEnabled = false;

                // add the standard user to the added users list
                addedUserList.Add(user_User);
            }

            // if we added at least one user, persist the changes into the DB
            if (addedUserList.Count > 0)
                await _ctx.SaveChangesAsync();

            return new JsonResult(new
            {
                Count = addedUserList.Count,
                Users = addedUserList
            });
        }

        // public void SeedData()
        // {
        //     if (!_ctx.Cryptos.Any())
        //     {
        //         SeedCryptos();
        //         _ctx.SaveChanges();
        //     }
        // }

        // private void SeedCryptos()
        // {
        //     List<Crypto> cryptos = BuildCryptoList();
        //
        //     foreach(var crypto in cryptos)
        //     {
        //         _ctx.Cryptos.Add(crypto);
        //     }
        // }
        //
        // private List<Crypto> BuildCryptoList()
        // {
        //     return new List<Crypto>()
        //     {
        //         new Crypto {
        //             idCrypto = 8000,
        //             Name = "Bitcoin",
        //             Symbol = "BTC",
        //             Rank = 1001,
        //             Price = "4000",
        //             Change24h = "30",
        //             Change7d = "4",
        //         },
        //         new Crypto {
        //             idCrypto = 8001,
        //             Name = "Ethereum",
        //             Symbol = "ETH",
        //             Rank = 1002,
        //             Price = "200",
        //             Change24h = "5",
        //             Change7d = "10",
        //         },
        //         new Crypto {
        //             idCrypto = 8002,
        //             Name = "Litecoin",
        //             Symbol = "LTC",
        //             Rank = 1003,
        //             Price = "40",
        //             Change24h = "-2",
        //             Change7d = "-10",
        //         }
        //
        //     };
        // }
    }
}
namespace VanillaArtStore.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Data.Models;
    using VanillaArtStore.Models.Users;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Details()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var user = new UserDetailsFormModel
            {
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                UserName = currentUser.UserName,
                Adress = currentUser.Adress,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber
            };

            return this.View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Details(UserDetailsFormModel form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (form.FirstName != currentUser.FirstName)
            {
                currentUser.FirstName = form.FirstName;
                return View(form);
            }

            return BadRequest();
        }
    }
}

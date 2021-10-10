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
                Address = currentUser.Address,
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

            if (form.FirstName != null && form.FirstName != currentUser.FirstName)
            {
                currentUser.FirstName = form.FirstName;
            }

            if (form.LastName != null &&  form.LastName != currentUser.LastName)
            {
                currentUser.LastName = form.LastName;
            }

            if (form.UserName != null && form.UserName != currentUser.UserName)
            {
                currentUser.UserName = form.UserName;
            }

            if (form.Email != null && form.Email != currentUser.Email)
            {
                currentUser.Email = form.Email;
            }

            if (form.PhoneNumber != null && form.PhoneNumber != currentUser.PhoneNumber)
            {
                currentUser.PhoneNumber = form.PhoneNumber;
            }

            if (form.Address != null && form.Address != currentUser.Address)
            {
                currentUser.Address = form.Address;
            }

            if (form.NewPassword == form.ConfirmPassword && form.NewPassword != null)
            {
                var passwordChanged = await this.userManager.ChangePasswordAsync(currentUser, form.Password, form.NewPassword);

                if (!passwordChanged.Succeeded)
                {
                    var errors = passwordChanged.Errors.Select(e => e.Description);

                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }

                    return View(errors);
                }
            }
            var result = await this.userManager.UpdateAsync(currentUser);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View(errors);
            }

            return RedirectToAction("Details", "Users"); 
        }
    }
}

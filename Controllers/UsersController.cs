namespace VanillaArtStore.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Data;
    using VanillaArtStore.Data.Models;
    using VanillaArtStore.Models.Users;
    using VanillaArtStore.Services.Messages;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly VanillaArtDbContext data;
        private readonly IMessageService messages; 

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, VanillaArtDbContext data, IMessageService messages)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.data = data;
            this.messages = messages;
        }

        public async Task<IActionResult> All()
        {
            var allUsersAsQuery =  this.userManager.Users.ToList();

            var allUsers = new List<UserDetailsQueryModel>();

            foreach (var user in allUsersAsQuery)
            {
                Address userAddress = this.data.Addresses.Where(a => a.UserId == user.Id).FirstOrDefault();
                user.Address = userAddress;
                if (user.Address == null)
                {
                    user.Address = new Address
                    {
                        AddressLine = "No Address",
                        Country = "No Country",
                        ZipCode = 0,
                        Town = "No Town",
                        UserId = user.Id,
                    };
                }

                var currentUserMessages = this.messages.GetAllUserMessages(user.Id);

                var currentUser = new UserDetailsQueryModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Address = user.Address.AddressLine,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    HasDiscount = user.HasDiscount,
                    Messages = currentUserMessages
                };

                allUsers.Add(currentUser);
            }

            return this.View(allUsers);
        }

        public async Task<IActionResult> Details()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            Address userAddress = this.data.Addresses.Where(a => a.UserId == currentUser.Id).FirstOrDefault();
            currentUser.Address = userAddress;
            
            var user = new UserDetailsFormModel
            {
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                UserName = currentUser.UserName,
                Address = userAddress,
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


            currentUser.Address = this.data.Addresses.Where(a => a.UserId == currentUser.Id).FirstOrDefault();

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

            if (form.Address.Country != null && form.Address.Country != currentUser.Address.Country)
            {
                currentUser.Address.Country = form.Address.Country;
            }

            if (form.Address.Town != null && form.Address.Town != currentUser.Address.Town)
            {
                currentUser.Address.Town = form.Address.Town;
            }

            if (form.Address.ZipCode != currentUser.Address.ZipCode)
            {
                currentUser.Address.ZipCode = form.Address.ZipCode;
            }
            if (form.Address.AddressLine != null && form.Address.AddressLine != currentUser.Address.AddressLine)
            {
                currentUser.Address.AddressLine = form.Address.AddressLine;
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

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (email == currentUser.Email)
            {
                currentUser.HasDiscount = true;

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

                return RedirectToAction("Index", "Home");
            };

            return RedirectToAction("Index", "Home");
        }
    }
}

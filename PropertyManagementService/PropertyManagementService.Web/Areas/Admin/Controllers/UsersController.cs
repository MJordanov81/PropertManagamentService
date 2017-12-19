namespace PropertyManagementService.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services.Contracts;
    using PropertyManagementService.Services.Models.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService users, UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        public IActionResult Index(string search, List<string> roles, int page = 1)
        {
            if (search == null)
            {
                search = string.Empty;
            }

            UsersAdminPaginatedModel users = this.users.GetUsers(search, roles);

            users.Search = search;
            users.Page = page;
            users.ItemsPerPage = 10;
           
            users.Users = users.Users.Skip(users.ItemsPerPage * (page - 1)).Take(users.ItemsPerPage).ToList();

            return this.View(users);
        }

        public IActionResult Edit(string id)
        {
            UserModifyDataModel user = this.users.GetUserToEdit(id);

            if (user == null)
            {
                return this.NotFound();
            }

            this.ViewData["Roles"] = this.users.GetRolesNames();

            return this.View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserModifyDataModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            if (model.Roles == null || model.Roles.Count == 0)
            {
                return this.View(model);
            }

            User user = this.users.GetUser(model.Id);

            if (user == null)
            {
                return this.NotFound();
            }

            ICollection<string> userNewRoles = model.Roles;
            IList<string> userCurrentRoles = await this.userManager.GetRolesAsync(user);

            if (!userNewRoles.Contains("Admin") && this.userManager.GetUserId(this.User) == user.Id)
            {
                this.ModelState.AddModelError("Roles", "Cannot remove 'Admin' role from current user.");

                this.ViewData["Roles"] = this.users.GetRolesNames();

                return this.View(model);
            }

            this.users.EditUser(model.Id, userNewRoles);

            await this.UpdateUserRoles(user, userNewRoles, userCurrentRoles);

            return this.Redirect("/admin/users/index");

        }

        private async Task UpdateUserRoles(User user, ICollection<string> userNewRoles, IList<string> userCurrentRoles)
        {

            IList<string> roles = new List<string>();

            foreach (var role in userCurrentRoles)
            {
                if (!userNewRoles.Contains(role))
                {
                    roles.Add(role);
                }
            }

            await this.userManager.RemoveFromRolesAsync(user, roles);

            roles.Clear();

            foreach (var role in userNewRoles)
            {
                if (!userCurrentRoles.Contains(role))
                {
                    roles.Add(role);
                }
            }

            await this.userManager.AddToRolesAsync(user, roles);
        }
    }
}
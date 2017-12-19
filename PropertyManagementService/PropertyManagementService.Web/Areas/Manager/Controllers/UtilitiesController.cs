namespace PropertyManagementService.Web.Areas.Manager.Controllers
{
    using Infrastructure.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services.Contracts;
    using PropertyManagementService.Services.Models.Utility;
    using PropertyManagementService.Web.Areas.Manager.Models.Utilities;
    using System;
    using System.Linq;

    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class UtilitiesController : Controller
    {
        private readonly IUtilityService utilities;

        private readonly UserManager<User> userManager;

        public UtilitiesController(IUtilityService utilities, UserManager<User> userManager)
        {
            this.utilities = utilities;
            this.userManager = userManager;
        }

        public IActionResult Index(int id, string search, int page = 1)
        {
            if (search == null)
            {
                search = string.Empty;
            }

            UtilitiesPaginatedModel utilities = this.utilities.GetUtilitiesForBuilding(id, search);

            utilities.Search = search;
            utilities.Page = page;
            utilities.ItemsPerPage = Constants.ItemsPerPageUtilities;

            utilities.Utilities = utilities.Utilities.Skip(utilities.ItemsPerPage * (page - 1)).Take(utilities.ItemsPerPage).ToList();

            return this.View(utilities);
        }

        public IActionResult Create(int id)
        {
            UtilityCreateViewModel model = new UtilityCreateViewModel()
            {
                BuildingId = id
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UtilityCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            string currentUserId = this.userManager.GetUserId(this.User);

            try
            {
                this.utilities.Create(currentUserId, model.Name, model.Description, model.Price, model.IsSubscribable, model.Routine, model.IsPerResident, model.BuildingId);
            }

            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);

                return this.View(model);
            }

            return this.Redirect("/manager/utilities/index?id=" + model.BuildingId);
        }
    }
}
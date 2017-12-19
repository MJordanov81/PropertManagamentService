namespace PropertyManagementService.Web.Areas.Manager.Controllers
{
    using Infrastructure.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services.Contracts;
    using PropertyManagementService.Services.Models.Building;
    using System.Linq;

    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class BuildingsController : Controller
    {
        private readonly IUserService users;

        private readonly IBuildingService buildings;

        private readonly UserManager<User> userManager;

        public BuildingsController(IUserService users, IBuildingService buildings, UserManager<User> userManager)
        {
            this.users = users;
            this.buildings = buildings;
            this.userManager = userManager;
        }

        public IActionResult Index(string search, int page = 1)
        {
            if (search == null)
            {
                search = string.Empty;
            }

            string managerId = this.userManager.GetUserId(this.User);

            BuildingsPaginatedModel<BuildingManagerListModel> buildings = this.buildings.GetBuildings<BuildingManagerListModel>(search, managerId);

            buildings.Search = search;
            buildings.Page = page;
            buildings.ItemsPerPage = Constants.ItemsPerPageBuildings;

            buildings.Buildings = buildings.Buildings.Skip(buildings.ItemsPerPage * (page - 1)).Take(buildings.ItemsPerPage).ToList();

            return this.View(buildings);
        }

        public IActionResult Details(int id)
        {
            BuildingManagerDetailsModel building = this.buildings.GetBuildingDetails(id);

            if (building == null)
            {
                return this.NotFound();
            }

            return this.View(building);
        }
    }
}
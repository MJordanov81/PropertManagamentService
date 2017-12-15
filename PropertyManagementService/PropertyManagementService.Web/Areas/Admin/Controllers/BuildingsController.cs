namespace PropertyManagementService.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PropertyManagementService.Services.Contracts;
    using PropertyManagementService.Services.Models.Building;
    using PropertyManagementService.Web.Areas.Admin.Models.Buildings;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class BuildingsController : Controller
    {
        private readonly IUserService users;

        private readonly IBuildingService buildings;

        public BuildingsController(IUserService users, IBuildingService buildings)
        {
            this.users = users;
            this.buildings = buildings;
        }

        public IActionResult Index(string search, int page = 1)
        {
            if (search == null)
            {
                search = string.Empty;
            }

            BuildingsAdminPaginatedModel buildings = this.buildings.GetBuildings(search);

            buildings.Search = search;
            buildings.Page = page;
            buildings.ItemsPerPage = 5;

            buildings.Buildings = buildings.Buildings.Skip(buildings.ItemsPerPage * (page - 1)).Take(buildings.ItemsPerPage).ToList();

            return this.View(buildings);
        } 

        public IActionResult Create()
        {
            this.ViewData["Managers"] = this.users.GetUsersEmailsList("Manager");

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BuildingCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.buildings.Create(model.Contract, model.Address, model.ServiceStartDate, model.ServiceEndDate, model.ManagerId);

            return RedirectToAction(nameof(BuildingsController.Index));
        }

        public IActionResult Edit(int id)
        {
            BuildingModifyDataModel building = this.buildings.GetBuildingToEdit(id);

            if (building == null)
            {
                return this.NotFound();
            }

            this.ViewData["Managers"] = this.users.GetUsersEmailsList("Manager");

            return this.View(building);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BuildingModifyDataModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.buildings.EditBuilding(id, model.Contract, model.Address, model.ServiceStartDate, model.ServiceEndDate, model.ManagerId);

            return this.RedirectToAction(nameof(BuildingsController.Index));
        }
    }
}
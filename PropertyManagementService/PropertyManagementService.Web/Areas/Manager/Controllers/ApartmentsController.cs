namespace PropertyManagementService.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Apartments;
    using PropertyManagementService.Services.Contracts;
    using System;

    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class ApartmentsController : Controller
    {
        private readonly IUserService users;

        private readonly IApartmentService apartments;

        public ApartmentsController(IUserService users, IApartmentService apartments)
        {
            this.users = users;
            this.apartments = apartments;
        }

        public IActionResult Create(int buildingId)
        {
            ApartmentCreateViewModel model = new ApartmentCreateViewModel()
            {
                BuildingId = buildingId,
                Owners = this.users.GetOwners()
        };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApartmentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Owners = this.users.GetOwners();

                return this.View(model);
            }

            try
            {
                this.apartments.Create(model.Number, model.Residents, model.Dogs, model.Area, model.OwnerId, model.BuildingId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("All", e.Message);

                model.Owners = this.users.GetOwners();

                return this.View(model);
            }
         
            return this.Redirect("/manager/buildings/details/" + model.BuildingId);
        }
    }
}
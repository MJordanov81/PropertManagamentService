namespace PropertyManagementService.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PropertyManagementService.Services.Contracts;
    using PropertyManagementService.Services.Models.Bill;
    using System;

    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class BillsController : Controller
    {
        private readonly IBillService bills;

        public BillsController(IBillService bills)
        {
            this.bills = bills;
        }

        public IActionResult Index(int id)
        {
            BillsForBuildingModel bills = this.bills.GetBillsForBuilding(id, true);

            return View(bills);
        }

        public IActionResult Generate(int id)
        {
            return this.View(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Generate(int buildingId, int period, int year)
        {
            try
            {
                this.bills.GenerateBills(buildingId, period, year);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("All", e.Message);

                return this.View(buildingId);
            }


            return this.View(buildingId);
        }

        public void DeleteMultiple(int [] bills)
        {
            
        }

        public void ConfirmMultiple(int [] bills)
        {
            
        }

        public IActionResult UpdateGeneratedBills(int id)
        {
            return ViewComponent("GeneratedBills", new { id = id });
        }
    }
}
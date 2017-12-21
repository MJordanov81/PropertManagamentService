namespace PropertyManagementService.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services.Contracts;
    using PropertyManagementService.Services.Models.Bill;
    using System;
    using System.Threading.Tasks;
    using PropertyManagementService.Web.Infrastructure.Constants;
    using System.Linq;

    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class BillsController : Controller
    {
        private readonly IBillService bills;

        private readonly UserManager<User> userManager;

        public BillsController(IBillService bills, UserManager<User> userManager)
        {
            this.bills = bills;
            this.userManager = userManager;
        }

        public IActionResult Index(int id, string search, int page = 1)
        {
            if (search == null)
            {
                search = string.Empty;
            }

            BillsForBuildingModel bills = this.bills.GetBillsForBuilding(search, id, true);

            bills.Search = search;
            bills.Page = page;
            bills.ItemsPerPage = Constants.ItemsPerPageBills;

            bills.Bills = bills.Bills.Skip(bills.ItemsPerPage * (page - 1)).Take(bills.ItemsPerPage).ToList();

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
            string currentUserId = this.userManager.GetUserId(this.User);

            try
            {
                this.bills.GenerateBills(currentUserId, buildingId, period, year);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("All", e.Message);

                return this.View(buildingId);
            }


            return this.View(buildingId);
        }

        public async Task<int> DeleteMultiple(int [] bills)
        {
            string currentUserId = this.userManager.GetUserId(this.User);

            return await this.bills.DeleteMultiple(currentUserId, bills);
        }

        public async Task<int> ConfirmMultiple(int [] bills)
        {
            string currentUserId = this.userManager.GetUserId(this.User);

            return await this.bills.ConfirmMultiple(currentUserId, bills);
        }

        public IActionResult UpdateGeneratedBills(int id)
        {
            return ViewComponent("GeneratedBills", new { id = id });
        }
    }
}
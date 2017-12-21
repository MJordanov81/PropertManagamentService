namespace PropertyManagementService.Web.Areas.Manager.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using PropertyManagementService.Services.Contracts;
    using PropertyManagementService.Services.Models.Bill;
    using System;

    public class GeneratedBillsViewComponent : ViewComponent
    {
        private readonly IBillService bills;

        public GeneratedBillsViewComponent(IBillService bills)
        {
            this.bills = bills;
        }

        public IViewComponentResult Invoke(int id)
        {
            BillsForBuildingModel bills = this.bills.GetBillsForBuilding(string.Empty, id, false);

            return View(bills);
        }
    }
}

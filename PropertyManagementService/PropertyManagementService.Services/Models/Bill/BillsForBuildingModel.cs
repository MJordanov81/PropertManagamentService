namespace PropertyManagementService.Services.Models.Bill
{
    using AutoMapper;
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;
    using System.Collections.Generic;
    using System.Linq;

    public class BillsForBuildingModel : IMapFrom<Building>, IHaveCustomMapping
    {
        public int BuildingId { get; set; }

        public int UnpaidBills { get; set; }

        public decimal UnpaidBillsAmount { get; set; }

        public ICollection<BillManagerListModel> Bills { get; set; } = new List<BillManagerListModel>();

        //pagination properties
        public int ItemsPerPage { get; set; }

        public int ItemsCount { get; set; }

        public int Page { get; set; }

        public string Search { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<Building, BillsForBuildingModel>()
                .ForMember(m => m.UnpaidBillsAmount, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Bills.Where(bill => !bill.IsPaid && bill.IsConfirmed).Sum(bill => bill.TotalAmount))))
                .ForMember(m => m.UnpaidBills, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Bills.Where(bill => !bill.IsPaid && bill.IsConfirmed).Count())))

            .ForMember(m => m.BuildingId, cfg => cfg.MapFrom(b => b.Id));
        }
    }
}

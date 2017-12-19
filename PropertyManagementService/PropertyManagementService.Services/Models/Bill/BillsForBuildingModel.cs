namespace PropertyManagementService.Services.Models.Bill
{
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;
    using System.Collections.Generic;
    using AutoMapper;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    public class BillsForBuildingModel : IMapFrom<Building>, IHaveCustomMapping
    {
        public int BuildingId { get; set; }

        public int UnpaidBills { get; set; }

        public decimal UnpaidBillsAmount { get; set; }

        public ICollection<BillManagerListModel> Bills { get; set; } = new List<BillManagerListModel>();

        public void Configure(Profile profile)
        {
            profile.CreateMap<Building, BillsForBuildingModel>()
            .ForMember(m => m.UnpaidBillsAmount, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Bills.Where(bill => !bill.IsPaid && bill.IsConfirmed).Sum(bill => bill.TotalAmount))))
            .ForMember(m => m.UnpaidBills, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Bills.Where(bill => !bill.IsPaid && bill.IsConfirmed).Count())))
            
            .ForMember(m => m.BuildingId, cfg => cfg.MapFrom(b => b.Id));
        }
    }
}

namespace PropertyManagementService.Services.Models.Building
{
    using AutoMapper;
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services.Models.Apartment;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BuildingManagerDetailsModel : IMapFrom<Building>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Contract { get; set; }

        public string Address { get; set; }

        public DateTime ServiceStartDate { get; set; }

        public DateTime? ServiceEndDate { get; set; }

        public int ApartmentsCount { get; set; }

        public int ResidentsCount { get; set; }

        public int DogsCount { get; set; }

        public int ApartmentsTotalArea { get; set; }

        public int UnpaidBills { get; set; }

        public decimal UnpaidBillsAmount { get; set; }

        public int UtilitiesCount { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<ApartmentListModel> Apartments { get; set; } = new List<ApartmentListModel>();

        public void Configure(Profile profile)
        {
            profile.CreateMap<Building, BuildingManagerDetailsModel>()
                .ForMember(m => m.ApartmentsCount, cfg => cfg.MapFrom(b => b.Apartments.Count))
                .ForMember(m => m.ApartmentsTotalArea, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Area)))
                .ForMember(m => m.ResidentsCount, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Residents)))
                .ForMember(m => m.DogsCount, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Dogs)))
                .ForMember(m => m.UnpaidBillsAmount, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Bills.Where(bill => !bill.IsPaid && bill.IsConfirmed).Sum(bill => bill.TotalAmount))))
                .ForMember(m => m.UnpaidBills, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Bills.Where(bill => !bill.IsPaid && bill.IsConfirmed).Count())))
                .ForMember(m => m.UtilitiesCount, cfg => cfg.MapFrom(b => b.Utilities.Count));
        }
    }
}

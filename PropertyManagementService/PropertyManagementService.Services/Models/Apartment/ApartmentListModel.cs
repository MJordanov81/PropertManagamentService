namespace PropertyManagementService.Services.Models.Apartment
{
    using AutoMapper;
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;
    using System.Linq;

    public class ApartmentListModel : IMapFrom<Apartment>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int Residents { get; set; }

        public int Dogs { get; set; }

        public int Area { get; set; }

        public string OwnerName { get; set; }

        public int UnsubscribedUtilities { get; set; }

        public int UnpaidBills { get; set; }

        public decimal UnpaidBillsAmount { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<Apartment, ApartmentListModel>()
                .ForMember(m => m.OwnerName, cfg => cfg.MapFrom(a => a.Owner.Name))
                .ForMember(m => m.UnsubscribedUtilities, cfg => cfg.MapFrom(a => a.UnsubscribedUtilities.Count))
                .ForMember(m => m.UnpaidBillsAmount, cfg => cfg.MapFrom(a => a.Bills.Where(b => !b.IsPaid && b.IsConfirmed).Sum(b => b.TotalAmount)))
                .ForMember(m => m.UnpaidBills, cfg => cfg.MapFrom(a => a.Bills.Where(b => !b.IsPaid && b.IsConfirmed).Count()));
        }
    }
}

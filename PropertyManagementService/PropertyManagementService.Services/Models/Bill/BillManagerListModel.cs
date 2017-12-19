namespace PropertyManagementService.Services.Models.Bill
{
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;
    using System;
    using AutoMapper;

    public class BillManagerListModel : IMapFrom<Bill>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public int? Period { get; set; }

        public int Year { get; set; }

        public decimal TotalAmount { get; set; }

        public string Apartment { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsConfirmed { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<Bill, BillManagerListModel>()
                .ForMember(m => m.Apartment, cfg => cfg.MapFrom(b => b.Apartment.Number));
        }
    }
}

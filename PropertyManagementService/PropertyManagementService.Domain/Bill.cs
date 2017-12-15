namespace PropertyManagementService.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Bill
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public int Period { get; set; }

        public DateTime DueDate { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; }

        public int ApartmentId { get; set; }

        public Apartment Apartment { get; set; }

        public ICollection<BillUtility> Utilities { get; set; } = new List<BillUtility>();
    }
}

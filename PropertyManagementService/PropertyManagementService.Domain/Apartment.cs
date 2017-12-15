namespace PropertyManagementService.Domain
{
    using System.Collections.Generic;

    public class Apartment
    {
        public int Id { get; set; }

        public int Residents { get; set; }

        public int Dogs { get; set; }

        public int Area { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public int BuildingId { get; set; }

        public Building Building { get; set; }

        public ICollection<UnsubscribedUtility> UnsubscribedUtilities { get; set; } = new List<UnsubscribedUtility>();

        public ICollection<Bill> Bills { get; set; } = new List<Bill>();
    }
}

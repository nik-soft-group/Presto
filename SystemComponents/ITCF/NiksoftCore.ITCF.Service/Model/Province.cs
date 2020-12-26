using System.Collections.Generic;

namespace NiksoftCore.ITCF.Service
{
    public class Province
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Business> Businesses { get; set; }
        public virtual ICollection<IndustrialPark> IndustrialParks { get; set; }
    }
}

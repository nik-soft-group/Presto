using System.Collections.Generic;

namespace NiksoftCore.ITCF.Service
{
    public class IndustrialPark
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Business> Businesses { get; set; }
    }
}

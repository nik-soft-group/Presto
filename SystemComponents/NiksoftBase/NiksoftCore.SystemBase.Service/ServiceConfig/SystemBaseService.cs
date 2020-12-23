using Microsoft.Extensions.Configuration;

namespace NiksoftCore.SystemBase.Service
{
    public interface ISystemBaseService
    {
        ISystemSettingService iSystemSettingServ { get; set; }
        IPortalLanguageService iPortalLanguageServ { get; set; }
        IPanelMenuService iPanelMenuService { get; set; }
        IUserProfileService iUserProfileServ { get; set; }
        ICountryService iCountryServ { get; set; }
        IProvinceService iProvinceServ { get; set; }
        ICityService iCityServ { get; set; }
    }

    public class SystemBaseService : ISystemBaseService
    {
        public ISystemSettingService iSystemSettingServ { get; set; }
        public IPortalLanguageService iPortalLanguageServ { get; set; }
        public IPanelMenuService iPanelMenuService { get; set; }
        public IUserProfileService iUserProfileServ { get; set; }
        public ICountryService iCountryServ { get; set; }
        public IProvinceService iProvinceServ { get; set; }
        public ICityService iCityServ { get; set; }

        private IConfiguration Config { get; }

        public SystemBaseService(IConfiguration Configuration)
        {
            ISystemUnitOfWork uow = new SystemBaseDbContext(Configuration.GetConnectionString("SystemBase"));
            iSystemSettingServ = new SystemSettingService(uow);
            iPortalLanguageServ = new PortalLanguageService(uow);
            iPanelMenuService = new PanelMenuService(uow);
            iUserProfileServ = new UserProfileService(uow);
            iCountryServ = new CountryService(uow);
            iProvinceServ = new ProvinceService(uow);
            iCityServ = new CityService(uow);
        }


    }
}

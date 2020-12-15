using Microsoft.Extensions.Configuration;

namespace NiksoftCore.SystemBase.Service
{
    public class SystemBaseService: ISystemBaseService
    {
        public ISystemSettingService iSystemSettingServ { get; set; }
        private IConfiguration Config { get; }

        public SystemBaseService(IConfiguration Configuration)
        {
            ISystemUnitOfWork uow = new SystemBaseDbContext(Configuration.GetConnectionString("SupplierSystem"));
            iSystemSettingServ = new SystemSettingService(uow);
        }


    }
}

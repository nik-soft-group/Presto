using Microsoft.Extensions.Configuration;

namespace NiksoftCore.ITCF.Service
{
    public interface IITCFService
    {
        IUserLegalFormService IUserLegalFormServ { get; set; }
        IBusinessService IBusinessServ { get; set; }
        IBusinessCategoryService IBusinessCategoryServ { get; set; }
        IIndustrialParkService IIndustrialParkServ { get; set; }
    }

    public class ITCFService : IITCFService
    {
        public IUserLegalFormService IUserLegalFormServ { get; set; }
        public IBusinessService IBusinessServ { get; set; }
        public IBusinessCategoryService IBusinessCategoryServ { get; set; }
        public IIndustrialParkService IIndustrialParkServ { get; set; }

        private IConfiguration Config { get; }

        public ITCFService(IConfiguration Configuration)
        {
            IITCFUnitOfWork uow = new ITCFDbContext(Configuration.GetConnectionString("SystemBase"));
            IUserLegalFormServ = new UserLegalFormService(uow);
            IBusinessServ = new BusinessService(uow);
            IBusinessCategoryServ = new BusinessCategoryService(uow);
            IIndustrialParkServ = new IndustrialParkService(uow);

        }


    }
}

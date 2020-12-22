using Microsoft.Extensions.Configuration;

namespace NiksoftCore.ITCF.Service
{
    public interface IITCFService
    {
        IUserLegalFormService IUserLegalFormServ { get; set; }
    }

    public class ITCFService : IITCFService
    {
        public IUserLegalFormService IUserLegalFormServ { get; set; }

        private IConfiguration Config { get; }

        public ITCFService(IConfiguration Configuration)
        {
            IITCFUnitOfWork uow = new ITCFDbContext(Configuration.GetConnectionString("SystemBase"));
            IUserLegalFormServ = new UserLegalFormService(uow);

        }


    }
}

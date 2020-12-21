using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.SystemBase.Service;
using NiksoftCore.ViewModel.SystemBase;
using System.Collections.Generic;

namespace NiksoftCore.MiddlController.Middles
{
    public class NikController : Controller
    {
        public IConfiguration Config { get; }
        public ISystemBaseService ISystemBaseServ { get; set; }

        public List<NikMessage> Messages;
        public PortalLanguage defaultLang;

        public NikController(IConfiguration Configuration)
        {
            Config = Configuration;
            Messages = new List<NikMessage>();
            ISystemBaseServ = new SystemBaseService(Configuration);
            defaultLang = ISystemBaseServ.iPortalLanguageServ.Find(x => x.IsDefault);
        }

        public void AddError(string message)
        {
            Messages.Add(new NikMessage { 
                Message = message,
                Type = MessageType.Error
            });
        }

        public void AddSuccess(string message)
        {
            Messages.Add(new NikMessage
            {
                Message = message,
                Type = MessageType.Success
            });
        }


    }
}

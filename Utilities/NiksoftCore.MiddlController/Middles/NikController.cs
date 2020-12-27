using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.SystemBase.Service;
using NiksoftCore.ViewModel;
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
            ISystemBaseServ = new SystemBaseService(Configuration.GetConnectionString("SystemBase"));
            defaultLang = ISystemBaseServ.iPortalLanguageServ.Find(x => x.IsDefault);
        }

        public void AddError(string message, string lang)
        {
            Messages.Add(new NikMessage { 
                Message = message,
                Language = lang,
                Type = MessageType.Error
            });
        }

        public void AddSuccess(string message, string lang)
        {
            Messages.Add(new NikMessage
            {
                Message = message,
                Language = lang,
                Type = MessageType.Success
            });
        }

        public string GetViewName(string queryLang, string baseName)
        {
            if (string.IsNullOrEmpty(queryLang))
            {
                queryLang = defaultLang.ShortName;
            }

            if (queryLang.ToLower() == "en")
            {
                return baseName;
            }

            return defaultLang.ShortName + baseName;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.ITCF.Service
{
    public static class Tools
    {
        public static string GetStatusName(BusinessStatus status, string lang)
        {
            switch (status)
            {
                case BusinessStatus.RegisterRequest:
                    if (lang == "en")
                    {
                        return "Waiting for confirme";
                    }
                    else
                    {
                        return "در انتظار تایید";
                    }
                    
                case BusinessStatus.RegisterConfirme:
                    if (lang == "en")
                    {
                        return "Confirmed";
                    }
                    else
                    {
                        return "تایید ثبت";
                    }
                case BusinessStatus.EditRequest:
                    if (lang == "en")
                    {
                        return "Waiting for edit request Confirme";
                    }
                    else
                    {
                        return "در انتظار تایید خواست ویرایش";
                    }
                case BusinessStatus.EditConfirme:
                    if (lang == "en")
                    {
                        return "Edit request Confirmed";
                    }
                    else
                    {
                        return "تایید ویرایش";
                    }
                default:
                    if (lang == "en")
                    {
                        return "Unknown";
                    }
                    else
                    {
                        return "نامشخص";
                    }
            }
        }
    }
}

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
                        return "در انتظار تایید درخواست ویرایش";
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

        public static string GetUnitKey(int unit)
        {
            switch (unit)
            {
                case 1:
                    return "unit1";
                case 2:
                    return "unit2";
                case 3:
                    return "unit3";
                case 4:
                    return "unit4";
                case 5:
                    return "unit5";
                default:
                    return "";
            }
        }

        public static string GetUnitTitle(int unit)
        {
            switch (unit)
            {
                case 1:
                    return "تصویر و متن جایگاه اول";
                case 2:
                    return "تصاویر و متن های جایگاه دوم";
                case 3:
                    return "محتوای جایگاه سوم";
                case 4:
                    return "unit4";
                case 5:
                    return "unit5";
                default:
                    return "";
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.MiddlController.Routing
{
    public class NikRoute : RouteValueAttribute
    {
        public NikRoute(string routeKey, string routeValue) : base(routeKey, routeValue)
        {

        }

    }
}

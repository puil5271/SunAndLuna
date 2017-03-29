using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Owin;

namespace SunAndLuna
{
    public class Startup : Hub
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
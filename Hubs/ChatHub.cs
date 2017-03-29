using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SunAndLuna.Hubs
{
    [HubName("Chat")]

    public class ChatHub : Hub
    {
        //  <summary>
        //  C   ->  S
        //  from Client to Server, Sending msg
        //  </summary>
        //  <param name = "msg"></param>

        public void ClientToServer(string msg)
        {
            //  S   -> Cs
            Clients.All.serverToClient(msg);
        }

    }
}
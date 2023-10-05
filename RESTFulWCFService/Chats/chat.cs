using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Transports;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Timers;
using RESTFulWCFService;
using System.Collections.Concurrent;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for chat
/// </summary>
    [HubName("chatHub")]
public class Chat : Hub
    {
        public Task JoinGroup()
        {            
            return Groups.Add(Context.ConnectionId, "foo");            
        }

        public override Task OnConnected()
        {
            var newUsers = OnlineUser.userObj.Where(item => item.newStatus == true).Select(item => item.userId).ToList();
            var newUsers_name = OnlineUser.userObj.Where(item => item.newStatus == true).Select(item => item.userName).ToList();  
            UserModal user = OnlineUser.userObj.Where(item => item.sessionId == HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value.ToString()).SingleOrDefault();
            user.connectionId = Context.ConnectionId;
            return Clients.All.joined(Context.ConnectionId, newUsers, newUsers_name);
        }

        public void Send(string message, string groupName,string from_id,string from_name)
        {
          //  Business.BusinessMPartener objbusinessmpartener = new Business.BusinessMPartener();
            if (Clients != null)
            {
                Clients.Group(groupName).addMessage(message,groupName,from_id,from_name);
                //SqlHelper.ExecuteNonQuery(Config.Crebas, "p_i_messages", Convert.ToInt32(from_id), Convert.ToInt32(0), message);
              //  int i = objbusinessmpartener.insert_messages(Convert.ToInt32(from_id), Convert.ToInt32(groupName), message);
            }
        }
        public void signal_typing(string sign, string groupName, string from_id, string from_name)
        {
            if (Clients != null)
            {
                Clients.Group(groupName).addsign(sign, groupName, from_id, from_name);
            }
        }

        public void GetAllOnlineStatus()
        {
            Clients.Caller.OnlineStatus(Context.ConnectionId, OnlineUser.userObj.Select(item => item.userId).ToList(),
                OnlineUser.userObj.Select(item => item.userName).ToList());
                
        }

        public void CreateGroup(string currentUserId, string toConnectTo)
        {
            string strGroupName = GetUniqueGroupName(currentUserId, toConnectTo);
            string connectionId_To = OnlineUser.userObj.Where(item => item.userId == toConnectTo).Select(item=>item.connectionId).SingleOrDefault();
            if (!string.IsNullOrEmpty(connectionId_To))
            {
                Groups.Add(Context.ConnectionId, strGroupName);
                Groups.Add(connectionId_To, strGroupName);
                Clients.Caller.setChatWindow(strGroupName, toConnectTo,currentUserId);
            }
        }

        private string GetUniqueGroupName(string currentUserId, string toConnectTo)
        {
            return (currentUserId.GetHashCode() ^ toConnectTo.GetHashCode()).ToString();
        }
        static ConcurrentDictionary<string, string> dic = new ConcurrentDictionary<string, string>();

        public void Notify(string id, string name)
        {
            dic.TryAdd(name, id);
        }
    }	 


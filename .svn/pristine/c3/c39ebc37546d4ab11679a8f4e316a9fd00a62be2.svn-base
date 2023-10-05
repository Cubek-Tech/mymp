using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RESTFulWCFService
{
    public static class ChatManager
    {
        private const int TIMEOUT = 60; // Timeout in seconds for user to appear offline

        public static List<Message> Messages = new List<Message>();
        public static List<TypingSignal> TypingSignals = new List<TypingSignal>();
        public static Dictionary<int, DateTime> LastSeen = new Dictionary<int, DateTime>();
        public static List<int> OfflineUsers = new List<int>();

        public static bool IsUserOnline(int userId)
        {
            if (OfflineUsers.Contains(userId))
            {
                return false;
            }

            if (LastSeen.ContainsKey(userId))
            {
                return DateTime.Now.Subtract(LastSeen[userId]).TotalSeconds < TIMEOUT;
            }
            return false;
        }
    }

    [DataContract]
    public class Update
    {
        [DataMember]
        public List<TypingSignal> TypingSignals { get; set; }
        [DataMember]
        public List<Message> Messages { get; set; }
    }

    [DataContract]
    public class TypingSignal
    {
        [DataMember]
        public int To { get; set; }
        [DataMember]
        public int From { get; set; }
    }
    
    [DataContract]
    public class Message
    {
        [DataMember]
        public int To { get; set; }
        [DataMember]
        public int From { get; set; }
        [DataMember]
        public string MessageText { get; set; }
    }
}
using ChatCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamPartyCommand.Events
{
    public class JoinChannelEventArgs : EventArgs
    {
        public IChatService ChatService { get; private set; }
        public IChatChannel ChatChannel { get; private set; }
        public JoinChannelEventArgs(IChatService service, IChatChannel chatChannel)
        {
            this.ChatService = service;
            this.ChatChannel = chatChannel;
        }
    }
}

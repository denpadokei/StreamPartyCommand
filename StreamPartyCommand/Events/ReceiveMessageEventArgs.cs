using ChatCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamPartyCommand.Events
{
    public class ReceiveMessageEventArgs : EventArgs
    {
        public IChatService ChatService { get; private set; }
        public IChatMessage ChatMessage { get; private set; }

        public ReceiveMessageEventArgs(IChatService service, IChatMessage chatMessage)
        {
            this.ChatService = service;
            this.ChatMessage = chatMessage;
        }
    }
}

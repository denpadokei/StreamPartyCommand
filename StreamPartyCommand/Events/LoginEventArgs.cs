using ChatCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamPartyCommand.Events
{
    public class LoginEventArgs
    {
        public IChatService ChatService { get; private set; }

        public LoginEventArgs(IChatService service)
        {
            this.ChatService = service;
        }
    }
}

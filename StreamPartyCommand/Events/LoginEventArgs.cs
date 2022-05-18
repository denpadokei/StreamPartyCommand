using CatCore.Services.Multiplexer;

namespace StreamPartyCommand.Events
{
    public class LoginEventArgs
    {
        public MultiplexedPlatformService ChatService { get; private set; }

        public LoginEventArgs(MultiplexedPlatformService service)
        {
            this.ChatService = service;
        }
    }
}

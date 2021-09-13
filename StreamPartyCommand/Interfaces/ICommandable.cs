using ChatCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamPartyCommand.Interfaces
{
    //!bomb
    //!setnotecolor left color right color
    //!setsabercolor left color right color
    //!setlightcolor left color right color
    //!setwallcolor color

    public interface ICommandable
    {
        void Execute(IChatService service, IChatMessage message);
    }
}

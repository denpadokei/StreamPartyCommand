using ChatCore.Interfaces;
using StreamPartyCommand.Interfaces;
using StreamPartyCommand.Staics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StreamPartyCommand.CommandControllers
{
    public class NoteColorController : MonoBehaviour, ICommandable
    {
        public string Key => CommandKey.NOTE_COLOR;

        public void Execute(IChatService service, IChatMessage message) => throw new NotImplementedException();
    }
}

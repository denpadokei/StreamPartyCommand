using ChatCore.Interfaces;
using StreamPartyCommand.Interfaces;
using StreamPartyCommand.Staics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.CommandControllers
{
    public class NoteColorController : MonoBehaviour, ICommandable
    {
        public string Key => CommandKey.NOTE_COLOR;

        public void Execute(IChatService service, IChatMessage message)
        {

        }

        [Inject]
        public void Constractor(ColorScheme scheme)
        {
            this._noteAColor = scheme.saberAColor;
            this._noteBColor = scheme.saberBColor;
        }

        private Color _noteAColor;
		private Color _noteBColor;
	}
}

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
    public class SaberColorController : MonoBehaviour, ICommandable
    {
        public string Key => CommandKey.SABER_COLOR;

        public void Execute(IChatService service, IChatMessage message)
        {

        }
        [Inject]
        public void Constractor(ColorScheme scheme)
        {
            this._saberAColor = scheme.saberAColor;
            this._saberBColor = scheme.saberBColor;
        }
        private Color _saberAColor;
		private Color _saberBColor;
	}
}

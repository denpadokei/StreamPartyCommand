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
    public class LightColorController : MonoBehaviour, ICommandable
    {
        public string Key => CommandKey.LIGHT_COLOR;

        public void Execute(IChatService service, IChatMessage message)
        {

        }

        [Inject]
        public void Constractor(ColorScheme scheme)
        {
            this._environmentColor0 = scheme.environmentColor0;
            this._environmentColor1 = scheme.environmentColor1;
        }

		private Color _environmentColor0;
		private Color _environmentColor1;
	}
}

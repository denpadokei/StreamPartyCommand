using ChatCore.Interfaces;
using StreamPartyCommand.HarmonyPathches;
using StreamPartyCommand.Interfaces;
using StreamPartyCommand.Staics;
using StreamPartyCommand.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.CommandControllers
{
    public class WallColorController : MonoBehaviour, ICommandable
    {
        public string Key => CommandKey.WALL_COLOR;

        public void Execute(IChatService service, IChatMessage message)
        {
            var messageArray = message.Message.Split(' ');
            if (messageArray.Length != 2) {
                return;
            }
            if (ColorUtil.Colors.TryGetValue(messageArray[1], out var color)) {
                StretchableObstaclePatch.WallColor = color;
            }
        }
        [Inject]
        public void Constractor(ColorManager manager)
        {
            StretchableObstaclePatch.WallColor = manager.obstaclesColor;
        }
        
	}
}

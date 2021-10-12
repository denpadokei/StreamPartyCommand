using ChatCore.Interfaces;
using StreamPartyCommand.Configuration;
using StreamPartyCommand.HarmonyPathches;
using StreamPartyCommand.Interfaces;
using StreamPartyCommand.Models;
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

        private void Start()
        {
            StretchableObstaclePatch.Enable = !this._util.IsChroma;
        }
        public void Execute(IChatService service, IChatMessage message)
        {
            if (PluginConfig.Instance.IsWallColorEnable != true) {
                return;
            }
            var messageArray = message.Message.Split(' ');
            if (messageArray.Length != 2) {
                return;
            }
            if (ColorUtil.Colors.TryGetValue(messageArray[1], out var color)) {
                StretchableObstaclePatch.WallColor = color;
            }
        }
        private BeatmapUtil _util;
        [Inject]
        public void Constractor(ColorManager manager, BeatmapUtil util)
        {
            this._util = util;
            StretchableObstaclePatch.WallColor = manager.obstaclesColor;
        }
	}
}

using ChatCore.Interfaces;
using IPA.Loader;
using StreamPartyCommand.Configuration;
using StreamPartyCommand.HarmonyPathches;
using StreamPartyCommand.Interfaces;
using StreamPartyCommand.Models;
using StreamPartyCommand.Staics;
using StreamPartyCommand.Utilities;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.CommandControllers
{
    public class WallColorController : MonoBehaviour, ICommandable
    {
        public bool IsInstallTwitchFX { get; set; }
        public string Key => CommandKey.WALL_COLOR;

        private void Start()
        {
            StretchableObstaclePatch.Enable = !this._util.IsNoodle && !this._util.IsChroma;
        }

        public void Execute(IChatService service, IChatMessage message)
        {
            if (PluginConfig.Instance.IsWallColorEnable != true) {
                return;
            }
            if (this.IsInstallTwitchFX) {
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
            this.IsInstallTwitchFX = PluginManager.GetPluginFromId("TwitchFX") != null;
            this._util = util;
            StretchableObstaclePatch.WallColor = manager.obstaclesColor;
        }
    }
}

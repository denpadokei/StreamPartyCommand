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

            if (ColorUtil.IsRainbow(messageArray[1])) {
                this._rainbow = true;
            }
            if (ColorUtil.Colors.TryGetValue(messageArray[1], out var color)) {
                StretchableObstaclePatch.WallColor = color;
                this._rainbow = false;
            }
        }

        private void Update()
        {
            if (this._rainbow) {
                this._rainbowUtil.SetWallRainbowColor(this._rainbowUtil.WallRainbowColor());
            }
        }

        private BeatmapUtil _util;
        private RainbowUtil _rainbowUtil;
        private bool _rainbow = false;
        [Inject]
        public void Constractor(ColorManager manager, BeatmapUtil util, RainbowUtil rainbowUtil)
        {
            this.IsInstallTwitchFX = PluginManager.GetPluginFromId("TwitchFX") != null;
            this._util = util;
            this._rainbowUtil = rainbowUtil;
            this._rainbow = false;
            StretchableObstaclePatch.WallColor = manager.obstaclesColor;
        }
    }
}

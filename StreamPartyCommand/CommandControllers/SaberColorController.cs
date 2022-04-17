using ChatCore.Interfaces;
using IPA.Loader;
using SiraUtil.Sabers;
using StreamPartyCommand.Configuration;
using StreamPartyCommand.Interfaces;
using StreamPartyCommand.Models;
using StreamPartyCommand.Staics;
using StreamPartyCommand.Utilities;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.CommandControllers
{
    public class SaberColorController : MonoBehaviour, ICommandable
    {
        public bool IsInstallTwitchFX { get; set; }
        public string Key => CommandKey.SABER_COLOR;
        private SaberModelManager _saberModelManager;

        private void Start()
        {
            this.enable = !this._util.IsNoodle && !this._util.IsChroma;
        }
        public void Execute(IChatService service, IChatMessage message)
        {
            if (!this.enable) {
                return;
            }
            if (PluginConfig.Instance.IsSaberColorEnable != true) {
                return;
            }
            if (this.IsInstallTwitchFX) {
                return;
            }
            var prams = message.Message.Split(' ');
            if (prams.Length != 3) {
                return;
            }
            var leftColor = prams[1];
            var rightColor = prams[2];
            if (ColorUtil.Colors.TryGetValue(leftColor, out var color0)) {
                this._saberModelManager.SetColor(this._saberManager.leftSaber, color0);
            }
            if (ColorUtil.Colors.TryGetValue(rightColor, out var color1)) {
                this._saberModelManager.SetColor(this._saberManager.rightSaber, color1);
            }
        }
        private BeatmapUtil _util;
        [Inject]
        public void Constractor(SaberManager saberManager, BeatmapUtil util, SaberModelManager modelManager)
        {
            this.IsInstallTwitchFX = PluginManager.GetPluginFromId("TwitchFX") != null;
            this._util = util;
            this._saberManager = saberManager;
            this._saberModelManager = modelManager;
        }
        private bool enable;
        private SaberManager _saberManager;
    }
}

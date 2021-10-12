using ChatCore.Interfaces;
using SiraUtil;
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
    public class SaberColorController : MonoBehaviour, ICommandable
    {
        public string Key => CommandKey.SABER_COLOR;
        private void Start()
        {
            this.enable = !this._util.IsNoodle && !this._util.IsChroma;
        }
        public void Execute(IChatService service, IChatMessage message)
        {
            if (!enable) {
                return;
            }
            if (PluginConfig.Instance.IsSaberColorEnable != true) {
                return;
            }
            var prams = message.Message.Split(' ');
            if (prams.Length != 3) {
                return;
            }
            var leftColor = prams[1];
            var rightColor = prams[2];
            if (ColorUtil.Colors.TryGetValue(leftColor, out var color0)) {
                this._saberManager.leftSaber.ChangeColor(color0);
            }
            if (ColorUtil.Colors.TryGetValue(rightColor, out var color1)) {
                this._saberManager.rightSaber.ChangeColor(color1);
            }
        }
        private BeatmapUtil _util;
        [Inject]
        public void Constractor(ColorScheme scheme, SaberManager saberManager, BeatmapUtil util)
        {
            this._util = util;
            this._saberManager = saberManager;
        }
        private bool enable;
        private SaberManager _saberManager;
    }
}

using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StreamPartyCommand.HarmonyPathches
{
    [HarmonyPatch(typeof(LightSwitchEventEffect), nameof(LightSwitchEventEffect.GetNormalColor), new Type[] { typeof(int), typeof(bool) })]
    public class GetNormalColorPatch
    {
        public static void Postfix(ref int beatmapEventValue, ref bool colorBoost, ref Color __result, LightSwitchEventEffect __instance)
        {
            if (!__instance.IsColor0(beatmapEventValue)) {
                __result = LeftColor;
            }
            else {
                __result = RightColor;
            }
        }
        internal static Color RightColor { get; set; }
        internal static Color LeftColor { get; set; }
    }
}

using HarmonyLib;
using System;
using UnityEngine;

namespace StreamPartyCommand.HarmonyPathches
{
    [HarmonyPatch(typeof(LightSwitchEventEffect), nameof(LightSwitchEventEffect.GetNormalColor), new Type[] { typeof(int), typeof(bool) })]
    public class GetNormalColorPatch
    {
        public static void Postfix(ref int beatmapEventValue, ref bool colorBoost, ref Color __result, LightSwitchEventEffect __instance)
        {
            if (!Enable) {
                return;
            }
            if (!__instance.IsColor0(beatmapEventValue)) {
                __result = LeftColor;
            }
            else {
                __result = RightColor;
            }
        }
        internal static bool Enable { get; set; }
        internal static Color RightColor { get; set; }
        internal static Color LeftColor { get; set; }
    }
}

using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StreamPartyCommand.HarmonyPathches
{
    [HarmonyPatch(typeof(ColorManager), nameof(ColorManager.ColorForType), new Type[] { typeof(ColorType) })]
    public class ColorManagerColorForTypePatch
    {
        public static bool Prefix(ref ColorType type, ref Color __result)
        {
            switch (type) {
                case ColorType.ColorA:
                    __result = LeftColor;
                    break;
                case ColorType.ColorB:
                    __result = RightColor;
                    break;
                case ColorType.None:
                    break;
                default:
                    __result = Color.black;
                    break;
            }
            return false;
        }
        internal static Color LeftColor { get; set; }
        internal static Color RightColor { get; set; }
    }
}

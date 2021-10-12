using HarmonyLib;
using StreamPartyCommand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StreamPartyCommand.HarmonyPathches
{
    [HarmonyPatch(typeof(GameNoteController), "Awake")]
    public class GameNoteControllerPatch
    {
        public static void Postfix(GameNoteController __instance)
        {
            __instance.gameObject.AddComponent<DummyBomb>();
        }
    }
}

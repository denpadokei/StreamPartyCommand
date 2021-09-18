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
            Logger.Debug("GameNoteController.Awake call");
            var dummybomb = __instance.gameObject.AddComponent<DummyBomb>();
            //dummybomb.DummyBombMesh = BombMesh;
        }
    }
}

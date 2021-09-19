using HarmonyLib;
using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StreamPartyCommand.HarmonyPathches
{
    [HarmonyPatch(typeof(BombNoteController), "Awake")]
    public class BombNoteControllerPatch
    {
        public static MeshRenderer BombMesh { get; internal set; }
        public static void Postfix(BombNoteController __instance)
        {
            if (!BombMesh) {
                BombMesh = GameObject.Instantiate(__instance.GetComponentInChildren<MeshRenderer>());
                BombMesh.transform.SetParent(null);
                BombMesh.enabled = true;
            }
        }
    }
}

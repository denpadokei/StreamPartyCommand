using HarmonyLib;
using StreamPartyCommand.Models;

namespace StreamPartyCommand.HarmonyPathches
{
    [HarmonyPatch(typeof(GameNoteController), "Awake")]
    public class GameNoteControllerPatch
    {
        public static void Postfix(GameNoteController __instance) => __instance.gameObject.AddComponent<DummyBomb>();
    }
}

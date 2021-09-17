using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamPartyCommand.HarmonyPathches
{
    [HarmonyPatch(typeof(BeatmapObjectManager), nameof(BeatmapObjectManager.SpawnBasicNote), new Type[] { typeof(NoteData), typeof(BeatmapObjectSpawnMovementData.NoteSpawnData), typeof(float), typeof(float) })]
    public class SpawnBasicNotePatch
    {
        public static void Postfix(ref NoteData noteData, ref BeatmapObjectSpawnMovementData.NoteSpawnData noteSpawnData, ref float rotation, ref float cutDirectionAngleOffset, ref NoteController __result)
        {
            Logger.Debug("SpawnBasicNote call");
            OnSpownBasicNote?.Invoke(noteData, noteSpawnData, rotation, cutDirectionAngleOffset);
        }
        public static event Action<NoteData, BeatmapObjectSpawnMovementData.NoteSpawnData, float, float> OnSpownBasicNote;
    }
}

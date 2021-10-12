using IPA.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace StreamPartyCommand.Models
{
    public class BeatmapUtil : IInitializable
    {
        public IDifficultyBeatmap Currentmap { get; private set; }
        public bool IsNoodle { get; private set; }
        public bool IsChroma { get; private set; }

        [Inject]
        public BeatmapUtil(IDifficultyBeatmap level)
        {
            Currentmap = level;
        }

        public static bool IsNoodleMap(IDifficultyBeatmap level)
        {
            // thanks kinsi
            if (PluginManager.EnabledPlugins.Any(x => x.Name == "NoodleExtensions")) {
                bool isIsNoodleMap = SongCore.Collections.RetrieveDifficultyData(level)?
                    .additionalDifficultyData?
                    ._requirements?.Any(x => x == "Noodle Extensions") == true;
                return isIsNoodleMap;
            }
            else {
                return false;
            }
        }
        public static bool IsChromaMap(IDifficultyBeatmap level)
        {
            
            if (PluginManager.EnabledPlugins.Any(x => x.Name == "Chroma")) {
                bool isIsNoodleMap = SongCore.Collections.RetrieveDifficultyData(level)?
                    .additionalDifficultyData?
                    ._requirements?.Any(x => x == "Chroma") == true;
                isIsNoodleMap = isIsNoodleMap || SongCore.Collections.RetrieveDifficultyData(level)?
                    .additionalDifficultyData?
                    ._suggestions?.Any(x => x == "Chroma") == true;
                return isIsNoodleMap;
            }
            else {
                return false;
            }   
        }

        public void Initialize()
        {
            IsNoodle = IsNoodleMap(Currentmap);
            IsChroma = IsChromaMap(Currentmap);
            Plugin.Log.Debug($"Noodle?:{this.IsNoodle}");
            Plugin.Log.Debug($"Chroma?:{this.IsChroma}");
        }
    }
}

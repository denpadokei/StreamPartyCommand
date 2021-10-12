using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StreamPartyCommand.Models
{
    public class ParticleAssetLoader : PersistentSingleton<ParticleAssetLoader>
    {
        private static readonly string FontAssetPath = Path.Combine(Environment.CurrentDirectory, "UserData", "SPCParticleAssets");

        private static Material _default;

        private static Material Default
        {
            get
            {
                return _default ?? (_default = Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(x => x.name == "FireworkExplosion"));
            }
        }

        public bool IsInitialized { get; private set; } = false;

        private ParticleSystem _particle = null;

        public ParticleSystem Particle
        {
            get => this._particle;
            private set => this._particle = value;
        }

        public void SerchShader()
        {
            foreach (var shader in Resources.FindObjectsOfTypeAll<Shader>().OrderBy(x => x.name)) {
                Plugin.Log.Debug($"{shader}, {shader.name}");
            }
        }

        private void Awake() => HMMainThreadDispatcher.instance.Enqueue(this.LoadParticle());
        public IEnumerator LoadParticle()
        {
            Plugin.Log.Debug("1");
            this.IsInitialized = false;
            yield return new WaitWhile(() => Default == null);
            Plugin.Log.Debug("2");
            if (this.Particle != null) {
                Destroy(this.Particle);
            }
            if (!Directory.Exists(FontAssetPath)) {
                Directory.CreateDirectory(FontAssetPath);
            }
            Plugin.Log.Debug("3");
            AssetBundle bundle = null;
            foreach (var filename in Directory.EnumerateFiles(FontAssetPath, "*.particle", SearchOption.TopDirectoryOnly)) {
                using (var fs = File.OpenRead(filename)) {
                    bundle = AssetBundle.LoadFromStream(fs);
                }
                if (bundle != null) {
                    Plugin.Log.Debug("3.5");
                    break;
                }
            }
            Plugin.Log.Debug("4");
            if (bundle != null) {
                Plugin.Log.Debug("5");
                foreach (var bundleItem in bundle.GetAllAssetNames()) {
                    var asset = bundle.LoadAsset<GameObject>(Path.GetFileNameWithoutExtension(bundleItem));
                    Plugin.Log.Debug($"{asset}");
                    if (asset != null) {
                        this.Particle = asset.GetComponent<ParticleSystem>();
                        var renderer = Particle.GetComponent<ParticleSystemRenderer>();
                        renderer.material = Instantiate(Default);
                        bundle.Unload(false);
                        break;
                    }
                }
                Plugin.Log.Debug("6");
            }
            Plugin.Log.Debug("7");
            Plugin.Log.Debug($"{this.Particle}");
            this.IsInitialized = true;
        }
    }
}

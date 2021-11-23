using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;

namespace StreamPartyCommand.Models
{
    public class ParticleAssetLoader : PersistentSingleton<ParticleAssetLoader>
    {
        private static readonly string FontAssetPath = Path.Combine(Environment.CurrentDirectory, "UserData", "SPCParticleAssets");
        public bool IsInitialized { get; private set; } = false;

        private ParticleSystem _particle = null;
        public ParticleSystem Particle
        {
            get => this._particle;
            private set => this._particle = value;
        }
        private void Awake() => this.LoadParticle();
        protected override void OnDestroy()
        {
            if (this.Particle != null) {
                Destroy(this.Particle);
            }
        }

        public void LoadParticle()
        {
            this.IsInitialized = false;
            
            if (this.Particle != null) {
                Destroy(this.Particle);
            }
            if (!Directory.Exists(FontAssetPath)) {
                Directory.CreateDirectory(FontAssetPath);
            }
            AssetBundle bundle = null;
            foreach (var filename in Directory.EnumerateFiles(FontAssetPath, "*.particle", SearchOption.TopDirectoryOnly)) {
                using (var fs = File.OpenRead(filename)) {
                    bundle = AssetBundle.LoadFromStream(fs);
                }
                if (bundle != null) {
                    break;
                }
            }
            if (bundle != null) {
                foreach (var bundleItem in bundle.GetAllAssetNames()) {
                    var asset = bundle.LoadAsset<GameObject>(Path.GetFileNameWithoutExtension(bundleItem));
                    if (asset != null) {
                        this.Particle = asset.GetComponent<ParticleSystem>();
                        this.Particle.Stop();
                        bundle.Unload(false);
                        break;
                    }
                }
            }
            this.IsInitialized = true;
        }
    }
}

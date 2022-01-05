using System;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Models
{
    public class DummyBombExprosionEffect : MonoBehaviour
    {
        public void Awake()
        {
            if (this._particleSystem != null) {
                Destroy(this._particleSystem);
            }
            this._particleSystem = Instantiate(ParticleAssetLoader.instance.Particle);
            this._particleSystem.transform.SetParent(this.transform, false);
            this._particleSystem.Stop();
        }
        public void OnDestroy()
        {
            if (this._particleSystem != null) {
                Destroy(this._particleSystem);
                this._particleSystem = null;
            }
        }

        public virtual void SpawnExplosion(Vector3 pos)
        {
            this.transform.position = pos;
            try {
                this._particleSystem.Emit(50000);
            }
            catch (Exception e) {
                Plugin.Log.Error(e);
            }
        }
        private ParticleSystem _particleSystem;
    }
}

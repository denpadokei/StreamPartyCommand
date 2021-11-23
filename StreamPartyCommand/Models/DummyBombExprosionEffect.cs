using System;
using UnityEngine;

namespace StreamPartyCommand.Models
{
    public class DummyBombExprosionEffect : MonoBehaviour
    {
        public virtual void Awake()
        {
            this._emitParams = default;
            this._emitParams.applyShapeToPosition = true;
            if (this._particleSystem != null) {
                Destroy(this._particleSystem);
            }
            this._particleSystem = Instantiate(ParticleAssetLoader.instance.Particle);
            this._particleSystem.transform.SetParent(this.transform, false);
            this._particleSystem.Stop();
        }

        public virtual void OnDestroy()
        {
            if (this._particleSystem != null) {
                Destroy(this._particleSystem);
            }
        }

        public virtual void SpawnExplosion(Vector3 pos)
        {
            this._emitParams.position = pos;
            try {
                this._particleSystem.Emit(this._emitParams, 50000);
            }
            catch (Exception e) {
                Plugin.Log.Error(e);
            }
        }
        protected ParticleSystem.EmitParams _emitParams;
        private ParticleSystem _particleSystem;
    }
}

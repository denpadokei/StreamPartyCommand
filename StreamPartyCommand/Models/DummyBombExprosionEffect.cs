using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StreamPartyCommand.Models
{
    public class DummyBombExprosionEffect : MonoBehaviour
    {
		public virtual void Awake()
		{
			this._emitParams = default(ParticleSystem.EmitParams);
			this._emitParams.applyShapeToPosition = true;
		}
		private void OnDestroy()
        {
			Destroy(this._debrisPS.gameObject);
			Destroy(this._explosionPS.gameObject);
        }
		public virtual void SpawnExplosion(Vector3 pos)
		{
			this._emitParams.position = pos;
			this._debrisPS.Emit(this._emitParams, this._debrisCount);
			this._explosionPS.Emit(this._emitParams, this._explosionParticlesCount);
		}
		public void SetEffect(ParticleSystem debri, ParticleSystem explosion)
        {
			this._debrisPS = debri;
			this._explosionPS = explosion;
        }
		protected ParticleSystem _debrisPS;
		protected ParticleSystem _explosionPS;
		protected int _debrisCount = 40;
		protected int _explosionParticlesCount = 70;
		protected ParticleSystem.EmitParams _emitParams;
		protected ParticleSystem.EmitParams _explosionPSEmitParams;
	}
}

using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Models
{
    public class BombEffectSpowner : MonoBehaviour, IFlyingObjectEffectDidFinishEvent
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void Start()
        {
            this._beatmapObjectManager.noteWasCutEvent += this.OnNoteWasCutEvent;
            this._beatmapObjectManager.noteWasMissedEvent += this.OnNoteWasMissedEvent;
        }
        public void OnDestroy()
        {
            this._beatmapObjectManager.noteWasCutEvent -= this.OnNoteWasCutEvent;
            this._beatmapObjectManager.noteWasMissedEvent -= this.OnNoteWasMissedEvent;
        }

        public void HandleFlyingObjectEffectDidFinish(FlyingObjectEffect flyingObjectEffect)
        {
            flyingObjectEffect.didFinishEvent.Remove(this);
            this._flyingBombNameEffectPool.Free(flyingObjectEffect as FlyingBombNameEffect);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void OnNoteWasCutEvent(NoteController noteController, in NoteCutInfo noteCutInfo)
        {
            var dummyBomb = noteController.gameObject.GetComponent<DummyBomb>();
            if (dummyBomb == null) {
                return;
            }
            if (string.IsNullOrEmpty(dummyBomb.Text)) {
                return;
            }
            var bombEffect = this._dummyBombExprosionEffectPool.Alloc();
            bombEffect.SpawnExplosion(noteCutInfo.cutPoint);
            this._dummyBombExprosionEffectPool.Free(bombEffect);
            var effect = this._flyingBombNameEffectPool.Alloc();
            effect.transform.localPosition = noteCutInfo.cutPoint;
            effect.didFinishEvent.Add(this);
            var targetpos = noteController.worldRotation * (new Vector3(0, 1.7f, 10f));
            effect.InitAndPresent(dummyBomb.Text, 1f, targetpos, noteController.worldRotation, Color.white, 10, false);
            dummyBomb.Text = "";
        }
        private void OnNoteWasMissedEvent(NoteController noteController)
        {
            var dummyBomb = noteController.gameObject.GetComponent<DummyBomb>();
            if (dummyBomb == null) {
                return;
            }
            if (string.IsNullOrEmpty(dummyBomb.Text)) {
                return;
            }
            var effect = this._flyingBombNameEffectPool.Alloc();
            effect.transform.localPosition = Vector3.zero;
            effect.didFinishEvent.Add(this);
            var targetpos = noteController.worldRotation * (new Vector3(0, 1.7f, 10f));
            effect.InitAndPresent(dummyBomb.Text, 0.7f, targetpos, noteController.worldRotation, Color.red, 10, false);
            dummyBomb.Text = "";
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private BeatmapObjectManager _beatmapObjectManager;
        private ObjectMemoryPool<DummyBombExprosionEffect> _dummyBombExprosionEffectPool;
        private ObjectMemoryPool<FlyingBombNameEffect> _flyingBombNameEffectPool;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        [Inject]
        public void Constractor(BeatmapObjectManager manager, ObjectMemoryPool<FlyingBombNameEffect> nameEffect, ObjectMemoryPool<DummyBombExprosionEffect> bombEffect)
        {
            this._beatmapObjectManager = manager;
            this._dummyBombExprosionEffectPool = bombEffect;
            this._flyingBombNameEffectPool = nameEffect;
        }
        #endregion
    }
}
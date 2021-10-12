using IPA.Utilities;
using StreamPartyCommand.HarmonyPathches;
using StreamPartyCommand.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Models
{
    public class DummyBomb : MonoBehaviour, INoteControllerDidInitEvent
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        public GameNoteController Controller { get; private set; }
        public string Text { get; set; }
        public static ConcurrentQueue<string> Senders = new ConcurrentQueue<string>();
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // イベント
        public static event NoteWasCutEventHandler OnNoteWasCutEvent;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected void Awake()
        {
            if (CustomNoteUtil.TryGetGameNoteController(this.gameObject, out var component)) {
                this.Controller = component;
                this.Controller.didInitEvent.Add(this);
            }
            this._noteCube = this.gameObject.transform.Find("NoteCube");
            if (CustomNoteUtil.TryGetColorNoteVisuals(this.gameObject, out var visuals)) {
                this._colorManager = visuals.GetField<ColorManager, ColorNoteVisuals>("_colorManager");
            }
            this._noteMesh = GetComponentInChildren<MeshRenderer>();
        }
        protected void OnDestroy()
        {
            if (this.Controller != null) {
                this.Controller.didInitEvent.Remove(this);
            }
            if (this._bombMesh != null) {
                Destroy(this._bombMesh);
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void HandleNoteControllerDidInit(NoteControllerBase noteController)
        {
            if (this.Controller.gameNoteType == GameNoteController.GameNoteType.Ghost) {
                return;
            }
            // ここで置き換えるノーツの設定をする（ボムにするとかなんとか）
            // ふぁっきんかすたむのーつ
            if (CustomNoteUtil.IsInstallCustomNote && 1 <= CustomNoteUtil.SelectedNoteIndex) {
                if (Senders.TryDequeue(out var sender)) {
                    this.Text = sender;
                    if (this._bombMesh == null && BombNoteControllerPatch.BombMesh != null) {
                        _bombMesh = Instantiate(BombNoteControllerPatch.BombMesh);
                        _bombMesh.gameObject.transform.SetParent(_noteCube, false);
                    }
                    if (_bombMesh != null) {
                        _bombMesh.enabled = true;
                        var color = this._colorManager.ColorForType(noteController.noteData.colorType);
                        this._bombMesh.material.SetColor("_SimpleColor", color);
                    }
                }
                else {
                    this.Text = "";
                    if (_bombMesh != null) {
                        _bombMesh.enabled = false;
                    }
                }
            }
            else {
                if (this._bombMesh == null && BombNoteControllerPatch.BombMesh != null) {
                    _bombMesh = Instantiate(BombNoteControllerPatch.BombMesh);
                    _bombMesh.gameObject.transform.SetParent(_noteCube, false);
                }
                if (Senders.TryDequeue(out var sender)) {
                    this.Text = sender;
                    _noteMesh.forceRenderingOff = true;
                    if (_bombMesh != null) {
                        _bombMesh.enabled = true;
                    }
                }
                else {
                    _noteMesh.forceRenderingOff = false;
                    if (_bombMesh != null) {
                        _bombMesh.enabled = false;
                    }
                }
                var color = this._colorManager.ColorForType(noteController.noteData.colorType);
                if (_bombMesh != null) {
                    this._bombMesh.material.SetColor("_SimpleColor", color);
                }
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private Transform _noteCube;
        [DoesNotRequireDomainReloadInit]
        protected static readonly int _noteColorId = Shader.PropertyToID("_Color");
        protected static readonly int _bombColorId = Shader.PropertyToID("_SimpleColor");
        private MeshRenderer _bombMesh;
        private MeshRenderer _noteMesh;
        private ColorManager _colorManager;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
    }
    public delegate void NoteWasCutEventHandler(GameNoteController controller, in NoteCutInfo noteCutInfo);
}

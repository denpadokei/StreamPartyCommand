using IPA.Utilities;
using StreamPartyCommand.HarmonyPathches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Models
{
    public class DummyBombObjectSpowner : MonoBehaviour, INoteControllerNoteWasCutEvent, INoteControllerNoteWasMissedEvent, INoteControllerNoteDidDissolveEvent, INoteControllerNoteDidFinishJumpEvent
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
		public void OnDespown()
		{
			SpawnBasicNotePatch.OnSpownBasicNote -= this.SpawnBasicNotePatch_OnSpownBasicNote;
		}

		public NoteController SpownDummyBomb(NoteData noteData, BeatmapObjectSpawnMovementData.NoteSpawnData noteSpawnData, float rotation, string text)
		{
			var noteController = this.SpawnDummyBombNoteInternal(noteData, noteSpawnData, rotation, text);
			if (noteController != null) {
				this.SetNoteControllerEventCallbacks(noteController);
				noteController.ManualUpdate();
			}
			return noteController;
		}

		public void HandleNoteControllerNoteWasCut(NoteController noteController, in NoteCutInfo noteCutInfo)
		{
			this.Despawn(noteController);
		}
		public void HandleNoteControllerNoteWasMissed(NoteController noteController)
		{
			this.Despawn(noteController);
		}
		public void HandleNoteControllerNoteDidDissolve(NoteController noteController)
		{
			this.Despawn(noteController);
		}

		public void HandleNoteControllerNoteDidFinishJump(NoteController noteController)
		{
			this.Despawn(noteController);
		}
		#endregion
		//ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
		#region // プライベートメソッド
		private void SpawnBasicNotePatch_OnSpownBasicNote(NoteData arg1, BeatmapObjectSpawnMovementData.NoteSpawnData arg2, float arg3, float arg4)
		{
#if DEBUG
			this.SpownDummyBomb(arg1, arg2, arg3, "Test message");
#else
			if (this._chatCoreWrapper.RecieveChatMessage.TryDequeue(out var message)) {
				this.SpownDummyBomb(arg1, arg2, arg3, message.ChatMessage.Sender.DisplayName);
			}
#endif
		}
		protected NoteController SpawnDummyBombNoteInternal(NoteData bombNoteData, BeatmapObjectSpawnMovementData.NoteSpawnData noteSpawnData, float rotation, string text)
		{
			var bombNoteController = this._bombNotePoolContainer.Spawn();
			bombNoteController.Init(bombNoteData, rotation, noteSpawnData.moveStartPos, noteSpawnData.moveEndPos, noteSpawnData.jumpEndPos, noteSpawnData.moveDuration, noteSpawnData.jumpDuration, noteSpawnData.jumpGravity,
				//this._movement, this._noteTranseform, this._cuttableBySaber, this._wrapperGO,
				text);
			return bombNoteController;
		}

		private void SetNoteControllerEventCallbacks(NoteController noteController)
		{
			noteController.noteDidFinishJumpEvent.Add(this);
			noteController.noteWasCutEvent.Add(this);
			noteController.noteWasMissedEvent.Add(this);
			noteController.noteDidDissolveEvent.Add(this);
		}
		private void RemoveNoteControllerEventCallbacks(NoteController noteController)
		{
			noteController.noteDidFinishJumpEvent.Remove(this);
			noteController.noteWasCutEvent.Remove(this);
			noteController.noteWasMissedEvent.Remove(this);
			noteController.noteDidDissolveEvent.Remove(this);
		}

		private void Despawn(NoteController noteController)
		{
			this.RemoveNoteControllerEventCallbacks(noteController);
			this._bombNotePoolContainer.Despawn(noteController as DummyBomb);
		}
		#endregion
		//ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
		#region // メンバ変数
		private MemoryPoolContainer<DummyBomb> _bombNotePoolContainer;
		private ChatCoreWrapper _chatCoreWrapper;
		//private NoteMovement _movement;
		//private Transform _noteTranseform;
		//private CuttableBySaber _cuttableBySaber;
		//private GameObject _wrapperGO;
		#endregion
		//ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
		#region // 構築・破棄
		[Inject]
		public void Constractor(DummyBomb.Pool pool, ChatCoreWrapper wrapper)
		{
			this._bombNotePoolContainer = new MemoryPoolContainer<DummyBomb>(pool);
			this._chatCoreWrapper = wrapper;
			SpawnBasicNotePatch.OnSpownBasicNote += this.SpawnBasicNotePatch_OnSpownBasicNote;
			
		}
		#endregion
	}
}

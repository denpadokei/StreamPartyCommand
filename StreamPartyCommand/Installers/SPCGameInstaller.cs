using SiraUtil.Extras;
using SiraUtil.Objects.Beatmap;
using StreamPartyCommand.CommandControllers;
using StreamPartyCommand.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Installers
{
    public class SPCGameInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<StreamPartyCommandController>().FromNewComponentOnNewGameObject().AsCached().NonLazy();
            this.Container.BindMemoryPool<FlyingBombNameEffect, FlyingBombNameEffect.Pool>().WithInitialSize(10).FromComponentInNewPrefab(this._flyingBombNameEffect).AsCached();
            this.Container.BindInterfacesAndSelfTo<BombEffectSpowner>().FromNewComponentOnNewGameObject().AsCached().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BeatmapUtil>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<RainbowUtil>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BombMeshGetter>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BombCommandController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<WallColorController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<LightColorController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<NoteColorController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<SaberColorController>().FromNewComponentOnNewGameObject().AsSingle();

            this.Container.RegisterRedecorator(new BasicNoteRegistration(this.RedecoreteNoteController));
        }

        private GameNoteController RedecoreteNoteController(GameNoteController noteController)
        {
            noteController.gameObject.AddComponent<DummyBomb>();
            noteController.gameObject.AddComponent<DummyBombExprosionEffect>();
            return noteController;
        }

        private readonly FlyingBombNameEffect _flyingBombNameEffect;

        public SPCGameInstaller()
        {
            this._flyingBombNameEffect = new GameObject("FlyingBombNameEffect", typeof(FlyingBombNameEffect), typeof(TextMeshPro)).GetComponent<FlyingBombNameEffect>();
        }

        ~SPCGameInstaller()
        {
            if (this._flyingBombNameEffect != null) {
                GameObject.Destroy(this._flyingBombNameEffect.gameObject);
            }
        }
    }
}

using SiraUtil;
using StreamPartyCommand.CommandControllers;
using StreamPartyCommand.HarmonyPathches;
using StreamPartyCommand.Models;
using StreamPartyCommand.Utilities;
using Zenject;

namespace StreamPartyCommand.Installers
{
    public class SPCGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            if (BombNoteControllerPatch.BombMesh != null) {
                Destroy(BombNoteControllerPatch.BombMesh);
                BombNoteControllerPatch.BombMesh = null;
            }
            this.Container.BindInterfacesAndSelfTo<StreamPartyCommandController>().FromNewComponentOnNewGameObject(nameof(StreamPartyCommandController)).AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<DummyBombExprosionEffect>().FromNewComponentOnNewGameObject(nameof(DummyBombExprosionEffect)).AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BombEffectSpowner>().FromNewComponentOnNewGameObject(nameof(BombEffectSpowner)).AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BeatmapUtil>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<CustomNoteUtil>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BombCommandController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<WallColorController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<LightColorController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<NoteColorController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<SaberColorController>().FromNewComponentOnNewGameObject().AsSingle();
            //this.Container.BindMemoryPool<DummyBomb, DummyBomb.Pool>().WithInitialSize(16)
        }
    }
}

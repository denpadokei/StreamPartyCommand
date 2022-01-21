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
            this.Container.BindInterfacesAndSelfTo<StreamPartyCommandController>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            var memoryPool = new ObjectMemoryPool<DummyBombExprosionEffect>(4);
            this.Container.BindInterfacesAndSelfTo<ObjectMemoryPool<DummyBombExprosionEffect>>().FromInstance(memoryPool).AsCached();
            var namePool = new ObjectMemoryPool<FlyingBombNameEffect>(8);
            this.Container.BindInterfacesAndSelfTo<ObjectMemoryPool<FlyingBombNameEffect>>().FromInstance(namePool).AsCached();
            this.Container.BindInterfacesAndSelfTo<BombEffectSpowner>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BeatmapUtil>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BombMeshGetter>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BombCommandController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<WallColorController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<LightColorController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<NoteColorController>().FromNewComponentOnNewGameObject().AsSingle();
            this.Container.BindInterfacesAndSelfTo<SaberColorController>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}

using SiraUtil;
using StreamPartyCommand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Installers
{
    public class SPCGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<StreamPartyCommandController>().FromNewComponentOnNewGameObject(nameof(StreamPartyCommandController)).AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<DummyBombExprosionEffect>().FromNewComponentOnNewGameObject(nameof(DummyBombExprosionEffect)).AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BombEffectSpowner>().FromNewComponentOnNewGameObject(nameof(BombEffectSpowner)).AsSingle().NonLazy();
        }
    }
}

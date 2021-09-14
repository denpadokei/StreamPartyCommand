using SiraUtil;
using StreamPartyCommand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace StreamPartyCommand.Installers
{
    public class SPCGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<StreamPartyCommandController>().FromNewComponentOnNewGameObject(nameof(StreamPartyCommandController)).AsCached();
            this.Container.BindMemoryPool<DummyBomb.Pool>().WithFixedSize(64);
        }
    }
}

using SiraUtil;
using StreamPartyCommand.CommandControllers;
using StreamPartyCommand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace StreamPartyCommand.Installers
{
    public class SPCAppInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<ChatCoreWrapper>().AsSingle().NonLazy();
        }
    }
}

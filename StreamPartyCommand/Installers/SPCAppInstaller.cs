using StreamPartyCommand.Models;
using Zenject;

namespace StreamPartyCommand.Installers
{
    public class SPCAppInstaller : Installer
    {
        public override void InstallBindings() => this.Container.BindInterfacesAndSelfTo<ChatCoreWrapper>().AsSingle().NonLazy();
    }
}

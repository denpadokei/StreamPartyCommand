using StreamPartyCommand.Models;
using StreamPartyCommand.Utilities;
using Zenject;

namespace StreamPartyCommand.Installers
{
    public class SPCAppInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<ChatCoreWrapper>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<CustomNoteUtil>().AsSingle().NonLazy();
        }
    }
}

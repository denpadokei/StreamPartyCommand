﻿using CatCore.Services.Multiplexer;

namespace StreamPartyCommand.Interfaces
{
    public interface ICommandable
    {
        string Key { get; }
        /// <summary>
        /// コマンドを実行するところ。Updateで呼ばれるためめちゃ軽い処理のみお願いします。
        /// </summary>
        /// <param name="service"></param>
        /// <param name="message"></param>
        void Execute(MultiplexedPlatformService service, MultiplexedMessage message);
    }
}

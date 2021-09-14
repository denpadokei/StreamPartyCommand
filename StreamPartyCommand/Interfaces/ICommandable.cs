﻿using ChatCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        void Execute(IChatService service, IChatMessage message);
    }
}

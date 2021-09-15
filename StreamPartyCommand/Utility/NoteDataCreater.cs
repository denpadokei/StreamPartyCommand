using StreamPartyCommand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamPartyCommand.Utility
{
    public class NoteDataCreater
    {
        public static NoteData CreateDummyBomb(float time, int lineIndex, NoteLineLayer noteLineLayer)
        {
            return new NoteData(time, lineIndex, noteLineLayer, noteLineLayer, (ColorType)DummyBomb.DUMMY_BOMB_VALUE, NoteCutDirection.None, 0f, 0f, lineIndex, 0f, 0f, false, false);
        }
    }
}

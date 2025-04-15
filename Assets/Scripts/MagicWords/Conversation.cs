using System;
using System.Collections.Generic;

namespace Assets.Scripts.MagicWords
{
    [Serializable]
    public sealed class Conversation
    {
        public List<Dialogue> Dialogue;
        public List<Emoji> Emojies;
        public List<Avatar> Avatars;
    }
}
using System;
using System.Collections.Generic;

namespace Assets.Scripts.MagicWords
{
    [Serializable]
    public sealed class Conversation
    {
        public List<Dialogue> dialogue;
        public List<Emoji> emojies;
        public List<Avatar> avatars;
    }
}
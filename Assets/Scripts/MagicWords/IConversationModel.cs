using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    public interface IConversationModel
    {
        Conversation Conversation { get; set; }
        List<Sprite> Emojis { get; set; }
        List<Sprite> Avatars { get; set; }
        TMP_SpriteAsset EmojisSpriteAsset { get; set; }
    }
}
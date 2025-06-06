using Game.Common.Runtime.MVVM;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    public sealed class ConversationViewModel : ViewModelBase
    {
        private const string DialogueViewName = "Dialogue";
        private readonly Regex _emojiTagRegex = new(@"\{(.*?)\}");

        private readonly IViewFactory _viewFactory;
        private readonly IConversationModel _conversationModel;

        public ConversationViewModel(IConversationModel conversationModel, IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
            _conversationModel = conversationModel;
        }

        public void GenerateDialogues(Transform dialogueParent)
        {
            for (var i = 0; i < _conversationModel.Conversation.dialogue.Count; i++)
            {
                var dialogue = _conversationModel.Conversation.dialogue[i];

                var avatar = _conversationModel.Conversation.avatars.Find(avatar =>
                    avatar.name == dialogue.name);

                var avatarSprite = _conversationModel.Avatars.Find(sprite => sprite.name == avatar.name);
                var characterName = dialogue.name;
                var dialogueText = dialogue.text;

                dialogueText = ReplaceEmojiTags(dialogueText);

                if (avatar.position == "right")
                {
                    var view = _viewFactory.Create<DialogueRightView>(
                        $"{DialogueViewName}{i}",
                        dialogueParent) as DialogueViewBase;

                    view!.Initialize(avatarSprite, characterName, dialogueText, _conversationModel.EmojisSpriteAsset);
                }
                else
                {
                    var view = _viewFactory.Create<DialogueLeftView>(
                        $"{DialogueViewName}{i}",
                        dialogueParent) as DialogueViewBase;

                    view!.Initialize(avatarSprite, characterName, dialogueText, _conversationModel.EmojisSpriteAsset);
                }
            }
        }

        public string ReplaceEmojiTags(string input)
        {
            return _emojiTagRegex.Replace(input, match =>
            {
                var tag = match.Groups[1].Value;

                var id = _conversationModel.EmojisSpriteAsset.spriteInfoList
                    .Find(sprite => sprite.name == tag)
                    .id;

                Debug.Log(id);
                Debug.Log(_conversationModel.EmojisSpriteAsset.material);

                return $"<sprite={id}>";
            });
        }
    }
}
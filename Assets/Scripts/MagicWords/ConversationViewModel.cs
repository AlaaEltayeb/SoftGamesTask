using Assets.Scripts.MVVM;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    public sealed class ConversationViewModel : ViewModelBase
    {
        private const string DialogueViewName = "Dialogue";

        private readonly IConversationModel _conversationModel;
        private readonly IViewFactory _viewFactory;

        public ConversationViewModel(IConversationModel conversationModel, IViewFactory viewFactory)
        {
            _conversationModel = conversationModel;
            _viewFactory = viewFactory;
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

                if (avatar.position == "right")
                {
                    var view = _viewFactory.Create<DialogueRightView>(
                        $"{DialogueViewName}{i}",
                        dialogueParent) as DialogueRightView;

                    view!.Initialize(avatarSprite, characterName, dialogueText);
                }
                else
                {
                    var view = _viewFactory.Create<DialogueLeftView>(
                        $"{DialogueViewName}{i}",
                        dialogueParent) as DialogueLeftView;

                    view!.Initialize(avatarSprite, characterName, dialogueText);
                }
            }
        }
    }
}
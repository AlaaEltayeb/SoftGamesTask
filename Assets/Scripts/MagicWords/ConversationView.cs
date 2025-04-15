using Assets.Scripts.MVVM;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    public sealed class ConversationView : ViewBase<ConversationViewModel>
    {
        [SerializeField]
        private Transform _dialogueParent;

        protected override void Bind()
        {
            base.Bind();

            ViewModel.GenerateDialogues(_dialogueParent);
        }
    }
}
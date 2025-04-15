using Assets.Scripts.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MagicWords
{
    public sealed class DialogueView : ViewBase<DialogueViewModel>
    {
        [SerializeField]
        private Image _characterImage;

        [SerializeField]
        private TextMeshProUGUI _characterNameText;

        [SerializeField]
        private TextMeshProUGUI _dialogueText;

        protected override void Bind()
        {
            base.Bind();
        }
    }
}
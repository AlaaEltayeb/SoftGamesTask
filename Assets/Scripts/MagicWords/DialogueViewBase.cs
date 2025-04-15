using Assets.Scripts.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MagicWords
{
    public abstract class DialogueViewBase : ViewBase<DialogueViewModel>
    {
        [SerializeField]
        private Image _characterImage;

        [SerializeField]
        private TextMeshProUGUI _characterNameText;

        [SerializeField]
        private TextMeshProUGUI _dialogueText;

        public void Initialize(
            Sprite characterImage,
            string characterName,
            string dialogue,
            TMP_SpriteAsset spriteAsset)
        {
            _characterImage.sprite = characterImage;
            _characterNameText.text = characterName;
            _dialogueText.spriteAsset = spriteAsset;
            _dialogueText.text = dialogue;
        }
    }
}
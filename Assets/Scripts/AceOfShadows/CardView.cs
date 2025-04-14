using Assets.Scripts.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AceOfShadows
{
    public sealed class CardView : ViewBase<CardViewModel>
    {
        [SerializeField]
        private Image _cardImage;

        protected override void Bind()
        {
            base.Bind();

            _cardImage.color = GenerateRandomColor();

            ViewModel.OnCardPopped += MoveCard;
        }

        private Color GenerateRandomColor()
        {
            return new Color(Random.value, Random.value, Random.value);
        }

        private void MoveCard()
        {
            gameObject.SetActive(false);
        }
    }
}
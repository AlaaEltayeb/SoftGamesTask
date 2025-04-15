using Assets.Scripts.MVVM;
using DG.Tweening;
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

            ViewModel.OnMoveCard += MoveCard;
        }

        private Color GenerateRandomColor()
        {
            return new Color(Random.value, Random.value, Random.value);
        }

        private void MoveCard()
        {
            transform.parent = ViewModel.CardTargetParent;
            transform.DOMove(ViewModel.CardTargetParent.position, 2f);
        }
    }
}
using DG.Tweening;
using Game.Common.Runtime.MVVM;
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
            ViewModel.OnCardSetup += SetupCard;
        }

        private void SetupCard(float offset)
        {
            transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
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
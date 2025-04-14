using Assets.Scripts.MVVM;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.AceOfShadows
{
    public class AceOfShadowsGameManager : MonoBehaviour
    {
        private IViewFactory _viewFactory;

        [SerializeField]
        private Transform _cardViewParent;

        [Inject]
        private void Construct(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;

            for (var i = 0; i < 10; i++)
            {
                _viewFactory.Create<CardView>(
                    "CardView",
                    _cardViewParent);
            }
        }
    }
}
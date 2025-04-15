using Assets.Scripts.AceOfShadows;
using Assets.Scripts.MagicWords;
using Assets.Scripts.MVVM;
using Assets.Scripts.Particle;
using Assets.Scripts.SceneHolder;
using UnityEngine;

namespace Assets.Scripts.InGameMenu
{
    public sealed class InGameMenuViewModel : ViewModelBase
    {
        private readonly IViewFactory _viewFactory;
        private readonly ISceneHolder _sceneHolder;

        public InGameMenuViewModel(IViewFactory viewFactory, ISceneHolder sceneHolder)
        {
            _viewFactory = viewFactory;
            _sceneHolder = sceneHolder;
        }

        public void OnAceOfShadowsSelected()
        {
            CreateScene<AceOfShadowsView>();
        }

        public void OnMagicWordsSelected()
        {
            CreateScene<ConversationView>();
        }

        public void OnPhoenixFlameSelected()
        {
            CreateScene<ParticleView>();
        }

        private void CreateScene<TView>() where TView : IView
        {
            var view = _viewFactory.Create<TView>(
                $"{nameof(TView)}");

            _sceneHolder.SetActiveScene(view);
        }

        public void Invoke()
        {
            Debug.Log("Invoked");
        }
    }
}
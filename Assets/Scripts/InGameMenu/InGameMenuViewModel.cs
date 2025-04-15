using Assets.Scripts.AceOfShadows;
using Assets.Scripts.MagicWords;
using Assets.Scripts.MVVM;
using Assets.Scripts.Particle;
using Assets.Scripts.SceneHolder;

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
            var type = typeof(TView);
            var view = _viewFactory.Create<TView>(
                $"{nameof(type)}");

            _sceneHolder.SetActiveScene(view);
        }
    }
}
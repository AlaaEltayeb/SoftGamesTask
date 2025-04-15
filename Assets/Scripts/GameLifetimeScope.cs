using Assets.Scripts.AceOfShadows;
using Assets.Scripts.Command;
using Assets.Scripts.Common;
using Assets.Scripts.InGameMenu;
using Assets.Scripts.Loading;
using Assets.Scripts.MagicWords;
using Assets.Scripts.MVVM;
using Assets.Scripts.Particle;
using Assets.Scripts.SceneHolder;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private ViewContainer _viewContainer;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<IViewContainer>(_viewContainer);

            builder.Register<ISceneHolder, SceneHolder.SceneHolder>(Lifetime.Singleton);

            builder.Register<ICommandFactory, CommandFactory>(Lifetime.Singleton);
            builder.Register<ICommandDispatcher, CommandDispatcher>(Lifetime.Singleton);

            builder.Register<IViewFactory, ViewFactory>(Lifetime.Singleton);

            builder.Register<ConversationModel>(Lifetime.Singleton).AsImplementedInterfaces();

            AddViewsAndViewModels(builder);
        }

        private static void AddViewsAndViewModels(IContainerBuilder builder)
        {
            RegisterViewWithViewModelOnNewGameObject<LoadingView, LoadingViewModel>(builder, Lifetime.Transient);

            RegisterViewWithViewModelOnNewGameObject<UIView, UIViewModel>(builder, Lifetime.Transient);
            RegisterViewWithViewModelOnNewGameObject<FPSView, FPSViewModel>(builder, Lifetime.Transient);
            RegisterViewWithViewModelOnNewGameObject<InGameMenuView, InGameMenuViewModel>(builder, Lifetime.Scoped);

            RegisterViewWithViewModelOnNewGameObject<ConversationView, ConversationViewModel>(builder,
                Lifetime.Transient);
            RegisterViewWithViewModelOnNewGameObject<DialogueLeftView, DialogueViewModel>(builder, Lifetime.Transient);
            RegisterViewWithViewModelOnNewGameObject<DialogueRightView, DialogueViewModel>(builder, Lifetime.Transient);

            RegisterViewWithViewModelOnNewGameObject<AceOfShadowsView, AceOfShadowsViewModel>(builder,
                Lifetime.Transient);
            RegisterViewWithViewModelOnNewGameObject<CardView, CardViewModel>(builder, Lifetime.Transient);

            RegisterViewWithViewModelOnNewGameObject<ParticleView, ParticleViewModel>(builder,
                Lifetime.Transient);
            RegisterViewWithViewModelOnNewGameObject<FireControllerView, FireControllerViewModel>(builder,
                Lifetime.Transient);
        }

        private static void RegisterViewWithViewModelOnNewGameObject<TView, TViewModel>(
            IContainerBuilder builder,
            Lifetime lifetime,
            string newGameObjectName = null)
            where TViewModel : class, IViewModel
            where TView : ViewBase<TViewModel>, IView
        {
            builder.RegisterComponentOnNewGameObject<TView>(lifetime, newGameObjectName);
            builder.Register<TViewModel>(lifetime);
        }

        private static void RegisterComponentsInHierarchy<TView, TViewModel>(
            IContainerBuilder builder,
            Lifetime lifetime)
            where TViewModel : class, IViewModel
            where TView : ViewBase<TViewModel>, IView
        {
            builder.RegisterComponentInHierarchy<TView>();
            builder.Register<TViewModel>(lifetime);
        }
    }
}
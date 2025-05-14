using Assets.Scripts.AceOfShadows;
using Assets.Scripts.Common;
using Assets.Scripts.InGameMenu;
using Assets.Scripts.Loading;
using Assets.Scripts.MagicWords;
using Assets.Scripts.Particle;
using Assets.Scripts.SceneHolder;
using Game.Common.Runtime.Command;
using Game.Common.Runtime.MVVM;
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
            AddUIViews(builder);
            AddMagicWordsViews(builder);
            AddAceOfShadowsViews(builder);
            AddPhoenixFlameViews(builder);
        }

        private static void AddUIViews(IContainerBuilder builder)
        {
            builder.RegisterViewWithViewModelOnNewGameObject<LoadingView, LoadingViewModel>(Lifetime.Transient);

            builder.RegisterViewWithViewModelOnNewGameObject<UIView, UIViewModel>(Lifetime.Transient);
            builder.RegisterViewWithViewModelOnNewGameObject<FPSView, FPSViewModel>(Lifetime.Transient);
            builder.RegisterViewWithViewModelOnNewGameObject<InGameMenuView, InGameMenuViewModel>(Lifetime.Transient);
        }

        private static void AddMagicWordsViews(IContainerBuilder builder)
        {
            builder.RegisterViewWithViewModelOnNewGameObject<ConversationView, ConversationViewModel>(
                Lifetime.Transient);
            builder.RegisterViewWithViewModelOnNewGameObject<DialogueLeftView, DialogueViewModel>(Lifetime.Transient);
            builder.RegisterViewWithViewModelOnNewGameObject<DialogueRightView, DialogueViewModel>(Lifetime.Transient);
        }

        private static void AddAceOfShadowsViews(IContainerBuilder builder)
        {
            builder.RegisterViewWithViewModelOnNewGameObject<AceOfShadowsView, AceOfShadowsViewModel>(
                Lifetime.Transient);
            builder.RegisterViewWithViewModelOnNewGameObject<CardView, CardViewModel>(Lifetime.Transient);
        }

        private static void AddPhoenixFlameViews(IContainerBuilder builder)
        {
            builder.RegisterViewWithViewModelOnNewGameObject<ParticleView, ParticleViewModel>(Lifetime.Transient);
            builder.RegisterViewWithViewModelOnNewGameObject<FireControllerView, FireControllerViewModel>(
                Lifetime.Transient);
        }
    }
}
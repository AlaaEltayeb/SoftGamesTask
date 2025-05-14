using Assets.Scripts.InGameMenu;
using Game.Common.Runtime.MVVM;
using Game.Common.Runtime.Command;
using VContainer;

namespace Assets.Scripts.Loading
{
    public sealed class ShowMainMenuCommand : ICommand
    {
        private IViewFactory _viewFactory;

        [Inject]
        private void Construct(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void Execute()
        {
            _viewFactory.Create<UIView>($"{typeof(UIView)}");
        }
    }
}
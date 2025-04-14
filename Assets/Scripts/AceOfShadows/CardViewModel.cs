using Assets.Scripts.Command;
using Assets.Scripts.MVVM;
using JetBrains.Annotations;

namespace Assets.Scripts.AceOfShadows
{
    [UsedImplicitly]
    public sealed class CardViewModel : ViewModelBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public CardViewModel(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public void OnButtonClicked()
        {
            _commandDispatcher.Execute(new TestCommand());
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
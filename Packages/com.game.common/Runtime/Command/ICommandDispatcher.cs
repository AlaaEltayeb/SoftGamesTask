namespace Game.Common.Runtime.Command
{
    public interface ICommandDispatcher
    {
        void Execute(ICommand command);
    }
}
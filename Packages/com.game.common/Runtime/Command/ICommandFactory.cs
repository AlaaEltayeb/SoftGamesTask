namespace Game.Common.Runtime.Command
{
    public interface ICommandFactory
    {
        void Populate<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
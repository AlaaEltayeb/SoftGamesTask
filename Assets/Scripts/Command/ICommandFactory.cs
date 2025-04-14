namespace Assets.Scripts.Command
{
    public interface ICommandFactory
    {
        void Populate<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
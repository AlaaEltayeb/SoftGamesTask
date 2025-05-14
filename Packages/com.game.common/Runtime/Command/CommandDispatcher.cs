using System;
using UnityEngine;

namespace Game.Common.Runtime.Command
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandFactory _commandFactory;

        public CommandDispatcher(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        public void Execute(ICommand command)
        {
            try
            {
                _commandFactory.Populate(command);
            }
            catch (Exception ex)
            {
                Debug.Log($"Exception: {ex}");
                return;
            }

            command.Execute();
        }
    }
}
using System;

namespace Unchord
{
    public interface ICommand
    {
        void Execute(CommandQueueCallback _callbackOnEnd);
    }
}
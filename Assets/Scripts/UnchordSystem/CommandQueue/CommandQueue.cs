using System.Collections.Generic;

namespace Unchord
{
    public class CommandQueue : ICommandQueue
    {
        public int Count => m_cmds.Count;

        private Queue<Command> m_cmds;

        public CommandQueue(int _capacity = 1)
        {
            m_cmds = new Queue<Command>(_capacity);
        }

        public ICommandQueue Enqueue(ICommand _cmd)
        {
            m_cmds.Enqueue(_cmd.Execute);
            return this;
        }

        public ICommandQueue Enqueue(Command _cmd)
        {
            m_cmds.Enqueue(_cmd);
            return this;
        }

        public bool Execute()
        {
            if(m_cmds.Count == 0)
                return true;

            bool cmdExecutionResult = m_cmds.Peek()();

            if(cmdExecutionResult)
                m_cmds.Dequeue();

            return cmdExecutionResult;
        }
    }
}
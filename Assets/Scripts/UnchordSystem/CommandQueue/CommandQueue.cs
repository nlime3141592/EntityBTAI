using System;
using System.Collections.Generic;

namespace Unchord
{
    public class CommandQueue : ICommandQueue
    {
        public int Count => m_cmds.Count;
        public int CountBegin => m_cntBegin;

        private Queue<Command> m_cmds;
        private int m_cntBegin;
        private bool m_bCalledOnEnd;

        public CommandQueue(int _capacity = 1)
        {
            m_cmds = new Queue<Command>(_capacity);
            m_cntBegin = -1;
            m_bCalledOnEnd = false;
        }

        public ICommandQueue Enqueue(ICommand _cmd)
        {
            if(m_cntBegin <= 0)
                m_cmds.Enqueue(_cmd.Execute);
            return this;
        }

        public ICommandQueue Enqueue(Command _cmd)
        {
            if(m_cntBegin <= 0)
                m_cmds.Enqueue(_cmd);
            return this;
        }

        public void Execute(CommandQueueCallback _callbackOnEnd = null)
        {
            if(m_cntBegin <= 0)
            {
                if(m_cmds.Count > 0)
                    m_OnStartExecute();
                return;
            }
            else if(m_cmds.Count <= 0)
                m_OnEndExecute(_callbackOnEnd);
            else
                m_OnExecute();
        }

        private void m_OnStartExecute()
        {
            m_cntBegin = m_cmds.Count;
        }

        private void m_OnEndExecute(CommandQueueCallback _callbackOnEnd)
        {
            m_cntBegin = -1;
            _callbackOnEnd?.Invoke();
        }

        private void m_OnExecute()
        {
            m_cmds.Peek()(() => m_bCalledOnEnd = true);

            if(m_bCalledOnEnd)
            {
                m_bCalledOnEnd = false;
                m_cmds.Dequeue();
            }
        }
    }
}
using System;

namespace Unchord
{
    public class PolyominoException : Exception
    {
        public PolyominoException()
        {

        }

        public PolyominoException(string message)
        : base(message)
        {

        }
    }
}
namespace Unchord
{
    public class PolyominoPieceTransformationException : PolyominoException
    {
        public PolyominoPiece piece { get; private set; }

        public PolyominoPieceTransformationException(PolyominoPiece _piece, string message)
        : base(message)
        {
            
        }
    }
}
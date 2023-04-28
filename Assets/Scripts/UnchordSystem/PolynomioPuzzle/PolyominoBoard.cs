namespace Unchord
{
    public class PolyominoBoard
    {
        private int[,] m_board;

        public PolyominoBoard(int _sx, int _sy)
        {
            m_board = new int[_sx, _sy];
        }

        public bool Place(PolyominoPiece _piece)
        {
            if(_piece.bPlacedOnBoard)
                return false;

            return true;
        }
    }
}
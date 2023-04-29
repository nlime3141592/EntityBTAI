namespace Unchord
{
    public class PolyominoBoard
    {
        private PolyominoPiece[] m_pieces;
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

        public bool Remove(PolyominoPiece _piece)
        {
            return true;
        }

        public void RemoveAll()
        {

        }

        public void RemoveFrom(object _source)
        {

        }
    }
}
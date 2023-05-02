namespace Unchord
{
    public class PolyominoBoard
    {
        internal readonly int i_sx;
        internal readonly int i_sy;
        internal readonly int[,] i_board;
        internal readonly PolyominoPiece[] i_placed;

        // _sx: size of board-x.
        // _sy: size of board-y.
        // _capacity: how many pieces available on this board.
        internal PolyominoBoard(int _sx, int _sy, int _capacity)
        {
            i_sx = _sx;
            i_sy = _sy;
            i_board = new int[_sx, _sy];
            i_placed = new PolyominoPiece[_capacity];
        }

        // DEPRECATED; public void RemoveFrom(object _source) {}
    }
}
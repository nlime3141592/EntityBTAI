using System;
using System.Collections.Generic;

namespace Unchord
{
    public class PolyominoPiece
    {
        public bool bPlacedOnBoard => i_id > 0;
        // DEPRECATED; public object source { get; set; }

        // 생성 시 고정되는 값
        public int dimension { get; private set; }
        internal readonly int[] i_pieceBase;
        internal int i_cntBlock;

        // 운영 시 변경될 수 있는 값
        internal int i_id = -1;
        internal int i_placedCx = -1;
        internal int i_placedCy = -1;
        internal PolyominoBoard i_placedBoard;
        internal int i_rotation = 0;

        internal PolyominoPiece(int _dimension)
        {
            dimension = _dimension;
            i_pieceBase = new int[_dimension + _dimension];
        }

        // public PolyominoPiece(int[] _pieceBase) { i_pieceBase = _pieceBase; }

/*
        public void Draw(string name)
        {
            int[,] board = new int[9,9];
            int cx = 4;
            int cy = 4;

            for(int i = 0; i < i_pieceBase.Length; i += 2)
            {
                int px = cx + i_pieceBase[i];
                int py = cy + i_pieceBase[i + 1];
                board[px, py] = 1;
            }

            Console.WriteLine("Piece: {0}", name);
            for(int y = 8; y >= 0; --y)
            {
                for(int x = 0; x <= 8; ++x)
                {
                    if(board[x, y] == 1)
                        Console.Write("■");
                    else
                        Console.Write("□");
                }
                Console.WriteLine();
            }
        }
*/
    }
}
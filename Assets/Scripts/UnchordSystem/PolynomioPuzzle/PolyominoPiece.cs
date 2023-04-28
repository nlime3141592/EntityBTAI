using System;
using System.Collections.Generic;

namespace Unchord
{
    public class PolyominoPiece
    {
        public bool bPlacedOnBoard { get; internal set; } = false;
        public PolyominoBoard placedBoard => bPlacedOnBoard ? i_placedBoard : null;

        internal PolyominoBoard i_placedBoard;

        private int[] m_pieceBase;
        private int m_rotation = 0;

        public PolyominoPiece(int _countPolyBlock)
        {
            m_pieceBase = new int[_countPolyBlock + _countPolyBlock];
        }

        public PolyominoPiece(int[] _pieceBase)
        {
            m_pieceBase = _pieceBase;
        }

        public void GetPolyBlock(out int _dx, out int _dy, int _numBlock)
        {
            int indexPointer = _numBlock + _numBlock;
            int x = m_pieceBase[indexPointer];
            int y = m_pieceBase[indexPointer + 1];

            switch(m_rotation)
            {
                case 0:
                    _dx = x;
                    _dy = y;
                    break;
                case 1:
                    _dx = -y;
                    _dy = x;
                    break;
                case 2:
                    _dx = -x;
                    _dy = -y;
                    break;
                case 3:
                    _dx = y;
                    _dy = -x;
                    break;
                default:
                    throw new Exception("invalid rotation value.");
            }
        }

        public void SetPolyBlock(int _numBlock, int _dx, int _dy)
        {
            if(bPlacedOnBoard)
                throw new PolyominoPieceTransformationException(this, "can't change options when piece on board.");

            int indexPointer = _numBlock + _numBlock;

            m_pieceBase[indexPointer] = _dx;
            m_pieceBase[indexPointer + 1] = _dy;
        }

        public void Rotate()
        {
            if(bPlacedOnBoard)
                throw new PolyominoPieceTransformationException(this, "can't change options when piece on board.");

            m_rotation = (m_rotation + 1) % 4;
        }

        public void RotateReverse()
        {
            if(bPlacedOnBoard)
                throw new PolyominoPieceTransformationException(this, "can't change options when piece on board.");

            m_rotation = (m_rotation + 3) % 4;
        }

        public void SetRotation(PolyominoPieceRotation _rotation)
        {
            if(bPlacedOnBoard)
                throw new PolyominoPieceTransformationException(this, "can't change options when piece on board.");

            m_rotation = (int)_rotation;
        }

        public void PickUp()
        {
            if(!bPlacedOnBoard)
                return;
        }

        public void Draw(string name)
        {
            int[,] board = new int[9,9];
            int cx = 4;
            int cy = 4;

            for(int i = 0; i < m_pieceBase.Length; i += 2)
            {
                int px = cx + m_pieceBase[i];
                int py = cy + m_pieceBase[i + 1];
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
    }
}
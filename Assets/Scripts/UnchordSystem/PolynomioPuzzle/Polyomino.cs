using System;

namespace Unchord
{
    public static class Polyomino
    {
        // [폴리오미노 기본 설명]
        // - 정의
        // 폴리오미노란, 크기가 같은 2개 이상의 정사각형을 변끼리 맞닿아 만든 도형입니다.
        // - 특징
        // 서로 다른 두 폴리오미노를 회전시키거나 뒤집었을 때 모든 정사각형이 겹쳐지면 같은 폴리오미노입니다.
        // 정사각형 개수가 n이라 하면, n=5일 때만 n-폴리오미노 집합에 속하는 모든 도형으로 직사각형을 만들 수 있습니다.

        // [폴리오미노 시스템 설명]
        // - 시스템 정의
        // 폴리오미노 조각: 크기가 같은 2개 이상의 정사각형을 변끼리 맞닿아 만든 도형
        // 폴리오미노 조각 보드판: 폴리오미노 조각을 배치할 수 있는 공간
        // - 시스템 제약 사항
        // 폴리오미노 조각에 대해 회전은 가능하지만, 뒤집기는 불가능합니다.
        // - 시스템 흐름
        // 1. 새로운 폴리오미노 조각 보드판을 생성합니다.
        //    - 폴리오미노 조각 보드판의 가로, 세로 크기를 지정합니다.
        //    - 폴리오미노 조각 보드판에 배치할 수 있는 최대 폴리오미노 조각 개수를 지정합니다.
        // 2. 새로운 폴리오미노 조각을 생성합니다.
        //    - 차원 값을 지정합니다. 차원 값이란, 폴리오미노 조각을 구성하는 정사각형 개수를 의미합니다.
        // 3. 폴리오미노 조각을 구성합니다.
        //    - 새로운 정사각형을 추가하기 위해 블록의 상대적 위치를 지정합니다.
        //    - 정사각형의 개수는 폴리오미노 조각의 차원 값과 일치해야 합니다.
        //    - 폴리오미노 조각을 회전시킬 수 있습니다.
        // 4. 폴리오미노 조각을 폴리오미노 조각 보드판에 배치합니다.
        // 5. 필요한 경우, 폴리오미노 조각 보드판에 배치된 폴리오미노 조각을 회수할 수 있습니다.

#region Extension of PolyominoPiece
        public static PolyominoPiece GetNewPiece(int _dimension)
        {
            // 반드시 1차원 이상의 벡터가 생성되어야 함.
            if(_dimension < 1)
                return null;

            return new PolyominoPiece(_dimension);
        }

        public static PolyominoPiece AddBlock(this PolyominoPiece _piece, int _dx, int _dy)
        {
            // capacity overflew.
            if(_piece.i_cntBlock == _piece.dimension)
                throw new PolyominoException("");

            int basePointer = _piece.i_cntBlock + _piece.i_cntBlock;

            _piece.i_pieceBase[basePointer] = _dx;
            _piece.i_pieceBase[basePointer + 1] = _dy;

            ++_piece.i_cntBlock;
            return _piece;
        }

        public static PolyominoPiece RotatePositive(this PolyominoPiece _piece)
        {
            _piece.i_rotation = (_piece.i_rotation + 1) % 4;
            return _piece;
        }

        public static PolyominoPiece RotateHalf(this PolyominoPiece _piece)
        {
            _piece.i_rotation = (_piece.i_rotation + 2) % 4;
            return _piece;
        }

        public static PolyominoPiece RotateNegative(this PolyominoPiece _piece)
        {
            _piece.i_rotation = (_piece.i_rotation + 3) % 4;
            return _piece;
        }

        public static bool bCanPlace(this PolyominoPiece _piece, PolyominoBoard _board, int _cx, int _cy)
        {
            int length = _piece.i_pieceBase.Length;
            int pureDx, pureDy, rot;
            int px, py;

            for(int i = 0; i < length; i += 2)
            {
                pureDx = _piece.i_pieceBase[i];
                pureDy = _piece.i_pieceBase[i + 1];
                rot = _piece.i_rotation;

                px = _cx + Polyomino.s_m_GetDx(pureDx, pureDy, rot);
                py = _cy + Polyomino.s_m_GetDy(pureDx, pureDy, rot);

                if(px < 0 || py < 0 || px >= _board.i_sx || py >= _board.i_sy || _board.i_board[px, py] != 0)
                    return false;
            }

            return true;
        }

        public static bool RemoveSelf(this PolyominoPiece _piece)
        {
            if(_piece.i_placedBoard != null)
                return _piece.i_placedBoard.Remove(_piece);
            else
                return false;
        }
#endregion

#region Extension of PolyominoBoard
        public static PolyominoBoard GetNewBoard(int _sx, int _sy, int _capacity)
        {
            if(_sx < 1 || _sy < 1 || _capacity < 1)
                return null;

            return new PolyominoBoard(_sx, _sy, _capacity);
        }

        public static bool Place(this PolyominoPiece _piece, PolyominoBoard _board, int _cx, int _cy)
        {
            if(!_piece.bCanPlace(_board, _cx, _cy))
                return false;

            int id = -1;
            int length = _board.i_placed.Length;

            while(++id < length && _board.i_placed[id] != null);

            if(id == length)
                return false;

            Polyomino.s_m_UpdateBoard(_board, _piece, _cx, _cy, id + 1);
            _board.i_placed[id] = _piece;
            _piece.i_id = id;
            _piece.i_placedCx = _cx;
            _piece.i_placedCy = _cy;
            _piece.i_placedBoard = _board;

            return true;
        }

        public static bool Remove(this PolyominoBoard _board, PolyominoPiece _piece)
        {
            if(!_piece.bPlacedOnBoard)
                return false;
            else if(_piece.i_placedBoard != _board)
                return false;

            int id = _piece.i_id;
            int cx = _piece.i_placedCx;
            int cy = _piece.i_placedCy;

            Polyomino.s_m_UpdateBoard(_board, _piece, cx, cy, 0);
            _board.i_placed[id] = null;
            _piece.i_id = -1;
            _piece.i_placedCx = -1;
            _piece.i_placedCy = -1;
            _piece.i_placedBoard = null;

            return true;
        }

        public static void RemoveAll(this PolyominoBoard _board)
        {
            for(int i = 0; i < _board.i_sx; ++i)
            for(int j = 0; j < _board.i_sy; ++j)
                if(_board.i_board[i, j] != -1)
                    _board.i_board[i, j] = 0;

            int length = _board.i_placed.Length;
            PolyominoPiece piece;

            for(int i = 0; i < length; ++i)
            {
                piece = _board.i_placed[i];

                if(piece == null)
                    continue;

                piece.i_id = -1;
                piece.i_placedCx = -1;
                piece.i_placedCy = -1;
                piece.i_placedBoard = null;
            }
        }

        public static PolyominoBoard EnableCell(this PolyominoBoard _board, int _x, int _y)
        {
            if(_board.i_board[_x, _y] != -1)
                return _board;

            _board.i_board[_x, _y] = 0;
            return _board;
        }

        public static PolyominoBoard DisableCell(this PolyominoBoard _board, int _x, int _y)
        {
            if(_board.i_board[_x, _y] != 0)
                return _board;

            _board.i_board[_x, _y] = -1;
            return _board;
        }
#endregion

#region Polyomino Utilities
        private static void s_m_UpdateBoard(PolyominoBoard _board, PolyominoPiece _piece, int _cx, int _cy, int _value)
        {
            int length = _piece.i_pieceBase.Length;
            int pureDx, pureDy, rot;
            int px, py;

            for(int i = 0; i < length; i += 2)
            {
                pureDx = _piece.i_pieceBase[i];
                pureDy = _piece.i_pieceBase[i + 1];
                rot = _piece.i_rotation;

                px = _cx + Polyomino.s_m_GetDx(pureDx, pureDy, rot);
                py = _cy + Polyomino.s_m_GetDy(pureDx, pureDy, rot);
                _board.i_board[px, py] = _value;
            }
        }

        private static int s_m_GetDx(int _pureDx, int _pureDy, int _rotation)
        {
            int dx = (_rotation & 1) == 0 ? _pureDx : _pureDy;
            return _rotation % 3 == 0 ? dx : -dx;
        }

        private static int s_m_GetDy(int _pureDx, int _pureDy, int _rotation)
        {
            int dy = (_rotation & 1) == 0 ? _pureDy : _pureDx;
            return _rotation < 2 ? dy : -dy;
        }
#endregion
    }
}
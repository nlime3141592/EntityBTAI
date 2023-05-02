/*
namespace Unchord
{
    public static class PrimitivePolyomino
    {
        public static PolyominoPiece p1 => new PolyominoPiece(p1_Base);
        
        public static PolyominoPiece p2_I => new PolyominoPiece(p2_iBase);

        public static PolyominoPiece p3_I => new PolyominoPiece(p3_iBase);
        public static PolyominoPiece p3_V => new PolyominoPiece(p3_vBase);

        public static PolyominoPiece p4_I => new PolyominoPiece(p4_iBase);
        public static PolyominoPiece p4_J => new PolyominoPiece(p4_jBase);
        public static PolyominoPiece p4_L => new PolyominoPiece(p4_lBase);
        public static PolyominoPiece p4_O => new PolyominoPiece(p4_oBase);
        public static PolyominoPiece p4_S => new PolyominoPiece(p4_sBase);
        public static PolyominoPiece p4_T => new PolyominoPiece(p4_tBase);
        public static PolyominoPiece p4_Z => new PolyominoPiece(p4_zBase);

        public static PolyominoPiece p5_F => new PolyominoPiece(p5_fBase);
        public static PolyominoPiece p5_flipedF => new PolyominoPiece(p5_fBaseF);
        public static PolyominoPiece p5_I => new PolyominoPiece(p5_iBase);
        public static PolyominoPiece p5_L => new PolyominoPiece(p5_lBase);
        public static PolyominoPiece p5_flipedL => new PolyominoPiece(p5_lBaseF);
        public static PolyominoPiece p5_N => new PolyominoPiece(p5_nBase);
        public static PolyominoPiece p5_flipedN => new PolyominoPiece(p5_nBaseF);
        public static PolyominoPiece p5_P => new PolyominoPiece(p5_pBase);
        public static PolyominoPiece p5_flipedP => new PolyominoPiece(p5_pBaseF);
        public static PolyominoPiece p5_T => new PolyominoPiece(p5_tBase);
        public static PolyominoPiece p5_U => new PolyominoPiece(p5_uBase);
        public static PolyominoPiece p5_V => new PolyominoPiece(p5_vBase);
        public static PolyominoPiece p5_W => new PolyominoPiece(p5_wBase);
        public static PolyominoPiece p5_X => new PolyominoPiece(p5_xBase);
        public static PolyominoPiece p5_Y => new PolyominoPiece(p5_yBase);
        public static PolyominoPiece p5_flipedY => new PolyominoPiece(p5_yBaseF);
        public static PolyominoPiece p5_Z => new PolyominoPiece(p5_zBase);
        public static PolyominoPiece p5_flipedZ => new PolyominoPiece(p5_zBaseF);

        private static int[] p1_Base => new int[]{ 0,0 };

        private static int[] p2_iBase => new int[]{ 0,0, 0,1 };

        private static int[] p3_iBase => new int[]{ 0,0, 0,1, 0,2 };
        private static int[] p3_vBase => new int[]{ 0,0, 0,1, 1,0 };

        private static int[] p4_iBase => new int[]{ 0,0, 0,1, 0,2, 0,3 };
        private static int[] p4_jBase => new int[]{ -1,0, 0,0, 0,1, 0,2 };
        private static int[] p4_lBase => new int[]{ 0,0, 0,1, 0,2, 1,0 };
        private static int[] p4_oBase => new int[]{ 0,0, 0,1, 1,0, 1,1 };
        private static int[] p4_sBase => new int[]{ -1,0, 0,0, 0,1, 1,1 };
        private static int[] p4_tBase => new int[]{ -1,0, 0,-1, 0,0, 1,0 };
        private static int[] p4_zBase => new int[]{ -1,1, 0,0, 0,1, 1,0 };

        private static int[] p5_fBase => new int[]{ -1,0, 0,-1, 0,0, 0,1, 1,1 };
        private static int[] p5_fBaseF => new int[]{ -1,1, 0,-1, 0,0, 0,1, 1,0 };
        private static int[] p5_iBase => new int[]{ 0,0, 0,1, 0,2, 0,3, 0,4 };
        private static int[] p5_lBase => new int[]{ 0,0, 0,1, 0,2, 0,3, 1,0 };
        private static int[] p5_lBaseF => new int[]{ -1,0, 0,0, 0,1, 0,2, 0,3 };
        private static int[] p5_nBase => new int[]{ 0,-2, 0,-1, 0,0, 1,0, 1,1 };
        private static int[] p5_nBaseF => new int[]{ -1,0, -1,1, 0,-2, 0,-1, 0,0 };
        private static int[] p5_pBase => new int[]{ 0,-1, 0,0, 0,1, 1,0, 1,1 };
        private static int[] p5_pBaseF => new int[]{ -1,0, -1,1, 0,-1, 0,0, 0,1 };
        private static int[] p5_tBase => new int[]{ -1,0, 0,-2, 0,-1, 0,0, 1,0 };
        private static int[] p5_uBase => new int[]{ -1,0, -1,1, 0,0, 1,0, 1,1 };
        private static int[] p5_vBase => new int[]{ 0,0, 0,1, 0,2, 1,0, 2,0 };
        private static int[] p5_wBase => new int[]{ -1,0, -1,1, 0,-1, 0,0, 1,-1 };
        private static int[] p5_xBase => new int[]{ -1,0, 0,-1, 0,0, 0,1, 1,0 };
        private static int[] p5_yBase => new int[]{ -1,0, 0,-2, 0,-1, 0,0, 0,1 };
        private static int[] p5_yBaseF => new int[]{ 0,-2, 0,-1, 0,0, 0,1, 1,0 };
        private static int[] p5_zBase => new int[]{ -1,1, 0,-1, 0,0, 0,1, 1,-1 };
        private static int[] p5_zBaseF => new int[]{ -1,-1, 0,-1, 0,0, 0,1, 1,1 };
    }
}
*/
namespace Unchord
{
    public static class MachineConstant
    {
        // st : state number
        public const int c_st_MACHINE_OFF = -1;
        public const int c_st_MACHINE_PAUSE = -2;

        // lt : literal
        public const int c_lt_END = -1000;
        public const int c_lt_HALT = -1001;
        public const int c_lt_CONTINUE = -2000;
        public const int c_lt_PASS = -3000;
    }
}
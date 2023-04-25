using System;

namespace Unchord
{
    public static partial class Utilities
    {
        public static T Max<T>(T _a, T _b)
        where T : IComparable
        {
            return _a.CompareTo(_b) > 0 ? _a : _b;
        }

        public static T Min<T>(T _a, T _b)
        where T : IComparable
        {
            return _a.CompareTo(_b) < 0 ? _a : _b;
        }

        public static T Mid<T>(T _a, T _b, T _c)
        where T : IComparable
        {
            if(_c.CompareTo(_a) < 0)
            {
                if(_a.CompareTo(_b) < 0) return _a;
                else if(_b.CompareTo(_c) < 0) return _c;
                else return _b;
            }
            else if(_b.CompareTo(_a) < 0) return _a;
            else if(_b.CompareTo(_c) < 0) return _b;
            else return _c;
        }
    }
}
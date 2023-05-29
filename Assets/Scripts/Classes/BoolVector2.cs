using System;

namespace Unchord
{
    [Serializable]
    public struct BoolVector2
    {
        public static BoolVector2 True => true;
        public static BoolVector2 False => false;
        public static BoolVector2 X => new BoolVector2(true, false);
        public static BoolVector2 Y => new BoolVector2(false, true);

        public bool x;
        public bool y;

        public BoolVector2(bool _x, bool _y)
        {
            x = _x;
            y = _y;
        }

        public static implicit operator BoolVector2(bool _value)
        {
            return new BoolVector2(_value, _value);
        }

        public static BoolVector2 operator !(BoolVector2 _vector)
        {
            return new BoolVector2(!_vector.x, !_vector.y);
        }

        public static BoolVector2 operator &(BoolVector2 _v1, BoolVector2 _v2)
        {
            return new BoolVector2(_v1.x & _v2.x, _v1.y & _v2.y);
        }

        public static BoolVector2 operator |(BoolVector2 _v1, BoolVector2 _v2)
        {
            return new BoolVector2(_v1.x | _v2.x, _v1.y | _v2.y);
        }

        public static BoolVector2 operator ^(BoolVector2 _v1, BoolVector2 _v2)
        {
            return new BoolVector2(_v1.x != _v2.x, _v1.y != _v2.y);
        }
    }
}
using UnityEngine;

namespace UnchordMetroidvania
{
    public class _EntityEditorPlayer : _EntityBase
    {
        [Header("Move X")]
        public bool Positive_X;
        public bool Negative_X;
        private bool prevPos_X;
        private bool prevNeg_X;
        [Header("Move Y")]
        public bool Positive_Y;
        public bool Negative_Y;
        private bool prevPos_Y;
        private bool prevNeg_Y;

        protected override void Update()
        {
            UpdateMovementBooleans(ref Positive_X, ref Negative_X, ref prevPos_X, ref prevNeg_X);
            UpdateMovementBooleans(ref Positive_Y, ref Negative_Y, ref prevPos_Y, ref prevNeg_Y);

            base.axisInput.x = GetAxisInput(Positive_X, Negative_X);
            base.axisInput.y = GetAxisInput(Positive_Y, Negative_Y);
        }

        private void UpdateMovementBooleans(
            ref bool positive, ref bool negative,
            ref bool prevPos, ref bool prevNeg
            )
        {
            byte curCode = GetMovementCode(positive, negative);
            byte prevCode = GetMovementCode(prevPos, prevNeg);
            byte nextCode;

            if(curCode == 0)
                nextCode = 0;
            else if(curCode == 3)
                nextCode = (byte)((3 - prevCode) % 3);
            else
                nextCode = curCode;

            prevPos = positive;
            prevNeg = negative;

            positive = (nextCode & 2) != 0;
            negative = (nextCode & 1) != 0;
        }

        private byte GetMovementCode(bool pos, bool neg)
        {
            byte value = 0;
            if(pos) value += 2;
            if(neg) value += 1;
            return value;
        }

        private float GetAxisInput(bool pos, bool neg)
        {
            if(pos ^ neg)
                return pos ? 1.0f : -1.0f;
            else
                return 0.0f;
        }
    }
}
using System;
using System.Runtime.InteropServices;

namespace Unchord
{
    public class Keyboard
    {
        [DllImport("user32.dll")]
        private static extern Int32 GetAsyncKeyState(Int32 vKey);

        private readonly static byte[] s_m_histories = new byte[0x100];

        public static float GetAxis(bool bNegative, bool bPositive)
        {
            float axis = 0;
            if(bNegative) axis -= 1;
            if(bPositive) axis += 1;
            return axis;
        }

        public static bool GetKeyDown(KeyboardKey vKey, params KeyboardKey[] requires)
        {
            byte nextState = s_m_UpdateHistory(vKey);
            int iKey = (int)vKey;

            s_m_histories[iKey] = nextState;

            return nextState == 1 && s_m_CheckRequires(requires);
        }

        public static bool GetKeyPress(KeyboardKey vKey, params KeyboardKey[] requires)
        {
            byte nextState = s_m_UpdateHistory(vKey);
            int iKey = (int)vKey;

            s_m_histories[iKey] = nextState;

            return nextState == 3 && s_m_CheckRequires(requires);
        }

        public static bool GetKeyUp(KeyboardKey vKey, params KeyboardKey[] requires)
        {
            byte nextState = s_m_UpdateHistory(vKey);
            int iKey = (int)vKey;

            if(nextState == 2)
            {
                s_m_histories[iKey] = 0;
                return true && s_m_CheckRequires(requires);
            }
            else
            {
                s_m_histories[iKey] = nextState;
                return false;
            }
        }

        private static byte s_m_UpdateHistory(KeyboardKey vKey)
        {
            int iKey = (int)vKey;
            int history = s_m_histories[iKey] & 1;

            if(GetAsyncKeyState(iKey) > 0x7fff)
                return (byte)(2 * history + 1);
            if(GetAsyncKeyState(iKey) == 0 && history == 1)
                return 2;

            return s_m_histories[iKey];
        }

        private static bool s_m_CheckRequires(KeyboardKey[] requires)
        {
            if(requires == null || requires.Length == 0)
                return true;

            for(int i = 0; i < requires.Length; ++i)
                if(!Keyboard.GetKeyPress(requires[i]))
                    return false;

            return true;
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace UnchordMetroidvania
{
    public static class ProgramManager
    {
        public static int maxProcessCount
        {
            get => s_m_maxProcessCount;
            set => s_m_maxProcessCount = value < 1 ? 1 : value; // max(1, value)
        }

        private static int s_m_maxProcessCount = 1;

        public static void Halt()
        {
            Process.GetCurrentProcess().Close();
        }

        public static void Kill()
        {
            Process.GetCurrentProcess().Kill();
        }

        public static int GetProcessCount()
        {
            string programPath = Assembly.GetEntryAssembly().Location;
            string programName = Path.GetFileNameWithoutExtension(programPath);
            return Process.GetProcessesByName(programName).Length;
        }

        /*
        최상위 창을 찾는 소스 코드.
        WinAPI 이용해서 접근해야 하는 듯.
        GetForegroundWindow() 함수라던가 등등..
        */
        public static void PrintProcess()
        {
            Process cur = Process.GetCurrentProcess();
            int id = cur.Id;
            bool response = cur.Responding;
            // response = Window.Topmost;
            char cResponse = response ? 'T' : 'F';
            Console.Write($"[{id}..{cResponse}] ");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace DotaHook
{
    class KeyHook
    {

        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;

        private static LowLevelKeyboardProc _proc = HookCallback;

        private static IntPtr _hookID = IntPtr.Zero;

        public static void start()
        {
            Console.WriteLine("Invalid input");
            initQueue();
            _hookID = SetHook(_proc);

            //Application.Run();

            //UnhookWindowsHookEx(_hookID);
        }
        private static IntPtr SetHook(LowLevelKeyboardProc proc)

        {

            using (Process curProcess = Process.GetCurrentProcess())

            using (ProcessModule curModule = curProcess.MainModule)

            {

                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,

                    GetModuleHandle(curModule.ModuleName), 0);

            }

        }


        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);


        const int SIZE = 1000;

        static string[] queue = new string[SIZE];
        static int index = 0;

        //private static int value(Keys inputKey)
        //{
        //    switch (inputKey)
        //    {
        //        case Keys.NumPad0:
        //            return 0;
        //        case Keys.D0:
        //            return 0;

        //        case Keys.NumPad1:
        //            return 1;
        //        case Keys.D1:
        //            return 1;

        //        case Keys.NumPad2:
        //            return 2;
        //        case Keys.D2:
        //            return 2;

        //        case Keys.NumPad3:
        //            return 3;
        //        case Keys.D3:
        //            return 3;

        //        case Keys.NumPad4:
        //            return 4;
        //        case Keys.D4:
        //            return 4;

        //        case Keys.NumPad5:
        //            return 5;
        //        case Keys.D5:
        //            return 5;

        //        case Keys.NumPad6:
        //            return 6;
        //        case Keys.D6:
        //            return 6;

        //        case Keys.NumPad7:
        //            return 7;
        //        case Keys.D7:
        //            return 7;

        //        case Keys.NumPad8:
        //            return 8;
        //        case Keys.D8:
        //            return 8;

        //        case Keys.NumPad9:
        //            return 9;
        //        case Keys.D9:
        //            return 9;

        //    }

        //    return -1;
        //}
        static bool checkAlph(System.Windows.Forms.Keys e)
        {
            int keyValue = (int)e;

            if ((keyValue >= 0x30 && keyValue <= 0x39) // numbers
              || (keyValue >= 0x41 && keyValue <= 0x5A) // letters
              //|| (keyValue >= 0x60 && keyValue <= 0x69)
              || e == Keys.Space
              ) // numpad
            {
                return true;
            }
            return false;
        }
        static string convert(System.Windows.Forms.Keys e)
        {
            int keyValue = (int)e;
            if (e == Keys.Space) return " ";
            if ((keyValue >= 0x30 && keyValue <= 0x39) // numbers
              
              ) // numpad
            {
                return e.ToString().Replace("D", "");
            }

            if ((keyValue >= 0x30 && keyValue <= 0x39) // numbers
              || (keyValue >= 0x41 && keyValue <= 0x5A) // letters
                                                        //|| (keyValue >= 0x60 && keyValue <= 0x69)
              ) // numpad
            {
                return e.ToString();
            }
            return "";
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                var keys = (Keys)vkCode;
                Console.WriteLine(keys);

                if (keys == Keys.Enter)
                {
                    String msg = check();
                    if (msg.Length == 0)
                    {
                        Console.WriteLine("Invalid input");
                    }
                    else if (KeyHook.textBox != null)
                    {
                        if (KeyHook.textBox.Enabled)
                        {
                            KeyHook.textBox.Text = msg;
                            if (KeyHook.frmMain != null)
                            {
                                KeyHook.frmMain.onMessage(msg);
                            }
                        }
                    }
                    Console.WriteLine("KeyHook: (" + msg + ")");

                }
                //var v = value(keys);
                if (index < SIZE && checkAlph(keys))
                {
                    queue[index] = convert(keys);
                    index = (index + 1);
                }

            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);

        }
        public static Label textBox = null;
        public static Form1 frmMain = null;
        static String check()
        {
            String resultString = "";

            for (int i = 0; i < SIZE; i++)
            {
                if (queue[i] == "")
                {
                    break;
                }
                resultString = resultString + queue[i];
            }
            initQueue();
            return resultString;
        }

        static void initQueue()
        {
            index = 0;
            for (int i = 0; i < SIZE; i++)
            {
                queue[i] = "";
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr SetWindowsHookEx(int idHook,

            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool UnhookWindowsHookEx(IntPtr hhk);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,

            IntPtr wParam, IntPtr lParam);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}


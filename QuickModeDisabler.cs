namespace QuickModeDisabler
{
    using System;
    using System.Runtime.InteropServices;

    public class QuickModeDisabler
    {
        const uint ENABLE_QUICK_EDIT_MODE = 0x0040; // QuickEdit mode
        const uint ENABLE_INSERT_MODE = 0x0020;     // Insert mode
        const uint ENABLE_MOUSE_INPUT = 0x0010;     // Mouse input mode

        const int STD_INPUT_HANDLE = -10;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        public static void Main(string[] args)
        {
            DisableQuickEditMode();
        }

        public static void DisableQuickEditMode()
        {
            IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            if (GetConsoleMode(consoleHandle, out uint consoleMode)) {
                consoleMode &= ~ENABLE_QUICK_EDIT_MODE; // Clear the QuickEdit mode flag
                SetConsoleMode(consoleHandle, consoleMode);
            }

            Console.WriteLine("QuickEdit mode is disabled. The application won't pause if you click in the console window.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

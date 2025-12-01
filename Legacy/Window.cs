using System.Runtime.InteropServices;

namespace Legacy
{
    public class Window
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern bool SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private const int SW_MAXIMIZE = 3;
        private const int MF_BYCOMMAND = 0x00000000;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000;

        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x00010000;
        public static void SetParameters()
        {
            IntPtr consoleWindow = GetConsoleWindow();
            ShowWindow(consoleWindow, SW_MAXIMIZE);
            DisableAllWindowControls(consoleWindow);
        }
        private static void DisableAllWindowControls(IntPtr consoleWindow)
        {
            IntPtr sysMenu = GetSystemMenu(consoleWindow, false);

            DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);

            DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);

            DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);

            int style = GetWindowLong(consoleWindow, GWL_STYLE);
            style &= ~WS_MAXIMIZEBOX;
            SetWindowLong(consoleWindow, GWL_STYLE, style);
        }
    }
}

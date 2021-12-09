using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace UiApprovalTest.TestFramework
{
    public class TestClass
    {
        private const int WM_CLOSE = 16;
        private const int BN_CLICKED = 245;
        private const int WM_SETTEXT = 12;

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
        private static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public static void ClickButton(int hwnd, string windowTitle)
        {
            SetActiveWindow((IntPtr)hwnd);
            IntPtr hwndChild = FindWindowEx((IntPtr)hwnd, IntPtr.Zero, "ThunderRT6CommandButton", windowTitle);

            if ((int)hwndChild == 0)
            {
                throw new AssertException("No button was found with name " + windowTitle);
            }

            _ = SendMessage(hwndChild, BN_CLICKED, 0, IntPtr.Zero);
        }

        public static string GetText(IntPtr hWnd)
        {
            int length = GetWindowTextLength(hWnd);
            var sb = new StringBuilder(length + 1);
            _ = GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        public static Bitmap GetWindowBitmap(int hwnd)
        {
            GetWindowRect((IntPtr)hwnd, out RECT rc);

            var bitmap = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            using var bitmapGraphics = Graphics.FromImage(bitmap);
            var hdcBitmap = bitmapGraphics.GetHdc();

            PrintWindow((IntPtr)hwnd, hdcBitmap, 0);

            bitmapGraphics.ReleaseHdc(hdcBitmap);

            return bitmap;
        }

        public static void TypeText(int hwnd, string windowTitle, string text)
        {
            IntPtr hwndChild = FindWindowEx((IntPtr)hwnd, IntPtr.Zero, "ThunderRT6TextBox", windowTitle);
            if ((int)hwndChild == 0)
            {
                throw new AssertException("No text box was found with name " + windowTitle);
            }
            _ = SendMessage(hwndChild, WM_SETTEXT, IntPtr.Zero, text);
        }

        public static void CloseWindow(string windowName)
        {
            int hwnd = FindWindow(string.Empty, windowName);
            if (hwnd == 0)
            {
                throw new AssertException("No window could be found to close with name " + windowName);
            }

            CloseWindow(hwnd);
        }

        public static void CloseWindow(int hwnd)
        {
            _ = SendMessage((IntPtr)hwnd, WM_CLOSE, 0, IntPtr.Zero);
        }

        public static int GetWindowHandle(string windowName)
        {
            int hwnd = FindWindow(string.Empty, windowName);
            if (hwnd == 0)
            {
                throw new AssertException("Window could not be found of title " + windowName);
            }

            return hwnd;
        }

        public static void AssertThatWindowMatchesExpectedState(int window, string className, string testName, string stepName)
        {
            var bitmap = GetWindowBitmap(window);

            const string FILE_PATH_FORMAT = @"..\..\..\..\OrderCore.Client.UiTests\Approval\{0}_{1}_{2}_{3}.bmp";
            string actualPath = string.Format(FILE_PATH_FORMAT, className, testName, stepName, "actual");
            string expectedPath = string.Format(FILE_PATH_FORMAT, className, testName, stepName, "expected");

            bitmap.Save(actualPath);
            var actualBytes = File.ReadAllBytes(actualPath);
            if (!File.Exists(expectedPath))
            {
                throw new AssertException($"Expected to find file {expectedPath} but could not find it; new image at {actualPath} has been saved. Please review and rename if appropriate.");
            }
            var expectedBytes = File.ReadAllBytes(expectedPath);

            Assert.AreEqual(actualBytes, expectedBytes);

            File.Delete(actualPath);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        private int _Left;
        private int _Top;
        private int _Right;
        private int _Bottom;

        public RECT(RECT Rectangle) : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
        {
        }
        public RECT(int Left, int Top, int Right, int Bottom)
        {
            _Left = Left;
            _Top = Top;
            _Right = Right;
            _Bottom = Bottom;
        }

        public int X
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Y
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Left
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Top
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Right
        {
            get { return _Right; }
            set { _Right = value; }
        }
        public int Bottom
        {
            get { return _Bottom; }
            set { _Bottom = value; }
        }
        public int Height
        {
            get { return _Bottom - _Top; }
            set { _Bottom = value + _Top; }
        }
        public int Width
        {
            get { return _Right - _Left; }
            set { _Right = value + _Left; }
        }
        public Point Location
        {
            get { return new Point(Left, Top); }
            set
            {
                _Left = value.X;
                _Top = value.Y;
            }
        }
        public Size Size
        {
            get { return new Size(Width, Height); }
            set
            {
                _Right = value.Width + _Left;
                _Bottom = value.Height + _Top;
            }
        }

        public static implicit operator Rectangle(RECT Rectangle)
        {
            return new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
        }
        public static implicit operator RECT(Rectangle Rectangle)
        {
            return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
        }
        public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
        {
            return Rectangle1.Equals(Rectangle2);
        }
        public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
        {
            return !Rectangle1.Equals(Rectangle2);
        }

        public override string ToString()
        {
            return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals(RECT Rectangle)
        {
            return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
        }

        public override bool Equals(object? Object)
        {
            if (Object is RECT rect)
            {
                return Equals(rect);
            }
            else if (Object is Rectangle rectangle)
            {
                return Equals(new RECT(rectangle));
            }

            return false;
        }
    }
}
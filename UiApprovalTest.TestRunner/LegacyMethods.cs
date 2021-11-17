using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace UiApprovalTest.TestRunner
{
    public static class LegacyMethods
    {
        public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            var gch = GCHandle.FromIntPtr(pointer);
            if (gch.Target is not List<IntPtr> list)
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            list.Add(handle);
            return true;
        }

        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            var result = new List<IntPtr>();
            var listHandle = GCHandle.Alloc(result);
            try
            {
                var childProc = new Win32Callback(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        public static string GetWinClass(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
                return null;
            var classname = new StringBuilder(100);
            var result = GetClassName(hwnd, classname, classname.Capacity);
            if (result != IntPtr.Zero)
                return classname.ToString();
            return null;
        }

        public static IEnumerable<IntPtr> EnumAllWindows(IntPtr hwnd, string childClassName)
        {
            List<IntPtr> children = GetChildWindows(hwnd);
            if (children == null)
            {
                MessageBox.Show("HEY");
                yield break;
            }
            foreach (IntPtr child in children)
            {
                if (GetWinClass(child) == childClassName)
                {
                    MessageBox.Show("nong");
                    yield return child;
                }
                foreach (var childchild in EnumAllWindows(child, childClassName))
                {
                    MessageBox.Show("man");
                    yield return childchild;
                }
            }
        }

        public static List<AutomationControl> ListAllChildren(IntPtr hwnd)
        {
            var children = GetChildWindows(hwnd);
            var list = new List<AutomationControl>();
            if (children == null)
            {
                return list;
            }
            foreach (IntPtr child in children)
            {
                string className = GetWinClass(child);
                list.Add(new AutomationControl
                {
                    ClassName = className,
                    Parent = hwnd,
                    WindowHandle = child
                });
                var childList = ListAllChildren(child);
                list.AddRange(childList);
            }
            return list;
        }
    }
}

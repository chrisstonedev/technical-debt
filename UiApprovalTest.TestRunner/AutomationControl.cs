using System;

namespace UiApprovalTest.TestRunner
{
    public class AutomationControl
    {
        public string ClassName { get; internal set; }
        public IntPtr Parent { get; internal set; }
        public IntPtr WindowHandle { get; internal set; }
    }
}

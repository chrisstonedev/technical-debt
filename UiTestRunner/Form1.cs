using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using UiTestRunner.TestFramework;

namespace UiTestRunner
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void RunClientTestButton_Click(object sender, EventArgs e)
        {
            var classes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace.EndsWith(".Tests", StringComparison.Ordinal))
                .ToArray();

            var list = new List<MethodInfo>();

            foreach (var type in classes)
            {
                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    if (method.GetCustomAttribute(typeof(TestRunAttribute), false) != null)
                    {
                        list.Add(method);
                    }
                }
            }

            foreach (var method in list)
            {
                try
                {
                    await (method.Invoke(null, null) as Task);
                    SetTestStatus(method.Name, true, DateTime.Now.ToString(new CultureInfo("en-US")));
                }
                catch (Exception ex)
                {
                    SetTestStatus(method.Name, false, ex.ToString());
                }
            }
        }

        private void SetTestStatus(string testName, bool wasSuccessful, string data)
        {
            var status = wasSuccessful ? "Success" : "Failed";
            foreach (DataGridViewRow row in testResultsDataGridView.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(testName, StringComparison.Ordinal))
                {
                    row.Cells[1].Value = status;
                    row.Cells[2].Value = data;
                    return;
                }
            }
            _ = testResultsDataGridView.Rows.Add(testName, status, data);
        }

        private void CloseProgramButton_Click(object sender, EventArgs e)
        {
            TestClass.CloseWindow(windowTitleTextBox.Text);
        }

        private void CheckProgramIsOpenButton_Click(object sender, EventArgs e)
        {
            try
            {
                _ = TestClass.GetWindowHandle(windowTitleTextBox.Text);
                _ = MessageBox.Show("The program is open! Tests are ready to run.", "TestWinAPI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (AssertException)
            {
                if (MessageBox.Show("Couldn't find the program. Do you want to start it?", "TestWinAPI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var process = Process.Start(@"..\..\..\..\OrderCore.Client\bin\Debug\net5.0-windows\OrderCore.Client.exe");
                    windowControlTextBox.Text = ((int)process.Handle).ToString(new CultureInfo("en-US"));
                }
            }
        }

        private void ListWindowControlsButton_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();

            try
            {
                var hwnd = windowControlTextBox.TextLength > 0
                    ? int.Parse(windowControlTextBox.Text, new CultureInfo("en-US"))
                    : TestClass.GetWindowHandle(windowTitleTextBox.Text);
                var children = LegacyMethods.ListAllChildren((IntPtr)hwnd);

                foreach (var something in children)
                {
                    _ = dataGridView.Rows.Add(something.WindowHandle, something.ClassName, something.Parent);
                }
            }
            catch (AssertException)
            {
                _ = MessageBox.Show("Couldn't find the program.", "TestWinAPI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}

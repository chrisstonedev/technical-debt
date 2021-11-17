using System.Threading.Tasks;
using UiTestRunner.TestFramework;

namespace OrderCore.Client.UiTests
{
    public class OrderCoreClientTests : TestClass
    {
        private const int MILLISECOND_DELAY_BETWEEN_STEPS = 250;

        [TestRun]
        public static async Task SuccessfulOrderTest()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            try
            {
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                var window = GetWindowHandle("Order Client");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderCoreClientTests", "SuccessfulOrderTest", "Ready");

                mainWindow.ClickButton("StartButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.TypeText("DataTextBox", "0142");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderCoreClientTests", "SuccessfulOrderTest", "NumericEntry");

                mainWindow.ClickButton("SendButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.TypeText("DataTextBox", "baba");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderCoreClientTests", "SuccessfulOrderTest", "CharacterEntry");

                mainWindow.ClickButton("SendButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderCoreClientTests", "SuccessfulOrderTest", "Confirmation");

                mainWindow.ClickButton("SendButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderCoreClientTests", "SuccessfulOrderTest", "Finished");
            }
            finally
            {
                mainWindow.Close();
            }
        }

        [TestRun]
        public static async Task DataValidationTest()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            try
            {
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                var window = GetWindowHandle("Order Client");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.ClickButton("StartButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.TypeText("DataTextBox", "aaaa");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.ClickButton("SendButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderCoreClientTests", "DataValidationTest", "TextInCustomer");

                mainWindow.TypeText("DataTextBox", "0142");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.ClickButton("SendButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.TypeText("DataTextBox", "b");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.ClickButton("SendButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderCoreClientTests", "DataValidationTest", "ProductTooShort");

                mainWindow.TypeText("DataTextBox", "123456789012");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.ClickButton("SendButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderCoreClientTests", "DataValidationTest", "ProductTooLong");

                mainWindow.TypeText("DataTextBox", "12&34");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.ClickButton("SendButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                mainWindow.ClickButton("SendButton");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderCoreClientTests", "DataValidationTest", "IncorrectXml");
            }
            finally
            {
                mainWindow.Close();
            }
        }
    }
}

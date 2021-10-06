using System.Diagnostics;
using System.Threading.Tasks;
using UiTestRunner.TestFramework;

namespace UiTestRunner.Tests
{
    public class OrderClientTests : TestClass
    {
        private const string EXECUTABLE_PATH = @"..\..\..\..\OrderCore.Client\bin\Debug\net5.0-windows\OrderCore.Client.exe";
        private const int MILLISECOND_DELAY_BETWEEN_STEPS = 250;

        [TestRun]
        public static async Task SuccessfulOrderTest()
        {
            _ = Process.Start(EXECUTABLE_PATH);
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            var window = GetWindowHandle("Order Client");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            AssertThatWindowMatchesExpectedState(window, "SuccessfulOrderTest", "Ready");

            ClickButton(window, "S&tart");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            TypeText(window, "", "0142");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            AssertThatWindowMatchesExpectedState(window, "SuccessfulOrderTest", "NumericEntry");

            ClickButton(window, "&Send");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            TypeText(window, "", "baba");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            AssertThatWindowMatchesExpectedState(window, "SuccessfulOrderTest", "CharacterEntry");

            ClickButton(window, "&Send");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            AssertThatWindowMatchesExpectedState(window, "SuccessfulOrderTest", "Confirmation");

            ClickButton(window, "&Submit");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            AssertThatWindowMatchesExpectedState(window, "SuccessfulOrderTest", "Finished");

            CloseWindow(window);
        }

        [TestRun]
        public static async Task DataValidationTest()
        {
            _ = Process.Start(EXECUTABLE_PATH);
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            var window = GetWindowHandle("Order Client");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            ClickButton(window, "S&tart");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            TypeText(window, "", "aaaa");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            ClickButton(window, "&Send");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            AssertThatWindowMatchesExpectedState(window, "DataValidationTest", "TextInCustomer");

            TypeText(window, "", "0142");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            ClickButton(window, "&Send");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            TypeText(window, "", "b");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            ClickButton(window, "&Send");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            AssertThatWindowMatchesExpectedState(window, "DataValidationTest", "ProductTooShort");

            TypeText(window, "", "123456789012");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            ClickButton(window, "&Send");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            AssertThatWindowMatchesExpectedState(window, "DataValidationTest", "ProductTooLong");

            TypeText(window, "", "12&34");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            ClickButton(window, "&Send");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            ClickButton(window, "&Submit");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            AssertThatWindowMatchesExpectedState(window, "DataValidationTest", "IncorrectXml");

            CloseWindow(window);
        }
    }
}

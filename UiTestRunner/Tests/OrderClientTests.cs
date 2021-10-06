using System.Diagnostics;
using System.Threading.Tasks;
using UiTestRunner.TestFramework;

namespace UiTestRunner.Tests
{
    public class OrderClientTests : TestClass
    {
        [TestRun]
        public static async Task SuccessfulOrderTest()
        {
            const int MillisecondsDelay = 250;

            Process.Start(@"..\..\..\..\OrderClient\OrderClient.exe");
            await Task.Delay(MillisecondsDelay);
            int window = GetWindowHandle("Order Client");
            await Task.Delay(MillisecondsDelay);
            AssertThatWindowMatchesExpectedState(window, "SuccessfulOrderTest", "Ready");

            ClickButton(window, "S&tart");
            await Task.Delay(MillisecondsDelay);
            TypeText(window, "", "0142");
            await Task.Delay(MillisecondsDelay);
            AssertThatWindowMatchesExpectedState(window, "SuccessfulOrderTest", "NumericEntry");

            ClickButton(window, "&Send");
            await Task.Delay(MillisecondsDelay);
            TypeText(window, "", "baba");
            await Task.Delay(MillisecondsDelay);
            AssertThatWindowMatchesExpectedState(window, "SuccessfulOrderTest", "CharacterEntry");

            ClickButton(window, "&Send");
            await Task.Delay(MillisecondsDelay);
            AssertThatWindowMatchesExpectedState(window, "SuccessfulOrderTest", "Confirmation");

            ClickButton(window, "&Submit");
            await Task.Delay(MillisecondsDelay);
            AssertThatWindowMatchesExpectedState(window, "SuccessfulOrderTest", "Finished");

            CloseWindow(window);
        }

        [TestRun]
        public static async Task DataValidationTest()
        {
            const int MillisecondsDelay = 250;

            Process.Start(@"..\..\..\..\OrderClient\OrderClient.exe");
            await Task.Delay(MillisecondsDelay);
            int window = GetWindowHandle("Order Client");
            await Task.Delay(MillisecondsDelay);
            ClickButton(window, "S&tart");
            await Task.Delay(MillisecondsDelay);
            TypeText(window, "", "aaaa");
            await Task.Delay(MillisecondsDelay);
            ClickButton(window, "&Send");
            await Task.Delay(MillisecondsDelay);
            AssertThatWindowMatchesExpectedState(window, "DataValidationTest", "TextInCustomer");

            TypeText(window, "", "0142");
            await Task.Delay(MillisecondsDelay);
            ClickButton(window, "&Send");
            await Task.Delay(MillisecondsDelay);
            TypeText(window, "", "b");
            await Task.Delay(MillisecondsDelay);
            ClickButton(window, "&Send");
            await Task.Delay(MillisecondsDelay);
            AssertThatWindowMatchesExpectedState(window, "DataValidationTest", "ProductTooShort");

            TypeText(window, "", "123456789012");
            await Task.Delay(MillisecondsDelay);
            ClickButton(window, "&Send");
            await Task.Delay(MillisecondsDelay);
            AssertThatWindowMatchesExpectedState(window, "DataValidationTest", "ProductTooLong");

            TypeText(window, "", "12&34");
            await Task.Delay(MillisecondsDelay);
            ClickButton(window, "&Send");
            await Task.Delay(MillisecondsDelay);
            ClickButton(window, "&Submit");
            await Task.Delay(MillisecondsDelay);
            AssertThatWindowMatchesExpectedState(window, "DataValidationTest", "IncorrectXml");

            CloseWindow(window);
        }
    }
}

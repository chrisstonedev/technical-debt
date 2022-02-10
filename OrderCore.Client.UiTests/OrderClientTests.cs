using System.Diagnostics;
using UiApprovalTest.TestFramework;

namespace OrderCore.Client.UiTests
{
    public class OrderClientTests : TestClass
    {
        private const int MILLISECOND_DELAY_BETWEEN_STEPS = 250;


        private const string VB6_PROGRAM_ID 
            = @"..\..\..\..\OrderClient\OrderClient.exe";

        [TestRun]
        public static async Task SuccessfulOrderTest()
        {
            _ = Process.Start(VB6_PROGRAM_ID);
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            var window = GetWindowHandle("Order Client");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);

            try
            {
                AssertThatWindowMatchesExpectedState(window, "OrderClientTests", "SuccessfulOrderTest", "Ready");

                ClickButton(window, "S&tart");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                TypeText(window, "", "0142");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderClientTests", "SuccessfulOrderTest", "NumericEntry");

                ClickButton(window, "&Send");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                TypeText(window, "", "baba");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderClientTests", "SuccessfulOrderTest", "CharacterEntry");

                ClickButton(window, "&Send");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderClientTests", "SuccessfulOrderTest", "Confirmation");

                ClickButton(window, "&Submit");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderClientTests", "SuccessfulOrderTest", "Finished");
            }
            finally
            {
                CloseWindow(window);
            }
        }

        [TestRun]
        public static async Task DataValidationTest()
        {
            _ = Process.Start(VB6_PROGRAM_ID);
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
            var window = GetWindowHandle("Order Client");
            await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);

            try
            {
                ClickButton(window, "S&tart");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                TypeText(window, "", "aaaa");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                ClickButton(window, "&Send");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderClientTests", "DataValidationTest", "TextInCustomer");

                TypeText(window, "", "0142");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                ClickButton(window, "&Send");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                TypeText(window, "", "b");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                ClickButton(window, "&Send");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderClientTests", "DataValidationTest", "ProductTooShort");

                TypeText(window, "", "123456789012");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                ClickButton(window, "&Send");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderClientTests", "DataValidationTest", "ProductTooLong");

                TypeText(window, "", "12&34");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                ClickButton(window, "&Send");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                ClickButton(window, "&Submit");
                await Task.Delay(MILLISECOND_DELAY_BETWEEN_STEPS);
                AssertThatWindowMatchesExpectedState(window, "OrderClientTests", "DataValidationTest", "IncorrectXml");
            }
            finally
            {
                CloseWindow(window);
            }
        }
    }
}

using System;

namespace OrderCore.Server.Functions
{
    public class Order
    {
        private string lineBuffer;

        public Order()
        {
            lineBuffer = new string(' ', 52);
            DateOrdered = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string Customer
        {
            get => lineBuffer.Substring(0, 10);
            set => lineBuffer = (value + new string(' ', 10)).Substring(0, 10) + lineBuffer.Substring(10);
        }

        public string DateOrdered
        {
            get => lineBuffer.Substring(10, 19);
            set => lineBuffer = lineBuffer.Substring(0, 10) + (value + new string(' ', 19)).Substring(0, 19) + lineBuffer.Substring(29);
        }

        public string Price
        {
            get => lineBuffer.Substring(29, 8);
            set => lineBuffer = lineBuffer.Substring(0, 29) + (value + new string(' ', 8)).Substring(0, 8) + lineBuffer.Substring(37);
        }

        public string Product
        {
            get => lineBuffer.Substring(37, 10);
            set => lineBuffer = lineBuffer.Substring(0, 37) + (value + new string(' ', 10)).Substring(0, 10) + lineBuffer.Substring(47);
        }

        public string Quantity
        {
            get => lineBuffer.Substring(47, 5);
            set => lineBuffer = lineBuffer.Substring(0, 47) + (value + new string(' ', 5)).Substring(0, 5);
        }

        public void FromDatabaseFormat(string lineBuffer)
        {
            this.lineBuffer = lineBuffer;
        }

        public string ToDatabaseFormat()
        {
            return lineBuffer;
        }
    }
}
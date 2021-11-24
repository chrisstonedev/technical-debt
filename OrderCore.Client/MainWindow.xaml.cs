using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace OrderCore.Client
{
    public partial class MainWindow : Window
    {
        private const int port = 187;

        private string activeFieldId = string.Empty;
        private string activeFieldName;
        private List<Response> allResponses;
        private Socket socket;

        private delegate void ParameterizedMethodInvoker(string arg);

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ClickButton(string buttonName)
        {
            var button = LayoutRoot.FindName(buttonName) as ButtonBase;
            button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "OrderCore Client", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                QuestionLabel.Content = "Click Start to begin!";
                ErrorLabel.Content = string.Empty;
                ChangeActiveView(OrderView.Start);
            }
        }

        public void TypeText(string textBoxName, string text)
        {
            var textBox = LayoutRoot.FindName(textBoxName) as TextBox;
            textBox.Text = text;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string strTemp;
            if (activeFieldId.Length > 0 && activeFieldId != "\0")
            {
                Send(socket, "T" + activeFieldId + DataTextBox.Text);
                DisableButtons();
            }
            else
            {
                strTemp = "<xml>";
                foreach (var objResponse in allResponses)
                {
                    strTemp += $"<response id=\"{objResponse.FieldId}\">{objResponse.UserResponse}</response>";
                }
                strTemp += "</xml>";
                Send(socket, "F" + strTemp);
                DisableButtons();
            }
        }

        private void DisableButtons()
        {
            CancelButton.IsEnabled = false;
            DataTextBox.IsEnabled = false;
            SendButton.IsEnabled = false;
            StartButton.IsEnabled = false;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Send(socket, "S");
            activeFieldId = string.Empty;
            allResponses = new List<Response>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartClient();
        }

        private void HandleDataArrival(string returnData)
        {
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                _ = Dispatcher.Invoke(new ParameterizedMethodInvoker(HandleDataArrival), returnData);
                return;
            }

            var strData = returnData;
            string strData2;
            string strData3;
            strData2 = strData.Substring(0, 1);
            strData = strData[1..];
            switch (strData2)
            {
                case "C": // Connection request
                    StatusLabel.Content = "Status: Connected to Server";
                    QuestionLabel.Content = "Click to Start!";
                    ChangeActiveView(OrderView.Start);
                    break;
                case "R": // Request for more data
                    strData3 = strData.Length > 0 ? strData.Substring(0, 1) : string.Empty;
                    strData = strData.Length > 0 ? strData[1..] : strData;
                    if (activeFieldId.Length > 0)
                    {
                        allResponses.Add(new Response
                        {
                            FieldId = activeFieldId,
                            FieldName = activeFieldName,
                            UserResponse = DataTextBox.Text
                        });
                    }
                    activeFieldId = strData3;
                    if (activeFieldId.Length > 0 && activeFieldId != "\0" && activeFieldId != string.Empty)
                    {
                        activeFieldName = strData.Substring(0, 10).Trim();
                        strData = strData[10..].Trim();

                        QuestionLabel.Content = strData;
                        ChangeActiveView(OrderView.TextInput);
                    }
                    else
                    {
                        QuestionLabel.Content = "Please confirm all fields, then submit:";
                        foreach (var objResponse in allResponses)
                        {
                            QuestionLabel.Content += $"\r\n{objResponse.FieldName}: {objResponse.UserResponse}";
                        }
                        ChangeActiveView(OrderView.Confirmation);
                    }
                    break;
                case "F": // Finished order
                    QuestionLabel.Content = "Order has been complete!";
                    ChangeActiveView(OrderView.Start);
                    break;
                case "E": // Error on input
                    ErrorLabel.Content = strData;
                    CancelButton.IsEnabled = true;
                    SendButton.IsEnabled = true;
                    if (DataTextBox.IsVisible)
                    {
                        DataTextBox.IsEnabled = true;
                        DataTextBox.SelectAll();
                        _ = DataTextBox.Focus();
                    }
                    else
                    {
                        _ = SendButton.Focus();
                    }
                    break;
                default:
                    break;
            }
        }

        private void StartClient()
        {
            try
            {
                IPAddress localIp;
                using (var localSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    localSocket.Connect("8.8.8.8", 65530);
                    var endPoint = localSocket.LocalEndPoint as IPEndPoint;
                    localIp = endPoint.Address;
                }

                var remoteEP = new IPEndPoint(localIp, port);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), socket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                Console.WriteLine("Socket connected to " + client.RemoteEndPoint.ToString());
                Receive(client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                var state = new StateObject
                {
                    workSocket = client
                };
                _ = client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    _ = state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    _ = client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    if (state.sb.Length > 0)
                    {
                        string value = state.sb.ToString();
                        state.sb.Length = 0;
                        HandleDataArrival(value);
                    }
                }
                Receive(client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket client, string data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            _ = client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        private void ChangeActiveView(OrderView newView)
        {
            ErrorLabel.Content = string.Empty;
            SendButton.Content = newView == OrderView.Confirmation ? "_Submit" : "_Send";
            StartButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
            DataTextBox.IsEnabled = true;
            SendButton.IsEnabled = true;
            DataTextBox.Text = string.Empty;
            StartButton.Visibility = SetVisibility(newView == OrderView.Start);
            CancelButton.Visibility = SetVisibility(newView != OrderView.Start);
            DataTextBox.Visibility = SetVisibility(newView == OrderView.TextInput);
            SendButton.Visibility = SetVisibility(newView != OrderView.Start);
            ItemSelection.Visibility = SetVisibility(newView == OrderView.SelectItem);
            SetFocusForNewView(newView);

            static Visibility SetVisibility(bool shouldBeVisible) => shouldBeVisible ? Visibility.Visible : Visibility.Hidden;

            void SetFocusForNewView(OrderView change) => _ = change switch
            {
                OrderView.Start => StartButton.Focus(),
                OrderView.TextInput => DataTextBox.Focus(),
                OrderView.Confirmation or OrderView.SelectItem => SendButton.Focus(),
                _ => false,
            };
        }

        public class StateObject
        {
            public Socket workSocket;
            public const int BufferSize = 256;
            public byte[] buffer = new byte[BufferSize];
            public StringBuilder sb = new();
        }

        public enum OrderView
        {
            Start,
            TextInput,
            Confirmation,
            SelectItem
        }

        private void GingerbreadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CookieButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CaneButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChampagneButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CocoaButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EggnogButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
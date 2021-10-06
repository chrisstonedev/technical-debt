using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OrderCore.Client
{
    public partial class MainWindow : Window
    {
        private string m_strFieldID;
        private List<Response> m_objResponses;
        private readonly TcpClient clientSocket = new();
        private NetworkStream serverStream;
        delegate void ParameterizedMethodInvoker(string arg);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "OrderCore Client", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                QuestionLabel.Content = "Click Start to begin!";
                ErrorLabel.Content = string.Empty;
                StartButton.Visibility = Visibility.Visible;
                CancelButton.Visibility = Visibility.Hidden;
                DataTextBox.Visibility = Visibility.Hidden;
                SendButton.Visibility = Visibility.Hidden;
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string strTemp;
            if (m_strFieldID.Length > 0 && m_strFieldID != "\0")
            {
                await SendDataAsync("T" + m_strFieldID + DataTextBox.Text);
                CancelButton.IsEnabled = false;
                DataTextBox.IsEnabled = false;
                SendButton.IsEnabled = false;
            }
            else
            {
                strTemp = "<xml>";
                foreach (var objResponse in m_objResponses)
                {
                    strTemp += $"<response id=\"{objResponse.FieldId}\">{objResponse.UserResponse}</response>";
                }
                strTemp += "</xml>";
                await SendDataAsync("F" + strTemp);
                CancelButton.IsEnabled = false;
                DataTextBox.IsEnabled = false;
                SendButton.IsEnabled = false;
            }
        }

        private async Task SendDataAsync(string stringData)
        {
            var outStream = Encoding.ASCII.GetBytes(stringData);
            await serverStream.WriteAsync(outStream.AsMemory(0, outStream.Length));
            await serverStream.FlushAsync();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            await SendDataAsync("S");
            m_strFieldID = "";
            m_objResponses = new List<Response>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string localIp;
            using (var localSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                localSocket.Connect("8.8.8.8", 65530);
                var endPoint = localSocket.LocalEndPoint as IPEndPoint;
                localIp = endPoint.Address.ToString();
            }

            clientSocket.Connect(localIp, 187);
            serverStream = clientSocket.GetStream();

            var ctThread = new Thread(GetMessage);
            ctThread.Start();
        }

        private void GetMessage(object obj)
        {
            while (true)
            {
                serverStream = clientSocket.GetStream();
                var inStream = new byte[clientSocket.ReceiveBufferSize];
                var buffSize = clientSocket.ReceiveBufferSize;
                _ = serverStream.Read(inStream, 0, buffSize);
                var returnData = Encoding.ASCII.GetString(inStream);
                HandleDataArrival(returnData);
            }
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
                    StartButton.Visibility = Visibility.Visible;
                    StartButton.IsEnabled = true;
                    CancelButton.Visibility = Visibility.Hidden;
                    DataTextBox.Visibility = Visibility.Hidden;
                    SendButton.Visibility = Visibility.Hidden;
                    break;
                case "R": // Request for more data
                    strData3 = strData.Substring(0, 1);
                    strData = strData[1..];
                    if (m_strFieldID.Length > 0)
                    {
                        m_objResponses.Add(new Response
                        {
                            FieldId = m_strFieldID,
                            UserResponse = DataTextBox.Text
                        });
                    }
                    m_strFieldID = strData3;
                    if (m_strFieldID.Length > 0 && m_strFieldID != "\0")
                    {
                        QuestionLabel.Content = strData;
                        ErrorLabel.Content = string.Empty;
                        StartButton.Visibility = Visibility.Hidden;
                        CancelButton.Visibility = Visibility.Visible;
                        CancelButton.IsEnabled = true;
                        DataTextBox.Visibility = Visibility.Visible;
                        DataTextBox.IsEnabled = true;
                        DataTextBox.Text = string.Empty;
                        _ = DataTextBox.Focus();
                        SendButton.Visibility = Visibility.Visible;
                        SendButton.IsEnabled = true;
                        SendButton.Content = "_Send";
                    }
                    else
                    {
                        QuestionLabel.Content = "Please confirm all fields, then submit:";
                        foreach (var objResponse in m_objResponses)
                        {
                            QuestionLabel.Content += $"\r\n{objResponse.FieldId}: {objResponse.UserResponse}";
                        }
                        ErrorLabel.Content = string.Empty;
                        StartButton.Visibility = Visibility.Hidden;
                        CancelButton.Visibility = Visibility.Visible;
                        CancelButton.IsEnabled = true;
                        DataTextBox.Visibility = Visibility.Hidden;
                        DataTextBox.IsEnabled = true;
                        DataTextBox.Text = string.Empty;
                        SendButton.Visibility = Visibility.Visible;
                        SendButton.IsEnabled = true;
                        SendButton.Content = "_Submit";
                        _ = SendButton.Focus();
                    }
                    break;
                case "F": // Finished order
                    QuestionLabel.Content = "Order has been complete!";
                    ErrorLabel.Content = string.Empty;
                    StartButton.Visibility = Visibility.Visible;
                    CancelButton.Visibility = Visibility.Hidden;
                    DataTextBox.Visibility = Visibility.Hidden;
                    SendButton.Visibility = Visibility.Hidden;
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

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            clientSocket.Close();
            serverStream.Close();
        }
    }
}

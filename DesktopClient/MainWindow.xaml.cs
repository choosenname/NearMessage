using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7079/chat")
                .Build();

            connection.On<string, string>("Receive", Receive);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // подключемся к хабу
                await connection.StartAsync();
                chatbox.Items.Add("Вы вошли в чат");
                sendBtn.IsEnabled = true;
            }
            catch (Exception ex)
            {
                chatbox.Items.Add(ex.Message);
            }
        }

        private void Receive(string user, string message)
        {
            Dispatcher.Invoke(() =>
            {
                var newMessage = $"{user}: {message}";
                chatbox.Items.Insert(0, newMessage);
            });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // отправка сообщения
                await connection.InvokeAsync("Send", userTextBox.Text, messageTextBox.Text);
            }
            catch (Exception ex)
            {
                chatbox.Items.Add(ex.Message);
            }
        }
    }
}

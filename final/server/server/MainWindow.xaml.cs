using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() //main constructor
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //running connection(runServer) in anther thread
            runServer runserver = new runServer(this);
            Thread server = new Thread(runserver.run) { IsBackground = true };
            server.Start();
        }

        //this function show a message in the main window like errors and connections steps 
        public void DisplayMessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { textBox1.Text = message + "\n" + textBox1.Text; });
        }
    }
}

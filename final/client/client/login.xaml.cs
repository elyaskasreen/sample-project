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
using System.Windows.Shapes;
using System.Threading;

namespace client
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    /// 
    public partial class login : Window
    {

        public MainWindow mainwindow;

        //constructor
        public login(MainWindow mainwindow)
        {
            InitializeComponent();
            this.mainwindow = mainwindow;
        }

        //cancel and close window
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //send username and password to server to match
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.UserName = textBox1.Text;
           mainwindow.password =passwordBox1.Password;
            mainwindow.runclient.sendpassword();
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        //receives matching result
        public void matchingresult(bool verified)
        {
            if (verified)
            {
               
                this.Dispatcher.BeginInvoke((ThreadStart)delegate() 
                {
                    mainwindow.verified();
                    this.Close(); 
                });
            }
            else
            {
                MessageBox.Show("username or password are wrong please try again");
                this.Dispatcher.BeginInvoke((ThreadStart)delegate() 
                {
                    passwordBox1.Password = "";
                    button1.IsEnabled = true;
                    button2.IsEnabled = true;
                });
                
            }
        }
    }
}

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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        MainWindow mainwindow;

        //constructor
        public ChangePassword(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
        }

        //cancel changing and close window
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //ask server to change password and send old and new password
        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (passwordBox1.Password == passwordBox2.Password && passwordBox1.Password != "")
                {
                    string userID = mainwindow.UserID;
                    string[] cells = new string[5];
                    cells[0] = "2251";
                    cells[1] = userID;
                    cells[2] = oldpassword.Password;
                    cells[3] = passwordBox1.Password;

                    mainwindow.runclient.m.send(cells);
                }
                else
                {
                    MessageBox.Show("new password don't match Please reWrite");
                    passwordBox1.Clear();
                    passwordBox2.Clear();
                }
            }
            catch { }
        }

        //getting changing result from server
        public void result(string[] cells)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
            {
                if (bool.Parse(cells[1]))
                {
                    this.Close();
                }
                if (!bool.Parse(cells[1]))
                {
                    MessageBox.Show("wronge Password Please Try again");
                    oldpassword.Clear();
                    passwordBox1.Clear();
                    passwordBox2.Clear();
                }
            });
        }

    }
}

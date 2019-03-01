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

namespace client
{
    /// <summary>
    /// Interaction logic for sendtest.xaml
    /// </summary>
    public partial class sendtest : Window
    {
        MainWindow mainwindow;

        //constructor
        public sendtest(MainWindow mainwindow)
        {
            InitializeComponent();
            this.mainwindow = mainwindow;
        }

        //send connection test
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.runclient.send("3");
        }

        //show message that connection test done succesfully
        public void showmessage()
        {
            MessageBox.Show("test request had don");
        }


    }
}

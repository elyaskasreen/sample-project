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
    /// Interaction logic for EmpTimeInOut.xaml
    /// </summary>
    public partial class EmpTimeInOut : Window
    {
        MainWindow mainwindow;

        //constructor
        public EmpTimeInOut(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
        }

        //functions to do when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshlists();
        }

        //display messages in window messages textbox
        public void showmessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { txt_message.Text = message + "\n" + txt_message.Text; });
        }

        //receives a list of employees's names and there location (in OR out work)
        public void listsfill(string[] cells)
        {
            if (cells[0] == "1212")
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    list_in.Items.Clear();
                    showmessage("logged in Employees list");
                    for (int i = 1; i + 1 < cells.Count(); i += 2)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = cells[i + 1];
                        item.Tag = int.Parse(cells[i]);

                        list_in.Items.Add(item);
                    }
                });
            }
            else if (cells[0] == "1211")
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    list_out.Items.Clear();
                    showmessage("logged out Employees list");
                    for (int i = 1; i + 1 < cells.Count(); i += 2)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = cells[i + 1];
                        item.Tag = int.Parse(cells[i]);

                        list_out.Items.Add(item);
                    }
                });
            }
        }

        //refresh lists of the employees
        private void refreshlists()
        {
            mainwindow.runclient.send("211");
            mainwindow.runclient.send("212");
        }

        //ask server to register employee exit
        private void list_in_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                ListBoxItem item = (ListBoxItem)list_in.SelectedItem;

                string[] cells = new string[2];
                cells[0] = "231";
                cells[1] = item.Tag.ToString();
                mainwindow.runclient.send(cells);
            }
            catch
            {

            }
            refreshlists();
        }

        //ask server to register employee login
        private void list_out_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListBoxItem item = (ListBoxItem)list_out.SelectedItem;
                string[] cells = new string[2];
                cells[0] = "221";
                cells[1] = item.Tag.ToString();
                mainwindow.runclient.send(cells);
            }
            catch
            {
            }
            refreshlists();
        }
    }
}

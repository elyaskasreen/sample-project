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
using System.Timers;
using System.Windows.Threading;

namespace client
{
    /// <summary>
    /// Interaction logic for Kitchen.xaml
    /// </summary>
    public partial class Kitchen : Window
    {
        MainWindow mainwindow;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();//refresh timer

        //constructer
        public Kitchen(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
        }

        //functions to do when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainwindow.runclient.send("711");
            dispatcherTimer.Tick += new EventHandler(getFoodDetails);//set function to timer
            dispatcherTimer.Interval = new TimeSpan(0,0,10);//set timer period
            dispatcherTimer.Start();//timer start

        }

        //display messages in window messages textbox
        public void showmessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { txt_message.Text = message + "\n" + txt_message.Text; });
        }

        //ask server to send list of unserved food
        private void getFoodDetails(object sender, EventArgs e)
        {
            mainwindow.runclient.send("711");
        }

        //receives unserved food list and create button for each
        public void fillPanels(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    stackPanel1.Children.Clear();
                    int table1NUM = 0;
                    WrapPanel wrapP = new WrapPanel();

                    for (int i = 0; i + 4 < cells.Length; i += 4)
                    {
                        int table2NUM = int.Parse(cells[i + 2]);
                        Button button = new Button();
                        button.Height = 40;
                        button.Tag = cells[i + 1];
                        button.Content = "  (" + cells[i + 4] + ")  " + cells[i + 3] + "  ";
                        button.Click += new RoutedEventHandler(btn_fooddetails);
                        if (table1NUM != table2NUM)
                        {
                            wrapP = new WrapPanel();
                            wrapP.Height = 40;
                            Button btn_tablenum = new Button();
                            btn_tablenum.Height = 40;
                            btn_tablenum.Content = "Table (" + table2NUM + ") :";
                            btn_tablenum.Width = 70;
                            btn_tablenum.Background = new SolidColorBrush(Colors.LightGreen);
                            btn_tablenum.Click += new RoutedEventHandler(btn_serveAllLine);
                            wrapP.Children.Add(btn_tablenum);
                            wrapP.Children.Add(button);
                            stackPanel1.Children.Add(wrapP);
                            table1NUM = table2NUM;
                        }
                        else
                        {
                            wrapP.Children.Add(button);
                        }
                    }
                });
            }
            catch { showmessage("ERROR 711 cant,t fill Panel "); }
        }

        //ask server to change specific food status to served
        public void btn_fooddetails(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string[] cells = new string[2];
                cells[0] = "731";
                cells[1] = btn.Tag.ToString();
                mainwindow.runclient.send(cells);
            }
            catch { showmessage("ERROR 731 cann't serve one food"); }
        }

        //ask server to change specific table foods to served
        public void btn_serveAllLine(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn_table = (Button)sender;
                WrapPanel wrapP = (WrapPanel)btn_table.Parent;
                string[] cells = new string[wrapP.Children.Count];
                cells[0] = "732";
                int i = 1;
                foreach (Button c in wrapP.Children)
                {
                    if (c != btn_table)
                    {
                        cells[i] = c.Tag.ToString();
                        i++;
                    }
                }
                mainwindow.runclient.send(cells);
            }
            catch { showmessage("ERROR 732 cann't serve Table"); }
        }

        //stop timer when user close the window
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dispatcherTimer.Stop();
        }


    }
}

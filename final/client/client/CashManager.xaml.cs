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
    /// Interaction logic for CashManager.xaml
    /// </summary>
    public partial class CashManager : Window
    {
        MainWindow mainwindow;
        
        //constructer
        public CashManager(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
        }

        //functions to do when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            buildColumns(dataGrid1);
            getAccounts();
            getTransfer();
            getUsers();
            setDate();
        }

        public void showmessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { txt_message.Text = message + "\n" + txt_message.Text; });
        }//display messages in window messages textbox
        private void buildColumns(DataGrid datagrid)
        {
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "In_Money";
                c1.Binding = new Binding("in_money");
                c1.Width = 60;
                datagrid.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "Out_Money";
                c2.Width = 60;
                c2.Binding = new Binding("out_money");
                datagrid.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "Account";
                c3.Width = 70;
                c3.Binding = new Binding("accountName");
                datagrid.Columns.Add(c3);
                DataGridTextColumn c4 = new DataGridTextColumn();
                c4.Header = "Explanation";
                c4.Width = 200;
                c4.Binding = new Binding("explanation");
                datagrid.Columns.Add(c4);
                DataGridTextColumn c5 = new DataGridTextColumn();
                c5.Header = "Date";
                c5.Width = 70;
                c5.Binding = new Binding("date");
                datagrid.Columns.Add(c5);
                DataGridTextColumn c6 = new DataGridTextColumn();
                c6.Header = "TransferType";
                c6.Width = 90;
                c6.Binding = new Binding("transferType");
                datagrid.Columns.Add(c6);
                DataGridTextColumn c7 = new DataGridTextColumn();
                c7.Header = "User";
                c7.Width = 70;
                c7.Binding = new Binding("username");
                datagrid.Columns.Add(c7);

        }//build DataGrid columns

        private void getcashdetails()
        {
            try
            {
                string start = "NULL";
                string end = "NULL";
                string transferID = "NULL";
                string accountID = "NULL";
                string userID = "NULL";

                try
                {
                    start = ((DateTime)date_start.SelectedDate).ToString();
                }
                catch { }
                try
                {
                    end = ((DateTime)date_end.SelectedDate).ToString();
                }
                catch { }
                try
                {
                    transferID = (cmb_transfer.SelectedItem as ComboBoxItem).Tag.ToString();
                }
                catch { }
                try
                {
                    accountID = (cmb_account.SelectedItem as ComboBoxItem).Tag.ToString();
                }
                catch { }
                try
                {
                    userID = (cmb_user.SelectedItem as ComboBoxItem).Tag.ToString();
                }
                catch { }

                mainwindow.runclient.send("911", start, end,transferID,accountID,userID);
            }
            catch { showmessage("ERROR 911 getcashdetails()"); }
        }//ask server to send list of cashmoney registers after applying filters
        private void getAccounts()
        {
            mainwindow.runclient.send("912");
        }//ask server to send list of accounts's names
        private void getTransfer()
        {
            mainwindow.runclient.send("913");
        }//ask server to send list of transfering types
        private void getUsers()
        {
            mainwindow.runclient.send("914");
        }//ask server to send list of users's names

        public void fillAccountsCombo(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    for (int i = 0; i + 2 < cells.Length; i += 2)
                    {
                        ComboBoxItem CBI = new ComboBoxItem();
                        ComboBoxItem CBI2 = new ComboBoxItem();
                        CBI.Tag = cells[i + 1];
                        CBI.Content = cells[i + 2];
                        CBI2.Tag = cells[i + 1];
                        CBI2.Content = cells[i + 2];
                        cmb_account.Items.Add(CBI);
                        cmb_add_account.Items.Add(CBI2);
                    }
                    cmb_add_account.SelectedIndex = 0;
                });
            }
            catch { }
        }//receives accounts's names and fill combo with it
        public void fillTransferCombo(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    for (int i = 0; i + 2 < cells.Length; i += 2)
                    {
                        ComboBoxItem CBI = new ComboBoxItem();
                        ComboBoxItem CBI2 = new ComboBoxItem();
                        CBI.Tag = cells[i + 1];
                        CBI.Content = cells[i + 2];
                        CBI2.Tag = cells[i + 1];
                        CBI2.Content = cells[i + 2];
                        cmb_transfer.Items.Add(CBI);
                        cmb_add_transfer.Items.Add(CBI2);
                    }
                    cmb_add_transfer.SelectedIndex = 0;
                });
            }
            catch { }
        }//receives transfering types and fill combo with it
        public void fillUsersCombo(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    for (int i = 0; i + 2 < cells.Length; i += 2)
                    {
                        ComboBoxItem CBI = new ComboBoxItem();
                        CBI.Tag = cells[i + 1];
                        CBI.Content = cells[i + 2];
                        cmb_user.Items.Add(CBI);
                    }
                });
            }
            catch { }
        }//receives users's names and fill combo with it
        public void fillcashGrid(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    dataGrid1.ItemsSource = null;
                    int in_money = 0;
                    int out_money = 0;

                    List<CashMoneyItem> list = new List<CashMoneyItem>();
                    for (int i = 0; i+7 < cells.Length;i+=7 )
                    {
                        CashMoneyItem CMI = new CashMoneyItem();
                        CMI.in_money = int.Parse(cells[i+1]);
                        CMI.out_money = int.Parse(cells[i + 2]);
                        CMI.accountName = cells[i + 3];
                        CMI.explanation = cells[i + 4];
                        CMI.date = cells[i + 5];
                        CMI.transferType = cells[i + 6];
                        CMI.username = cells[i + 7];

                        in_money += CMI.in_money;
                        out_money += CMI.out_money;

                        list.Add(CMI);
                    }
                    lbl_inMoney.Content = in_money;
                    lbl_outMoney.Content = out_money;
                    lbl_total.Content = in_money - out_money;
                    dataGrid1.ItemsSource = list;
                });
            }
            catch { showmessage("ERROR 1911 fillcashGrid"); }
        }//receives cashMoney registers and fill DataGrid with it
        public void setDate()
        {
            int month = DateTime.Today.Month;
            int year = DateTime.Today.Year;
            date_start.SelectedDate = DateTime.Parse("1/"+month+"/"+year);
            date_end.SelectedDate = DateTime.Parse(DateTime.DaysInMonth(year, month) + "/" + month + "/" + year);
        }//set date on window to today

        private void addNewREG()
        {
            try
            {
                string userId = "1";// mainwindow.userid.ToString();
                int inMoney = 0;
                int outMoney = 0;
                int.TryParse(txt_in_money.Text, out inMoney);
                int.TryParse(txt_out_money.Text, out outMoney);
                string accountID = (cmb_add_account.SelectedItem as ComboBoxItem).Tag.ToString();
                string explanation = txt_explanation.Text;
                string transferID = (cmb_add_transfer.SelectedItem as ComboBoxItem).Tag.ToString();
                mainwindow.runclient.send("921",userId, inMoney.ToString(), outMoney.ToString(), accountID, explanation, transferID);
            }
            catch { showmessage("ERROR 921  Missing Informations addNewREG"); }
        }//ask server to add anew register

        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            getcashdetails();
        }//get registers button click
        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            cmb_account.SelectedItem = null;
            cmb_transfer.SelectedItem = null;
            cmb_user.SelectedItem = null;
            date_start.SelectedDate = null;
            date_end.SelectedDate = null;
        }//clear filters button click
        private void btn_addNewREG_Click(object sender, RoutedEventArgs e)
        {
            addNewREG();
        }//add new register button click

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                var paginator = new RandomTabularPaginator(dataGrid1.Items.Count,
              new Size(printDialog.PrintableAreaWidth,
                printDialog.PrintableAreaHeight), dataGrid1, "Cash Money");

                printDialog.PrintDocument(paginator, "Cash Money");
            }
        }//print DataGrid contents
    }

    //class of cashMoney registers uses to fill DataGrid
    public class CashMoneyItem
    {
        public int in_money { get; set; }
        public int out_money { get; set; }
        public string accountName { get; set; }
        public string explanation { get; set; }
        public string date { get; set; }
        public string transferType { get; set; }
        public string username { get; set; }
    }
}

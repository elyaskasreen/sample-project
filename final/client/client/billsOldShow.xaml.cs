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
    /// Interaction logic for billsOldShow.xaml
    /// </summary>
    public partial class billsOldShow : Window
    {
        MainWindow mainwindow;
        public billsOldShow(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
        }

        //functions to do when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            buildColumns1(grid_billsdetails);//build bill details DataGrid Columns
            buildColumns2(grid_oldbills); ;//build saved Bills DataGrid Columns
            getcaptains();//ask server to send list of captains
        }
        public void showmessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { txt_message.Text = message + "\n" + txt_message.Text; });
        }//display message in the window
        private void buildColumns1(DataGrid datagrid)
        {
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "ArabicName";
                c1.Binding = new Binding("ArabicName");
                c1.Width = 80;
                datagrid.Columns.Add(c1);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "Amount";
                c3.Width = 60;
                c3.Binding = new Binding("Amount");
                datagrid.Columns.Add(c3);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "Price";
                c2.Width = 50;
                c2.Binding = new Binding("Price");
                datagrid.Columns.Add(c2);
                DataGridTextColumn c4 = new DataGridTextColumn();
                c4.Header = "SUM";
                c4.Width = 70;
                c4.Binding = new Binding("SUM");
                datagrid.Columns.Add(c4);
        }//build bill details DataGrid Columns
        private void buildColumns2(DataGrid datagrid)
        {
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "BillID";
            c1.Binding = new Binding("billID");
            c1.Width = 40;
            datagrid.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Table#";
            c2.Width = 50;
            c2.Binding = new Binding("tableNumber");
            datagrid.Columns.Add(c2);
            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "FinalValue";
            c3.Width = 70;
            c3.Binding = new Binding("billFinalValue");
            datagrid.Columns.Add(c3);
            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "Date";
            c4.Width = 70;
            c4.Binding = new Binding("startdate");
            datagrid.Columns.Add(c4);
        }//build saved Bills DataGrid Columns

        private void getcaptains()
        {
            mainwindow.runclient.send("613");
        }//ask server to send captain's names
        private void getoldbills()
        {
            mainwindow.runclient.send("611","0");
        }//ask server to send list of saved bills
        private void getBillsByDate()
        {
            try
            {
                string fromDate = DateTime.Today.ToString();
                string toDate = DateTime.Today.ToString();
                try
                {
                    fromDate=((DateTime)datePicker1.SelectedDate).ToString();
                }
                catch { datePicker1.SelectedDate = DateTime.Today; }
                try
                {
                    toDate=((DateTime)datePicker2.SelectedDate).ToString();
                }
                catch { datePicker2.SelectedDate = DateTime.Today; }
                mainwindow.runclient.send("611", "1", fromDate, toDate);
            }
            catch { showmessage("ERROR getBillsByDate"); }
        }//ask server to send list of saved bills filterd by date
        private void getBillsByCaptain()
        {
            string captainID = (cmb_captain.SelectedItem as ComboBoxItem).Tag.ToString();
            mainwindow.runclient.send("611", "2", captainID);
        }//ask server to send list of saved bills filterd by captain name
        private void getBillsByCapANDDate()
        {
            try
            {
                string captainID = (cmb_captain.SelectedItem as ComboBoxItem).Tag.ToString();
                string fromDate = DateTime.Today.ToString();
                string toDate = DateTime.Today.ToString();
                try
                {
                    fromDate = ((DateTime)datePicker1.SelectedDate).ToString();
                }
                catch { datePicker1.SelectedDate = DateTime.Today; }
                try
                {
                    toDate = ((DateTime)datePicker2.SelectedDate).ToString();
                }
                catch { datePicker2.SelectedDate = DateTime.Today; }
                mainwindow.runclient.send("611", "3", fromDate, toDate ,captainID);
            }
            catch { showmessage("ERROR getBillsByCapANDDate"); }
        }//ask server to send list of saved bills filterd by captain and date
        private void getolddetails(string billnum)
        {
            mainwindow.runclient.send("612",billnum.ToString());
        }//ask server to send list of specific bill details

        private void clearLabels()
        {
            lbl_allDiscount.Content = "";
            lbl_allfinal.Content = "";
            lbl_allNet.Content = "";
            lbl_allTax1.Content = "";
            lbl_allTax2.Content = "";
            lbl_captain.Content = "";
            lbl_client.Content = "";
            lbl_discount.Content = "";
            lbl_endDate.Content = "";
            lbl_finalTotal.Content = "";
            lbl_NetTotal.Content = "";
            lbl_startDate.Content = "";
            lbl_table.Content = "";
            lbl_tax1.Content = "";
            lbl_tax2.Content = "";
        }//clearing all lables in the window

        public void fillcaptaincombo(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    for (int i = 0; i + 2 < cells.Length; i += 2)
                    {
                        ComboBoxItem cmi = new ComboBoxItem();
                        cmi.Content = cells[i + 2];
                        cmi.Tag = cells[i + 1];
                        cmb_captain.Items.Add(cmi);
                    }
                    cmb_captain.SelectedIndex = 0;
                });

            }
            catch { showmessage("can't fill captain combo"); }


        }//receives captains's names and fill captain combo with it
        public void fillbillsgrid(string[] cells)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    clearLabels();
                    grid_oldbills.ItemsSource = null;
                    List<oldbillsItems> list = new List<oldbillsItems>();
                    int allnet = 0;
                    int alltax1 = 0;
                    int alltax2 = 0;
                    int alldiscount = 0;
                    int allfinal = 0;
                    for (int i = 0; i + 12 < cells.Length; i += 12)
                    {
                        oldbillsItems OBI = new oldbillsItems();

                        OBI.billID = cells[i + 1];
                        OBI.billFinalValue = cells[i + 2];
                        OBI.tableNumber = cells[i + 3];
                        OBI.startdate = cells[i + 4];
                        OBI.enddate = cells[i + 5];
                        OBI.discount = cells[i + 6];
                        OBI.tax1 = cells[i + 7];
                        OBI.tax2 = cells[i + 8];
                        OBI.billNetValue = cells[i + 9];
                        OBI.captain = cells[i + 10];
                        OBI.client = cells[i + 11];
                        OBI.username = cells[i + 12];
                        allnet += int.Parse(OBI.billNetValue);
                        alltax1 += int.Parse(OBI.tax1);
                        alltax2 += int.Parse(OBI.tax2);
                        alldiscount += int.Parse(OBI.discount);
                        allfinal += int.Parse(OBI.billFinalValue);

                        list.Add(OBI);
                    }
                    lbl_allNet.Content = allnet.ToString();
                    lbl_allTax1.Content = alltax1.ToString();
                    lbl_allTax2.Content = alltax2.ToString();
                    lbl_allDiscount.Content = alldiscount.ToString();
                    lbl_allfinal.Content = allfinal.ToString();
                    grid_oldbills.ItemsSource = list;
                });
        }//receives saved bills list and fill DataGrid with it
        public void fillbillsdetailsgrid(string[] cells)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
            {
                grid_billsdetails.ItemsSource = null;
                List<foodDetailsItem> list = new List<foodDetailsItem>();

                for (int i = 0; i + 4 < cells.Length; i += 4)
                {
                    foodDetailsItem FDI = new foodDetailsItem();

                    FDI.ArabicName = cells[i + 1];
                    FDI.Price = cells[i + 2];
                    FDI.Amount = cells[i + 3];
                    FDI.SUM = cells[i + 4];


                    list.Add(FDI);
                }
                grid_billsdetails.ItemsSource = list;
            });
        }//receives saved bill details list and fill DataGrid with it

        //ask server to send a specific saved bill details and change lables values to the new choosen bill values
        private void grid_oldbills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                    {
                        oldbillsItems ODI = (oldbillsItems)grid_oldbills.SelectedItem;
                        if (ODI == null)
                        {
                            grid_billsdetails.ItemsSource = null;
                        }
                        else
                        {
                            getolddetails(ODI.billID);
                            lbl_captain.Content = ODI.captain;
                            lbl_discount.Content = ODI.discount;
                            lbl_endDate.Content = ODI.enddate;
                            lbl_finalTotal.Content = ODI.billFinalValue;
                            lbl_NetTotal.Content = ODI.billNetValue;
                            lbl_startDate.Content = ODI.startdate;
                            lbl_table.Content = ODI.tableNumber;
                            lbl_tax1.Content = ODI.tax1;
                            lbl_tax2.Content = ODI.tax2;
                            lbl_client.Content = ODI.client;
                        }
                    });
            }
            catch
            {
                mainwindow.DisplayMessage("ERROR: grid_oldbills selection");
            }
        }

        private void btn_all_Click(object sender, RoutedEventArgs e)
        {
            getoldbills();
        }//ask server to send list of saved bills
        private void btn_date_Click(object sender, RoutedEventArgs e)
        {
            getBillsByDate();
        }//ask server to send list of saved bills filterd by date
        private void btn_captain_Click(object sender, RoutedEventArgs e)
        {
            getBillsByCaptain();
        }//ask server to send list of saved bills filterd by captain name
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            getBillsByCapANDDate();
        }//ask server to send list of saved bills filterd by captain and date

        //print choosen bill when user double click on it
        public void printBill()
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    BillsPrint billsprint = new BillsPrint();
                    oldbillsItems OBI = (grid_oldbills.SelectedItem as oldbillsItems);
                    if (OBI == null) { showmessage("ERROR Cann't Print Please Choose a Bill"); }
                    else
                    {
                        billsprint.dataGrid1.ItemsSource = grid_billsdetails.ItemsSource;
                        billsprint.dataGrid1.Height = 25 + grid_billsdetails.Items.Count * 20;

                        billsprint.lbl_num.Content = OBI.billID;
                        billsprint.lbl_date.Content = OBI.startdate;
                        billsprint.lbl_netsum.Content = OBI.billNetValue;
                        billsprint.lbl_discount.Content = OBI.discount;
                        billsprint.lbl_tax1.Content = OBI.tax1;
                        billsprint.lbl_tax2.Content = OBI.tax2;
                        billsprint.lbl_sum.Content = OBI.billFinalValue;
                        billsprint.ShowDialog();
                    }
                });
            }
            catch { showmessage("ERROR Cann't Print"); }
        }
        private void grid_oldbills_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            printBill();
        }
    }

    //class of saved bill informations uses to fill DataGrid
    public class oldbillsItems
    {
        public string billID { get; set; }
        public string billFinalValue { get; set; }
        public string tableNumber { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string discount { get; set; }
        public string tax1 { get; set; }
        public string tax2 { get; set; }
        public string billNetValue { get; set; }
        public string captain { get; set; }
        public string client { get; set; }
        public string username { get; set; }
    }
}

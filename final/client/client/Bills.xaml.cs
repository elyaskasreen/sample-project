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
    /// Interaction logic for Bills.xaml
    /// </summary>
    public partial class Bills : Window
    {
        MainWindow mainwindow;
        List<foodBUT> foodlist;//list of menu foods to choose between
        tableBUT choosentable;//choosen table to send id to server when needed

        //constructor
        public Bills(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
        }

        //functions to do when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            buildColumns(grid_fooddetails);//building DataGrid Columns
            getallfood();//ask server to send list of menu food
            gettables();//ask server to send list of open tables
            getcaptains();//ask server to send list captain names
            getCountingUnits();//ask server to send list of counting units
            getclients();//ask server to send list clients's names
        }

        public void showmessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { txt_message.Text = message + "\n" + txt_message.Text; });
        }//show messages in the window

        //get food list
        public void AllFoodListFill(string[] cells)
        {

            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {

                    foodlist = new List<foodBUT>();
                    for (int i = 0; i + 4 < cells.Length; i += 4)
                    {

                        foodBUT newbut = new foodBUT();
                        newbut.Height = 30;
                        newbut.Width = 80;
                        newbut.foodID = int.Parse(cells[i + 1]);
                        newbut.parentID = int.Parse(cells[i + 2]);
                        newbut.arabicName = cells[i + 3];
                        newbut.isAParent = bool.Parse(cells[i + 4]);
                        newbut.Content = newbut.arabicName;
                        newbut.Click += new RoutedEventHandler(btn_allfood_click);
                        foodlist.Add(newbut);

                    }
                    fillFoodPanel(0);
                });
        }//function receives menu food and fill it in food list
        private void btn_allfood_click(object sender, RoutedEventArgs e)//menu food button click (add food to table OR open new category items)
        {
            foodBUT but = (foodBUT)sender;
            if (but.isAParent)
            {
                
                fillFoodPanel(but.foodID);
            }
            else
            {
                string []boxes=new string [2];
                boxes[0] = "Insert Amount Please";
                boxes[1] = "Amont";
                GetValue getvalue = new GetValue(boxes);
                boxes = getvalue.show();
                if (boxes[0] == "NULL") {}
                else
                {
                    addfooddetails(but.foodID, int.Parse(boxes[0]));
                }
                fillFoodPanel(0);
            }
            
        }
        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            fillFoodPanel(0);
        }//refresh button back to the main menu food category
        private void fillFoodPanel(int num)
        {
            wpan_allfood.Children.Clear();

            foreach (foodBUT T in foodlist)
            {
                if (T.parentID == num)
                {
                    wpan_allfood.Children.Add(T);
                }
            }
        }//costructing button for every menu food in specific categiry
        private void getallfood()
        {
            mainwindow.runclient.send("512");
        }//ask server to send list of menu foods

        //get table list
        private void gettables()
        {
            mainwindow.runclient.send("511");
        }//ask server to send list of open tables
        public void fillTables(string[] cells)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    wPan_Tabels.Children.Clear();
                    for (int i = 0; i + 3 < cells.Length; i += 3)
                    {
                        tableBUT tablebut = new tableBUT();
                        tablebut.Height = 30;
                        tablebut.Width = 80;
                        tablebut.tempBillID = int.Parse(cells[i + 1]);
                        tablebut.tableNumber = int.Parse(cells[i + 2]);
                        tablebut.captinName = cells[i + 3];
                        tablebut.Content = cells[i + 2];
                        wPan_Tabels.Children.Add(tablebut);
                        tablebut.Click += new RoutedEventHandler(btn_Tablechoose_click);
                    }
                    grid_fooddetails.Items.Clear();
                });

        }//costructing button for every open table
        private void btn_Tablechoose_click(object sender, RoutedEventArgs e)
        {
            tableBUT but = (tableBUT)sender;
            try
            {
                choosentable.ClearValue(Control.BackgroundProperty);
            }
            catch { }
            choosentable = but;
            choosentable.Background = new SolidColorBrush(Colors.LightGreen);

            getTableDetails(but.tempBillID);
            fillFoodPanel(0);
        }//table button click set the click table to the public variable "choosetable"

        //get table details
        private void getTableDetails(int tempBillID)
        {
            mainwindow.runclient.send("513", tempBillID.ToString());
        }//ask server to send list of a specific table details
        public void fillTableDetails(string[] cells)
        {
            try
            {

                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    int total = 0;
                    grid_fooddetails.Items.Clear();
                    txt_discount.Text = "0";
                    for (int i = 0; i+5 < cells.Length; i+=5)
                    {
                        DataGridRow dgr = new DataGridRow();
                        foodDetailsItem fdi = new foodDetailsItem();

                        fdi.ArabicName = cells[i + 1];
                        fdi.Amount = cells[i + 3];
                        fdi.Price = cells[i + 2];
                        fdi.SUM = cells[i + 4];
                        dgr.Item = fdi;
                        total += int.Parse(cells[i + 4]);

                        if (bool.Parse(cells[i + 5])) { dgr.Background = new SolidColorBrush(Colors.LightGreen); }
                        else { dgr.Background = new SolidColorBrush(Colors.LightPink); }

                        lbl_netTotal.Content = total.ToString();
                        int tax1 = Convert.ToInt32(Math.Ceiling((double)total * 5 / 100));
                        int tax2 = Convert.ToInt32(Math.Ceiling((double)total * 25 / 10000));
                        lbl_tax1.Content = tax1.ToString();
                        lbl_tax2.Content = tax2.ToString();
                        lbl_finalTotal.Content = (total + tax1 + tax2).ToString();
                        grid_fooddetails.Items.Add(dgr);
                    }
                });
            }
            catch
            {
                mainwindow.DisplayMessage("ERROR fillTableDetails");
            }
        }//filling DataGrid of the table details
        private void buildColumns(DataGrid datagrid)
        {

            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
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
            });

        }//building DataGrid Columns
        private void txt_discount_TextChanged(object sender, TextChangedEventArgs e)
        {
            int discount = 0;
            if (int.TryParse(txt_discount.Text, out discount)) { }
            else { txt_discount.Text = ""; }
            int netTotal = int.Parse(lbl_netTotal.Content.ToString());
            int tax1 = int.Parse(lbl_tax1.Content.ToString());
            int tax2 = int.Parse(lbl_tax2.Content.ToString());
            int finalTotal = netTotal + tax1 + tax2 - discount;
            if (finalTotal > 0)
            {
                lbl_finalTotal.Content = finalTotal.ToString();
            }
            else { txt_discount.Text = ""; }
        }//calculating total sum while changing discount value 

        //add food to table details
        private void addfooddetails(int foodID,int amount)
        {
            try
            {
                string tablenum = choosentable.tempBillID.ToString();
                mainwindow.runclient.send("522", tablenum, foodID.ToString(),amount.ToString());
            }
            catch { showmessage("ERROR send add food details 522"); }
        }//send message to server asking to add food to table details

        //add new Table
        private void btn_addTable_Click(object sender, RoutedEventArgs e)
        {
            int tablenumber = 0;
            int captain = 0;
            string user = mainwindow.UserID.ToString();
            try
            {
                if (int.TryParse(((ComboBoxItem)cmb_captains.SelectedItem).Tag.ToString(), out captain))
                {
                    if (int.TryParse(txt_addtable.Text, out tablenumber))
                    {
                        mainwindow.runclient.send("521", tablenumber.ToString(), captain.ToString(), user);
                    }
                }
            }
            catch { showmessage("ERROR 1521 can,t add new table"); }
            txt_addtable.Clear();
        }//send message to server asking to open new table

        //get Capitans
        private void getcaptains()
        {
            mainwindow.runclient.send("514");
        }//ask server to send list of captains's names
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
                        cmb_captains.Items.Add(cmi);
                    }
                    cmb_captains.SelectedIndex = 0;
                });

            }
            catch { showmessage("can't fill captain combo"); }
        }//fill combo with captains's names

        //get Clients
        private void getclients()
        {
            mainwindow.runclient.send("516");
        }//ask server to send list of clients's names
        public void fillclientscombo(string[] cells)
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
                        cmb_clients.Items.Add(cmi);
                    }
                    cmb_clients.SelectedIndex = 4;
                });

            }
            catch { showmessage("can't fill clients combo"); }


        }//fill combo with clients's names

        //save&Print
        private void btn_saveBill_Click(object sender, RoutedEventArgs e)
        {
            int discount = 0;
            int.TryParse(txt_discount.Text, out discount);
            string clientID = (cmb_clients.SelectedItem as ComboBoxItem).Tag.ToString();
            mainwindow.runclient.send("551", choosentable.tempBillID.ToString(), discount.ToString(), clientID);
            grid_fooddetails.Items.Clear();
            lbl_netTotal.Content = "0";
            lbl_tax1.Content = "0";
            lbl_tax2.Content = "0";
            lbl_finalTotal.Content = "0";
            txt_discount.Text = "0";
        }//send message to server to saves bill 
        private void btn_saveWithPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int discount = 0;
                int.TryParse(txt_discount.Text, out discount);
                string clientID = (cmb_clients.SelectedItem as ComboBoxItem).Tag.ToString();
                mainwindow.runclient.send("552", choosentable.tempBillID.ToString(), discount.ToString(),clientID);
            }
            catch { showmessage("ERROR 552 Please choose a Table "); }
        }//send message to server to saves bill and send back printing informations
        public void printBill(string[] cells)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() 
            {
            BillsPrint billsprint = new BillsPrint();

            List<foodDetailsItem> list = new List<foodDetailsItem>();
            for (int i = 0; i+4 < cells.Length; i+=4)
            {
                foodDetailsItem FDI = new foodDetailsItem();
                FDI.ArabicName = cells[i + 2];
                FDI.Amount = cells[i + 3];
                FDI.Price=cells[i+4];
                FDI.SUM=cells[i+5];
                list.Add(FDI);
            }
            billsprint.dataGrid1.ItemsSource=list;
            billsprint.dataGrid1.Height = 25 + grid_fooddetails.Items.Count * 20;
            billsprint.lbl_num.Content = cells[1];
            int total = int.Parse(lbl_netTotal.Content.ToString());
            billsprint.lbl_date.Content = DateTime.Now.Date.ToString();
            billsprint.lbl_netsum.Content = total;
            int tax1 = Convert.ToInt32(Math.Ceiling((double)total * 5 / 100));
            int tax2 = Convert.ToInt32(Math.Ceiling((double)total * 25 / 10000));
            int discount = 0;
            int.TryParse(txt_discount.Text,out discount);
            billsprint.lbl_discount.Content = discount.ToString();
            billsprint.lbl_tax1.Content = (tax1).ToString();
            billsprint.lbl_tax2.Content = (tax2).ToString();
            billsprint.lbl_sum.Content = (total + tax1 + tax2-discount).ToString();
            billsprint.ShowDialog();
            grid_fooddetails.Items.Clear();
            lbl_netTotal.Content = "0";
            lbl_tax1.Content = "0";
            lbl_tax2.Content = "0";
            lbl_finalTotal.Content = "0";
            txt_discount.Text = "0";
            });
        }//recieve printing information message and print bill

        //add New Food To Menu
        private void addNewFood()
        {
            try
            {
                string parentID = (wpan_allfood.Children[0] as foodBUT).parentID.ToString();
                string arabicName = txt_arabicName.Text;
                string EnglishName = txt_englishName.Text;
                string unitID = "";
                try { unitID = (cmb_countingUnit.SelectedItem as ComboBoxItem).Tag.ToString(); }
                catch { }
                string Price = txt_price.Text;
                string isAParent = chk_isAParent.IsChecked.ToString();
                mainwindow.runclient.m.send("523", parentID, arabicName, EnglishName, unitID, Price, isAParent,"TRUE");
            }
            catch { showmessage("ERROR 523 addNewFood()"); }
        }//ask server to add new food to the menu
        private void getCountingUnits()
        {
            mainwindow.runclient.send("515");
        }//ask server to send a list of counting units
        public void fillCountingUnits(string[] cells)
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
                        cmb_countingUnit.Items.Add(CBI);
                    }
                    cmb_countingUnit.SelectedIndex = 0;
                });
            }
            catch { showmessage("ERROR  fillCountingUnits()"); }
        }//fill combo with counting units
        private void btn_food_add_Click(object sender, RoutedEventArgs e)
        {
            addNewFood();
        }//menu food click
        private void chk_isAParent_Checked(object sender, RoutedEventArgs e)
        {
            txt_price.Text = "";
            cmb_countingUnit.SelectedItem = null;
            txt_price.IsEnabled = false;
            cmb_countingUnit.IsEnabled = false;
        }
        private void chk_isAParent_Unchecked(object sender, RoutedEventArgs e)
        {
            txt_price.IsEnabled = true;
            cmb_countingUnit.IsEnabled = true;
        }
    }

    //class of buttons to represent menu foods and categories
    public class foodBUT : Button
    {
        public int foodID { get; set; }
        public int parentID { get; set; }
        public string arabicName { get; set; }
        public bool isAParent { get; set; }
    }

    //class of buttons to represent open tables
    public class tableBUT : Button
    {
        public int tempBillID { get; set; }
        public int tableNumber { get; set; }
        public string captinName { get; set; }
    }

    //class of table details to fill DataGrid
    public class foodDetailsItem 
    {
        public string ArabicName { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string SUM { get; set; }
    }
}

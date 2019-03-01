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
    /// Interaction logic for Salaries.xaml
    /// </summary>
    public partial class Salaries : Window
    {
        MainWindow mainwindow;
        List<salary> salaryList = new List<salary>();//list of employees salaries filled depends on data from server
        DateTime month;//choosen month send to server to get salaries data

        //constructor
        public Salaries(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
        }

        //functions to do when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            buildColumns();
            setMonth();
        }

        public void showmessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { txt_message.Text = message + "\n" + txt_message.Text; });
        }//display messages in window messages textbox
        private void buildColumns()
        {
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "accountName";
            c1.Binding = new Binding("accountName");
            c1.Width = 60;
            dataGrid1.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "empSalary";
            c2.Width = 60;
            c2.Binding = new Binding("empSalary");
            dataGrid1.Columns.Add(c2);
            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "daysbycheck";
            c3.Width = 80;
            c3.Binding = new Binding("daysbycheck");
            dataGrid1.Columns.Add(c3);
            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "Real Minuts";
            c4.Width = 60;
            c4.Binding = new Binding("realminuts");
            dataGrid1.Columns.Add(c4);
            DataGridTextColumn c5 = new DataGridTextColumn();
            c5.Header = "Minuts";
            c5.Width = 60;
            c5.Binding = new Binding("minuts");
            dataGrid1.Columns.Add(c5);
            DataGridTextColumn c6 = new DataGridTextColumn();
            c6.Header = "netSalary";
            c6.Width = 60;
            c6.Binding = new Binding("netSalary");
            dataGrid1.Columns.Add(c6);
            DataGridTextColumn c7 = new DataGridTextColumn();
            c7.Header = "bonuses";
            c7.Width = 60;
            c7.Binding = new Binding("bonuses");
            dataGrid1.Columns.Add(c7);
            DataGridTextColumn c8 = new DataGridTextColumn();
            c8.Header = "discount";
            c8.Width = 60;
            c8.Binding = new Binding("discount");
            dataGrid1.Columns.Add(c8);
            DataGridTextColumn c9 = new DataGridTextColumn();
            c9.Header = "totalSalary";
            c9.Width = 60;
            c9.Binding = new Binding("totalSalary");
            dataGrid1.Columns.Add(c9);
            DataGridTextColumn c10 = new DataGridTextColumn();
            c10.Header = "Transfered";
            c10.Width = 60;
            c10.Binding = new Binding("transferd");
            dataGrid1.Columns.Add(c10);
        }//build salaries DataGrid columns

        public void fillSalarylist1(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    salaryList = new List<salary>();
                    for (int i = 0; i + 4 < cells.Length; i += 4)
                    {
                        salary sal = new salary();
                        sal.employeeID = cells[i + 1];
                        sal.realminuts = cells[i + 2];
                        int realminuts = int.Parse(sal.realminuts);
                        int day = realminuts / 60 / 7;
                        int hours = (realminuts - (day * 60 * 7)) / 60;
                        int min = (realminuts % (60 * 7)) % 60;
                        sal.daysbycheck = day.ToString() + " Days " + hours.ToString() + ":" + min.ToString();
                        sal.accountName = cells[i + 3];
                        sal.empSalary = cells[i + 4];
                        salaryList.Add(sal);
                    }
                });
                mainwindow.runclient.send("112", month.ToString());
            }
            catch { showmessage("ERROR fillSalarylist1"); }

        }//ask server to send list of employees names that worked in specific month
        public void fillSalarylist2(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    for (int j = 0; j < salaryList.Count; j++)
                    {
                        for (int i = 0; i + 9 < cells.Length; i += 9)
                        {
                            if (salaryList[j].employeeID == cells[i + 2])
                            {
                                salaryList[j].salaryID = cells[i + 1];
                                salaryList[j].netSalary = cells[i + 3];
                                salaryList[j].minuts = cells[i + 4];
                                salaryList[j].bonuses = cells[i + 5];
                                salaryList[j].discount = cells[i + 6];
                                salaryList[j].totalSalary = cells[i + 7];
                                salaryList[j].transferd = cells[i + 8];
                                month = DateTime.Parse(cells[i + 9]);
                            }
                        }
                    }
                    dataGrid1.ItemsSource = salaryList;
                });
            }
            catch { showmessage("ERROR fillSalarylist2"); }
        }//ask server to send list of saved salaries in specific month

        private void setMonth()
        {
            DateTime month = DateTime.Today;
            DateTime date = DateTime.Parse("1/" + month.Month.ToString() + "/" + month.Year.ToString());
            datePicker1.SelectedDate = date.AddMonths(-1);
            this.month = date.AddMonths(-1);
        }//set date in the window = last month

        //ask server to send salaries 
        private void btn_getSalaries_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = (DateTime)datePicker1.SelectedDate;
                string month2 = date.Month.ToString();
                string year = date.Year.ToString();
                this.month = DateTime.Parse("1/" + month2 + "/" + year);
                mainwindow.runclient.send("111", month.ToString());
            }
            catch { showmessage("ERROR btn_getSalaries_Click"); }

        }

        //fills textboxes when DataGrid selection changed
        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                salary sal = (salary)dataGrid1.SelectedItem;
                lbl_Name.Content = sal.accountName;
                txt_bonuse.Text = sal.bonuses;
                txt_discount.Text = sal.discount;
                txt_empsalary.Text = sal.empSalary;
                txt_netSalary.Text = sal.netSalary;
                txt_totalSalary.Text = sal.totalSalary;
                txt_min.Text = sal.realminuts;
                if (txt_totalSalary.Text == "") { calculate(); }
            }
            catch
            {
                lbl_Name.Content = "Emp Name";
                txt_bonuse.Text = "0";
                txt_discount.Text = "0";
                txt_empsalary.Text = "0";
                txt_netSalary.Text = "0";
                txt_totalSalary.Text = "0";
                txt_min.Text = "0";
            }
        }

        private void calculate()
        {
            try
            {
                int minuts = 0;
                float empsalary = 0;
                int netsalary = 0;
                int discount = 0;
                int bonuse = 0;
                int total = 0;

                int.TryParse(txt_min.Text, out minuts);
                float.TryParse(txt_empsalary.Text, out empsalary);
                netsalary = Convert.ToInt32(empsalary / 12600 * minuts);
                int.TryParse(txt_discount.Text, out discount);
                int.TryParse(txt_bonuse.Text, out bonuse);
                total = netsalary + bonuse - discount;
                txt_netSalary.Text = netsalary.ToString();
                txt_totalSalary.Text = total.ToString();
                txt_bonuse.Text = bonuse.ToString();
                txt_discount.Text = discount.ToString();
            }
            catch { showmessage("ERROR calculate"); }
        }//calculate salaries according to working time of the employee

        private void btn_calculate_Click(object sender, RoutedEventArgs e)
        {
            calculate();
        }//calculate button

        private void txt_bonuse_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculate();
        }//calculate when bounse textbox value changed

        private void btn_savePerm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int valid = 0;
                salary sal = (salary)dataGrid1.SelectedItem;
                if (sal.transferd != "True")
                {
                    string[] cells = new string[11];
                    cells[0] = "121";
                    cells[1] = sal.employeeID;
                    cells[2] = month.ToString();
                    cells[3] = txt_netSalary.Text;
                    int min = 0;
                    if (!int.TryParse(txt_min.Text, out min)) { valid++; }
                    cells[4] = min.ToString();
                    int bonuse = 0;
                    if (!int.TryParse(txt_bonuse.Text, out bonuse)) { valid++; }
                    cells[5] = bonuse.ToString();
                    int discount = 0;
                    if (!int.TryParse(txt_discount.Text, out discount)) { valid++; }
                    cells[6] = discount.ToString();
                    cells[7] = txt_totalSalary.Text;
                    cells[8] = "True";
                    int ID = 0;
                    int.TryParse(sal.salaryID, out ID);
                    cells[9] = ID.ToString();
                    cells[10] = mainwindow.UserID;
                    if (valid == 0)
                    {
                        mainwindow.runclient.send(cells);
                    }
                }
                else { showmessage("You Can't Edit Transfered salary"); }
            }
            catch { showmessage("ERROR btn_savePerm_Click"); }
        }//ask server to save salary permanently and register value in cashmoney

        private void btn_saveTemp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int valid = 0;
                salary sal = (salary)dataGrid1.SelectedItem;
                if (sal.transferd != "True")
                {
                    string[] cells = new string[10];
                    cells[0] = "121";
                    cells[1] = sal.employeeID;
                    cells[2] = month.ToString();
                    cells[3] = txt_netSalary.Text;
                    int min = 0;
                    if (!int.TryParse(txt_min.Text, out min)) { valid++; }
                    cells[4] = min.ToString();
                    int bonuse = 0;
                    if (!int.TryParse(txt_bonuse.Text, out bonuse)) { valid++; }
                    cells[5] = bonuse.ToString();
                    int discount = 0;
                    if (!int.TryParse(txt_discount.Text, out discount)) { valid++; }
                    cells[6] = discount.ToString();
                    cells[7] = txt_totalSalary.Text;
                    cells[8] = "False";
                    int ID = 0;
                    int.TryParse(sal.salaryID, out ID);
                    cells[9] = ID.ToString();
                    if (valid == 0)
                    {
                        mainwindow.runclient.send(cells);
                    }
                }
                else { showmessage("You Can't Edit Transfered salary"); }
            }
            catch { showmessage("ERROR btn_savePerm_Click"); }
        }//ask server to save salary temporary

    }
    //class of salary uses to fill DataGrid
    public class salary
    {
        public string accountName { get; set; }
        public string salaryID { get; set; }
        public string employeeID { get; set; }
        public string monthNUM { get; set; }
        public string netSalary { get; set; }
        public string daysbycheck { get; set; }
        public string realminuts { get; set; }
        public string minuts { get; set; }
        public string bonuses { get; set; }
        public string discount { get; set; }
        public string totalSalary { get; set; }
        public string transferd { get; set; }
        public string empSalary { get; set; }
    }
}

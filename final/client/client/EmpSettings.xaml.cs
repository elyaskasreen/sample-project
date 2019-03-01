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
    /// Interaction logic for EmpSettings.xaml
    /// </summary>
    public partial class EmpSettings : Window
    {
        MainWindow mainwindow;

        //constructer
        public EmpSettings(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
        }

        //functions to do when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sendRequestAccountNames();
            sendRequestWorkType();
        }
        public void showmessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { txt_message.Text = message + "\n" + txt_message.Text; });
        }//display messages in window messages textbox

        private void sendRequestAccountNames()
        {
            mainwindow.runclient.send("411");

        }//ask server to send list of employees accounts's names
        private void sendRequestWorkType()
        {
            mainwindow.runclient.send("413");
        }//ask server to send list of working's types

        public void fill_list(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    list_emp.Items.Clear();
                    for (int i = 1; i < cells.Length - 1; i += 2)
                    {
                        ListBoxItem LBI = new ListBoxItem();


                        try { LBI.Tag = int.Parse(cells[i]); }
                        catch { mainwindow.DisplayMessage("ERROR List int parse"); }
                        LBI.Content = cells[i + 1];

                        list_emp.Items.Add(LBI);
                    }
                });
            }
            catch { mainwindow.DisplayMessage("ERROR Primary Materials Fill list"); }


        }//receives employees accounts's names from server and fill DataGrid
        public void fillComboWorkType(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    comb_worktype.Items.Clear();
                    for (int i = 1; i < cells.Length - 1; i += 2)
                    {
                        ComboBoxItem CBI = new ComboBoxItem();
                        try { CBI.Tag = int.Parse(cells[i]); }
                        catch { mainwindow.DisplayMessage("ERROR Combo int parse"); }
                        CBI.Content = cells[i + 1];

                        comb_worktype.Items.Add(CBI);

                    }
                });
            }
            catch { mainwindow.DisplayMessage("ERROR ComboWork Type Fill combo"); }
        }//receives working's types from server and fill combo
        public void fillRecords(string[] cells)
        {
            try
            {

                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    txt_empacountname.Tag = cells[2];
                    txt_empacountname.Text = cells[3];
                    txt_empnationalnumber.Tag = cells[1];
                    txt_empnationalnumber.Text = cells[4];
                    date_birthday.Text = cells[5];
                    date_employment.Text = cells[6];
                    txt_empsalary.Text = cells[7];
                    date_demission.Text = cells[8];


                    foreach (ListBoxItem item in comb_worktype.Items)
                    {
                        if (item.Tag.ToString() == cells[9])
                        {
                            item.IsSelected = true;
                        }
                    }
                });
            }
            catch
            {
                showmessage("Problem with fill records");
            }

        }//receives employee informations and fill textboxes with it

        //ask server to send a list of specific employee informatios
        private void list_emp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBoxItem item = (ListBoxItem)list_emp.SelectedItem;
                string[] cells = new string[2];
                cells[0] = "412";
                cells[1] = item.Tag.ToString();
                mainwindow.runclient.send(cells);
            }
            catch{}
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBoxItem CBI = (ComboBoxItem)comb_worktype.SelectedItem;
                string WorkTypeId = CBI.Tag.ToString();

                string[] cells = new string[8];
                cells[0] = "431";
                cells[1] = txt_empnationalnumber.Tag.ToString();
                cells[2] = txt_empnationalnumber.Text;
                cells[3] = date_birthday.Text;
                cells[4] = date_employment.Text;
                cells[5] = txt_empsalary.Text;
                cells[6] = date_demission.SelectedDate.ToString();
                cells[7] = WorkTypeId;

                mainwindow.runclient.send(cells);
            }
            catch
            {
                showmessage("No data in the records");
            }
        }//ask server to edit employee informations
        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    txt_empacountname.Text = "";
                    txt_empnationalnumber.Text = "";
                    date_birthday.Text = "";
                    date_employment.Text = "";
                    txt_empsalary.Text = "";
                    date_demission.Text = "";
                    comb_worktype.Text = "";
                });

        }//clear informations textboxes in the window
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_empacountname.Text == "" || txt_empnationalnumber.Text == "" || date_birthday.Text == ""
                    || date_employment.Text == "" || txt_empsalary.Text == "" || comb_worktype.Text == "")
                {
                    showmessage("No data in the records");
                }

                else
                {
                    ComboBoxItem item = (ComboBoxItem)comb_worktype.SelectedItem;

                    string WorkTypeId = item.Tag.ToString();
                    string[] cells = new string[7];
                    cells[0] = "421";
                    cells[1] = txt_empacountname.Text;
                    cells[2] = txt_empnationalnumber.Text;
                    cells[3] = date_birthday.Text;
                    cells[4] = date_employment.Text;
                    cells[5] = txt_empsalary.Text;
                    cells[6] = WorkTypeId;

                    for (int i = 0; i < cells.Count(); i++)
                    {
                        showmessage(cells[i]);
                    }

                    mainwindow.runclient.send(cells);

                }

            }
            catch
            {
                showmessage("No data in the records");
            }
        }//ask server to add employee informations
        private void btn_addnewworktype_Click(object sender, RoutedEventArgs e)
        {
            if (txt_newworktype.Text != "")
            {
                string name = txt_newworktype.Text;
                string[] cells = new string[2];
                cells[0] = "422";
                cells[1] = name;
                mainwindow.runclient.send(cells);
            }
            else
            {
                showmessage(" New work data is empty");

            }

        }//ask server to add working's Types
    }
}
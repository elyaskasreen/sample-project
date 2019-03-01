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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        MainWindow mainwindow;

        //constructor
        public Settings(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
        }

        //functions to do when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            build_grid_FoodMenu();
            build_grid_permissions();
            build_grid_Users();
            build_grid_PrimaryMaterials();
            build_grid_Accounts();
            getFoodMenu();
            getCountingUnit();
            getPermisions();
            getUsers();
            getPrimaryMaterials();
            getAccounts();
        }

        //display messages in window messages textbox
        public void showmessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { txt_message.Text = message + "\n" + txt_message.Text; });
        }

        //build DataGrids Culomns
        private void build_grid_FoodMenu()
        {
            try
            {
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "arabicname";
                c1.Binding = new Binding("arabicname");
                c1.Width = 100;
                grid_foodMenu.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "price";
                c2.Width = 80;
                c2.Binding = new Binding("price");
                grid_foodMenu.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "Visible";
                c3.Width = 60;
                c3.Binding = new Binding("isvisible");
                grid_foodMenu.Columns.Add(c3);
            }
            catch { showmessage("ERROR  build_grid_FoodMenu()"); }
        }
        private void build_grid_permissions()
        {
            try
            {
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "PerName";
                c1.Binding = new Binding("perName");
                c1.Width = 50;
                grid_permissions.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "bills";
                c2.Width = 50;
                c2.Binding = new Binding("bills");
                grid_permissions.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "oldBills";
                c3.Width = 50;
                c3.Binding = new Binding("oldBills");
                grid_permissions.Columns.Add(c3);
                DataGridTextColumn c4 = new DataGridTextColumn();
                c4.Header = "store";
                c4.Width = 50;
                c4.Binding = new Binding("store");
                grid_permissions.Columns.Add(c4);
                DataGridTextColumn c5 = new DataGridTextColumn();
                c5.Header = "empSettings";
                c5.Width = 50;
                c5.Binding = new Binding("empSettings");
                grid_permissions.Columns.Add(c5);
                DataGridTextColumn c6 = new DataGridTextColumn();
                c6.Header = "empTimeInOut";
                c6.Width = 50;
                c6.Binding = new Binding("empTimeInOut");
                grid_permissions.Columns.Add(c6);
                DataGridTextColumn c7 = new DataGridTextColumn();
                c7.Header = "kitchen";
                c7.Width = 50;
                c7.Binding = new Binding("kitchen");
                grid_permissions.Columns.Add(c7);
                DataGridTextColumn c8 = new DataGridTextColumn();
                c8.Header = "foodRelations";
                c8.Width = 50;
                c8.Binding = new Binding("foodRelations");
                grid_permissions.Columns.Add(c8);
                DataGridTextColumn c9 = new DataGridTextColumn();
                c9.Header = "salaries";
                c9.Width = 50;
                c9.Binding = new Binding("salaries");
                grid_permissions.Columns.Add(c9);
                DataGridTextColumn c10 = new DataGridTextColumn();
                c10.Header = "settings";
                c10.Width = 50;
                c10.Binding = new Binding("settings");
                grid_permissions.Columns.Add(c10);
                DataGridTextColumn c11 = new DataGridTextColumn();
                c11.Header = "changePass";
                c11.Width = 50;
                c11.Binding = new Binding("changePass");
                grid_permissions.Columns.Add(c11);
                DataGridTextColumn c12 = new DataGridTextColumn();
                c12.Header = "CashManager";
                c12.Width = 50;
                c12.Binding = new Binding("CashManager");
                grid_permissions.Columns.Add(c12);
            }
            catch { showmessage("ERROR  build_grid_permissions"); }
        }
        private void build_grid_Users()
        {
            try
            {
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "username";
                c1.Binding = new Binding("username");
                c1.Width = 80;
                grid_users.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "userPerName";
                c2.Width = 80;
                c2.Binding = new Binding("userPerName");
                grid_users.Columns.Add(c2);
            }
            catch { }
        }
        private void build_grid_PrimaryMaterials()
        {
            try
            {
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "MaterialName";
                c1.Binding = new Binding("MaterialName");
                c1.Width = 100;
                grid_primaryMaterials.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "LastPrice";
                c2.Width = 80;
                c2.Binding = new Binding("LastPrice");
                grid_primaryMaterials.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "Visible";
                c3.Width = 60;
                c3.Binding = new Binding("isvisible");
                grid_primaryMaterials.Columns.Add(c3);
            }
            catch { showmessage("ERROR  build_grid_PrimaryMaterials()"); }
        }
        private void build_grid_Accounts()
        {
            try
            {
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "AccountName";
                c1.Binding = new Binding("accountName");
                c1.Width = 100;
                grid_Accounts.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "Visible";
                c2.Width = 60;
                c2.Binding = new Binding("Visible");
                grid_Accounts.Columns.Add(c2);
            }
            catch { showmessage("ERROR build_grid_Accounts()"); }
        }

        //get lists from server
        private void getFoodMenu()
        {
            mainwindow.runclient.send("2111");
        }
        private void getCountingUnit()
        {
            mainwindow.runclient.send("2112");
        }
        private void getPermisions()
        {
            mainwindow.runclient.send("2113");
        }
        private void getUsers()
        {
            mainwindow.runclient.send("2114");
        }
        private void getPrimaryMaterials()
        {
            mainwindow.runclient.send("2115");
        }
        private void getAccounts()
        {
            mainwindow.runclient.send("2116");
        }

        //fill window with data from server
        public void fillCountingUnits(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    cmb_countingUnit.Items.Clear();
                    cmb_countid.Items.Clear();
                    cmb_Count_Id.Items.Clear();
                    for (int i = 0; i + 2 < cells.Length; i += 2)
                    {
                        ComboBoxItem CBI1 = new ComboBoxItem();
                        CBI1.Tag = cells[i + 1];
                        CBI1.Content = cells[i + 2];
                        cmb_countingUnit.Items.Add(CBI1);
                        ComboBoxItem CBI2 = new ComboBoxItem();
                        CBI2.Tag = cells[i + 1];
                        CBI2.Content = cells[i + 2];
                        cmb_countid.Items.Add(CBI2);
                        ComboBoxItem CBI3 = new ComboBoxItem();
                        CBI3.Tag = cells[i + 1];
                        CBI3.Content = cells[i + 2];
                        cmb_Count_Id.Items.Add(CBI3);
                    }
                    cmb_countingUnit.SelectedIndex = 0;
                    cmb_countid.SelectedIndex = 0;
                    cmb_Count_Id.SelectedIndex = 0;
                });
            }
            catch { showmessage("ERROR  fillCountingUnits()"); }
        }
        public void fillFoodMenuGrid(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    grid_foodMenu.ItemsSource = null;
                    cmb_parent.Items.Clear();

                    List<FoodMenuItem> list = new List<FoodMenuItem>();
                    ComboBoxItem CBI1 = new ComboBoxItem();
                    CBI1.Content = "Base";
                    CBI1.Tag = 0;
                    cmb_parent.Items.Add(CBI1);

                    for (int i = 0; i + 8 < cells.Length; i += 8)
                    {
                        FoodMenuItem FMI = new FoodMenuItem();
                        FMI.FoodMenuID = int.Parse(cells[i + 1]);
                        FMI.parentID = int.Parse(cells[i + 2]);
                        FMI.arabicname = cells[i + 3];
                        FMI.unitID = cells[i + 4];
                        FMI.price = cells[i + 5];
                        FMI.isaparent = bool.Parse(cells[i + 6]);
                        FMI.isvisible = bool.Parse(cells[i + 7]);
                        FMI.englishName = cells[i + 8];

                        list.Add(FMI);
                        if (FMI.isaparent == true)
                        {
                            ComboBoxItem CBI2 = new ComboBoxItem();
                            CBI2.Content = FMI.arabicname;
                            CBI2.Tag = FMI.FoodMenuID;
                            cmb_parent.Items.Add(CBI2);
                        }
                    }
                    cmb_parent.SelectedIndex = 0;
                    grid_foodMenu.ItemsSource = list;
                });
            }
            catch { showmessage("ERROR  fillFoodMenuGrid"); }
        }
        public void fillPermissions(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    List<permissionsItem> list = new List<permissionsItem>();
                    for (int i = 0; i + 13 < cells.Length; i += 13)
                    {
                        permissionsItem per = new permissionsItem();
                        per.perID = cells[i + 1];
                        per.perName = cells[i + 2];
                        per.bills = cells[i + 3];
                        per.oldBills = cells[i + 4];
                        per.store = cells[i + 5];
                        per.empSettings = cells[i + 6];
                        per.empTimeInOut = cells[i + 7];
                        per.kitchen = cells[i + 8];
                        per.foodRelations = cells[i + 9];
                        per.salaries = cells[i + 10];
                        per.settings = cells[i + 11];
                        per.changePass = cells[i + 12];
                        per.CashManager = cells[i + 13];

                        list.Add(per);
                        ComboBoxItem CBI = new ComboBoxItem();
                        CBI.Content = per.perName;
                        CBI.Tag = per.perID;

                        cmb_userPermission.Items.Add(CBI);
                    }
                    grid_permissions.ItemsSource = list;
                });
            }
            catch { showmessage("ERROR fillPermissions"); }
        }
        public void fillUsers(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    List<UserItem> list = new List<UserItem>();
                    for (int i = 0; i + 4 < cells.Length; i += 4)
                    {
                        UserItem user = new UserItem();
                        user.userID = cells[i + 1];
                        user.username = cells[i + 2];
                        user.userPerID = cells[i + 3];
                        user.userPerName = cells[i + 4];

                        list.Add(user);
                    }
                    grid_users.ItemsSource = list;
                });
            }
            catch { showmessage("ERROR fillUsers"); }
        }
        public void fillPrimaryMaterialsGrid(string[] cells)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
            {

                grid_primaryMaterials.ItemsSource = null;
                List<PrimaryMaterialsItem> list = new List<PrimaryMaterialsItem>();
                for (int i = 0; i + 5 < cells.Length; i += 5)
                {
                    PrimaryMaterialsItem PMI = new PrimaryMaterialsItem();
                    PMI.PrimaryMaterialsID = int.Parse(cells[i + 1]);
                    PMI.MaterialName = cells[i + 2];
                    PMI.LastPrice = cells[i + 3];
                    PMI.isvisible = bool.Parse(cells[i + 4]);
                    PMI.CountingUnitId = cells[i + 5];
                    list.Add(PMI);
                }
                grid_primaryMaterials.ItemsSource = list;

            });
        }
        public void fillAccountsGrid(string[] cells)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
            {

                grid_Accounts.ItemsSource = null;
                List<Account> list = new List<Account>();
                for (int i = 0; i + 3 < cells.Length; i += 3)
                {
                    Account A = new Account();
                    A.accountId = int.Parse(cells[i + 1]);
                    A.accountName = cells[i + 2];
                    A.Visible = bool.Parse(cells[i + 3]);
                    list.Add(A);
                }
                grid_Accounts.ItemsSource = list;

            });

        }

        private void addNewFoodMenu()
        {
            try
            {
                string[] cells = new string[8];
                cells[0] = "2131";
                cells[1] = (cmb_parent.SelectedItem as ComboBoxItem).Tag.ToString();
                cells[2] = txt_arabicName.Text;
                cells[3] = txt_englishName.Text;
                cells[4] = (cmb_countingUnit.SelectedItem as ComboBoxItem).Tag.ToString();
                cells[5] = txt_price.Text;
                cells[6] = chk_isAParent.IsChecked.ToString();
                cells[7] = chk_visible.IsChecked.ToString();

                mainwindow.runclient.send(cells);
            }
            catch { showmessage("ERROR addNewFoodMenu"); }
        }//ask server to add new menu food
        private void EditFoodMenu()
        {
            try
            {
                int price = 0;
                int.TryParse(txt_price.Text, out price);
                FoodMenuItem FMI = (FoodMenuItem)grid_foodMenu.SelectedItem;
                string[] cells = new string[9];
                cells[0] = "2121";
                cells[1] = FMI.FoodMenuID.ToString();
                cells[2] = (cmb_parent.SelectedItem as ComboBoxItem).Tag.ToString();
                cells[3] = txt_arabicName.Text;
                cells[4] = txt_englishName.Text;
                cells[5] = (cmb_countingUnit.SelectedItem as ComboBoxItem).Tag.ToString();
                cells[6] = price.ToString();
                cells[7] = chk_isAParent.IsChecked.ToString();
                cells[8] = chk_visible.IsChecked.ToString();

                mainwindow.runclient.send(cells);
            }
            catch { showmessage("ERROR Edit Food Menu"); }
        }//ask server to edit menu food
        private void addNewPermission()
        {
            try
            {
                string[] cells = new string[13];
                cells[0] = "2132";
                cells[1] = txt_perName.Text;
                cells[2] = chk_bills.IsChecked.ToString();
                cells[3] = chk_oldBills.IsChecked.ToString();
                cells[4] = chk_store.IsChecked.ToString();
                cells[5] = chk_empSettings.IsChecked.ToString();
                cells[6] = chk_empTimeInOut.IsChecked.ToString();
                cells[7] = chk_kitchen.IsChecked.ToString();
                cells[8] = chk_foodRelations.IsChecked.ToString();
                cells[9] = chk_salaries.IsChecked.ToString();
                cells[10] = chk_settings.IsChecked.ToString();
                cells[11] = chk_changePassword.IsChecked.ToString();
                cells[12] = chk_cashManager.IsChecked.ToString();

                mainwindow.runclient.send(cells);
            }
            catch { showmessage("ERROR addNewPermission"); }
        }//ask server to add new permission
        private void EditPermission()
        {
            try
            {
                permissionsItem PerItem = (permissionsItem)grid_permissions.SelectedItem;
                string[] cells = new string[14];
                cells[0] = "2122";
                cells[1] = PerItem.perID;
                cells[2] = txt_perName.Text;
                cells[3] = chk_bills.IsChecked.ToString();
                cells[4] = chk_oldBills.IsChecked.ToString();
                cells[5] = chk_store.IsChecked.ToString();
                cells[6] = chk_empSettings.IsChecked.ToString();
                cells[7] = chk_empTimeInOut.IsChecked.ToString();
                cells[8] = chk_kitchen.IsChecked.ToString();
                cells[9] = chk_foodRelations.IsChecked.ToString();
                cells[10] = chk_salaries.IsChecked.ToString();
                cells[11] = chk_settings.IsChecked.ToString();
                cells[12] = chk_changePassword.IsChecked.ToString();
                cells[13] = chk_cashManager.IsChecked.ToString();
                mainwindow.runclient.send(cells);
            }
            catch { showmessage("ERROR EditPermission"); }
        }//ask server to edit permission
        private void addNewUser()
        {
            try
            {
                string[] cells = new string[4];
                cells[0] = "2133";
                cells[1] = txt_userName.Text;
                cells[2] = txt_userPassword.Text;
                cells[3] = (cmb_userPermission.SelectedItem as ComboBoxItem).Tag.ToString();

                mainwindow.runclient.send(cells);
                clearuser();
            }
            catch { showmessage("ERROR addNewUser"); }
        }//ask server to add new user
        private void EditUser()
        {
            try
            {
                string[] cells = new string[5];
                cells[0] = "2123";
                cells[1] = (grid_users.SelectedItem as UserItem).userID.ToString();
                cells[2] = txt_userName.Text;
                cells[3] = txt_userPassword.Text;
                cells[4] = (cmb_userPermission.SelectedItem as ComboBoxItem).Tag.ToString();

                mainwindow.runclient.send(cells);
                clearuser();
            }
            catch { showmessage("ERROR EditUser"); }
        }//ask server to edit user
        private void clearuser()
        {
            txt_userName.Text = "";
            txt_userPassword.Text = "";
            cmb_userPermission.SelectedIndex = 0;
        }//clear user textboxes
        private void addPrimaryMaterials()
        {
            try
            {
                string[] cells = new string[5];
                cells[0] = "822";
                cells[1] = txt_primaryMaterialName.Text;
                cells[2] = (cmb_countid.SelectedItem as ComboBoxItem).Tag.ToString();
                cells[3] = txt_lastPrice.Text;
                cells[4] = chk_primary_visible.IsChecked.ToString();
                mainwindow.runclient.send(cells);
                getPrimaryMaterials();
            }
            catch { showmessage("ERROR addPrimaryMaterials"); }
        }//ask server to add new primary materials

        //filling textboxes after DataGrid selection changed 
        private void grid_foodMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FoodMenuItem FMI = (FoodMenuItem)grid_foodMenu.SelectedItem;
                txt_arabicName.Text = FMI.arabicname;
                txt_englishName.Text = FMI.englishName;
                txt_price.Text = FMI.price;
                chk_isAParent.IsChecked = FMI.isaparent;
                chk_visible.IsChecked = FMI.isvisible;

                for (int i = 0; i < cmb_parent.Items.Count; i++)
                {
                    ComboBoxItem CBI = (ComboBoxItem)cmb_parent.Items[i];

                    if (FMI.parentID == int.Parse(CBI.Tag.ToString()))
                    {
                        cmb_parent.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < cmb_countingUnit.Items.Count; i++)
                {
                    ComboBoxItem CBI = (ComboBoxItem)cmb_countingUnit.Items[i];

                    if (FMI.unitID == CBI.Tag.ToString())
                    {
                        cmb_countingUnit.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch { }
        }
        private void grid_permissions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                permissionsItem per = (permissionsItem)grid_permissions.SelectedItem;
                txt_perName.Text = per.perName;
                chk_bills.IsChecked = bool.Parse(per.bills);
                chk_oldBills.IsChecked = bool.Parse(per.oldBills);
                chk_store.IsChecked = bool.Parse(per.store);
                chk_empSettings.IsChecked = bool.Parse(per.empSettings);
                chk_empTimeInOut.IsChecked = bool.Parse(per.empTimeInOut);
                chk_kitchen.IsChecked = bool.Parse(per.kitchen);
                chk_foodRelations.IsChecked = bool.Parse(per.foodRelations);
                chk_salaries.IsChecked = bool.Parse(per.salaries);
                chk_settings.IsChecked = bool.Parse(per.settings);
                chk_changePassword.IsChecked = bool.Parse(per.changePass);
                chk_cashManager.IsChecked = bool.Parse(per.CashManager);
            }
            catch { }
        }
        private void grid_users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UserItem user = (UserItem)grid_users.SelectedItem;
                txt_userName.Text = user.username;
                for (int i = 0; i < cmb_userPermission.Items.Count; i++)
                {
                    if (user.userPerID.ToString() == (cmb_userPermission.Items[i] as ComboBoxItem).Tag.ToString())
                    {
                        cmb_userPermission.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch { }
        }
        private void grid_primaryMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                PrimaryMaterialsItem PMI = (PrimaryMaterialsItem)grid_primaryMaterials.SelectedItem;
                txt_primaryMaterialName.Text = PMI.MaterialName;
                txt_lastPrice.Text = PMI.LastPrice;
                chk_primary_visible.IsChecked = PMI.isvisible;
                for (int i = 0; i < cmb_countid.Items.Count; i++)
                {
                    ComboBoxItem CBI = (ComboBoxItem)cmb_countid.Items[i];
                    if (PMI.CountingUnitId == CBI.Tag.ToString())
                    {
                        cmb_countid.SelectedIndex = i;
                        break;
                    }


                }
            }
            catch { }
        }
        private void grid_Accounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Account A = (Account)grid_Accounts.SelectedItem;
                txt_AccountName.Text = A.accountName;
                chk_account_visible.IsChecked = A.Visible;
            }
            catch { }
        }
        private void cmb_Count_Id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txt_CountinUnit.Text = (cmb_Count_Id.SelectedItem as ComboBoxItem).Content.ToString();
            }
            catch { }
        }

        private void btn_add_newFoodMenu_Click(object sender, RoutedEventArgs e)
        {
            addNewFoodMenu();
        }//ask server to add new menu food
        private void btn_menu_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditFoodMenu();
        }//ask server to edit menu food
        private void btn_food_clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmb_parent.SelectedIndex = 0;
                cmb_countingUnit.SelectedIndex = 0;
                txt_arabicName.Text = "";
                txt_englishName.Text = "";
                txt_price.Text = "";
                chk_isAParent.IsChecked = false;
                chk_visible.IsChecked = false;
            }
            catch { }
        }//clear food
        private void btn_per_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_perName.Text = "";
            chk_bills.IsChecked = false;
            chk_oldBills.IsChecked = false;
            chk_store.IsChecked = false;
            chk_empSettings.IsChecked = false;
            chk_empTimeInOut.IsChecked = false;
            chk_kitchen.IsChecked = false;
            chk_foodRelations.IsChecked = false;
            chk_salaries.IsChecked = false;
            chk_settings.IsChecked = false;
            chk_changePassword.IsChecked = false;
            chk_cashManager.IsChecked = false;
        }//clear permissions
        private void btn_per_addnew_Click(object sender, RoutedEventArgs e)
        {
            addNewPermission();
        }//ask server to add new permission
        private void btn_per_edit_Click(object sender, RoutedEventArgs e)
        {
            EditPermission();
        }//ask server to edit permission
        private void btn_user_clear_Click(object sender, RoutedEventArgs e)
        {
            clearuser();
        }//clear user textboxes
        private void btn_user_addNew_Click(object sender, RoutedEventArgs e)
        {
            addNewUser();
        }//ask server to add new user
        private void btn_user_edit_Click(object sender, RoutedEventArgs e)
        {
            EditUser();
        }//ask server to edit user
        private void btn_primary_add_Click(object sender, RoutedEventArgs e)
        {
            addPrimaryMaterials();
        }//ask server to add new primary materials

        private void btn_editCountingUnit_Click(object sender, RoutedEventArgs e)
        {
            string[] cells = new string[3];
            cells[0] = "2124";
            cells[1] = ((ComboBoxItem)cmb_Count_Id.SelectedItem).Tag.ToString();
            cells[2] = txt_CountinUnit.Text;
            mainwindow.runclient.send(cells);
            getCountingUnit();
        }//edit counting unit
        private void btn_editPrimaryMaterial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int lastPrice = 0;
                int.TryParse(txt_lastPrice.Text, out lastPrice);
                PrimaryMaterialsItem PMI = (PrimaryMaterialsItem)grid_primaryMaterials.SelectedItem;
                string[] cells = new string[6];
                cells[0] = "2125";
                cells[1] = PMI.PrimaryMaterialsID.ToString();
                cells[2] = txt_primaryMaterialName.Text;
                cells[3] = (cmb_countid.SelectedItem as ComboBoxItem).Tag.ToString();
                cells[4] = txt_lastPrice.Text;
                cells[5] = chk_primary_visible.IsChecked.ToString();
                mainwindow.runclient.send(cells);
                getPrimaryMaterials();
            }
            catch
            { showmessage("ERROR Edit Primary Material"); }
        }//edit primary materials
        private void btn_Edit_AccountName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account A = (Account)grid_Accounts.SelectedItem;
                string[] cells = new string[4];
                cells[0] = "2126";
                cells[1] = A.accountId.ToString();
                cells[2] = txt_AccountName.Text;
                cells[3] = chk_account_visible.IsChecked.ToString();
                mainwindow.runclient.send(cells);
            }
            catch
            { showmessage("ERROR Edit Account"); }
        }//edit accountname
        private void btn_addNewAccount_Click(object sender, RoutedEventArgs e)
        {
            if (txt_AccountName.Text == "")
            {
                showmessage("ERROR: Account Text is empty");
            }
            else
            {
                try
                {
                    string[] cells = new string[3];
                    cells[0] = "2134";
                    cells[1] = txt_AccountName.Text;
                    cells[2] = chk_account_visible.IsChecked.ToString();
                    mainwindow.runclient.send(cells);
                }
                catch { showmessage("ERROR Add New Account"); }
            }
        }//add new account
        private void btn_addNewCountingUnit_Click(object sender, RoutedEventArgs e)
        {
            if (txt_CountinUnit.Text == "")
            {
                showmessage("ERROR text Counting Unit Empty");
            }
            else
            {
                string[] cells = new string[3];
                cells[0] = "2135";
                cells[1] = txt_CountinUnit.Text;
                mainwindow.runclient.send(cells);
                getCountingUnit();
            }
        }//add new counting unit
        private void btn_counting_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_CountinUnit.Clear();
        }//clear counting units textboxes
        private void btn_ClearP_Click(object sender, RoutedEventArgs e)
        {
            txt_lastPrice.Clear();
            txt_primaryMaterialName.Clear();
            cmb_countid.SelectedIndex = 0;
            chk_primary_visible.IsChecked = false;
        }//clear permissions textboxes
        private void btn_ClearAc_Click(object sender, RoutedEventArgs e)
        {
            chk_account_visible.IsChecked = false;
            txt_AccountName.Clear();
        }//clear accounts textboxes

    }
    //class of permissions uses to fill DataGrid
    public class permissionsItem
    {
        public string perID { get; set; }
        public string perName { get; set; }
        public string bills { get; set; }
        public string oldBills { get; set; }
        public string store { get; set; }
        public string empSettings { get; set; }
        public string empTimeInOut { get; set; }
        public string kitchen { get; set; }
        public string foodRelations { get; set; }
        public string salaries { get; set; }
        public string settings { get; set; }
        public string changePass { get; set; }
        public string CashManager { get; set; }
    }

    //class of users users to fill DataGrid
    public class UserItem
    {
        public string userID { get; set; }
        public string username { get; set; }
        public string userPerID { get; set;}
        public string userPerName { get; set; }
    }

    //class of accounts uses to fill DataGrid
    public class Account
    {
        public int accountId { get; set; }
        public string accountName { get; set; }
        public bool Visible { get; set; }
    }
}

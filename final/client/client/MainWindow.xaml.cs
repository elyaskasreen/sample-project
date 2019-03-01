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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    

    public partial class MainWindow : Window
    {
        //windows assigning
        public login login ;
        public Store store;
        public sendtest sendtest;
        public EmpSettings empsettings;
        public EmpTimeInOut emptimeinout;
        public Bills bills;
        public billsOldShow billsoldshow;
        public Kitchen kitchen;
        public FoodRelations foodrelations;
        public CashManager cashmanager;
        public Salaries salaries;
        public Settings settings;
        public ChangePassword changepassword;

        public runClient runclient;//connection class will use in onther thread
        public Thread client;//connection thread
        public string UserID;
        public string UserName;
        public string password;
        public bool logedin;//boolean to know logged status
        public permissions per = new permissions();//permissions object to save permissions befor send to client

        //constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //start connection thread
            runclient = new runClient(this);
            client = new Thread(runclient.run) { IsBackground = true };
            client.Start();
        }

        //display message on main window 
        public void DisplayMessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate(){textBox1.Text = message + "\n" + textBox1.Text;});
        }

        //when server send password check and its match
        //enable all buttons that have permission from server
        public void verified()
        {
            logedin = true;
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() 
            {
                this.Title = "User is "+UserName;
                btn_login.IsEnabled = false;
                btn_logout.IsEnabled = true;
                btn_sendtest.IsEnabled = true;
                btn_store.IsEnabled = per.store;
                btn_empsettings.IsEnabled = per.empSettings;
                btn_emptimeinout.IsEnabled = per.empTimeInOut;
                btn_Bills.IsEnabled = per.bills;
                btn_oldbills.IsEnabled = per.oldBills;
                btn_kitchen.IsEnabled = per.kitchen;
                btn_relations.IsEnabled = per.foodRelations;
                btn_cashManager.IsEnabled = per.cashManager;
                btn_salaries.IsEnabled = per.salaries;
                btn_settings.IsEnabled = per.settings;
                btn_changePassword.IsEnabled = per.changePass;
            });
        }

        //unenable all buttons and close connection
        //reset username & password
        public void logout()
        {
            logedin = false;
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
            {
                runclient.client.Close();
                runclient.verified = false;
                this.Title = "Login please ";
                UserName = "";
                password = "";
                btn_login.IsEnabled = false;
                btn_logout.IsEnabled = false;
                btn_sendtest.IsEnabled = false;
                btn_store.IsEnabled = false;
                btn_empsettings.IsEnabled = false;
                btn_emptimeinout.IsEnabled = false;
                btn_Bills.IsEnabled = false;
                btn_oldbills.IsEnabled = false;
                btn_kitchen.IsEnabled = false;
                btn_relations.IsEnabled = false;
                btn_cashManager.IsEnabled = false;
                btn_salaries.IsEnabled = false;
                btn_settings.IsEnabled = false;
                btn_changePassword.IsEnabled = false;
            });
        }

        //buttons that opens windows 
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            login = new login(this);
            login.ShowDialog();
        }
        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            logout();
        }
        private void btn_sendtest_Click(object sender, RoutedEventArgs e)
        {
            sendtest = new sendtest(this);
            sendtest.ShowDialog();
        }
        private void btn_store_Click(object sender, RoutedEventArgs e)
        {
            store = new Store(this);
            store.ShowDialog();
        }
        private void btn_empsettings_Click(object sender, RoutedEventArgs e)
        {
            empsettings = new EmpSettings(this);
            empsettings.ShowDialog();
        }
        private void btn_emptimeinout_Click(object sender, RoutedEventArgs e)
        {
            emptimeinout = new EmpTimeInOut(this);
            emptimeinout.ShowDialog();
        }
        private void btn_Bills_Click(object sender, RoutedEventArgs e)
        {
            bills = new Bills(this);
            bills.ShowDialog();
        }
        private void btn_oldbills_Click(object sender, RoutedEventArgs e)
        {
            billsoldshow = new billsOldShow(this);
            billsoldshow.ShowDialog();
        }
        private void btn_kitchen_Click(object sender, RoutedEventArgs e)
        {
            kitchen = new Kitchen(this);
            kitchen.ShowDialog();
        }
        private void btn_relations_Click(object sender, RoutedEventArgs e)
        {
            foodrelations = new FoodRelations (this);
            foodrelations.ShowDialog();
        }
        private void btn_cashManager_Click(object sender, RoutedEventArgs e)
        {
            cashmanager = new CashManager(this);
            cashmanager.ShowDialog();
        }
        private void btn_salaries_Click(object sender, RoutedEventArgs e)
        {
            salaries = new Salaries(this);
            salaries.ShowDialog();
        }
        private void but_settings_Click(object sender, RoutedEventArgs e)
        {
            settings = new Settings(this);
            settings.ShowDialog();
        }
        private void btn_changePassword_Click(object sender, RoutedEventArgs e)
        {
            changepassword = new ChangePassword(this);
            changepassword.ShowDialog();
        }
    }

    //permission class to save permissions inside
    public class permissions
    {
        public bool bills { get; set; }
        public bool oldBills { get; set; }
        public bool store { get; set; }
        public bool empSettings { get; set; }
        public bool empTimeInOut { get; set; }
        public bool kitchen { get; set; }
        public bool foodRelations { get; set; }
        public bool cashManager { get; set; }
        public bool salaries { get; set; }
        public bool settings { get; set; }
        public bool changePass { get; set; }

        public permissions()
        {
            bills = false;
            oldBills = false;
            store = false;
            empSettings = false;
            empTimeInOut = false;
            kitchen = false;
            foodRelations = false;
            cashManager = false;
            salaries = false;
            settings = false;
            changePass = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Threading;

namespace client
{
    class clientOrders
    {
        runClient runclient;//decliration run client so we can reach widows and other objects
        private BinaryWriter writer;
        private BinaryReader reader;

        //constructor
        public clientOrders(BinaryWriter writer, BinaryReader reader, runClient runclient)
        {
            this.runclient = runclient;
            this.writer = writer;
            this.reader = reader;
        }

        //function that routed message to the proper function depending on the first value of the recieved array cells[0]
        public void chooseOrder(string[] cells)
        {
            try
            {
                int mission = int.Parse(cells[0]);
                switch (mission)
                {
                    case 1:
                        setUser(cells);
                        break;
                    case 2:
                        getUserPermissions(cells);
                        break;
                    case 3:
                        reciveTest();
                        break;
                    case 9:
                        showMessage(cells);
                        break;
                        ////////////////////////////////////////**Salaries**//////////////////////////////////
                    case 1111:
                        getSalaries1(cells);
                        break;
                    case 1112:
                        getSalaries2(cells);
                        break;
                    ////////////////////////////////////**EMPLOEEY IN OUT**////////////////////////////////
                    case 1211:
                        receiveEmpTimeInOutTable(cells);
                        break;
                    case 1212:
                        receiveEmpTimeInOutTable(cells);
                        break;
                    ///////////////////////////////////**STORE**/////////////////////////////////////////
                    case 1311:
                        receivePrimaryMaterials(cells);
                        break;
                    case 1312:
                        printLastRecords(cells);
                        break;
                    case 1313:
                        print_All(cells);
                        break;
                    case 1314:
                        print_Today(cells);
                        break;
                    case 1315:
                        In_Out_Quan_Sum(cells);
                        break;
                    case 1316:
                        inventoryByBill(cells);
                        break;
                    ////////////////////////////////////**EmployeeSettings**/////////////////////////////
                    case 1411:
                        getAccountNames(cells);
                        break;
                    case 1412:
                        getInfEmployees(cells);
                        break;
                    case 1413:
                        getWorkType(cells);
                        break;
                    ///////////////////////////////////////**BILLS**/////////////////////////////////////
                    case 1511:
                        getTables(cells);
                        break;
                    case 1512:
                        getallfood(cells);
                        break;
                    case 1513:
                        getTableDetails(cells);
                        break;
                    case 1514:
                        getcaptains(cells);
                        break;
                    case 1515:
                        getCountingUnits2(cells);
                        break;
                    case 1516:
                        getClients(cells);
                        break;
                    case 1552:
                        printBills(cells);
                        break;
                    ///////////////////////////////////////**Show OLD BILLS**/////////////////////////////////////
                    case 1611:
                        getOLDBills(cells);
                        break;
                    case 1612:
                        getOLDBilldetails(cells);
                        break;
                    case 1613:
                        getcaptains2(cells);
                        break;
                    ////////////////////////////////////////**Kitchen**//////////////////
                    case 1711:
                        getKitchenFood(cells);
                        break;
                    //////////////////////////////////////**FoodRelations**////////////////
                    case 1811:
                        getPmaterials(cells);
                        break;
                    case 1812:
                        getFoodMenu(cells);
                        break;
                    case 1813:
                        getrelations(cells);
                        break;
                    case 1814:
                        getCountingUnits(cells);
                        break;
                    //////////////////////////////////////**CashManager**////////////////
                    case 1911:
                        getCashDetails(cells);
                        break;
                    case 1912:
                        getAccounts(cells);
                        break;
                    case 1913:
                        getTransfer(cells);
                        break;
                    case 1914:
                        getUsers(cells);
                        break;
                    //////////////////////////////////////**Settings**////////////////
                    case 3111:
                        getFoodMenu2(cells);
                        break;
                    case 3112:
                        getCountingUnits3(cells);
                        break;
                    case 3113:
                        getPermissions(cells);
                        break;
                    case 3114:
                        getAllUsers(cells);
                        break;
                    case 3115:
                        getPrimaryMaterias(cells);
                        break;
                    case 3116:
                        getAccounts2(cells);
                        break;
                    //////////////////////////////////////**ChangePassword**////////////////
                    case 3251:
                        ChangingResult(cells);
                        break;
                    default:
                        break;
                }
            }
            catch { runclient.DisplayMessage("ERROR chooseOrder"); }
        }

        private void setUser(string[] cells)
        {
            runclient.mainwindow.UserID = cells[1];
            runclient.mainwindow.UserName = cells[2];
        }//assign user name and userid to used later
        private void getUserPermissions(string[] cells)
        {
            try
            {
                permissions per = new permissions();

                per.bills = bool.Parse(cells[1]);
                per.oldBills = bool.Parse(cells[2]);
                per.store = bool.Parse(cells[3]);
                per.empSettings = bool.Parse(cells[4]);
                per.empTimeInOut = bool.Parse(cells[5]);
                per.kitchen = bool.Parse(cells[6]);
                per.foodRelations = bool.Parse(cells[7]);
                per.salaries = bool.Parse(cells[8]);
                per.settings = bool.Parse(cells[9]);
                per.changePass = bool.Parse(cells[10]);
                per.cashManager = bool.Parse(cells[11]);

                runclient.mainwindow.per = per;
                runclient.mainwindow.verified();
            }
            catch { }
        }//get permissons of the user that will enable the allowed windows buttons
        private void reciveTest()
        {
            runclient.mainwindow.sendtest.showmessage();
        }//receives connection test message
        private void showMessage(string[] cells)
        {
            try
            {
                switch (cells[1])
                {

                    case "1":
                        runclient.mainwindow.salaries.showmessage(cells[2]);
                        break;
                    case "2":
                        runclient.mainwindow.emptimeinout.showmessage(cells[2]);
                        break;
                    case "3":
                        runclient.mainwindow.store.showmessage(cells[2]);
                        break;
                    case "4":
                        runclient.mainwindow.empsettings.showmessage(cells[2]);
                        break;
                    case "5":
                        runclient.mainwindow.bills.showmessage(cells[2]);
                        break;
                    case "6":
                        runclient.mainwindow.billsoldshow.showmessage(cells[2]);
                        break;
                    case "7":
                        runclient.mainwindow.kitchen.showmessage(cells[2]);
                        break;
                    case "8":
                        runclient.mainwindow.foodrelations.showmessage(cells[2]);
                        break;
                    case "9":
                        runclient.mainwindow.cashmanager.showmessage(cells[2]);
                        break;
                    case "10":
                        runclient.mainwindow.settings.showmessage(cells[2]);
                        break;
                    default:
                        break;
                }
            }
            catch { }
        }//display message recieved from server and send it to the right window

        ///////////////////////////////////////**Salaries**//////////////////////////////////

        private void getSalaries1(string[] cells)
        {
            runclient.mainwindow.salaries.fillSalarylist1(cells);
        }//function receives a list of employees names that worked in specific month
        private void getSalaries2(string[] cells)
        {
            runclient.mainwindow.salaries.fillSalarylist2(cells);
        }//function receives a list of saved salaries 

        //////////////////////////////////////**EMPLOEEY IN OUT**/////////////////////////////

        private void receiveEmpTimeInOutTable(string[] cells)
        {
            runclient.mainwindow.emptimeinout.listsfill(cells);
        }//recieve list of employees in and out the work in this moment

        ///////////////////////////////////**STORE**////////////////////

        private void receivePrimaryMaterials(string[] cells)
        {
            runclient.mainwindow.store.FillCombo(cells);
        }//function receives list of the Primary materials that we deal with it
        private void printLastRecords(string[] cells)
        {
            runclient.mainwindow.store.fillGrid(cells);
        }//function receives a count of the last registers of a specific material
        private void print_Today(string[] cells)
        {
            runclient.mainwindow.store.fillGrid(cells);
        }//function receives all today's registers
        private void print_All(string[] cells)
        {
            runclient.mainwindow.store.fillGrid(cells);
        }//function receives a specific count of the last registers
        private void In_Out_Quan_Sum(string[] cells)
        {
            runclient.mainwindow.store.filllabels(cells);
        }//function receives Sum of in & out quantities of specific material
        private void inventoryByBill(string[] cells)
        {
            runclient.mainwindow.store.inventoryByBill(cells);
        }//function receives a list of all materials inventory compared to the related sold materials in bills of the restaurant

        ////////////////////////////////////**EmployeeSettings**/////////////////////////////

        private void getAccountNames(string[] cells)
        {
            runclient.mainwindow.empsettings.fill_list(cells);
        }//function receives a list of employee accounts name
        private void getWorkType(string[] cells)
        {
            runclient.mainwindow.empsettings.fillComboWorkType(cells);

        }//function receives a list of working type (manager, accountant, chef .......etc)
        private void getInfEmployees(string[] cells)
        {
            runclient.mainwindow.empsettings.fillRecords(cells);

        }//function receives a list of employee information (national number, salary, birth date .......etc)

        /////////////////////////////////**BILLS**///////////////////////

        private void getallfood(string[] cells)
        {
            runclient.mainwindow.bills.AllFoodListFill(cells);
        }//function receives a list of the menu food
        private void getTables(string[] cells)
        {
            runclient.mainwindow.bills.fillTables(cells);
        }//function receives a list of opened tables
        private void getTableDetails(string[] cells)
        {
            runclient.mainwindow.bills.fillTableDetails(cells);
        }//function receives a list of tables details
        private void getcaptains(string[] cells)
        {
            runclient.mainwindow.bills.fillcaptaincombo(cells);
        }//function receives a list of captains names
        private void getClients(string[] cells)
        {
            runclient.mainwindow.bills.fillclientscombo(cells);
        }//function receives a list of clients names
        private void getCountingUnits2(string[] cells)
        {
            runclient.mainwindow.bills.fillCountingUnits(cells);
        }//function receives a list of counting units
        private void printBills(string[] cells)
        {
            runclient.mainwindow.bills.printBill(cells);
        }//function receives a list of bill details to print

        ///////////////////////////////////////**Show OLD BILLS**/////////////////////////////////////

        private void getOLDBills(string[] cells)
        {
            runclient.mainwindow.billsoldshow.fillbillsgrid(cells);
        }//function receives a list of saved bills
        private void getOLDBilldetails(string[] cells)
        {
            runclient.mainwindow.billsoldshow.fillbillsdetailsgrid(cells);
        }//function receives a list of a specific saved bill details
        private void getcaptains2(string[] cells)
        {
            runclient.mainwindow.billsoldshow.fillcaptaincombo(cells);
        }//function receives a list of captains names

        ////////////////////////////////////////**Kitchen**//////////////////

        private void getKitchenFood(string[] cells)
        {
            runclient.mainwindow.kitchen.fillPanels(cells);
        }//function receives a list of unserved materials

        //////////////////////////////////////**FoodRelations**////////////////

        private void getPmaterials(string[] cells)
        {
            runclient.mainwindow.foodrelations.fillPMarterials(cells);
        }//function receives a list of primary materials
        private void getFoodMenu(string[] cells)
        {
            runclient.mainwindow.foodrelations.fillFoodMenu(cells);
        }//function receives a list of menu foods
        private void getrelations(string[] cells)
        {
            runclient.mainwindow.foodrelations.fillrelations(cells);
        }//function receives a list of a specific 
        private void getCountingUnits(string[] cells)
        {
            runclient.mainwindow.foodrelations.fillCountingUnits(cells);
        }//function receives a list of counting units

        //////////////////////////////////////**CashManager**////////////////

        private void getCashDetails(string[] cells)
        {
            try
            {
                runclient.mainwindow.cashmanager.fillcashGrid(cells);
            }
            catch { }
        }//function receives a list of cashmoney registers
        private void getAccounts(string[] cells)
        {
            runclient.mainwindow.cashmanager.fillAccountsCombo(cells);
        }//function receives a list of accounts's names
        private void getTransfer(string[] cells)
        {
            runclient.mainwindow.cashmanager.fillTransferCombo(cells);
        }//function receives a list of transferring types
        private void getUsers(string[] cells)
        {
            runclient.mainwindow.cashmanager.fillUsersCombo(cells);
        }//function receives a list of users names

        //////////////////////////////////////**Settings**////////////////

        private void getFoodMenu2(string[] cells)
        {
            runclient.mainwindow.settings.fillFoodMenuGrid(cells);
        }//function receives a list of menu foods
        private void getCountingUnits3(string[] cells)
        {
            runclient.mainwindow.settings.fillCountingUnits(cells);
        }//function receives a list of counting units
        private void getPermissions(string[] cells)
        {
            runclient.mainwindow.settings.fillPermissions(cells);
        }//function receives a list of permissions
        private void getAllUsers(string[] cells)
        {
            runclient.mainwindow.settings.fillUsers(cells);
        }//function receives a list of users name
        private void getPrimaryMaterias(string[] cells)
        {
            runclient.mainwindow.settings.fillPrimaryMaterialsGrid(cells);
        }//function receives a list of primary materials
        private void getAccounts2(string[] cells)
        {
            runclient.mainwindow.settings.fillAccountsGrid(cells);
        }//function receives a list of transferring types
        
        //////////////////////////////////////**ChangePassword**////////////////

        private void ChangingResult(string[] cells)
        {
            runclient.mainwindow.changepassword.result(cells);
        }//function receives password changing result
    }
}

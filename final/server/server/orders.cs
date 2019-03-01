using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace server
{
    class orders
    {
        private connectThread CT;
        DataClasses1DataContext d = new DataClasses1DataContext();//linq class that will connect to the database

        public orders(connectThread CT)
        {
            this.CT = CT;
        }

        public void chooseOrder(string[] cells)
        {
            int mission = int.Parse(cells[0]);
            switch (mission)
            {
                case 1:
                    break;
                case 3:
                    CT.runserver.DisplayMessage("Test Request Recieved");
                    testBack();
                    break;
                ///////////////////////////////////////**Salaries**/////////////////////////
                case 111:
                    getSalaries1(cells);
                    break;
                case 112:
                    getSalaries2(cells);
                    break;
                case 121:
                    addsalary(cells);
                    break;
                ////////////////////////////////////////**EmployeeInOut**/////////////
                case 211:
                    getloggedout();
                    break;
                case 212:
                    getloggedin();
                    break;
                case 221:
                    EmployeeEnter(cells);
                    break;
                case 231:
                    EmployeeExit(cells);
                    break;
                /////////////////////////////////////**Store**////////////////
                case 311:
                    getPrimaryMaterials();
                    break;
                case 312:
                    getLastRecords(cells);
                    break;
                case 313:
                    All_Records(cells);
                    break;
                case 314:
                    Today(cells);
                    break;
                case 315:
                    In_Out_Quantities(cells);
                    break;
                case 316:
                    InventoryByBills();
                    break;
                case 321:
                    Add_Amount(cells);
                    break;
                /////////////////////////////////////////**EmployeeSettings**////////////////////
                case 411:
                    sendAccountNames(cells);
                    break;
                case 412:
                    sendInfEmployees(cells);
                    break;
                case 413:
                    sendWorkType();
                    break;
                case 421:
                    addNewEmployee(cells);
                    break;
                case 422:
                    addNewWorkType(cells);
                    break;
                case 431:
                    updateInfEmployees(cells);
                    break;
                ////////////////////////////////////////////**Bills**/////////////////
                case 511:
                    getTables();
                    break;
                case 512:
                    getAllFood();
                    break;
                case 513:
                    getTableDetails(cells);
                    break;
                case 514:
                    getcaptains(cells);
                    break;
                case 515:
                    getCountingUnit(cells);
                    break;
                case 516:
                    getAccounts(cells);
                    break;
                case 521:
                    addNewTable(cells);
                    break;
                case 522:
                    addfooddetails(cells);
                    break;
                case 523:
                    addNewFoodToMenu(cells);
                    break;
                case 551:
                    savebills(cells);
                    break;
                case 552:
                    savebills(cells);
                    break;
                ////////////////////////////////////////////**Show OLD Bills**/////////////////
                case 611:
                    getoldbills(cells);
                    break;
                case 612:
                    getoldbilldetails(cells);
                    break;
                case 613:
                    getcaptains(cells);
                    break;
                //////////////////////////////////////**Kitchen**/////////////////////////////
                case 711:
                    getKitchenFood();
                    break;
                case 731:
                    serveOneFood(int.Parse(cells[1]));
                    break;
                case 732:
                    serveTable(cells);
                    break;
                //////////////////////////////////////**FoodRelations**////////////////////////
                case 811:
                    getPrimaryMaterials2();
                    break;
                case 812:
                    getFoodMenu();
                    break;
                case 813:
                    getrelations(int.Parse(cells[1]));
                    break;
                case 814:
                    getCountingUnit(cells);
                    break;
                case 821:
                    addRelation(cells);
                    break;
                case 822:
                    addPrimaryMaterial(cells);
                    break;
                case 831:
                    deleteRelation(cells);
                    break;
                /////////////////////////////////////**CashManager**/////////////////////////
                case 911:
                    getcashMony(cells);
                    break;
                case 912:
                    getAccounts(cells);
                    break;
                case 913:
                    getTransfer();
                    break;
                case 914:
                    getUsers();
                    break;
                case 921:
                    AddNewcashRegister(cells);
                    break;
                /////////////////////////////////////**Settings**/////////////////////////
                case 2111:
                    getFoodMenu2();
                    break;
                case 2112:
                    getCountingUnit(cells);
                    break;
                case 2113:
                    getpermissions();
                    break;
                case 2114:
                    getAllUsers();
                    break;
                case 2115:
                    getPrimaryMaterials3();
                    break;
                case 2116:
                    getAccounts2();
                    break;
                case 2121:
                    EditFoodMenu(cells);
                    break;
                case 2122:
                    editPermission(cells);
                    break;
                case 2123:
                    editUsers(cells);
                    break;
                case 2124:
                    EditCountingUnit(cells);
                    break;
                case 2125:
                    EditPrimaryMaterials(cells);
                    break;
                case 2126:
                    EditAccounts(cells);
                    break;
                case 2131:
                    addNewFoodToMenu2(cells);
                    break;
                case 2132:
                    addNewPermission(cells);
                    break;
                case 2133:
                    addNewUser(cells);
                    break;
                case 2134:
                    addNewAccount(cells);
                    break;
                case 2135:
                    addCountingUnit(cells);
                    break;
                //////////////////////////////////////**ChangePassword**///////////////
                case 2251:
                    changePassword(cells);
                    break;
                default:
                    break;
            }
        }//take the first cell of the array and routered it the propriate function

        //the password checking function
        public bool passwordcheck(string[] cells, messenger m, out string userID, out string username)
        {
            username = "";
            userID = "";
            try
            {
                string userName = cells[1];

                User u = d.Users.Single(c => c.username == userName);

                m.setrgbkey(u.passwordMD5);
                string password = m.Decrypt(cells[2]);
                MD5Cng md5 = new MD5Cng();
                byte[] BpasswordMD5 = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
                string SpasswordMD5 = Convert.ToBase64String(BpasswordMD5);

                if (u.passwordMD5 == SpasswordMD5)
                {
                    username = u.username;
                    userID = u.userID.ToString();
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }
        public void sendPermissions(string userID)
        {
            try
            {
                getPermission GP = (from t in d.getPermissions
                                    where t.userID == int.Parse(userID)
                                    select t).Single();

                string[] cells = new string[12];
                cells[0] = "2";
                cells[1] = GP.bills.ToString();
                cells[2] = GP.oldBills.ToString();
                cells[3] = GP.store.ToString();
                cells[4] = GP.empSettings.ToString();
                cells[5] = GP.empTineInOut.ToString();
                cells[6] = GP.kitchen.ToString();
                cells[7] = GP.foodRelations.ToString();
                cells[8] = GP.salaries.ToString();
                cells[9] = GP.settings.ToString();
                cells[10] = GP.changePass.ToString();
                cells[11] = GP.cashManager.ToString();

                CT.m.send(cells);
            }
            catch { CT.runserver.DisplayMessage("ERROR sendPermissions"); }
        }//the permission sending function
        private void testBack()
        {
            CT.m.send("3");
        }//just a connection test
        private void sendMessege(string window, string message)
        {
            string[] cells = new string[3];
            cells[0] = "9";
            cells[1] = window;
            cells[2] = message;
            CT.m.send(cells);
        }//function that send messages appear directly on client specific window

        //////////////////////////////////////**EmployeeInOut**///////////////

        public void EmployeeEnter(string[] cells)
        {
            string EmpID = cells[1];

            employeesInOut emio = new employeesInOut();
            try
            {
                emio.employeeID = int.Parse(EmpID);
                emio.timeIn = DateTime.Now.TimeOfDay;
                emio.Date = DateTime.Today;

                d.employeesInOuts.InsertOnSubmit(emio);
                d.SubmitChanges();

                string message = "SERVER: Employees number: " + emio + " logged in at:" + emio.timeIn.ToString();
                sendMessege("2", message);
            }
            catch
            {
                CT.runserver.DisplayMessage("Error 221 employees can't signin");

                string message = "SERVER: Error 221 employee can't signin";
                sendMessege("2", message);
            }
        }//function add to the registers that the employee came to work now
        private void EmployeeExit(string[] cells)
        {
            try
            {
                string EmpID = cells[1];
                employeesInOut emio = (from e in d.employeesInOuts
                                       where e.timeOut == null
                                       &&
                                       e.employeeID == int.Parse(EmpID)
                                       select e).Single();

                emio.timeOut = DateTime.Now.TimeOfDay;
                d.SubmitChanges();

                string message = "SERVER: Employees number: " + EmpID + " logged out at: " + emio.timeOut.ToString();
                sendMessege("2", message);
            }
            catch
            {
                CT.runserver.DisplayMessage("Error 231 employee can't logout");

                string message = "SERVER: Error 231 employee can't logout";
                sendMessege("2", message);
            }
        }//function add to the registers that the employee leave the work now
        private void getloggedin()
        {
            try
            {
                List<getloggedin> gli = (from e in d.getloggedins
                                         select e).ToList();
                string[] cells = new string[(gli.Count * 2) + 1];
                cells[0] = "1212";
                for (int i = 0; i * 2 + 2 < cells.Length; i++)
                {
                    cells[i * 2 + 1] = gli[i].employeeID.ToString();
                    cells[i * 2 + 2] = gli[i].accountName;
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("Error getloggedin");
                sendMessege("2", "SERVER: Error getloggedin");
            }
        }//function send to client a list of the employees who are in the work now
        private void getloggedout()
        {
            try
            {
                List<getloggedout> glo = (from e in d.getloggedouts
                                          select e).ToList();
                string[] cells = new string[(glo.Count * 2) + 1];
                cells[0] = "1211";
                for (int i = 0; i * 2 + 2 < cells.Length; i++)
                {
                    cells[i * 2 + 1] = glo[i].employeeID.ToString();
                    cells[i * 2 + 2] = glo[i].accountName;
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage(" Error getloggedout");
                sendMessege("2", "SERVER: Error getloggedout");
            }
        }//function send to client a list of the employees who are not in the work now

        ///////////////////////////////////////////**Store**/////////////

        private void getPrimaryMaterials()
        {
            try
            {
                List<primaryMaterial> us = (from t in d.primaryMaterials select t).ToList();
                string[] cells2 = new String[(us.Count * 2) + 1];
                cells2[0] = "1311";

                for (int i = 0; i < us.Count; i++)
                {
                    cells2[(i * 2) + 1] = us[i].materialID.ToString();
                    cells2[(i * 2) + 2] = us[i].materialName;
                }
                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1311 getPrimaryMaterials()");
                sendMessege("3", "SERVER: ERROR 1311 getPrimaryMaterials()");
            }
        }//function send to client a list of the Primary materials that we deal with it
        private void getLastRecords(string[] cells)
        {
            try
            {
                int recordsnum = int.Parse(cells[2]); // number records which must be getting
                int prim_mat = int.Parse(cells[1]);// Id of Material Name

                List<getstorematerial> gsm = (from GSM in d.getstorematerials
                                              where (GSM.materialID == prim_mat)
                                              select GSM).Take(recordsnum).Take(recordsnum).ToList();

                string[] cells2 = new string[gsm.Count * 4 + 2];
                cells2[0] = "1312";
                cells2[1] = gsm.Count.ToString();
                for (int i = 0; i * 4 + 2 < cells2.Length; i++)
                {
                    cells2[i * 4 + 2] = gsm[i].materialName;
                    cells2[i * 4 + 3] = gsm[i].quantity.ToString();
                    cells2[i * 4 + 4] = gsm[i].date.ToShortDateString();
                    cells2[i * 4 + 5] = gsm[i].username;
                }
                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage(" Error 312: problem of getting last I/O records");
                sendMessege("3", "SERVER: Error 312: problem of getting last I/O records");
            }
        }//function send to client a count of the last registers of a specific material
        private void Today(string[] cells)
        {
            try
            {
                DateTime today = DateTime.Today;
                List<getstorematerial> gsm = (from GSM in d.getstorematerials
                                              where (GSM.date.Date == today)
                                              select GSM).ToList();
                string[] cells2 = new string[gsm.Count * 4 + 2];
                cells2[0] = "1314";
                cells2[1] = gsm.Count.ToString();
                for (int i = 0; i * 4 + 5 < cells2.Length; i++)
                {
                    cells2[i * 4 + 2] = gsm[i].materialName;
                    cells2[i * 4 + 3] = gsm[i].quantity.ToString();
                    cells2[i * 4 + 4] = gsm[i].date.ToShortDateString();
                    cells2[i * 4 + 5] = gsm[i].username;
                }
                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage(" Error 314: Today records problem");
                sendMessege("3", "SERVER: Error 314: Today records problem");
            }
        }//function send to client all today's registers
        private void All_Records(string[] cells)//function send to client a specific count of the last registers
        {
            try
            {
                int recordsnum = int.Parse(cells[1]);
                List<getstorematerial> gsm = (from GSM in d.getstorematerials
                                              select GSM).Take(recordsnum).ToList();
                string[] cells2 = new string[gsm.Count * 4 + 2];
                cells2[0] = "1313";
                cells2[1] = gsm.Count.ToString();
                for (int i = 0; i * 4 + 5 < cells2.Length; i++)
                {
                    cells2[i * 4 + 2] = gsm[i].materialName;
                    cells2[i * 4 + 3] = gsm[i].quantity.ToString();
                    cells2[i * 4 + 4] = gsm[i].date.ToShortDateString();
                    cells2[i * 4 + 5] = gsm[i].username;
                }
                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("Error 313 Problem of All records getting");
                sendMessege("3", "SERVER: Error 313 Problem of All records getting");
            }
        }
        private void In_Out_Quantities(string[] cells)
        {
            try
            {
                string index = cells[1];
                int In = 0;

                try
                {
                    In = (from t in d.storeHouses
                          where t.primaryMaterialID.ToString() == index
                          && t.quantity > 0
                          select t.quantity).Sum();
                }
                catch { }

                int Out = 0;
                try
                {
                    Out = (from t in d.storeHouses
                           where t.primaryMaterialID.ToString() == index
                           && t.quantity < 0
                           select t.quantity).Sum();
                }
                catch { }

                string[] cells1 = new string[3];
                cells1[0] = "1315";
                cells1[1] = In.ToString();
                cells1[2] = Out.ToString();
                CT.m.send(cells1);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 315 In_Out_Quantities");
                sendMessege("3", "SERVER: ERROR 315 In_Out_Quantities");
            }

        }//function send to client Sum of in & out quantities of specific material
        private void Add_Amount(string[] cells)
        {
            storeHouse st = new storeHouse();
            try
            {
                string id_material = cells[1];
                string Amount = cells[2];
                string id_user = cells[3];
                DateTime now = DateTime.Now;
                st.primaryMaterialID = int.Parse(id_material);
                st.quantity = int.Parse(Amount);
                st.date = now.Date;
                st.userID = int.Parse(id_user);
                d.storeHouses.InsertOnSubmit(st);
                d.SubmitChanges();

                sendMessege("3", "Insert: " + "Material ID: " + st.primaryMaterialID + " Quantity: " + st.quantity + " Date: " +
                    st.date + " UserId: " + st.userID + " successfully");
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR can't insert Material ID: " + st.primaryMaterialID);
                sendMessege("3", "SERVER:ERROR can't insert Material ID: " + st.primaryMaterialID);
            }
        }//function that add new register to tha data base with amount of material
        private void InventoryByBills()
        {
            try
            {
                List<inventoryByBill> IBB = (from t in d.inventoryByBills select t).ToList();

                string[] cells = new string[IBB.Count * 4 + 1];
                cells[0] = "1316";
                for (int i = 0; i < IBB.Count; i++)
                {
                    cells[i * 4 + 1] = IBB[i].materialName;
                    cells[i * 4 + 2] = IBB[i].bills.ToString();
                    cells[i * 4 + 3] = IBB[i].Namount.ToString();
                    cells[i * 4 + 4] = IBB[i].Pamount.ToString();
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR InventoryByBills");
                sendMessege("3", "SERVER: ERROR InventoryByBills");
            }
        }//function send to client all materials inventory compared to the related sold materials in bills of the restaurant

        ///////////////////////////////////////**EmployeeSettings**////////////////////

        private void sendAccountNames(string[] cells)
        {
            try
            {
                var us = from t in d.employees
                         join a in d.accounts
                         on t.accountID equals a.accountID
                         select t;
                string[] cells2 = new String[(us.Count() * 2) + 1];
                int j = 1;
                foreach (var c in us)
                {
                    cells2[j] = c.employeeID.ToString();
                    cells2[j + 1] = c.account.accountName;
                    j += 2;
                }
                cells2[0] = (int.Parse(cells[0]) + 1000).ToString();
                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1411 sendAccountNames");
                sendMessege("4", "SERVER: ERROR 1411 sendAccountNames");
            }
        }//function send to client a list of employee accounts name
        private void sendWorkType()
        {
            try
            {
                List<workType> wt = (from w in d.workTypes select w).ToList();
                string[] cells = new String[(wt.Count * 2) + 1];
                cells[0] = "1413";
                for (int i = 0; i < wt.Count; i++)
                {
                    cells[(i * 2) + 1] = wt[i].workTypeID.ToString();
                    cells[(i * 2) + 2] = wt[i].workType1;
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1413 sendWorkType");
                sendMessege("4", "SERVER: ERROR 1413 sendWorkType");
            }
        }//function send to client a list of working type (manager, accountant, chef .......etc)
        private void sendInfEmployees(string[] cells)
        {
            try
            {
                string id = cells[1];

                var result = from emp in d.employees
                             join ac in d.accounts
                             on emp.accountID equals ac.accountID
                             where emp.employeeID == int.Parse(id)
                             select new
                             {
                                 emp.employeeID,
                                 ac.accountID,
                                 ac.accountName,
                                 emp.empNationalNumber,
                                 emp.empBirthdate,
                                 emp.employmentDate,
                                 emp.empSalary,
                                 emp.DemissionDate,
                                 emp.workTypeID
                             };
                string employeeID = "";
                string accountID = "";
                string accountName = "";
                string empNationalNumber = "";
                string empBirthday = "";
                string employmentDate = "";
                string empSalary = "";
                string DemissionDate = "";
                string workTypeID = "";
                foreach (var c in result)
                {
                    employeeID = c.employeeID.ToString();
                    accountID = c.accountID.ToString();
                    accountName = c.accountName;
                    empNationalNumber = c.empNationalNumber;
                    empBirthday = c.empBirthdate.ToString();
                    employmentDate = c.employmentDate.ToString();
                    empSalary = c.empSalary.ToString();
                    DemissionDate = c.DemissionDate.ToString();
                    workTypeID = c.workTypeID.ToString();
                }
                string[] cells1 = new string[10];
                cells1[0] = "1412";
                cells1[1] = employeeID;
                cells1[2] = accountID;
                cells1[3] = accountName;
                cells1[4] = empNationalNumber;
                cells1[5] = empBirthday;
                cells1[6] = employmentDate;
                cells1[7] = empSalary;
                cells1[8] = DemissionDate;
                cells1[9] = workTypeID;
                CT.m.send(cells1);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1412 sendInfEmployees");
                sendMessege("4", "SERVER: ERROR 1412 sendInfEmployees");
            }
        }//function send to client a list of employee information (national number, salary, birth date .......etc)
        private void updateInfEmployees(string[] cells)
        {
            try
            {
                employee emp = (from e in d.employees
                                where e.employeeID == int.Parse(cells[1])
                                select e).Single();

                emp.empNationalNumber = cells[2];
                emp.empBirthdate = DateTime.Parse(cells[3]);
                emp.employmentDate = DateTime.Parse(cells[4]);
                emp.empSalary = int.Parse(cells[5]);
                emp.workTypeID = int.Parse(cells[7]);

                if (cells[6] != "")
                {
                    emp.DemissionDate = DateTime.Parse(cells[6]);
                }

                d.SubmitChanges();
                string[] cells2 = new string[1];
                cells[0] = "411";
                sendAccountNames(cells);
                sendMessege("4", "SERVER: employee Information Updated Succesfully");
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR updateInfEmployees");
                sendMessege("4", "SERVER: ERROR updateInfEmployees");
            }
        }//function update a specific employee information according to the array that the client has sent
        private void addNewEmployee(string[] cells)
        {
            try
            {
                DateTime dt_birthday = Convert.ToDateTime(cells[3]);
                DateTime dt_EmploymentDate = Convert.ToDateTime(cells[4]);
                try
                {
                    account ac1 = d.accounts.Single(q => q.accountName == cells[1]);
                    if (ac1 != null)
                    {
                        sendMessege("4", "SERVER: Account Name is exist in DB already!");
                    }
                }
                catch
                {
                    try
                    {
                        employee emp1 = d.employees.Single(p => p.empNationalNumber == cells[2]);
                        if (emp1 != null)
                        {
                            sendMessege("4", "SERVER: Employee National Number is exist in DB already!");
                        }
                    }
                    catch
                    {
                        account ac = new account();

                        ac.accountName = cells[1];
                        d.accounts.InsertOnSubmit(ac);
                        d.SubmitChanges();

                        employee emp = new employee();
                        emp.accountID = ac.accountID;


                        emp.empNationalNumber = cells[2];
                        emp.empBirthdate = dt_birthday;
                        emp.employmentDate = dt_EmploymentDate;
                        emp.empSalary = int.Parse(cells[5]);

                        emp.workTypeID = int.Parse(cells[6]);
                        d.employees.InsertOnSubmit(emp);
                        d.SubmitChanges();
                        string[] cells2 = new string[1];
                        cells[0] = "411";
                        sendAccountNames(cells);
                        sendMessege("4", "SERVER:" + emp.employeeID + "- " + ac.accountName + " Inserted Sucsessfully");
                    }
                }
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR addNewEmployee");
                sendMessege("4", "SERVER: ERROR addNewEmployee");
            }
        }//function add a new employee
        private void addNewWorkType(string[] cells)
        {
            try
            {
                try
                {
                    workType wt = d.workTypes.Single(p => p.workType1 == cells[1]);
                    if (wt != null)
                    {
                        sendMessege("4", "SERVER: Work type is already exist ");
                    }
                }
                catch
                {
                    string name = cells[1];
                    workType wt = new workType();
                    wt.workType1 = name;
                    d.workTypes.InsertOnSubmit(wt);
                    d.SubmitChanges();
                    sendWorkType();
                    sendMessege("4", "SERVER: Work type " + name + " had added succesfully");
                }
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR addNewWorkType");
                sendMessege("4", "SERVER: ERROR addNewWorkType ");
            }
        }//function add a new work type

        /////////////////////////////////////////**Bills**/////////////////

        private void getAllFood()
        {
            try
            {
                List<foodMaterial> FM = (from t in d.foodMaterials
                                         where t.visible != false
                                         select t).ToList();
                string[] cells = new String[(FM.Count * 4) + 1];
                cells[0] = "1512";
                for (int i = 0; i < FM.Count; i++)
                {
                    cells[(i * 4) + 1] = FM[i].foodID.ToString();
                    cells[(i * 4) + 2] = FM[i].parentID.ToString();
                    cells[(i * 4) + 3] = FM[i].arabicName.ToString();
                    cells[(i * 4) + 4] = FM[i].isAParent.ToString();
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1512 database select request ");
                sendMessege("5", "SERVER: ERROR 1512 getAllFood");
            }
        }//function send to client a list of the menu food
        private void getTables()
        {
            try
            {
                List<getTable> FM = (from t in d.getTables select t).ToList();
                string[] cells = new String[(FM.Count * 3) + 1];
                cells[0] = "1511";
                for (int i = 0; i < FM.Count; i++)
                {
                    cells[(i * 3) + 1] = FM[i].tempBillID.ToString();
                    cells[(i * 3) + 2] = FM[i].tableNumber.ToString();
                    cells[(i * 3) + 3] = FM[i].accountName;
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1511 getTables");
                sendMessege("5", "SERVER: ERROR 1511 getTables");
            }
        }//function send to client a list of opened tables
        private void getTableDetails(string[] cells)
        {
            int tempBillID = int.Parse(cells[1]);

            try
            {
                List<getTableDetail> GTD = (from t in d.getTableDetails
                                            where t.tempBillID == tempBillID
                                            select t).ToList();

                string[] cells2 = new String[(GTD.Count * 5) + 1];
                cells2[0] = "1513";
                for (int i = 0; i < GTD.Count; i++)
                {
                    cells2[(i * 5) + 1] = GTD[i].arabicName;
                    cells2[(i * 5) + 2] = GTD[i].price.ToString();
                    cells2[(i * 5) + 3] = GTD[i].amount.ToString();
                    cells2[(i * 5) + 4] = GTD[i].SUM.ToString();
                    cells2[(i * 5) + 5] = GTD[i].served.ToString();

                }
                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1513 getTableDetails");
                sendMessege("5", "SERVER: ERROR 1513 getTableDetails");
            }
        }//function send to client a list of tables details
        private void getcaptains(string[] cells)
        {
            try
            {
                List<getcaptain> GC = (from t in d.getcaptains select t).ToList();
                string[] cells2 = new String[(GC.Count * 2 + 1)];
                cells2[0] = (int.Parse(cells[0]) + 1000).ToString();
                for (int i = 0; i < GC.Count; i++)
                {
                    cells2[i * 2 + 1] = GC[i].employeeID.ToString();
                    cells2[i * 2 + 2] = GC[i].accountName;
                }
                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR getcaptains()");
                sendMessege("5", "server: ERROR getcaptains()");
            }
        }//function send to client a list of captains names
        private void addfooddetails(string[] cells)
        {
            try
            {
                int tempBillID = int.Parse(cells[1]);
                int foodID = int.Parse(cells[2]);
                int amount = int.Parse(cells[3]);
                int oldAmount = 0;
                if (amount == 0) { }
                else if (amount < 0)
                {
                    try
                    {
                        string sql = (from t in d.getTableDetail_notMinus
                                      where t.tempBillID == tempBillID & t.foodID == foodID
                                      select t.amount).Single().ToString();
                        int.TryParse(sql, out oldAmount);
                    }
                    catch { }

                }
                if (oldAmount + amount >= 0)
                {
                    tempBillsDetail fooddetail = new tempBillsDetail();
                    fooddetail.tempBillID = tempBillID;
                    fooddetail.foodID = foodID;
                    fooddetail.amount = amount;
                    fooddetail.served = false;

                    string Sprice = (from t in d.foodMaterials
                                     where t.foodID == foodID
                                     select t.price).Single().ToString();
                    int price = int.Parse(Sprice);
                    fooddetail.price = price;
                    d.tempBillsDetails.InsertOnSubmit(fooddetail);
                    d.SubmitChanges();
                    string[] cells2 = new string[2];
                    cells2[1] = tempBillID.ToString();
                    getTableDetails(cells2);
                }
                else { sendMessege("5", "SERVER: can't add Negative Values"); }
            }
            catch
            {
                CT.runserver.DisplayMessage("Error 522 Problem of add food details to table");
                sendMessege("5", "SERVER: ERROR 522 can't add food detail to table");
            }
        }//function add new food to the specific table
        private void addNewTable(string[] cells)
        {
            try
            {
                tempBill TB = new tempBill();
                DateTime datetime = DateTime.Now;
                TB.tableNumber = int.Parse(cells[1]);
                TB.captainID = int.Parse(cells[2]);
                TB.date = datetime;
                TB.userID = int.Parse(cells[3]);
                d.tempBills.InsertOnSubmit(TB);
                d.SubmitChanges();
                getTables();
            }
            catch
            {
                CT.runserver.DisplayMessage("Error 521 Problem of add New table");
                sendMessege("5", "SERVER: ERROR 521 addNewTable");
            }
        }//function add new table
        private void savebills(string[] cells)
        {
            try
            {
                int tempbillID = int.Parse(cells[1]);
                bill Bill = new bill();

                using (TransactionScope transaction = new TransactionScope())
                {
                    tempBill TB = (from t in d.tempBills
                                   where t.tempBillID == tempbillID
                                   select t).Single();

                    List<tempBillsDetail> TBD = (from t in d.tempBillsDetails
                                                 where t.tempBillID == tempbillID
                                                 select t).ToList();

                    int billnetvalue = 0;
                    for (int i = 0; i < TBD.Count; i++)
                    {
                        billnetvalue += TBD[i].amount * TBD[i].price;
                    }

                    Bill.billNetValue = billnetvalue;
                    Bill.tableNumber = int.Parse(TB.tableNumber.ToString());
                    Bill.startdate = TB.date;
                    Bill.enddate = DateTime.Now;

                    int tax1 = Convert.ToInt32(Math.Ceiling((double)billnetvalue * 5 / 100));
                    int tax2 = Convert.ToInt32(Math.Ceiling((double)billnetvalue * 25 / 10000));
                    int discount = int.Parse(cells[2]);
                    Bill.discount = discount;
                    Bill.tax1 = tax1;
                    Bill.tax2 = tax2;
                    Bill.billFinalValue = billnetvalue + tax1 + tax2 - discount;
                    Bill.captainID = int.Parse(TB.captainID.ToString());
                    Bill.userID = int.Parse(TB.userID.ToString());
                    Bill.clientID = int.Parse(cells[3]);

                    d.bills.InsertOnSubmit(Bill);
                    d.SubmitChanges();

                    for (int i = 0; i < TBD.Count; i++)
                    {
                        billDetail billdetail = new billDetail();
                        billdetail.billID = Bill.billID;
                        billdetail.foodID = TBD[i].foodID;
                        billdetail.price = TBD[i].price;
                        billdetail.amount = TBD[i].amount;

                        d.billDetails.InsertOnSubmit(billdetail);
                        d.tempBillsDetails.DeleteOnSubmit(TBD[i]);
                    }

                    d.tempBills.DeleteOnSubmit(TB);
                    d.SubmitChanges();

                    cashMoney cashmoney = new cashMoney();
                    cashmoney.userID = Bill.userID;
                    cashmoney.in_money = Bill.billFinalValue;
                    cashmoney.out_money = 0;
                    cashmoney.accountID = 5;
                    cashmoney.explanation = "Table " + Bill.tableNumber;
                    cashmoney.date = DateTime.Today;
                    cashmoney.transferTypeID = 1;
                    d.cashMoneys.InsertOnSubmit(cashmoney);
                    d.SubmitChanges();

                    if (Bill.clientID != 5)
                    {
                        cashMoney cashmoney2 = new cashMoney();
                        cashmoney2.userID = Bill.userID;
                        cashmoney2.in_money = 0;
                        cashmoney2.out_money = Bill.billFinalValue;
                        cashmoney2.accountID = int.Parse(Bill.clientID.ToString());
                        cashmoney2.explanation = "Table " + Bill.tableNumber;
                        cashmoney2.date = DateTime.Today;
                        cashmoney2.transferTypeID = 4;
                        d.cashMoneys.InsertOnSubmit(cashmoney2);
                        d.SubmitChanges();
                    }

                    transaction.Complete();
                }
                if (cells[0] == "551")
                {
                    getTables();
                }
                if (cells[0] == "552")
                {
                    List<getBillToPrint> GBTP = (from t in d.getBillToPrints
                                                 where t.billID == Bill.billID
                                                 select t).ToList();
                    string[] cells2 = new string[GBTP.Count * 4 + 2];
                    cells2[0] = "1552";
                    cells2[1] = Bill.billID.ToString();
                    for (int i = 0; i < GBTP.Count; i++)
                    {
                        cells2[i * 4 + 2] = GBTP[i].arabicName;
                        cells2[i * 4 + 3] = GBTP[i].Amount.ToString();
                        cells2[i * 4 + 4] = GBTP[i].Price.ToString();
                        cells2[i * 4 + 5] = GBTP[i].SUM.ToString();
                    }
                    CT.m.send(cells2);
                    getTables();
                }
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR can't Save the bills");
                sendMessege("5", "SERVER: ERROR can't Save the bills");
            }
        }//function save the bill and close it and send printing information if the client want to print
        private void addNewFoodToMenu(string[] cells)
        {
            try
            {
                foodMaterial FM = new foodMaterial();
                bool isAParent = bool.Parse(cells[6]);
                if (isAParent)
                {
                    FM.parentID = int.Parse(cells[1]);
                    FM.arabicName = cells[2];
                    FM.isAParent = isAParent;
                    FM.visible = bool.Parse(cells[7]);
                }
                else
                {
                    FM.parentID = int.Parse(cells[1]);
                    FM.arabicName = cells[2];
                    FM.englishName = cells[3];
                    FM.unitID = int.Parse(cells[4]);
                    FM.price = int.Parse(cells[5]);
                    FM.isAParent = isAParent;
                    FM.visible = bool.Parse(cells[7]);
                }
                d.foodMaterials.InsertOnSubmit(FM);
                d.SubmitChanges();
                getAllFood();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR addNewFoodToMenu()");
                sendMessege("5", "SERVER: ERROR addNewFoodToMenu");
            }
        }//function add new plate to the food menu

        ///////////////////////////////////////**Show OLD Bills**///////////////

        private void getoldbills(string[] cells)
        {
            try
            {
                List<getoldbill> GOB = new List<getoldbill>();
                if (cells[1] == "0")
                {
                    GOB = (from t in d.getoldbills select t).ToList();
                }
                if (cells[1] == "1")
                {
                    DateTime fromdate = DateTime.Parse(cells[2]);
                    DateTime todate = DateTime.Parse(cells[3]);
                    GOB = (from t in d.getoldbills where t.startdate >= fromdate & t.startdate <= todate select t).ToList();
                }
                if (cells[1] == "2")
                {
                    int captainID = int.Parse(cells[2]);
                    GOB = (from t in d.getoldbills where t.captainID == captainID select t).ToList();
                }
                if (cells[1] == "3")
                {
                    DateTime fromdate = DateTime.Parse(cells[2]);
                    DateTime todate = DateTime.Parse(cells[3]);
                    int captainID = int.Parse(cells[4]);
                    GOB = (from t in d.getoldbills
                           where t.startdate >= fromdate & t.startdate <= todate & t.captainID == captainID
                           select t).ToList();
                }
                string[] cells2 = new string[GOB.Count * 12 + 1];
                cells2[0] = "1611";
                for (int i = 0; i < GOB.Count; i++)
                {
                    cells2[i * 12 + 1] = GOB[i].billID.ToString();
                    cells2[i * 12 + 2] = GOB[i].billFinalValue.ToString();
                    cells2[i * 12 + 3] = GOB[i].tableNumber.ToString();
                    cells2[i * 12 + 4] = GOB[i].startdate.ToString();
                    cells2[i * 12 + 5] = GOB[i].enddate.ToString();
                    cells2[i * 12 + 6] = GOB[i].discount.ToString();
                    cells2[i * 12 + 7] = GOB[i].tax1.ToString();
                    cells2[i * 12 + 8] = GOB[i].tax2.ToString();
                    cells2[i * 12 + 9] = GOB[i].billNetValue.ToString();
                    cells2[i * 12 + 10] = GOB[i].captain.ToString();
                    try { cells2[i * 12 + 11] = GOB[i].client.ToString(); }
                    catch { cells2[i * 12 + 11] = ""; }
                    cells2[i * 12 + 12] = GOB[i].username.ToString();
                }

                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR getoldbills");
                sendMessege("6", "SERVER: ERROR getoldbills");
            }
        }//function send to client a list of all saved bills
        private void getoldbilldetails(string[] cells)
        {
            try
            {
                List<getbilldetail> BD = (from t in d.getbilldetails
                                          where t.billID == int.Parse(cells[1])
                                          select t).ToList();
                string[] cells2 = new string[BD.Count * 4 + 1];
                cells2[0] = "1612";
                for (int i = 0; i < BD.Count; i++)
                {
                    cells2[i * 4 + 1] = BD[i].arabicName;
                    cells2[i * 4 + 2] = BD[i].price.ToString();
                    cells2[i * 4 + 3] = BD[i].amount.ToString();
                    cells2[i * 4 + 4] = BD[i].SUM.ToString();
                }

                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR getoldbilldetails");
                sendMessege("6", "SERVER: ERROR getoldbilldetails");
            }
        }//function send to client a list saved bill details

        //////////////////////////////////////**Kitchen**/////////////////////////////

        private void getKitchenFood()
        {
            try
            {
                List<getKitchenFood> GKF = (from t in d.getKitchenFoods
                                            select t).ToList();
                string[] cells = new string[GKF.Count * 4 + 1];
                cells[0] = "1711";
                for (int i = 0; i < GKF.Count; i++)
                {
                    cells[i * 4 + 1] = GKF[i].tempBillDetailID.ToString();
                    cells[i * 4 + 2] = GKF[i].tableNumber.ToString();
                    cells[i * 4 + 3] = GKF[i].arabicName;
                    cells[i * 4 + 4] = GKF[i].amount.ToString();
                }

                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 711 cant,t get kitchen food ");
                sendMessege("7", "SERVER: ERROR 711 cant,t get kitchen food");
            }
        }//function send to client a list of the food that not served yet
        private void serveOneFood(int foodIDdetails)
        {
            try
            {
                tempBillsDetail TBD = (from t in d.tempBillsDetails
                                       where t.tempBillDetailID == foodIDdetails
                                       select t).Single();
                TBD.served = true;
                d.SubmitChanges();
                getKitchenFood();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 731 cant,t serve food " + foodIDdetails.ToString());
                sendMessege("7", "SERVER: ERROR 731 cant,t serve food");
            }
        }//function mark the food as served
        private void serveTable(string[] cells)
        {
            for (int i = 0; i + 2 < cells.Length; i++)
            {
                serveOneFood(int.Parse(cells[i + 1]));
            }
        }//function mark all the food of specific table as served

        /////////////////////////////////**FoodRelations**/////////////////////////

        private void getPrimaryMaterials2()
        {
            try
            {
                List<GetPMaterial> GPM = (from t in d.GetPMaterials
                                          select t).ToList();
                string[] cells = new String[(GPM.Count * 4) + 1];
                cells[0] = "1811";

                for (int i = 0; i < GPM.Count; i++)
                {
                    cells[(i * 4) + 1] = GPM[i].materialID.ToString();
                    cells[(i * 4) + 2] = GPM[i].materialName;
                    cells[(i * 4) + 3] = GPM[i].unitName;
                    cells[(i * 4) + 4] = GPM[i].lastPrice.ToString();

                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1811 getPrimaryMaterials2()");
                sendMessege("8", "SERVER: ERROR 1811 getPrimaryMaterials2()");
            }
        }//function send to client a list of the Primary materials that we deal with it
        private void getFoodMenu()
        {
            try
            {
                List<GetFoodMenu> GFM = (from t in d.GetFoodMenus
                                         select t).ToList();
                string[] cells = new String[(GFM.Count * 4) + 1];
                cells[0] = "1812";

                for (int i = 0; i < GFM.Count; i++)
                {
                    cells[(i * 4) + 1] = GFM[i].foodID.ToString();
                    cells[(i * 4) + 2] = GFM[i].arabicName;
                    cells[(i * 4) + 3] = GFM[i].unitName;
                    cells[(i * 4) + 4] = GFM[i].price.ToString();
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1812 getFoodMenu()");
                sendMessege("8", "SERVER: ERROR 1812 getFoodMenu()");
            }
        }//function send to client a list of the menu foods
        private void getrelations(int foodID)
        {
            try
            {
                List<getRelation> GR = (from t in d.getRelations
                                        where t.foodMaterialID == foodID
                                        select t).ToList();

                string[] cells2 = new String[(GR.Count * 5) + 1];
                cells2[0] = "1813";

                for (int i = 0; i < GR.Count; i++)
                {
                    cells2[(i * 5) + 1] = GR[i].relationID.ToString();
                    cells2[(i * 5) + 2] = GR[i].materialName;
                    cells2[(i * 5) + 3] = GR[i].unitName;
                    cells2[(i * 5) + 4] = GR[i].amount.ToString();
                    cells2[(i * 5) + 5] = GR[i].SUM.ToString();
                }
                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1813 getrelations()");
                sendMessege("8", "SERVER: ERROR 1813 getrelations()");
            }
        }//function sends to client a list of ingredients of specific food menu
        private void addRelation(string[] cells)
        {
            try
            {
                int foodMaterialsID = int.Parse(cells[1]);
                int PremaryMaterialID = int.Parse(cells[2]);
                decimal Amount = decimal.Parse(cells[3]);
                foodMaterialRelation FMR = new foodMaterialRelation();
                FMR.foodMaterialID = foodMaterialsID;
                FMR.primaryMaterialID = PremaryMaterialID;
                FMR.amount = Amount;

                d.foodMaterialRelations.InsertOnSubmit(FMR);
                d.SubmitChanges();
                getrelations(foodMaterialsID);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1821 addRelation()");
                sendMessege("8", "SERVER: ERROR 1821 addRelation()");
            }
        }//function add new ingredients to a specific food menu
        private void deleteRelation(string[] cells)
        {
            try
            {
                int relationID = int.Parse(cells[1]);
                int foodID = int.Parse(cells[2]);
                foodMaterialRelation FMR = new foodMaterialRelation();
                FMR = (from t in d.foodMaterialRelations
                       where t.relationID == relationID
                       select t).Single();
                d.foodMaterialRelations.DeleteOnSubmit(FMR);
                d.SubmitChanges();
                getrelations(foodID);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1831 deleteRelation()");
                sendMessege("8", "SERVER: ERROR 1831 deleteRelation()");
            }
        }//function delete ingredients from a specific food menu
        private void addPrimaryMaterial(string[] cells)
        {
            try
            {
                primaryMaterial PM = new primaryMaterial();
                PM.materialName = cells[1];
                PM.countUnitID = int.Parse(cells[2]);
                PM.lastPrice = int.Parse(cells[3]);
                PM.visible = bool.Parse(cells[4]);

                d.primaryMaterials.InsertOnSubmit(PM);
                d.SubmitChanges();

                getPrimaryMaterials2();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 1822 deleteRelation()");
                sendMessege("8", "SERVER: ERROR 1822 deleteRelation()");
            }
        }//function add new primary material
        private void getCountingUnit(string[] cells)
        {
            try
            {
                List<countingUnit> CU = (from t in d.countingUnits
                                         select t).ToList();
                string[] cells2 = new string[CU.Count * 2 + 1];
                cells2[0] = (int.Parse(cells[0]) + 1000).ToString();

                for (int i = 0; i < CU.Count; i++)
                {
                    cells2[i * 2 + 1] = CU[i].unitID.ToString();
                    cells2[i * 2 + 2] = CU[i].unitName;
                }
                CT.m.send(cells2);
            }
            catch
            {
                string window = cells[0][0].ToString();
                CT.runserver.DisplayMessage("ERROR " + (int.Parse(cells[0]) + 1000).ToString() + " getCountingUnit()");
                sendMessege(window, "SERVER: ERROR " + (int.Parse(cells[0]) + 1000).ToString() + " getCountingUnit()");
            }
        }//function send to client a list of counting unit we use in measuring

        ////////////////////////////////////**CashMoney**//////////////////////////////////

        private void AddNewcashRegister(string[] cells)
        {
            try
            {
                cashMoney CM = new cashMoney();
                CM.userID = int.Parse(cells[1]);
                CM.in_money = int.Parse(cells[2]);
                CM.out_money = int.Parse(cells[3]);
                CM.accountID = int.Parse(cells[4]);
                CM.explanation = cells[5];
                CM.date = DateTime.Now;
                CM.transferTypeID = int.Parse(cells[6]);

                d.cashMoneys.InsertOnSubmit(CM);
                d.SubmitChanges();

                string[] cells2 = new string[] { "911", "NULL", "NULL", "NULL", "NULL", "NULL" };
                getcashMony(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR AddNewcashRegister()");
                sendMessege("9", "SERVER: ERROR AddNewcashRegister");
            }
        }//function add new register to cash money table
        private void getcashMony(string[] cells)
        {
            try
            {
                List<getCashMoney> GCM = new List<getCashMoney>();
                GCM = (from t in d.getCashMoneys select t).ToList();
                if (cells[1] != "NULL")
                {
                    DateTime start = DateTime.Parse(cells[1]);
                    GCM = (from t in GCM where t.date >= start select t).ToList();
                }
                if (cells[2] != "NULL")
                {
                    DateTime end = DateTime.Parse(cells[2]);
                    GCM = (from t in GCM where t.date <= end select t).ToList();
                }
                if (cells[3] != "NULL")
                {
                    GCM = (from t in GCM
                           where t.transferTypeID.ToString() == cells[3]
                           select t).ToList();
                }
                if (cells[4] != "NULL")
                {
                    GCM = (from t in GCM
                           where t.accountID.ToString() == cells[4]
                           select t).ToList();
                }
                if (cells[5] != "NULL")
                {
                    GCM = (from t in GCM
                           where t.userID.ToString() == cells[5]
                           select t).ToList();
                }

                string[] cells2 = new string[GCM.Count * 7 + 1];

                cells2[0] = "1911";
                for (int i = 0; i < GCM.Count; i++)
                {
                    cells2[i * 7 + 1] = GCM[i].in_money.ToString();
                    cells2[i * 7 + 2] = GCM[i].out_money.ToString();
                    cells2[i * 7 + 3] = GCM[i].accountName.ToString();
                    cells2[i * 7 + 4] = GCM[i].explanation.ToString();
                    cells2[i * 7 + 5] = GCM[i].date.ToShortDateString();
                    cells2[i * 7 + 6] = GCM[i].transferType.ToString();
                    cells2[i * 7 + 7] = GCM[i].username.ToString();
                }
                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR  getcashMony()");
                sendMessege("9", "SERVER: ERROR  getcashMony");
            }
        }//function send to client a list of register from cashmoney table
        private void getAccounts(string[] cells)
        {
            try
            {
                List<account> acc = (from t in d.accounts select t).ToList();
                string[] cells2 = new string[acc.Count * 2 + 1];
                cells2[0] = (int.Parse(cells[0]) + 1000).ToString();
                for (int i = 0; i < acc.Count; i++)
                {
                    cells2[i * 2 + 1] = acc[i].accountID.ToString();
                    cells2[i * 2 + 2] = acc[i].accountName;
                }
                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR getAccounts");
                sendMessege("9", "SERVER: ERROR getAccounts");
            }
        }//function send to client a list of accounts
        private void getTransfer()
        {
            try
            {
                List<transferType> transfer = (from t in d.transferTypes select t).ToList();
                string[] cells = new string[transfer.Count * 2 + 1];
                cells[0] = "1913";
                for (int i = 0; i < transfer.Count; i++)
                {
                    cells[i * 2 + 1] = transfer[i].transferTypeID.ToString();
                    cells[i * 2 + 2] = transfer[i].transferType1;
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 913 getTransfer");
                sendMessege("9", "SERVER: ERROR 913 getTransfer");
            }
        }//function send to client a list of transfer kinds
        private void getUsers()
        {
            try
            {
                List<User> users = (from t in d.Users select t).ToList();
                string[] cells = new string[users.Count * 2 + 1];
                cells[0] = "1914";
                for (int i = 0; i < users.Count; i++)
                {
                    cells[i * 2 + 1] = users[i].userID.ToString();
                    cells[i * 2 + 2] = users[i].username;
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 914 getUsers");
                sendMessege("9", "SERVER: ERROR 914 getUsers");
            }
        }//function send to client a list of users

        ////////////////////////////////////**Salaries**/////////////////////////////////

        private void getSalaries1(string[] cells)
        {
            try
            {
                DateTime month = DateTime.Parse(cells[1]);

                var GS = (from t in d.getSalaries
                          where t.Date >= month & t.Date < month.AddMonths(1)
                          select t).GroupBy(t => t.EmpID).Select(a =>
                              new
                              {
                                  ID = a.Min(b => b.EmpID),
                                  DIFF = a.Sum(b => b.mi),
                                  Name = a.Min(b => b.accountName),
                                  EmpSalary = a.Min(b => b.empSalary)
                              }).ToList();

                string[] cells2 = new string[GS.Count * 4 + 1];
                cells2[0] = "1111";
                for (int i = 0; i < GS.Count; i++)
                {
                    cells2[i * 4 + 1] = GS[i].ID.ToString();
                    cells2[i * 4 + 2] = GS[i].DIFF.ToString();
                    cells2[i * 4 + 3] = GS[i].Name.ToString();
                    cells2[i * 4 + 4] = GS[i].EmpSalary.ToString();
                }

                CT.m.send(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 112 getSalaries2");
                sendMessege("1", "ERROR 112 getSalaries2");
            }
        }//function send to client a list of employees and working periods depending on working Schedule
        private void getSalaries2(string[] cells)
        {
            try
            {
                DateTime month = DateTime.Parse(cells[1]);
                var salary = (from t in d.getSavedSalaries
                              where t.monthNUM == month
                              select t).ToList();

                string[] cells3 = new string[salary.Count * 9 + 1];
                cells3[0] = "1112";
                for (int i = 0; i < salary.Count; i++)
                {
                    cells3[i * 9 + 1] = salary[i].salaryID.ToString();
                    cells3[i * 9 + 2] = salary[i].employeeID.ToString();
                    cells3[i * 9 + 3] = salary[i].netSalary.ToString();
                    cells3[i * 9 + 4] = salary[i].minuts.ToString();
                    cells3[i * 9 + 5] = salary[i].bonuses.ToString();
                    cells3[i * 9 + 6] = salary[i].discount.ToString();
                    cells3[i * 9 + 7] = salary[i].totalSalary.ToString();
                    cells3[i * 9 + 8] = salary[i].transferred.ToString();
                    cells3[i * 9 + 9] = month.ToString();
                }

                CT.m.send(cells3);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 112 getSalaries2");
                sendMessege("1", "SERVER: ERROR 112 getSalaries2");
            }
        }//function send to client a list of saved salaries for a specific month
        private void addsalary(string[] cells)
        {
            try
            {
                salary sal = new salary();
                sal.employeeID = int.Parse(cells[1]);
                sal.monthNUM = DateTime.Parse(cells[2]);
                sal.netSalary = int.Parse(cells[3]);
                sal.minuts = int.Parse(cells[4]);
                sal.bonuses = int.Parse(cells[5]);
                sal.discount = int.Parse(cells[6]);
                sal.totalSalary = int.Parse(cells[7]);
                sal.transferred = bool.Parse(cells[8]);
                int id = int.Parse(cells[9]);


                if (!bool.Parse(cells[8]))//temporary
                {
                    if (id == 0)//first Time To save
                    {
                        d.salaries.InsertOnSubmit(sal);
                        d.SubmitChanges();
                        string dd = sal.salaryID.ToString();
                    }
                    else//Update old one 
                    {
                        salary sal2 = (from t in d.salaries
                                       where t.salaryID == id
                                       select t).Single();
                        sal2.bonuses = sal.bonuses;
                        sal2.discount = sal.discount;
                        sal2.minuts = sal.minuts;
                        sal2.monthNUM = sal.monthNUM;
                        sal2.netSalary = sal.netSalary;
                        sal2.totalSalary = sal.totalSalary;
                        sal2.transferred = false;

                        d.SubmitChanges();
                    }
                }
                else//Permanently
                {
                    if (id != 0)//update and save Permanently
                    {
                        salary sal2 = (from t in d.salaries
                                       where t.salaryID == id
                                       select t).Single();
                        sal2.bonuses = sal.bonuses;
                        sal2.discount = sal.discount;
                        sal2.minuts = sal.minuts;
                        sal2.monthNUM = sal.monthNUM;
                        sal2.netSalary = sal.netSalary;
                        sal2.totalSalary = sal.totalSalary;
                        sal2.transferred = true;

                        d.SubmitChanges();
                    }
                    if (id == 0)//first Time To save
                    {
                        d.salaries.InsertOnSubmit(sal);
                        d.SubmitChanges();
                    }
                    int user = int.Parse(cells[10]);
                    string[] cells3 = new string[7];

                    cells3[1] = user.ToString();
                    cells3[2] = "0";
                    cells3[3] = sal.totalSalary.ToString();
                    cells3[4] = "9";
                    cells3[5] = "Salary";
                    cells3[6] = "3";
                    AddNewcashRegister(cells3);

                    cells3[2] = sal.totalSalary.ToString();
                    cells3[3] = "0";
                    int empID = int.Parse(cells[1]);
                    employee emp = (from t in d.employees
                                    where t.employeeID == sal.employeeID
                                    select t).Single();
                    cells3[4] = emp.accountID.ToString();
                    AddNewcashRegister(cells3);
                }
                string[] cells2 = new string[2];
                cells2[0] = "111";
                cells2[1] = cells[2];
                getSalaries1(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 121 addsalary  ");
                sendMessege("1", "SERVER: ERROR 121 addsalary");
            }
        }//function save salary and trasfer the money between accounts

        ////////////////////////////////////**Food Menu & Primary Materials Manager**////////////////////

        private void getFoodMenu2()
        {
            try
            {
                List<foodMaterial> FM = (from t in d.foodMaterials
                                         select t).ToList();
                string[] cells = new String[(FM.Count * 8) + 1];
                cells[0] = "3111";
                for (int i = 0; i < FM.Count; i++)
                {
                    cells[(i * 8) + 1] = FM[i].foodID.ToString();
                    cells[(i * 8) + 2] = FM[i].parentID.ToString();
                    cells[(i * 8) + 3] = FM[i].arabicName.ToString();
                    cells[(i * 8) + 4] = FM[i].unitID.ToString();
                    cells[(i * 8) + 5] = FM[i].price.ToString();
                    cells[(i * 8) + 6] = FM[i].isAParent.ToString();
                    cells[(i * 8) + 7] = FM[i].visible.ToString();
                    cells[(i * 8) + 8] = FM[i].englishName;
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR getFoodMenu");
                sendMessege("10", "SERVER: ERROR getFoodMenu");
            }
        }//function send to client a list of menu food
        private void EditFoodMenu(string[] cells)
        {
            try
            {
                foodMaterial FM = (from t in d.foodMaterials
                                   where t.foodID == int.Parse(cells[1])
                                   select t).Single();
                FM.parentID = int.Parse(cells[2]);
                FM.arabicName = cells[3];
                FM.englishName = cells[4];
                FM.unitID = int.Parse(cells[5]);
                FM.price = int.Parse(cells[6]);
                FM.isAParent = bool.Parse(cells[7]);
                FM.visible = bool.Parse(cells[8]);

                d.SubmitChanges();
                getFoodMenu2();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR EditFoodMenu");
                sendMessege("10", "SERVER: ERROR EditFoodMenu");
            }
        }//function edit food menu
        private void addNewFoodToMenu2(string[] cells)
        {
            try
            {
                foodMaterial FM = new foodMaterial();
                bool isAParent = bool.Parse(cells[6]);
                if (isAParent)
                {
                    FM.parentID = int.Parse(cells[1]);
                    FM.arabicName = cells[2];
                    FM.isAParent = isAParent;
                    FM.visible = bool.Parse(cells[7]);
                }
                else
                {
                    FM.parentID = int.Parse(cells[1]);
                    FM.arabicName = cells[2];
                    FM.englishName = cells[3];
                    FM.unitID = int.Parse(cells[4]);
                    FM.price = int.Parse(cells[5]);
                    FM.isAParent = isAParent;
                    FM.visible = bool.Parse(cells[7]);
                }

                d.foodMaterials.InsertOnSubmit(FM);
                d.SubmitChanges();
                getFoodMenu2();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR addNewFoodToMenu()");
                sendMessege("10", "SERVER: ERROR addNewFoodToMenu()");
            }
        }//function add food to menu
        private void getPrimaryMaterials3()
        {
            try
            {
                List<primaryMaterial> PM = (from t in d.primaryMaterials
                                            select t).ToList();
                string[] cells = new String[(PM.Count * 5) + 1];
                cells[0] = "3115";
                for (int i = 0; i < PM.Count; i++)
                {
                    cells[(i * 5) + 1] = PM[i].materialID.ToString();
                    cells[(i * 5) + 2] = PM[i].materialName;
                    cells[(i * 5) + 3] = PM[i].lastPrice.ToString();
                    cells[(i * 5) + 4] = PM[i].visible.ToString();
                    cells[(i * 5) + 5] = PM[i].countUnitID.ToString();
                }

                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR 3113 getPrimaryMaterials3()");
                sendMessege("10", "SERVER: ERROR 3113 getPrimaryMaterials3()");
            }
        }//function send to client a list of primary materials
        private void EditPrimaryMaterials(string[] cells)
        {
            try
            {
                primaryMaterial PM = (from t in d.primaryMaterials
                                      where t.materialID == int.Parse(cells[1])
                                      select t).Single();

                PM.materialName = cells[2];
                PM.countUnitID = int.Parse(cells[3]);
                PM.lastPrice = int.Parse(cells[4]);
                PM.visible = bool.Parse(cells[5]);

                d.SubmitChanges();
                getPrimaryMaterials3();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR EditPrimaryMaterials");
                sendMessege("10", "SERVER: ERROR EditPrimaryMaterials");
            }
        }//function edit primary materials

        ////////////////////////////////////**Accounts manager**////////////////////

        private void getAccounts2()
        {
            try
            {
                List<account> acc = (from t in d.accounts select t).ToList();
                string[] cells = new string[acc.Count * 3 + 1];
                cells[0] = "3116";
                for (int i = 0; i < acc.Count; i++)
                {
                    cells[i * 3 + 1] = acc[i].accountID.ToString();
                    cells[i * 3 + 2] = acc[i].accountName;
                    cells[i * 3 + 3] = acc[i].visible.ToString();
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR getAccounts");
                sendMessege("10", "SERVER: ERROR getAccounts");
            }
        }//function send to client a list of accounts
        private void addNewAccount(string[] cells)
        {
            try
            {
                account acc = new account();
                acc.accountName = cells[1];
                acc.visible = bool.Parse(cells[2]);

                d.accounts.InsertOnSubmit(acc);
                d.SubmitChanges();
                getAccounts2();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR addNewAccount");
                sendMessege("10", "SERVER: ERROR addNewAccount");
            }
        }//function add new account
        private void EditAccounts(string[] cells)
        {
            try
            {
                account acc = (from t in d.accounts
                               where t.accountID == int.Parse(cells[1])
                               select t).Single();
                acc.accountName = cells[2];
                acc.visible = bool.Parse(cells[3]);

                d.SubmitChanges();
                getAccounts2();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR EditAccounts");
                sendMessege("10", "SERVER: ERROR EditAccounts");
            }
        }//function edit account

        ////////////////////////////////////**counting Unit manager**////////////////////

        private void addCountingUnit(string[] cells)
        {
            try
            {
                countingUnit CU = new countingUnit();
                CU.unitName = cells[1];

                d.countingUnits.InsertOnSubmit(CU);
                d.SubmitChanges();
                string[] cells2 = new string[1];
                cells2[0] = "2112";
                getCountingUnit(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR addCountingUnit");
                sendMessege("10", "SERVER: ERROR addCountingUnit");
            }
        }//function add new counting unit
        private void EditCountingUnit(string[] cells)
        {
            try
            {
                countingUnit CU = (from t in d.countingUnits
                                   where t.unitID == int.Parse(cells[1])
                                   select t).Single();
                CU.unitName = cells[2];

                d.SubmitChanges();
                string[] cells2 = new string[1];
                cells2[0] = "2112";
                getCountingUnit(cells2);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR EditCountingUnit");
                sendMessege("10", "SERVER: ERROR EditCountingUnit");
            }
        }//function edit counting unit

        ////////////////////////////////////**Users & Permissions manager**////////////////////

        private void addNewUser(string[] cells)
        {
            try
            {
                User user = new User();
                string password = cells[2];

                MD5Cng md5 = new MD5Cng();
                byte[] BpasswordMD5 = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
                string SpasswordMD5 = Convert.ToBase64String(BpasswordMD5);

                user.username = cells[1];
                user.passwordMD5 = SpasswordMD5;
                user.Permissions = int.Parse(cells[3]);

                d.Users.InsertOnSubmit(user);
                d.SubmitChanges();
                getAllUsers();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR addNewUser");
                sendMessege("10", "SERVER: ERROR addNewUser");
            }
        }//function add new user
        private void editUsers(string[] cells)
        {
            try
            {
                User user = (from t in d.Users
                             where t.userID == int.Parse(cells[1])
                             select t).Single();

                if (cells[3] != "")
                {
                    string password = cells[3];
                    MD5Cng md5 = new MD5Cng();
                    byte[] BpasswordMD5 = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
                    string SpasswordMD5 = Convert.ToBase64String(BpasswordMD5);
                    user.passwordMD5 = SpasswordMD5;
                }
                user.username = cells[2];
                user.Permissions = int.Parse(cells[4]);

                d.SubmitChanges();
                getAllUsers();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR editUsers");
                sendMessege("10", "SERVER: ERROR editUsers");
            }
        }//function edit user
        private void editPermission(string[] cells)
        {
            try
            {
                Permission per = (from t in d.Permissions
                                  where t.permissionsID == int.Parse(cells[1])
                                  select t).Single();

                per.permissionName = cells[2];
                per.bills = bool.Parse(cells[3]);
                per.oldBills = bool.Parse(cells[4]);
                per.store = bool.Parse(cells[5]);
                per.empSettings = bool.Parse(cells[6]);
                per.empTineInOut = bool.Parse(cells[7]);
                per.kitchen = bool.Parse(cells[8]);
                per.foodRelations = bool.Parse(cells[9]);
                per.salaries = bool.Parse(cells[10]);
                per.settings = bool.Parse(cells[11]);
                per.changePass = bool.Parse(cells[12]);
                per.cashManager = bool.Parse(cells[13]);

                d.SubmitChanges();
                getpermissions();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR editPermission");
                sendMessege("10", "SERVER: ERROR editPermission");
            }
        }//function edit permissions of the specific user
        private void addNewPermission(string[] cells)
        {
            try
            {
                Permission per = new Permission();

                per.permissionName = cells[1];
                per.bills = bool.Parse(cells[2]);
                per.oldBills = bool.Parse(cells[3]);
                per.store = bool.Parse(cells[4]);
                per.empSettings = bool.Parse(cells[5]);
                per.empTineInOut = bool.Parse(cells[6]);
                per.kitchen = bool.Parse(cells[7]);
                per.foodRelations = bool.Parse(cells[8]);
                per.salaries = bool.Parse(cells[9]);
                per.settings = bool.Parse(cells[10]);
                per.changePass = bool.Parse(cells[11]);
                per.cashManager = bool.Parse(cells[12]);

                d.Permissions.InsertOnSubmit(per);
                d.SubmitChanges();
                getpermissions();
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR addNewPermission");
                sendMessege("10", "SERVER: ERROR addNewPermission");
            }
        }//function add new permission
        private void getpermissions()
        {
            try
            {
                List<Permission> per = (from t in d.Permissions select t).ToList();
                string[] cells = new string[per.Count * 13 + 1];
                cells[0] = "3113";
                for (int i = 0; i < per.Count; i++)
                {
                    cells[i * 13 + 1] = per[i].permissionsID.ToString();
                    cells[i * 13 + 2] = per[i].permissionName;
                    cells[i * 13 + 3] = per[i].bills.ToString();
                    cells[i * 13 + 4] = per[i].oldBills.ToString();
                    cells[i * 13 + 5] = per[i].store.ToString();
                    cells[i * 13 + 6] = per[i].empSettings.ToString();
                    cells[i * 13 + 7] = per[i].empTineInOut.ToString();
                    cells[i * 13 + 8] = per[i].kitchen.ToString();
                    cells[i * 13 + 9] = per[i].foodRelations.ToString();
                    cells[i * 13 + 10] = per[i].salaries.ToString();
                    cells[i * 13 + 11] = per[i].settings.ToString();
                    cells[i * 13 + 12] = per[i].changePass.ToString();
                    cells[i * 13 + 13] = per[i].cashManager.ToString();
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR getpermissions");
                sendMessege("10", "SERVER: ERROR getpermissions");
            }
        }//function send to client a list of his permissions
        private void getAllUsers()
        {
            try
            {
                List<getUser> GU = (from t in d.getUsers select t).ToList();
                string[] cells = new string[GU.Count * 4 + 1];
                cells[0] = "3114";
                for (int i = 0; i < GU.Count; i++)
                {
                    cells[i * 4 + 1] = GU[i].userID.ToString();
                    cells[i * 4 + 2] = GU[i].username;
                    cells[i * 4 + 3] = GU[i].permissionsID.ToString();
                    cells[i * 4 + 4] = GU[i].permissionName.ToString();
                }
                CT.m.send(cells);
            }
            catch
            {
                CT.runserver.DisplayMessage("ERROR getAllUsers");
                sendMessege("10", "SERVER: ERROR getAllUsers");
            }
        }//function send to client a list of users

        ///////////////////////////////////**Change User Password**//////////////////////////

        private void changePassword(string[] cells)
        {
            try
            {
                User user = (from t in d.Users
                             where t.userID == int.Parse(cells[1])
                             select t).Single();

                string oldpassword = cells[2];
                string newpassword = cells[3];

                MD5Cng md5 = new MD5Cng();
                byte[] BoldPasswordMD5 = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(oldpassword));
                string SoldPasswordMD5 = Convert.ToBase64String(BoldPasswordMD5);

                if (SoldPasswordMD5 == user.passwordMD5)
                {
                    byte[] BNewPasswordMD5 = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(newpassword));
                    string SNewPasswordMD5 = Convert.ToBase64String(BNewPasswordMD5);
                    user.passwordMD5 = SNewPasswordMD5;
                    d.SubmitChanges();
                    string[] cells2 = new string[2];
                    cells2[0] = "3251";
                    cells2[1] = "true";
                    CT.m.send(cells2);
                }
                else
                {
                    string[] cells2 = new string[2];
                    cells2[0] = "3251";
                    cells2[1] = "false";
                    CT.m.send(cells2);
                }
            }
            catch { CT.runserver.DisplayMessage("ERROR changePassword"); }
        }//function change password

    }
}


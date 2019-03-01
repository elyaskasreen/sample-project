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
    public partial class FoodRelations : Window
    {
        MainWindow mainwindow;

        //constructer
        public FoodRelations(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
        }

        //functions to do when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            build_grid_PMaterials();
            build_grid_FoodMenu();
            build_grid_relationss();
            getPMaterials();
            getFoodMenu();
            getCountingUnits();
        }
        public void showmessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { txt_message.Text = message + "\n" + txt_message.Text; });
        }//display messages in window messages textbox

        private void getPMaterials()
        {
            mainwindow.runclient.send("811");
        }//ask server to send list of primary materials
        private void getFoodRelations()
        {
            try
            {
                FoodMenuItem FMI = (FoodMenuItem)grid_foodmenu.SelectedItem;
                string foodID = FMI.FoodMenuID.ToString();
                mainwindow.runclient.send("813", foodID);
            }
            catch { showmessage("ERROR 813 getFoodRelations()"); }
        }//ask server to send list of specific menu food ingredients
        private void getFoodMenu()
        {
            mainwindow.runclient.send("812");
        }//ask server to send list of menu's foods
        private void getCountingUnits()
        {
            mainwindow.runclient.send("814");
        }//ask server to send list of counting's units

        public void fillPMarterials(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
               {
                   grid_primary.ItemsSource = null;
                   var list = new List<PrimaryMaterialsItem>();
                   for (int i = 0; i + 4 < cells.Length; i += 4)
                   {
                       PrimaryMaterialsItem PMI = new PrimaryMaterialsItem();
                       PMI.PrimaryMaterialsID = int.Parse(cells[i + 1]);
                       PMI.MaterialName = cells[i + 2];
                       PMI.UnitName = cells[i + 3];
                       PMI.LastPrice = cells[i + 4];
                       list.Add(PMI);
                   }
                   grid_primary.ItemsSource = list;
               });
            }
            catch { showmessage("ERROR  fillPMarterials()"); }
        }//receives list of primary materials and fills DataGrid
        public void fillFoodMenu(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    grid_foodmenu.ItemsSource = null;
                    var list = new List<FoodMenuItem>();
                    for (int i = 0; i + 4 < cells.Length; i += 4)
                    {
                        FoodMenuItem FMI = new FoodMenuItem();
                        FMI.FoodMenuID = int.Parse(cells[i + 1]);
                        FMI.arabicname = cells[i + 2];
                        FMI.unitName = cells[i + 3];
                        FMI.price = cells[i + 4];
                        list.Add(FMI);
                    }
                    grid_foodmenu.ItemsSource = list;
                });
            }
            catch { showmessage("ERROR  fillFoodMenu()"); }
        }//receives list of menu's foods and fills DataGrid
        public void fillrelations(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    grid_relations.ItemsSource = null;
                    decimal total = 0;
                    var list = new List<RelationsItems>();

                    for (int i = 0; i + 5 < cells.Length; i += 5)
                    {
                        RelationsItems RI = new RelationsItems();
                        RI.relationID = int.Parse(cells[i + 1]);
                        RI.materialName = cells[i + 2];
                        RI.UnitName = cells[i + 3];
                        RI.amount = cells[i + 4];
                        RI.SUM = cells[i + 5];
                        total += decimal.Parse(RI.SUM);
                        list.Add(RI);
                    }
                    grid_relations.ItemsSource = list;
                    lbl_TotalCost.Content = total.ToString();
                });
            }
            catch { showmessage("ERROR  fillrelations()"); }
        }//receives list of menu food's ingredients and fills DataGrid
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
        }//receives list of counting units and fills combo

        private void build_grid_PMaterials()
        {
            try
            {
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "MaterialName";
                c1.Binding = new Binding("MaterialName");
                c1.Width = 100;
                grid_primary.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "UnitName";
                c2.Width = 80;
                c2.Binding = new Binding("UnitName");
                grid_primary.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "LastPrice";
                c3.Width = 58;
                c3.Binding = new Binding("LastPrice");
                grid_primary.Columns.Add(c3);
            }
            catch { showmessage("ERROR  build_grid_PMaterials()"); }
        }//build primary materials DataGrid
        private void build_grid_relationss()
        {
            try
            {
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "materialName";
                c1.Binding = new Binding("materialName");
                c1.Width = 90;
                grid_relations.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "UnitName";
                c2.Width = 80;
                c2.Binding = new Binding("UnitName");
                grid_relations.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "amount";
                c3.Width = 65;
                c3.Binding = new Binding("amount");
                grid_relations.Columns.Add(c3);
                DataGridTextColumn c4 = new DataGridTextColumn();
                c4.Header = "SUM";
                c4.Width = 65;
                c4.Binding = new Binding("SUM");
                grid_relations.Columns.Add(c4);
            }
            catch { showmessage("ERROR  build_grid_relationss()"); }
        }//build ingredients DataGrid
        private void build_grid_FoodMenu()
        {
            try
            {
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "arabicname";
                c1.Binding = new Binding("arabicname");
                c1.Width = 100;
                grid_foodmenu.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "unitName";
                c2.Width = 80;
                c2.Binding = new Binding("unitName");
                grid_foodmenu.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "price";
                c3.Width = 58;
                c3.Binding = new Binding("price");
                grid_foodmenu.Columns.Add(c3);
            }
            catch { showmessage("ERROR  build_grid_FoodMenu()"); }
        }//build menu's foods DataGrid

        private void addRelation()
        {
            try
            {
                FoodMenuItem FMI = (FoodMenuItem)grid_foodmenu.SelectedItem;
                PrimaryMaterialsItem PMI = (PrimaryMaterialsItem)grid_primary.SelectedItem;
                string foodID = FMI.FoodMenuID.ToString();
                string primaryID = PMI.PrimaryMaterialsID.ToString();
                decimal amount = 0;
                string[] boxes = new string[2];
                boxes[0] = "Insert Amount Please";
                boxes[1] = "Amont";
                GetValue getvalue = new GetValue(boxes);
                boxes = getvalue.show();
                if (boxes[0] == "NULL") { }
                else
                {
                    if (decimal.TryParse(boxes[0], out amount))
                    {
                        mainwindow.runclient.send("821", foodID, primaryID, amount.ToString());
                    }
                }
            }
            catch { showmessage("ERROR 821 addRelation()"); }
             
        }//ask server to add ingredients to menu food
        private void deleteRelation()
        {
            try
            {
                string relationID = (grid_relations.SelectedItem as RelationsItems).relationID.ToString();
                string foodID = (grid_foodmenu.SelectedItem as FoodMenuItem).FoodMenuID.ToString();

                const string message ="Are you sure you want to delete the selected Relation?";
                const string caption = "Delete Relation";
                var result = MessageBox.Show(message, caption,
                    MessageBoxButton.OKCancel
                    , MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    mainwindow.runclient.send("831", relationID, foodID);
                }     
            }
            catch
            {
                showmessage("ERROR 831 deleteRelation()");
            }
        }//ask server to delete ingredient of menu food
        private void addPrimaryMaterials()
        {
            try
            {
                if (txt_MaterialName.Text != "")
                {
                    string MaterialName = txt_MaterialName.Text;
                    string countingID = (cmb_countingUnit.SelectedItem as ComboBoxItem).Tag.ToString();
                    int lastPrice = 0;
                    if (int.TryParse(txt_lastPrice.Text, out lastPrice)) { }
                    mainwindow.runclient.send("822", MaterialName, countingID, lastPrice.ToString(),"TRUE");
                }
            }
            catch { showmessage("ERROR 822 addRelation()"); }
        }//ask server to add primary materials

        //get menu food ingredients when food menu DataGrid selection changed
        private void grid_foodmenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getFoodRelations();
        }
        //ask server to add ingredients to menu food
        private void grid_primary_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            addRelation();
        }
        //ask server to delete ingredients of menu food
        private void grid_relations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            deleteRelation();
        }
        //ask server to add new primary material
        private void btn_PM_add_Click(object sender, RoutedEventArgs e)
        {
            addPrimaryMaterials();
        }
        //clear primary materials window textboxes
        private void btn_PM_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_lastPrice.Text = "";
            txt_MaterialName.Text = "";
            cmb_countingUnit.SelectedIndex = 0;
        }

    }

    //class of primary materials uses to fill DataGrid
    public class PrimaryMaterialsItem
    {
        public int PrimaryMaterialsID { get; set; }
        public string MaterialName { get; set; }
        public string CountingUnitId { get; set; }
        public string UnitName { get; set; }
        public string LastPrice { get; set; }
        public bool isvisible { get; set; }
    }

    //class of menu's food uses to fill DataGrid
    public class FoodMenuItem 
    {
        public int FoodMenuID { get; set; }
        public string arabicname { get; set; }
        public string unitName { get; set; }
        public string price { get; set; }
        public int parentID { get; set; }
        public string englishName { get; set; }
        public string unitID { get; set; }
        public bool isaparent { get; set; }
        public bool isvisible { get; set; }
    }

    //class of relations of ingredients uses to fill DataGrid
    public class RelationsItems 
    {
        public int relationID { get; set; }
        public string materialName { get; set; }
        public string UnitName { get; set; }
        public string amount { get; set; }
        public string SUM { get; set; }
    }
}

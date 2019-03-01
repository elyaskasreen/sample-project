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
using System.Data;

namespace client
{
    /// <summary>
    /// Interaction logic for Store.xaml
    /// </summary>
    public partial class Store : Window
    {
        MainWindow mainwindow;

        //constructor
        public Store(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
           
        }

        //functions to do when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getPrimaryMaterials();
        }

        public void showmessage(string message)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { txt_message.Text = message + "\n" + txt_message.Text; });
        }//display messages in window messages textbox
        
        private void getPrimaryMaterials()
        {
            mainwindow.runclient.send("311");
        }//ask server to send a list of primary materials
        public void FillCombo(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    for (int i = 1; i < cells.Length - 1; i += 2)
                    {
                        ComboBoxItem CBI = new ComboBoxItem();
                        try { CBI.Tag = int.Parse(cells[i]); }
                        catch { showmessage("ERROR Combo int parse"); }
                        CBI.Content = cells[i + 1];

                        comboBox1.Items.Add(CBI);
                    }
                });
            }
            catch { showmessage("ERROR Primary Materials Fill combo"); }
        }//receives primary materials and fill combo
        public void filllabels(string[] cells)
        {
            int In = int.Parse(cells[1]);
            int Out = int.Parse(cells[2]);
            int res = In + Out;
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
            {
                lbl_in.Content = In.ToString();
                lbl_out.Content = Out.ToString();
                lbl_total.Content = res.ToString();

            });
        }//receives sums values and fill lables
        public void fillGrid(string[] cells)
        {
            try
            {
                string len = cells[1];
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    dgd1.ItemsSource = null;
                    buildColumns1();
                    List<Item> list = new List<Item>();
                    for (int i = 0; i < int.Parse(len); i++)
                    {
                        Item item = new Item();
                            item.MaterialName = cells[i * 4 + 2];
                            item.Quantity = cells[i * 4 + 3];
                            item.Date = cells[i * 4 + 4];
                            item.UserName = cells[i * 4 + 5];
                            list.Add(item);
                    }
                    dgd1.ItemsSource = list;
                });
            }
            catch
            {
                showmessage(" Probelm with fill DataGrid");
            }
        }//receives inventory list and fill DataGrid
        public void inventoryByBill(string[] cells)
        {
            try
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    dgd1.ItemsSource = null;
                    buildColumns2();
                    List<Item2> list = new List<Item2>();
                    for (int i = 0; i + 4 < cells.Length; i += 4)
                    {
                        Item2 II = new Item2();
                        II.MaterialName = cells[i + 1];
                        II.BAmount = cells[i + 2];
                        II.NAmount = cells[i + 3];
                        II.PAmount = cells[i + 4];
                        int Namount = 0;
                        int Pamount = 0;
                        int.TryParse(II.PAmount, out Pamount);
                        int.TryParse(II.NAmount,out Namount);
                        II.Store = Pamount + Namount;
                        list.Add(II);
                    }
                    dgd1.ItemsSource = list;
                });
            }
            catch
            {
                showmessage("ERROR inventoryByBill");
            }
        }//receives inventory compared with bill values list and fill DataGrid
        private void buildColumns2()
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
            {
                dgd1.Columns.Clear();
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "MaterialName";
                c1.Binding = new Binding("MaterialName");
                c1.Width = 115;
                dgd1.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "BAmount";
                c2.Width = 90;
                c2.Binding = new Binding("BAmount");
                dgd1.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "NAmount";
                c3.Width = 90;
                c3.Binding = new Binding("NAmount");
                dgd1.Columns.Add(c3);
                DataGridTextColumn c4 = new DataGridTextColumn();
                c4.Header = "PAmount";
                c4.Width = 90;
                c4.Binding = new Binding("PAmount");
                dgd1.Columns.Add(c4);
                DataGridTextColumn c5 = new DataGridTextColumn();
                c5.Header = "Store";
                c5.Width = 90;
                c5.Binding = new Binding("Store");
                dgd1.Columns.Add(c5);
            });
        }//build inventoryByBills DataGrid Columns 
        private void buildColumns1()
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
            {
                dgd1.Columns.Clear();
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "MaterialName";
                c1.Binding = new Binding("MaterialName");
                c1.Width = 120;
                dgd1.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "Quantity";
                c2.Width = 120;
                c2.Binding = new Binding("Quantity");
                dgd1.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "Date";
                c3.Width = 120;
                c3.Binding = new Binding("Date");
                dgd1.Columns.Add(c3);
                DataGridTextColumn c4 = new DataGridTextColumn();
                c4.Header = "UserName";
                c4.Width = 120;
                c4.Binding = new Binding("UserName");
                dgd1.Columns.Add(c4);
            });
        }//build inventory DataGrid Columns 

        private void btn_today_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.runclient.m.send("314");
        }//today button ask server to send inventory list
        private void btn_all_Click(object sender, RoutedEventArgs e)
        {
            // All Materials accoarding to textbox value
            int records;
            if (int.TryParse(txt_registers.Text, out records))
            {
                string[] cells = new string[2];
                cells[0] = "313";
                cells[1] = txt_registers.Text;
                mainwindow.runclient.m.send(cells);
            }
            else { showmessage("please insert avalid number in the registers box"); }
        }//Last Registers button ask server to send inventory list
        private void btn_addamount_Click(object sender, RoutedEventArgs e)
        {
            int val;
            int id;
            if (int.TryParse(txt_amount.Text, out val))
            {
                string Sval = val.ToString();
                try
                {
                    ComboBoxItem CBI = (ComboBoxItem)comboBox1.SelectedItem;
                    id = (int)CBI.Tag;

                    string[] cells = new string[4];
                    cells[0] = "321";
                    cells[1] = (id).ToString();
                    cells[2] = Sval;
                    cells[3] = "1";
                    mainwindow.runclient.m.send(cells);
                }
                catch { showmessage("Please choose a Material"); }
            }
            else
            {
                showmessage("please insert avalid value in amount box");
                txt_amount.Clear();
            }
        }//add amoun button ask server to add new register
        private void btn_inventoryByBills_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.runclient.send("316");
        }//inventoryByBills button ask server to send inventory list

        //ask server to send inventory list of specific primary materials
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem CBI = (ComboBoxItem)comboBox1.SelectedItem;
            int id = (int)CBI.Tag;

            string[] cells = new string[3];
            cells[0] = "312";
            cells[1] = (id).ToString();
            cells[2] = txt_registers.Text;
            mainwindow.runclient.send(cells);
            InOutLabels();
        }
        private void InOutLabels()
        {
            ComboBoxItem CBI = (ComboBoxItem)comboBox1.SelectedItem;
            int id = (int)CBI.Tag;
            string[] cells = new string[2];
            cells[0] = "315";
            cells[1] = (id).ToString();
            mainwindow.runclient.m.send(cells);
        }

        //print DataGrid content
        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                var paginator = new RandomTabularPaginator(dgd1.Items.Count,
              new Size(printDialog.PrintableAreaWidth,
                printDialog.PrintableAreaHeight), dgd1, "Store Inventory");

                printDialog.PrintDocument(paginator, "Store Inventory");
            }
        }
    }

    
    public class Item
    {
        public string MaterialName { get; set; }
        public string Quantity { get; set; }
        public string Date { get; set; }
        public string UserName { get; set; }
    }//class of inventory uses to fill DataGrid
    public class Item2
    {
        public string MaterialName { get; set; }
        public string BAmount { get; set; }
        public string NAmount { get; set; }
        public string PAmount { get; set; }
        public int Store { get; set; }
    }//class of inventoryByBills uses to fill DataGrid

    //Two class help with printing any DataGrid content
    public class PageElement : UserControl
    {
        private const int PageMargin = 75;
        private const int HeaderHeight = 25;
        private const int LineHeight = 20;
        private int ColumnWidth = 100;
        private DataGrid gridprint;
        private string fisrtLineTitle;
        private int _CurrentRow;
        private int _Rows;

        public PageElement(int currentRow, int rows, DataGrid gridprint, string fisrtLineTitle)
        {
            this.gridprint = gridprint;
            this.fisrtLineTitle = fisrtLineTitle;
            Margin = new Thickness(PageMargin);
            _CurrentRow = currentRow;
            _Rows = rows;
            ColumnWidth = 600 / gridprint.Columns.Count;
            if (ColumnWidth > 120) { ColumnWidth = 120; }
        }

        public static int RowsPerPage(double height)
        {
            return (int)Math.Floor((height - (2 * PageMargin)
               - HeaderHeight) / LineHeight);
        }

        private static FormattedText MakeText(string text)
        {
            return new FormattedText(text, System.Globalization.CultureInfo.CurrentCulture,
              FlowDirection.LeftToRight, new Typeface("Tahoma"), 14, Brushes.Black);
        }

        protected override void OnRender(DrawingContext dc)
        {
            try
            {
                Point curPoint = new Point(0, 0);

                dc.DrawText(MakeText(fisrtLineTitle), curPoint);
                curPoint.Y += LineHeight;

                for (int i = 0; i < gridprint.Columns.Count; i++)
                {
                    dc.DrawText(MakeText(gridprint.Columns[i].Header.ToString()), curPoint);
                    curPoint.X += ColumnWidth;
                }
                curPoint.X = 0;
                curPoint.Y += LineHeight;

                dc.DrawRectangle(Brushes.Black, null,
                   new Rect(curPoint, new Size(Width, 2)));
                curPoint.Y += HeaderHeight - LineHeight;

                for (int i = _CurrentRow; i < _CurrentRow + _Rows; i++)
                {
                    for (int j = 0; j < gridprint.Columns.Count; j++)
                    {

                        gridprint.ScrollIntoView(gridprint.Items[i], gridprint.Columns[j]);
                        DataGridRow row = (DataGridRow)gridprint.ItemContainerGenerator.ContainerFromIndex(i);
                        TextBlock cellContent = gridprint.Columns[j].GetCellContent(row) as TextBlock;
                        string ss = cellContent.Text;
                        dc.DrawText(MakeText(ss), curPoint);
                        curPoint.X += ColumnWidth;

                    }
                    curPoint.Y += LineHeight;
                    curPoint.X = 0;
                }
                gridprint.ScrollIntoView(gridprint.Items[0], gridprint.Columns[0]);
            }
            catch { }
        }
    }
    public class RandomTabularPaginator : DocumentPaginator
    {
        private int _RowsPerPage;
        private Size _PageSize;
        private int _Rows;
        private DataGrid gridprint;
        private string fisrtLineTitle;

        public RandomTabularPaginator(int rows, Size pageSize, DataGrid gridprint,string fisrtLineTitle)
        {
            this.gridprint = gridprint;
            this.fisrtLineTitle = fisrtLineTitle;
            _Rows = rows;
            PageSize = pageSize;
        }

        public override bool IsPageCountValid
        { get { return true; } }
        public override int PageCount
        { get { return (int)Math.Ceiling(_Rows / (double)_RowsPerPage); } }
        public override Size PageSize
        {
            get { return _PageSize; }
            set
            {
                _PageSize = value;

                _RowsPerPage = PageElement.RowsPerPage(PageSize.Height);

                //Can't print anything if you can't fit a row on a page
                System.Diagnostics.Debug.Assert(_RowsPerPage > 0);
            }
        }
        public override IDocumentPaginatorSource Source
        { get { return null; } }
        public override DocumentPage GetPage(int pageNumber)
        {
            int currentRow = _RowsPerPage * pageNumber;

            var page = new PageElement(currentRow,
              Math.Min(_RowsPerPage, _Rows - currentRow), gridprint,fisrtLineTitle)
            {
                Width = PageSize.Width,
                Height = PageSize.Height,
            };

            page.Measure(PageSize);
            page.Arrange(new Rect(new Point(0, 0), PageSize));

            return new DocumentPage(page);
        }
    }
}

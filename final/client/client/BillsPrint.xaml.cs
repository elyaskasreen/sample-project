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
    /// Interaction logic for BillsPrint.xaml
    /// </summary>
    public partial class BillsPrint : Window
    {

        //constructor
        public BillsPrint()
        {
            InitializeComponent();
            buildColumns(dataGrid1);
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            { dialog.PrintVisual(wrapPanel1, "Print Bill"); }
        }//print 

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//cancel printing

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
        }//build bill DataGrid columns 
    }
}

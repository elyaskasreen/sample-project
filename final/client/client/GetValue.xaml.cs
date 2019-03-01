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

namespace client
{
    /// <summary>
    /// Interaction logic for GetValue.xaml
    /// </summary>
    public partial class GetValue : Window
    {
        string[] boxes;
        int count = 0;

        //constructor
        public GetValue(string[] boxes)
        {
            this.boxes = boxes;
            InitializeComponent();
        }

        //build textboxes and show window
        public string[] show()
        {
            buildboxes(boxes);
            boxes = new string[1];
            boxes[0] = "NULL";
            this.ShowDialog();
            return this.boxes;
        }

        //building textboxes according to constructor variabels
        public void buildboxes(string[] boxes)
        {
            count = boxes.Length - 1;
            groupBox1.Header = boxes[0];
            for (int i = 0; i+1 < boxes.Length;i++ )
            {
                Label lbl = new Label();
                lbl.Content = boxes[i+1];
                lbl.Width = 120;
                wrapPanel1.Children.Add(lbl);
                TextBox txt = new TextBox();
                txt.Text = "";
                txt.Name = "txt_get" + i;
                txt.Width = 120;
                wrapPanel1.Children.Add(txt);
                this.RegisterName("txt_get" + i, txt);
            }
            Button but_ok = new Button();
            but_ok.Width = 120;
            but_ok.Content = "OK";
            but_ok.IsDefault = true;
            but_ok.Click += new RoutedEventHandler(btn_OK_click);
            wrapPanel1.Children.Add(but_ok);

            Button but_cancel = new Button();
            but_cancel.Width = 120;
            but_cancel.Content = "CANCEL";
            but_cancel.IsCancel = true;
            but_cancel.Click += new RoutedEventHandler(but_cancel_click);
            wrapPanel1.Children.Add(but_cancel);
            (FindName("txt_get0") as TextBox).Focus();
        }

        //return textboxes values in array
        private void btn_OK_click(object sender, RoutedEventArgs e)
        {
            string[] boxes = new string[count];
            for (int i = 0; i < count; i++)
            {
                boxes[i] = (FindName("txt_get" + i) as TextBox).Text;
            }
            this.boxes = boxes;
            this.Close();
        }

        //return null and close window
        private void but_cancel_click(object sender, RoutedEventArgs e)
        {
            string[] boxes = new string[1];
            boxes[0] = "NULL";
            this.boxes = boxes;
            this.Close();
        }
    }
}

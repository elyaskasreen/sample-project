   M S B A M L   `   `   `  ����
  client    client.billsOldShow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  billsOldShow��5   	   $	��789��6   $	��615��6+    � WindowState$ Normal=�6N    � WindowStartupLocation$ CenterScreen=�6c   .��5      � $	��547��6   .+�5   
   �� -   5        $��	groupBox1��5   b   $��	OLD Bills��6   $	��349��6&   $
��Left=�63   $��
14,148,0,0q�6N   $	��Top=�6s   $	��252��6�   .��5      � .+�5      &   System.Windows.Controls.DataGrid  -   5        $��grid_oldbills��5   K      AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�6�    � ToolTip$&  "Double Click To Print The Bill"��6�   5	      5
      �� 5   
   -   5        $��	groupBox2��5   f   $��Bill Details��6   $	��349��6)   $
��Left=�66   $��270,148,0,0q�6Q   $	��Top=�6w   $	��267��6�   .��5      � .+�5        -   5        $��grid_billsdetails��5   I    .   . 5      5      �� 5   
   -   5        $��	groupBox3��5   P   $��Finance Details��6   $	��169��6,   $��205,503,180,0q�69   $	��Top=�6a   .��5      � .+�5      �� -   5        $��label1��5   i   $��Bill Net Total��6   $��28��61   $
��Left=�6=   $��6,6,0,0q�6X   $	��Top=�6w   �� 5      -   5        $��label2��5   `   $
��Tax1��6   $��28��6'   $
��Left=�63   $��6,32,0,0q�6N   $	��Top=�6n   �� 5      -	   5        $��label3��5   `   $
��Tax2��6   $��28��6'   $
��Left=�63   $��6,58,0,0q�6N   $	��Top=�6n   �� 5      -
   5        $��label4��5   d   $��Discount��6   $��28��6+   $
��Left=�67   $��6,84,0,0q�6R   $	��Top=�6r   �� 5      -   5        $��label5��5   m   $��Bill Final Total��6   $��28��63   $
��Left=�6?   $��	6,112,0,0q�6Z   $	��Top=�6{   �� 5      -   5        $��lbl_NetTotal��5   ^   $��0��6   $��28��6$   $
��Left=�60   $��	100,6,0,0q�6K   $	��Top=�6r   �� 5      -   5        $��lbl_tax1��5   _   $��0��6   $��28��6$   $
��Left=�60   $��
100,32,0,0q�6K   $	��Top=�6o   �� 5      -   5        $��lbl_tax2��5   _   $��0��6   $��28��6$   $
��Left=�60   $��
100,58,0,0q�6K   $	��Top=�6o   �� 5      -   5        $��lbl_discount��5   _   $��0��6   $��28��6$   $
��Left=�60   $��
100,84,0,0q�6K   $	��Top=�6s   �� 5      -   5        $��lbl_finalTotal��5   `   $��0��6   $��28��6$   $
��Left=�60   $��100,112,0,0q�6K   $	��Top=�6v   5      5      �� 5   
   -   5        $��	groupBox4��5   `   $��Details��6   $	��169��6$   $
��Left=�61   $��
14,503,0,0q�6L   $	��Top=�6q   $	��184��6�   .��5      � .+�5       �� -   5        $��label6��5    f   $��
Start Date��6   $��28��6-   $
��Left=�69   $��6,32,0,0q�6T   $	��Top=�6t   �� 5!      -   5        $��label7��5!   d   $��End Date��6   $��28��6+   $
��Left=�67   $��6,58,0,0q�6R   $	��Top=�6r   �� 5"      -   5        $��label8��5"   c   $��Captain��6   $��28��6*   $
��Left=�66   $��6,0,0,34q�6Q   $��Bottom=�6q   �� 5#      -   5        $��label9��5#   a   $��Table#��6   $��28��6)   $
��Left=�65   $��6,6,0,0q�6P   $	��Top=�6o   �� 5$      -   5        $��label10��5$   c   $��Client��6   $��28��6)   $
��Left=�65   $��	6,112,0,0q�6P   $	��Top=�6r   �� 5%      -   5        $��lbl_startDate��5%   g   $��
Start Date��6   $��28��6-   $
��Left=�69   $��	75,32,0,0q�6T   $	��Top=�6|   �� 5&      -   5        $��lbl_endDate��5&   e   $��End Date��6   $��28��6+   $
��Left=�67   $��	75,58,0,0q�6R   $	��Top=�6x   �� 5'      -   5        $��lbl_captain��5'   d   $��Captain��6   $��28��6*   $
��Left=�66   $��	75,84,0,0q�6Q   $	��Top=�6w   �� 5(      -   5        $��	lbl_table��5(   b   $��Table#��6   $��28��6)   $
��Left=�65   $��75,6,0,0q�6P   $	��Top=�6s   �� 5)      -   5        $��
lbl_client��5)   d   $��Client��6   $��28��6)   $
��Left=�65   $��
75,112,0,0q�6P   $	��Top=�6v   5*      5+      �� 5,   
   -   5        $��	groupBox5��5,   _   $��Filters��6   $	��136��6$   $
��Left=�61   $��	14,12,0,0q�6L   $	��Top=�6p   $	��521��6�   .��5-      � .+�5.      �� -   5        $��btn_all��5.   e   $��	All Bills��6   $��23��6-   $
��Left=�69   $��6,6,0,0q�6T   $	��Top=�6t   $��84��6�   6�   �� 5/      -   5        $��btn_date��5/   d   $��By Date��6   $��23��6+   $
��Left=�67   $��6,32,0,0q�6R   $	��Top=�6t   $��84��6�   6�   �� 50      -   5        $��btn_captain��50   g   $��
By Captain��6   $��23��6.   $
��Left=�6:   $��6,58,0,0q�6U   $	��Top=�6z   $��84��6�   6�   �� 51      -    5        $��label11��51   b   $
��From��6   $��28��6'   $
��Left=�63   $��
100,30,0,0q�6N   $	��Top=�6q   �� 52      -!   5        $��label12��52   `   $��To��6   $��28��6%   $
��Left=�61   $��
284,30,0,0q�6L   $	��Top=�6o   (  "System.Windows.Controls.DatePicker  53      -"   5        $��datePicker1��53   X   $��25��6   $
��Left=�6)   $��
141,31,0,0q�6D   $	��Top=�6k   $	��115��6�     54      -#   5        $��datePicker2��54   X   $��25��6   $
��Left=�6)   $��
313,31,0,0q�6D   $	��Top=�6k   $	��115��6�   �� 55      -$   5        $��cmb_captain��55   V   $��23��6   $
��Left=�6'   $��
141,59,0,0q�6B   $	��Top=�6i   $	��115��6�   �� 56      -%   5        $��button1��56   l   $��By Cap&Date��6   $��23��63   $
��Left=�6?   $��6,84,0,0q�6Z   $	��Top=�6{   $��84��6�   6�   57      58      �� 59   
   -&   5        $��	groupBox6��59   J   $��
All Totals��6   $	��169��6'   $��373,503,10,0q�64   $	��Top=�6[   .��5:      � .+�5;      �� -'   5        $��label13��5;   i   $��Bill Net Total��6   $��28��61   $
��Left=�6=   $��6,6,0,0q�6X   $	��Top=�6x   �� 5<      -(   5        $��label14��5<   `   $
��Tax1��6   $��28��6'   $
��Left=�63   $��6,32,0,0q�6N   $	��Top=�6o   �� 5=      -)   5        $��label15��5=   `   $
��Tax2��6   $��28��6'   $
��Left=�63   $��6,58,0,0q�6N   $	��Top=�6o   �� 5>      -*   5        $��label16��5>   d   $��Discount��6   $��28��6+   $
��Left=�67   $��6,84,0,0q�6R   $	��Top=�6s   �� 5?      -+   5        $��label17��5?   m   $��Bill Final Total��6   $��28��63   $
��Left=�6?   $��	6,112,0,0q�6Z   $	��Top=�6|   �� 5@      -,   5        $��
lbl_allNet��5@   Q   $��0��6   $
��Left=�6$   $��93,6,0,0q�6?   $��28��6c   $	��Top=�6o   �� 5A      --   5        $��lbl_allTax1��5A   ^   $��0��6   $��28��6$   $
��Left=�60   $��	93,32,0,0q�6K   $	��Top=�6q   �� 5B      -.   5        $��lbl_allTax2��5B   ^   $��0��6   $��28��6$   $
��Left=�60   $��	93,58,0,0q�6K   $	��Top=�6q   �� 5C      -/   5        $��lbl_allDiscount��5C   ^   $��0��6   $��28��6$   $
��Left=�60   $��	93,0,0,34q�6K   $��Bottom=�6u   �� 5D      -0   5        $��lbl_allfinal��5D   _   $��0��6   $��28��6$   $
��Left=�60   $��
93,112,0,0q�6K   $	��Top=�6s   5E      5F      �� 5G   
   -1   5        $��	groupBox7��5G   g   $��Status Messeges��6   $��71��6,   $
��Left=�68   $��
14,672,0,0q�6S   $	��Top=�6x   $	��521��6�   .��5H      �� -2   5        $��txt_message��5H   '   $��0,0,0,0q�6   6'   5I      5J      5K      
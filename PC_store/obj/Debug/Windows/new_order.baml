   M S B A M L   `   `   `  ����C  ?PC_store, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PC_storePC_store  5   	       PC_store.new_order   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:PC_store     5   	     � Title$  	new_order��5   	   $��338.455��6   $��395.665��6,    � 
ResizeMode$ NoResize=�6<    � WindowStyle$
 None=�6R   .��5	      � .+�5
   
   � -   5         �Name$ header��5
   �   ���	#FFF05219$
��Left=�6&   $��20��6A   $	��Top=�6M   $��0,0,0,0q�6e   $	��396��6v   .+�5      �� -   5         ��Name$
 exit��5   =      SysButtonStyle#��[  6   $��20��6K   $��20��6V   $��Center=�6b   $��Center=�6   $��X��6�   $��	376,0,0,0q�6�   �� 5      
�������$��	1,1,162,0q�61   #!оформление заказа6W   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5         AutoGenerateColumns . $
��Left=�6@   $	��Top=�6[   $��1,20,0,0q�6s   $	��316��6�   $	��241��6�   {�.  6�   �� 5   
   -   5         ��Name$ comboBox��5      $
��Left=�6%   $��244,98,-1,0q�6@   $	��Top=�6U   $	��153��6m   6y   �� 5   
   -   5        $��
new_client��5        ButtonStyle#��[ 6#   $&�� Создать уч.запись��6H   $��242,20,10,292q�6d   6{   �� 5   
   -   5        $��	id_client��5      $��№ клиента��6#   $��243,62,33,256q�64   �� 5   
   -   5         ��Name$ 	textBlock��5      $
��Left=�6'   $��
245,84,0,0q�6B   $
��Wrap=�6V   $��Скидка��6j   $	��Top=�6x   �� 5   
   -   5        $ create_order��5      $!��Оформить заказ��6'   #��[ 6@   $
��Left=�6e   $��241,160,0,0q�6�   $	��Top=�6�   $	��154��6�   6�   �� 5   
   $
��Left=�6   $��245,128,0,0q�6/   $��итого:��6D   $	��Top=�6R   �� 5   
   -	   5        $ price��5   d   $
��Left=�6   $��244,145,0,0q�6/   $�� ��6D   $	��Top=�6L   6d   �� 5   
   $
��Left=�6   $��361,145,0,0q�6/   $��руб.��6D   $	��Top=�6P   5      5      
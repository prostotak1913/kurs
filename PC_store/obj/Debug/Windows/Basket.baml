   M S B A M L   `   `   `  ����C  ?PC_store, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PC_storePC_store  5   	       PC_store.Basket_Window   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:PC_store     5   	     � Title$  Basket��5   	   $	��387��6   $	��414��6%    � WindowStyle$
 None=�61    � 
ResizeMode$ NoResize=�6D   
���   �$��1q�6n   .��5	      � .+�5
   
   � -   5         �Name$ header��5
   �   ���	#FFF05219$
��Left=�6&   $��20��6A   $	��Top=�6M   $��0,0,0,0q�6e   $	��412��6v   .+�5      �� -   5         ��Name$
 exit��5   =      SysButtonStyle#��[  6   $��20��6K   $��20��6V   $��Center=�6b   $��Center=�6   $��X��6�   $��	392,0,0,0q�6�   �� 5      
�������$��	1,1,147,0q�61   Корзина6M   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5         AutoGenerateColumns . $
��Left=�6@   $	��Top=�6[   $��0,46,0,0q�6s   $	��263��6�   $	��412��6�      CanUserAddRows .     CanUserDeleteRows .  	   
IsReadOnly	 . 6�   �� 5   
   -   5        
 ��Name$
 id_prod��5      $
��Left=�6#   $��24��6>   $��0,20,0,0q�6J    �� TextWrapping$
 Wrap=�6\   $��№ позиции��6p   $	��Top=�6�   $��72��6�   �� 5   
   -   5        $	 del��5      $��удалить��6     ButtonStyle#��[ 60   $
��Left=�6U   $��	76,20,0,0q�6p   $	��Top=�6�   $��80��6�   $��22��6�   6�   �� 5   
   -   5        $ new_emp��5      $��оформить��6"   #��[ 65   $
��Left=�6Z   $��	2,336,0,0q�6u   $	��Top=�6�   $	��247��6�   $��39��6�   6�   �� 5   
   $
��Left=�6   $��
11,314,0,0q�6/   $
��Wrap=�6C   $��Стоймость:��6W   $	��Top=�6i   $��65��6�   �� 5   
   -   5         ��Name$ 	all_price��5      $
��Left=�6'   $��
75,315,0,0q�6B   $
��Wrap=�6V   $	��Top=�6j   $	��142��6�   �� 5   
   $
��Left=�6   $��220,315,0,0q�6/   $
��Wrap=�6D   $��руб.��6X   $	��Top=�6d   $��27��6|   $��16��6�   5      5      
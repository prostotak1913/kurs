   M S B A M L   `   `   `  ����C  ?PC_store, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PC_storePC_store  5   	       PC_store.Find   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:PC_store     5   	     � Title$
  Find��5   	   $��372.5��6   $	��400��6%    � 
ResizeMode$ NoResize=�61    � WindowStyle$
 None=�6G   
���   �$��1q�6n   .��5	      � $��0,0,0,0q�6   .+�5
   
   � -   5         �Name$ header��5
   �   ���	#FFF05219$
��Left=�6&   $��20��6A   $	��Top=�6M   $��0,0,0,0q�6e   $	��400��6v   .+�5      �� -   5         ��Name$
 exit��5   =      SysButtonStyle#��[  6   $��20��6K   $��20��6V   $��Right=�6b   $��Center=�6~   $��X��6�   $��0,0,1,0q�6�   �� 5      
�������$��	1,1,162,0q�61   
Поиск6K   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $
��Left=�6%   $��0,0,0,0q�6@   $��Bottom=�6Q   $	��398��6l   $	��220��6x      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly . 6�   �� 5   
   -   5        	 ��Name$	 Search_text��5      {�.  $
��Left=�69   $��22��6T   $��	8,118,0,0q�6`   
 �� TextWrapping$

 Wrap=�6s   $	��...��6�   $	��Top=�6�   $	��205��6�   �� 5   
   $
��Left=�6   $��	10,65,0,0q�6/   $
��Wrap=�6B   $��В столбце��6V   $	��Top=�6g   �� 5   
   -   5         ��Name$	 col��5      {�.  $
��Left=�62   $��8,80,0,0q�6M   $	��Top=�6_   $	��120��6w   6�   �� 5   
   -   5        $	 tab��5      $
��Left=�6    $��8,39,0,0q�6;   $	��Top=�6M   $	��120��6e   6q   �� 5   
   $
��Left=�6   $��8,23,0,0q�6/   $
��Wrap=�6A   $��В таблице��6U   $	��Top=�6f   �� 5   
   -   5        $ search��5        ButtonStyle#��[ 6!   $��
Поиск��6F   {�.  $
��Left=�6h   $��212,116,0,0q�6�   $	��Top=�6�   $��84��6�   $��24��6�   6�   �� 5   
   -   5        $ clear��5      #��[ 6    $��очистить��6E   $
��Left=�6X   $��296,115,0,0q�6s   $	��Top=�6�   $��90��6�   $��24��6�   6�   5      5      
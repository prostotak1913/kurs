   M S B A M L   `   `   `  ����C  ?PC_store, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PC_storePC_store  5   	       PC_store.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:PC_store     5   	     � Title$  
MainWindow��5   	   $��628.5��6   $��807.333��6+    � WindowStyle$
 None=�6;   
���   �$��1q�6b    � 
ResizeMode$ NoResize=�6v   .��5	      � -   5        $
��Main��5	      $
��Left=�6   $	��807��62   $��0,0,-2,0q�6>   .+�5
   
   o� -   5         o�Name$ MainMenu��5
      $��28��6!   $	��Top=�6-   ���	#FFF05219$��0,0,2,0q�6\   .@�5      �� $,��&PC store                              ��6   
�������$��19(�6_   $��81��6m   �� 5      -   5         ��Name$ 	name_user��5      $	��392��6+   $��Ваше Имя��67   $��14.667(�6G   
�������6Y   m� 5      -   5         m�Name$ SignIn��5      $��26��6'   $��46��63   $��вход��6>   $��14.667(�6L   
�������{�.  6�   m� 5      -   5        $ config��5      $��26��6'   $��84��63   $��настройки��6>   $��14.667(�6Q   
�������.@�5      m� -   5        $ conect��5      $��Подключение��6+   ���	#FFF052196U   5      m� 5      -   5        $ Baskcet��5   .   $��26��6   $��70��6#   $��корзина��6?   
�������$��14.667(�6i   {�.  6�   5      �� 5   
   -   5         ��Name$
 exit��5   9      SysButtonStyle#��[  6   $��780,4,7,602q�6G   $��20��6\   $��20��6g   $��Center=�6s   $��Center=�6�   $��X��6�   6�   �� 5   
   -	   5        $ deAuth��5   9   #��[  6   $��840,4,63,730q�6I   $��50��6_   $��20��6j   $��Center=�6v   $��Center=�6�   $��
Выход��6�   $y�Hidden=�6�   o� 5   
   -
   5        $
 menu��5      {�.  $
��Left=�6/   $	��726��6J   $	��Top=�6W   $	��105��6o   $��0,28,0,0q�6{   
���P��.@�5      m� $��Товары��6   
�������$	��104��6@   .@�5      m� -   5        $ new_prod��5   C   $��Новый товар��6   
���P��6U   m� 5      -   5        $ Intake��5   C   $��Поступление��6   
���P��6S   m� 5      -   5        $
 sale��5   ?   $��Продажа��6   
���P��6M   5      m� 5      $��Сотрудники��6   
�������$	��104��6D   $��24��6P   .@�5      m� -   5        $ new_emp��5   B   $��Добавление��6   
���P��6S   m� 5      -   5        $ del_emp��5   @   $��Удаление��6   
���P��6Q   5      m� 5      $��Скидки��6   
�������$	��104��6@   $��24��6L   .@�5       m� -   5        $��add_dis��5    0   $��новая акция��6   
���P��6R   m� 5!      -   5        $ del_dis��5!   -   $��удаление��6   
���P��6Q   5"      m� 5#      -   5        $��f��5#   ^   $��
Поиск��6   
�������
���P��$	��106��6R   6g   m� 5$      -   5        $��output��5$   ^   $��
Вывод��6   
�������
���P��$	��106��6R   6l   5&      &   System.Windows.Controls.DataGrid  5'   
   -   5          Name$ dataGrid��5'      $
��Left=�6%   $��
105,28,0,0q�6@   $	��Top=�6T   $	��598��6l   $	��700��6y   5*      5+      
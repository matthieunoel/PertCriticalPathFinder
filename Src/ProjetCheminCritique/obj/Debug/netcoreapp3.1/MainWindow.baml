   M S B A M L   `   `   `  ����O  KProjetCheminCritique, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ProjetCheminCritiqueProjetCheminCritique  5   	   %    ProjetCheminCritique.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ProjetCheminCritique     5   	   -   5          �Name$  MainWindow1��5   	    � Title$ Critical Path Finder��5   	   $	��450��6&   $	��800��63    � 
ResizeMode$ 	CanResize=�6R   .��5	      � .+�5
   
   �� -   5         ��Name$ 
MainButton��5
      $��
MainButton��6%   $
��Left=�6:   $��	20,0,0,20q�6U   $��Bottom=�6h   $��Center=�6�   $��Center=�6�   $	��100��6�   $
��True��6�   $��33��6  �� 5   
   -   5        $ SecondaryButton��5      $��SecondaryButton��6*   $
��Left=�6D   $��	20,0,0,63q�6_   $��Bottom=�6r   $��Center=�6�   $��Center=�6�   $	��100��6�   $
��True��6�   $��33��6!  �� 5   
   -   5        $ TerciButton��5      $��TerciButton��6&   $
��Left=�6<   $��
20,0,0,106q�6W   $��Bottom=�6k   $��Center=�6�   $��Center=�6�   $	��100��6�   $
��True��6�   $��33��6  �� 5   
   -   5         ��Name$ DisplayArea��5      $��DisplayArea��6'   $��140,20,20,20q�6:   $��2,2,2,2q�6P   ���	#3F000000$
��Auto=�6�   $
��Auto=�6�    �� 
IsReadOnly$
 True��6�    � Cursor$ Arrow��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DisplayGrid��5      $��140,20,20,20q�6(   $��2,2,2,2q�6>   ���	#3F000000$
��Auto=�6p   $
��Auto=�6�      
IsReadOnly$
 True��6�   $ Arrow��6�   5      5      
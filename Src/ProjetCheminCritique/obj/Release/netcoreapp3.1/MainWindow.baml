   M S B A M L   `   `   `  ���� O  KProjetCheminCritique, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ProjetCheminCritiqueProjetCheminCritique  %    ProjetCheminCritique.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:ProjetCheminCritique     -     �Name$  MainWindow1�� � Title$ Critical Path Finder��$	��450��$	��800�� � 
ResizeMode$ 	CanResize=�.��� .+��� -    ��Name$ 
MainButton��$��
MainButton��$
��Left=�$��	20,0,0,20q�$��Bottom=�$��Center=�$��Center=�$	��100��$
��True��$��33���� -   $ SecondaryButton��$��SecondaryButton��$
��Left=�$��	20,0,0,63q�$��Bottom=�$��Center=�$��Center=�$	��100��$
��True��$��33���� -   $ TerciButton��$��TerciButton��$
��Left=�$��
20,0,0,106q�$��Bottom=�$��Center=�$��Center=�$	��100��$
��True��$��33���� -    ��Name$ DisplayArea��$��DisplayArea��$��140,20,20,20q�$��2,2,2,2q����	#3F000000$
��Auto=�$
��Auto=� �� 
IsReadOnly$
 True�� � Cursor$ Arrow��&   System.Windows.Controls.DataGrid  -     Name$ DisplayGrid��$��140,20,20,20q�$��2,2,2,2q����	#3F000000$
��Auto=�$
��Auto=�   
IsReadOnly$
 True��$ Arrow��
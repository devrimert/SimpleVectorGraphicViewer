   M S B A M L   `   `   `  ����V  RSimpleVectorGraphicViewerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SimpleVectorGraphicViewerV2.View SimpleVectorGraphicViewerV2.View  5   	   /    )SimpleVectorGraphicViewerV2.View.MainView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @local.clr-namespace:SimpleVectorGraphicViewerV2.View     5   	   -   5          � Icon$  /Images/AppIco.ico��5   	    � Title$" Simple Vector Graphic Viewer��5	   	   
����������#2e3c49$	��450��6Y   $��14(�6f   $	��800��6t   $	��400��6�   $	��450��6�   .��5
      �  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��25 �6   .+�5   
   o� $��0��6   $��0,0,0,2q�6   ���#2e3c49.@�5      m� $��_File��6   
�������.@�5      m� -   5         m�Name$ OpenFileBtn��5   *   $��_Open��6   
���   �6Y   m� 5      -   5        $ CloseFileBtn��5   +   $��_Close��6   
���   �6\   �� 5      m� 5      -   5        $ CloseProgramBtn��5   *   $��E_xit��6   
���   �6a   5      m� 5      $��_View��6   
�������.@�5      m� $��Show Axis Lines��6   
���   �.@�5      �� -   5         ��Name$ 
ShowAxisCB��5      $-��'Program will create X and Y Axis lines.��63    d� 	IsChecked$
 TrueY�6e   6�   5      m� 5       $��Scale In��6   
���   �.@�5!      �� -   5        $ 	ScaleInCB��5!      $6��0Program will scale in if the shape is too small.��62   5"      m� 5#      -   5        $ MinimizeBtn��5#   5   $��M_inimize Window��6   
���   �6d   m� 5$      -   5        $ MaximizeBtn��5$   5   $��M_aximize Window��6   
���   �6d   5%      m� 5&      $��_About��6   
�������.@�5'      m� -	   5        $ LinkedInPageBtn��5'   ?   $ ��Developer's _LinkedIn Page��6   
���   �6v   m� 5(      -
   5        $ GitHubPageBtn��5(   =   $��Developer's _GitHub Page��6   
���   �6p   m� 5)      -   5        $ XingPagEBtn��5)   ;   $��Develoepr's _Xing Page��6   
���   �6k   5*      5+      � 5.   
   $��1��6    � ColumnDefinitions 5/      �� 50      $��* �6#   �� 51      $	��200 �6#   .+�55      �� $��0��6   ._�56      �� ���#132130��.  $��Hidden=�6M   $��Hidden=�6t   .��57      �� -   5         ��Name$ canvas��57      $��75��6-   $��75��6<   ���#132130$��Center=�6_   $��Center=�6}   58   Q   59      5:      �� 5>      $��1��6   ._�5?      �� $
��Auto=�6   .��5@      &   System.Windows.Controls.DataGrid  -   5          Name$ PropertyViewDG��5@      ���#2e3c49	   HeadersVisibility$
	 None=�6L   
   AutoGenerateColumns
 .    
IsReadOnly . 6�   5A      5B      5D      �� 5G   
   $��2��6   ._�5H      �  5I      �� 5J      $��* �6'   .+�5L      �� -   5         ��Name$ 
mousePosTB��5L      $��0��60   $
��Left=�6@   $��5,0,0,5q�6[   �� 5M      -   5        $ activeFileNameTB��5M      $��1��66   $��Center=�6F   $��5,0,0,5q�6c   �� 5N      -   5        $ currentScaleTB��5N      $��2��64   $��Right=�6D   $��5,0,0,5q�6`   5O      5P      5R      5S      
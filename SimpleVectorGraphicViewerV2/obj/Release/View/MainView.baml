   M S B A M L   `   `   `  ���� V  RSimpleVectorGraphicViewerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SimpleVectorGraphicViewerV2.View SimpleVectorGraphicViewerV2.View  /    )SimpleVectorGraphicViewerV2.View.MainView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @local.clr-namespace:SimpleVectorGraphicViewerV2.View     -     � Icon$  /Images/AppIco.ico�� � Title$" Simple Vector Graphic Viewer��
����������#2e3c49$	��450��$��14(�$	��800��$	��400��$	��450��.���  � RowDefinitions �� $��20 ��� $��* ��� $��25 �.+�o� $��0��$��0,0,0,2q����#2e3c49.@�m� $��_File��
�������.@�m� -    m�Name$ OpenFileBtn��$��_Open��
���   �m� -   $ CloseFileBtn��$��_Close��
���   ��� m� -   $ CloseProgramBtn��$��E_xit��
���   �m� $��_View��
�������.@�m� $��Show Axis Lines��
���   �.@��� -    ��Name$ 
ShowAxisCB��$-��'Program will create X and Y Axis lines.�� d� 	IsChecked$
 TrueY�m� $��Scale In��
���   �.@��� -   $ 	ScaleInCB��$6��0Program will scale in if the shape is too small.��m� -   $ MinimizeBtn��$��M_inimize Window��
���   �m� -   $ MaximizeBtn��$��M_aximize Window��
���   �m� $��_About��
�������.@�m� -	   $ LinkedInPageBtn��$ ��Developer's _LinkedIn Page��
���   �m� -
   $ GitHubPageBtn��$��Developer's _GitHub Page��
���   �m� -   $ XingPagEBtn��$��Develoepr's _Xing Page��
���   �� $��1�� � ColumnDefinitions �� $��* ��� $	��200 �.+��� $��0��._��� ���#132130��.  $��Hidden=�$��Hidden=�.���� -    ��Name$ canvas��$��75��$��75�����#132130$��Center=�$��Center=��� $��1��._��� $
��Auto=�.��&   System.Windows.Controls.DataGrid  -     Name$ PropertyViewDG�����#2e3c49	   HeadersVisibility$
	 None=�
   AutoGenerateColumns
 .    
IsReadOnly . �� $��2��._��  �� $��* �.+��� -    ��Name$ 
mousePosTB��$��0��$
��Left=�$��5,0,0,5q��� -   $ activeFileNameTB��$��1��$��Center=�$��5,0,0,5q��� -   $ currentScaleTB��$��2��$��Right=�$��5,0,0,5q�
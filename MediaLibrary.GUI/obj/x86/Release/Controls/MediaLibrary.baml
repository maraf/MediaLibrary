   M S B A M L   `   `   `  ���� P  LDesktopCore.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH8clr-namespace:DesktopCore;assembly=DesktopCore.ResourcesDesktopCore  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem P7clr-namespace:System.Windows.Input;assembly=WindowsBaseSystem.Windows.Input  MediaLibrary.GUI(   "MediaLibrary.Controls.MediaLibrary   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    Dc8clr-namespace:DesktopCore;assembly=DesktopCore.Resources    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Gcm8clr-namespace:System.ComponentModel;assembly=WindowsBase     6sys&clr-namespace:System;assembly=mscorlib     Iinput7clr-namespace:System.Windows.Input;assembly=WindowsBase       � 	Resources   
  movies �� Source �� SortDescriptions+  %System.ComponentModel.SortDescription   PropertyName   	Direction  keysProvider S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeName  keys��%�   &	        &	 =     &	 p     7  ��  �� *Movies+   $
 Name��$ 	Ascending=�S� $ 	GetValues��"   M� $ 	input:Key���� -   8   .��� .+�� $	��510��	 � RowDefinitions	 �� $
��Auto ��� $
��Auto ��� �� $
��Auto ��� $
��Auto �.+�]� -   
 ]�Name$
 dplControls��$��0��.+���  
 border#��[ $��5,0,5,0q�._�� $��5,2,0,2q�.+���  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ btnSaveLibrary��  imageButton#��[  � ToolTip    DesktopCore.Resource  *SaveLibrary+.���� $5��//MediaLibrary.GUI;component/Images/FileSave.png���� -   $ btnRenameLibrary��#��[    *DatabaseConfiguration+.���� $7��1/MediaLibrary.GUI;component/Images/FileConfig.png���� -   $ btnCloseLibrary��#��[    *CloseLibrary+.���� $6��0/MediaLibrary.GUI;component/Images/FileClose.png���� $ 
Horizontal=�$��Center=�.+��� 
�������$��Center=����� *Name+�� $ 
Horizontal=�$��Right=�.+��� -   $ btnPasteLibrary��#��[    *PasteLibrary+.���� $6��0/MediaLibrary.GUI;component/Images/FilePaste.png���� -   $ btnCopyLibrary��#��[    *CopyLibrary+.���� $5��//MediaLibrary.GUI;component/Images/FileCopy.png���� -   $ btnExportLibrary��#��[    *ExportLibrary+.���� $7��1/MediaLibrary.GUI;component/Images/FileExport.png��9� -	    9�Name$ 	expFilter��$��1����]� $	��460��.+�d� -
    d�Name$ btnToggleStarredFilter��  toggleStarButton#��[ $��0q�$��Right=�   *ToggleStarred+ d� 	IsChecked �� *Filter.Starred+�� ��  *Filter+.���  � ColumnDefinitions �� $��70 ��� �� $��70 ��� 	 �� �� �� �� �� .+��� $��0��$��0����  *Name+�� $��0��$��1����  *
Category+�� $��0��$��2����  *	Storage+�� $��0��$��3����  *Actor+�� $��2��$��0����  *	Country+�� $��2��$��1����  *Year+�� $��2��$��2����  *Added+�� -    ��Name$ tbxNameFilter��$��5q�$��1��$��0������ *Filter.Name+��  �� 
IsEditable . $��5q�$��1��$��1�� �� Text �� *Filter.Category+���� *Movies.Categories+�� $��5q�$��1��$��2������ *Filter.Storage+��  . $��5q�$��1��$��3�� �� *Filter.Actors+���� *Movies.Actors+��  . $��5q�$��3��$��0�� �� *Filter.Country+���� *Movies.Countries+�� $��5q�$��3��$��1������ *Filter.Year+(  "System.Windows.Controls.DatePicker  $��5q�$��3��$��2��   SelectedDate �� *Filter.Added+�� $��5q�$��3��$��3�� �� *Filter.Starred+��  *	Starred+� $��5q�$��0��$��4��$��4��.+��� $ 
Horizontal=�.+��� -   $ 	btnFilter��$��0,0,5,0q���  *
DoFilter+�� -    ��Name$ cbxFilterPosition��.@���  �� 
IsSelected . ��  *Position.Start+�� ��  *Position.End+�� ��  *Position.In+�� ��  *Position.Match+�� $ 
Horizontal=�$��Right=�.+��� -   $ btnClearFilter��$��5,0,0,0q���  *ClearFilter+�� #��[ $	��5,0q�$��2��._�� 	 �� $
��Auto ��� .+�� -    �Name$ grdMovieControls��$��23��$��0,5,0,0q�$��0��.+��� $ 
Horizontal=�$
��Left=�.+��� -   $ btnCreateMovie��$��5,0,0,0q�
 � Tag �� *Movies+.���� $ 
Horizontal=�.+��� $4��./MediaLibrary.GUI;component/Images/FilmAdd.png�� �� BitmapScalingMode$ NearestNeighbor=��� $	��4,0q���  *CreateMovie+�� $ 
Horizontal=�$��Right=�.+��� -   $ btnEditMovie��$��0,0,4,0q���  *	EditMovie+ �� *Movies+�� -   $ btnDeleteMovie��$��0,0,4,0q���  *DeleteMovie+ �� *Movies+�� $ 
Horizontal=�$��Center=�.+��� -   $ btnCreateCategory��$��5,0,0,0q�$y�	Collapsed=���  *CategoryList+ �� *Movies+�� -    ��Name$ 	lvwMovies��$	��0,5q�$��0q�$��1������  �� Source# [   � ContextMenu �� .@�m� -    m�Name$ mniCopyMovie����  *Copy+m� -   $ mniPasteMovie����  *Paste+���� .R�� $	��450��$��5q�	 �� $
��Auto ��� $
��Auto �.+��� $ 
Horizontal=�$��0��.+��� $��20(����� *Name+�� $��5,0,0,0q�$��16��$1��+/MediaLibrary.GUI;component/Images/Star.png��y��� *	Starred+  boolToVisibility �� 	Converter# [ �� $ 
Horizontal=�$��Right=�$��0��.+��� $��20(����� *	Storage+  �� TargetNullValue    *Empty+�� $ 
Horizontal=�$��1��.+��� ���� *OriginalName+�� $�� ��y��� *OriginalName+  nullToVisibility# [ �� $��(��y��� *Actors+# [ �� ���� *Actors+  stringToArray# [ y��� *Actors+# [ �� $��)��y��� *Actors+# [ �� $ 
Horizontal=�$��Right=�$��1��.+��� $��(��y��� *
Language+# [ �� ���� *
Language+y��� *
Language+# [ �� $	��), ��y��� *
Language+# [ �� ���� *
Category+    *Empty+�� $��, ��y��� *Year+# [ �� ���� *Year+    *Empty+y��� *Year+# [ �� $��, ���� ���� *	Country+    *Empty+�� #��[ $��5q�$��3��._�� .+��� $ 
Horizontal=�$��5q�.+��� ��  *SortBy+�� -   $ btnSortName�� 	 link#��[	 $��5,0,0,0q���  *Name+�� -   $ btnSortCategory��#��[	 $��5,0,0,0q���  *
Category+�� -   $ btnSortStorage��#��[	 $��5,0,0,0q���  *	Storage+�� -   $ btnSortYear��#��[	 $��5,0,0,0q���  *Year+�� -   $ btnSortCountry��#��[	 $��5,0,0,0q���  *	Country+�� -   $ btnSortAdded��#��[	 $��5,0,0,0q���  *Added+�� $ 
Horizontal=�$��Right=�.+��� $��0,5,5,0q����� ! �� ElementName$! 	lvwMovies��" �� Path$" Items.Count�# �� StringFormat#   *
ItemsCount+�� -   $ ��Name$$ 	brdSelect��#��[ $	��5,0q�$��4��$y�	Collapsed=�._��� $ 
Horizontal=�$��5q�.+��� -   $ 	btnSelect��$	��4,2q���  *SelectMovie+� ���	#20ccccccy��� *RequiresPassword+# [ .+��� .+��� -   $ btnCloseLibrary2��$��16��$��18��#��[ % �� Left$% 53��
& �� Top$& 2��   *CloseLibrary+.���� $6��0/MediaLibrary.GUI;component/Images/FileClose.png���� $	��200��$��Center=�.+��� $
��Bold"�$��Center=���  *PasswordRequired+' �� Target' �� $! tbxPasswordRequired��G� -    ( G�Name$( tbxPasswordRequired��$��22��$��5q�
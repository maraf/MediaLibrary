   M S B A M L   `   `   `  ���� P  LDesktopCore.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH8clr-namespace:DesktopCore;assembly=DesktopCore.ResourcesDesktopCore   MediaLibrary.GUI,   &MediaLibrary.GUI.DatabaseConfiguration   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Fc8clr-namespace:DesktopCore;assembly=DesktopCore.Resources     $	��700��  � Icon$9  3/MediaLibrary.GUI;component/Images/MediaLibrary.png�� � ShowInTaskbar .   � SizeToContent$ Height=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Title    DesktopCore.Resource  *DatabaseConfiguration+.���� .+�9� -    9�Name$ 	expCommon�� 9� 
IsExpanded . ��  *
ConfCommon+.���  � RowDefinitions �� �� �� �� .+��� $��0��.+���  	  label#��[  	 �� Target	 �� 
 �� ElementName$
 tbxName����  *LibraryName+�� -    ��Name$ tbxName������ *Name+�� $	��0,5q�$��1��.+��� #��[  	 �� $
 	btnBrowse����  *LibraryLocation+]� .+��� -    ��Name$ 	btnBrowse��$	��...��$��5,0,0,0q�$	��5,0q���.  $��Right=��� ���� *
Location+� $	��0,5q�$��2�� � ColumnDefinitions �� �� .+��� $��0,0,5,0q�$��0��.+��� #��[  	 �� $
 tbxOnlineName����  *ConfOnlineName+�� -   $ tbxOnlineName������ *
OnlineName+�� $��0,0,5,0q�$��1��.+��� #��[  	 �� $
 tbxOnlineFormat����  *ConfOnlineFormat+�� -   $ tbxOnlineFormat������ *OnlineFormat+9� -   $ expPassword��$��5q���  *ConfPassword+.���  �� $��1* ��� $��2* � �� �� .+��� $��5q�$��0��$��0����  *EncryptByPassword+ d� 	IsChecked �� *	Encrypted+�� $��5q�$��0��$��1��$��2����  *SavePassword+ �� *SavePassword+{��� *	Encrypted+�� -   $ tbxPassword��$	��0,5q�$��1��$��0������ *
Password+ �� UpdateSourceTrigger$ PropertyChanged=�{��� *	Encrypted+9� -   $ 	expOnline��$��5q���  *
ConfOnline+.���  �� �� �� �� �� �� .+�� $	��0,5q�$��0�� �� �� .+��� $��0��.+��� #��[  	 �� $!
 tbxPublicDownloadUrlPattern����  *PublicDownloadUrlPattern+�� -	   $! tbxPublicDownloadUrlPattern������ *PublicDownloadUrlPattern+�� $	��4,0q�$��1��.+��� #��[  	 �� $
 tbxPublicUploadUrlPattern����  *PublicUploadUrlPattern+�� -
   $ tbxPublicUploadUrlPattern������ *PublicUploadUrlPattern+� $	��0,5q�$��1�� �� $��3* ��� $��2* ��� $��2* �.+��� $��0��.+��� #��[  	 �� $
 tbxPublicIdentifier����  *PublicIdentifier+�� -   $ tbxPublicIdentifier������ *PublicIdentifier+�� $	��4,0q�$��1��.+��� #��[  	 �� $
 tbxPublicUsername����  *PublicUsername+�� -   $ tbxUsername������ *PublicUsername+�� $��2��.+��� #��[  	 �� $
 tbxPublicPassword����  *PublicPassword+�� -   $ tbxPublicPassword������ *PublicPassword+�� -    ��Name$ cbxPublishOnSave��$	��0,4q�$��2����  *PublishOnSave+ �� *PublishOnSave+�� -   $ cbxDownloadOnLoad��$	��0,4q�$��3����  *DownloadOnLoad+ �� *DownloadOnLoad+�� -   $ cbxSavePublicPassword��$	��0,4q�$��4����  *SavePublicPassword+ �� *SavePublicPassword+]� $��5��.+��� -   $ btnPublishNow��$
��Left=�$	��0,2q�$
��Left=���  *
PublishNow+�� -   $ btnDownloadNow��$
��Left=�$��5,0,10,0q�$	��0,2q�$
��Left=���  *DownloadNow+�� -    ��Name$ tblOnlineMessage��$��Center=�
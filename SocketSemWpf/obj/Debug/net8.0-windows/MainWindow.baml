   M S B A M L   `   `   `  ����  SocketSemWpf    SocketSemWpf.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Game Server��5   	   $	��450��6   $	��800��6*   .��5      � ���#f0f4f8 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   $��Center=�6:   $	��Top=�6W   .+�5      �� $	��IP:��6   $��Center=�6#   $��0,0,5,0q�6>   $��14(�6O   $
��Bold"�6]   �� 5      -   5         ��Name$ 	IpTextBox��5      $	��150��6)   $��30��65   $��	127.0.0.1��6A   $��Center=�6R   $��5q�6m   �� 5      $��Port:��6   $��Center=�6%   $��10,0,5,0q�6@   $��14(�6R   $
��Bold"�6`   �� 5      -   5        $ PortTextBox��5      $��80��6+   $��30��66   $��12345��6B   $��Center=�6O   $��5q�6j   �� 5      -   5        $%��Запустить сервер��5      $
��10,0q�6J   $	��150��6X   $��30��6d   ���#007ACC
�������$��14(�6�   $��SemiBold"�6�   $��0q�6�   �� 5      -   5        $!��Открыть клиент��5      $	��150��6M   $��30��6Y   ���#28a745
�������$��14(�6�   $��SemiBold"�6�   $��0q�6�   5      �� 5   
   $��1��6   $��10q�6    �� CornerRadius$ 10��6*   
����������#ccc$��1q�6b    �� Padding$ 10q�6v   ._�5      �� $
��Auto=�6   .��5      �� -   5         ��Name$ 
LogListBox��5      5      5      5      5      
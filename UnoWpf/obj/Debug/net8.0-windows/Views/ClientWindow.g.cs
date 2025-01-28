﻿#pragma checksum "..\..\..\..\Views\ClientWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E1A42F071DBABE7736B72926DE911D93D64C9A78"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfAnimatedGif;


namespace UnoWpf.Views {
    
    
    /// <summary>
    /// ClientWindow
    /// </summary>
    public partial class ClientWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\..\Views\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IpTextBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Views\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PortTextBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Views\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox MessageListBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Views\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ActionPanel;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\..\Views\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NewGameButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UnoWpf;component/views/clientwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ClientWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.IpTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.PortTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            
            #line 24 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Connect_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MessageListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.ActionPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            
            #line 61 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 62 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 63 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 66 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 67 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 68 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 87 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 88 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 89 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 92 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 93 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 94 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 113 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 114 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 115 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 118 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 119 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 120 "..\..\..\..\Views\ClientWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActionButton_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            this.NewGameButton = ((System.Windows.Controls.Button)(target));
            
            #line 126 "..\..\..\..\Views\ClientWindow.xaml"
            this.NewGameButton.Click += new System.Windows.RoutedEventHandler(this.NewGameButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


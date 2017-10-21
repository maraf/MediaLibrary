﻿#pragma checksum "..\..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B6049E5AC94DFCA791BD8143A2385724"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.530
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DesktopCore;
using MediaLibrary.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace MediaLibrary.GUI {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateLibrary;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOpenLibrary;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCopyLibraries;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExportLibraries;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCompareLibraries;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveLibraries;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConfiguration;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl lvwDatabases;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MediaLibrary.GUI;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\MainWindow.xaml"
            ((MediaLibrary.GUI.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnCreateLibrary = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\MainWindow.xaml"
            this.btnCreateLibrary.Click += new System.Windows.RoutedEventHandler(this.btnCreateLibrary_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnOpenLibrary = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\MainWindow.xaml"
            this.btnOpenLibrary.Click += new System.Windows.RoutedEventHandler(this.btnOpenLibrary_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnCopyLibraries = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\MainWindow.xaml"
            this.btnCopyLibraries.Click += new System.Windows.RoutedEventHandler(this.btnCopyLibraries_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnExportLibraries = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\MainWindow.xaml"
            this.btnExportLibraries.Click += new System.Windows.RoutedEventHandler(this.btnExportLibraries_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnCompareLibraries = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\MainWindow.xaml"
            this.btnCompareLibraries.Click += new System.Windows.RoutedEventHandler(this.btnCompareLibraries_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnSaveLibraries = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\MainWindow.xaml"
            this.btnSaveLibraries.Click += new System.Windows.RoutedEventHandler(this.btnSaveLibraries_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnConfiguration = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\MainWindow.xaml"
            this.btnConfiguration.Click += new System.Windows.RoutedEventHandler(this.btnConfiguration_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lvwDatabases = ((System.Windows.Controls.ItemsControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

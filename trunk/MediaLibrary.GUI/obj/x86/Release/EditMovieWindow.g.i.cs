﻿#pragma checksum "..\..\..\EditMovieWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "46306430DC0F8E666A446033C08ECC75"
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
    /// EditMovieWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class EditMovieWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\EditMovieWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton btnToggleStarred;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\EditMovieWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBrowseWeb;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\EditMovieWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxName;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\EditMovieWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxCountry;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\EditMovieWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxCategory;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\EditMovieWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvwRelated;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\EditMovieWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRelatedAdd;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\EditMovieWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRelatedRemove;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\EditMovieWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxDescription;
        
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
            System.Uri resourceLocater = new System.Uri("/MediaLibrary.GUI;component/editmoviewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\EditMovieWindow.xaml"
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
            
            #line 6 "..\..\..\EditMovieWindow.xaml"
            ((MediaLibrary.GUI.EditMovieWindow)(target)).PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Window_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 6 "..\..\..\EditMovieWindow.xaml"
            ((MediaLibrary.GUI.EditMovieWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnToggleStarred = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 3:
            this.btnBrowseWeb = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\EditMovieWindow.xaml"
            this.btnBrowseWeb.Click += new System.Windows.RoutedEventHandler(this.btnBrowseWeb_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbxName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.cbxCountry = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.cbxCategory = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.lvwRelated = ((System.Windows.Controls.ListView)(target));
            
            #line 94 "..\..\..\EditMovieWindow.xaml"
            this.lvwRelated.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lvwRelated_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnRelatedAdd = ((System.Windows.Controls.Button)(target));
            
            #line 107 "..\..\..\EditMovieWindow.xaml"
            this.btnRelatedAdd.Click += new System.Windows.RoutedEventHandler(this.btnRelatedAdd_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnRelatedRemove = ((System.Windows.Controls.Button)(target));
            
            #line 110 "..\..\..\EditMovieWindow.xaml"
            this.btnRelatedRemove.Click += new System.Windows.RoutedEventHandler(this.btnRelatedRemove_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.tbxDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


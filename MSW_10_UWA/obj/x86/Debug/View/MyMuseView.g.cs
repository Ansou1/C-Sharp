﻿#pragma checksum "C:\Users\simon\Documents\GitHub\C-Sharp\MSW_10_UWA\View\MyMuseView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6CEAE45C6CD47C49C7B02F05B24BEE68"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MSW_10_UWA.View
{
    partial class MyMuseView : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.SearchAppBarButtonMuse = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 2:
                {
                    this.LogoutAppBarButtonMuse = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 3:
                {
                    this.LayoutRoot = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4:
                {
                    this.listViewMuse = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.CheckBox element5 = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    #line 39 "..\..\..\View\MyMuseView.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)element5).Click += this.CheckBox_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}


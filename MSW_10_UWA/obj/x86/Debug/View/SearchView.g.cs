﻿#pragma checksum "C:\Users\simon\Documents\Visual Studio 2015\Projects\MSW_10_UWA\MSW_10_UWA\View\SearchView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F73E5497C8615727CB24E8ED97CC11C5"
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
    partial class SearchView : 
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
                    this.PivotSearchPage = (global::Windows.UI.Xaml.Controls.Pivot)(target);
                }
                break;
            case 2:
                {
                    this.SearchPageScorePivotItem = (global::Windows.UI.Xaml.Controls.PivotItem)(target);
                    #line 29 "..\..\..\View\SearchView.xaml"
                    ((global::Windows.UI.Xaml.Controls.PivotItem)this.SearchPageScorePivotItem).GotFocus += this.SearchPageScorePivotItem_GotFocus;
                    #line 29 "..\..\..\View\SearchView.xaml"
                    ((global::Windows.UI.Xaml.Controls.PivotItem)this.SearchPageScorePivotItem).LostFocus += this.SearchPageScorePivotItem_LostFocus;
                    #line default
                }
                break;
            case 3:
                {
                    this.SearchPageUserPivotItem = (global::Windows.UI.Xaml.Controls.PivotItem)(target);
                    #line 54 "..\..\..\View\SearchView.xaml"
                    ((global::Windows.UI.Xaml.Controls.PivotItem)this.SearchPageUserPivotItem).GotFocus += this.SearchPageScorePivotItem_GotFocus;
                    #line 54 "..\..\..\View\SearchView.xaml"
                    ((global::Windows.UI.Xaml.Controls.PivotItem)this.SearchPageUserPivotItem).LostFocus += this.SearchPageScorePivotItem_LostFocus;
                    #line default
                }
                break;
            case 4:
                {
                    this.listViewSearchUser = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 5:
                {
                    this.listViewSearchScore = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 6:
                {
                    this.SearchTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7:
                {
                    this.LogoutAppBarButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
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

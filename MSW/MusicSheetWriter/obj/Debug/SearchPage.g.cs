﻿

#pragma checksum "C:\Users\simon\Desktop\WindowsPhone\trunk\MusicSheetWriter\SearchPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D1BD142261B6FBE608CF889B8551BCA6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicSheetWriter
{
    partial class SearchPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 31 "..\..\SearchPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.CheckBox_Click_1;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 20 "..\..\SearchPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.CheckBox_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 48 "..\..\SearchPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).GotFocus += this.PivotItem_GotFocus;
                 #line default
                 #line hidden
                #line 48 "..\..\SearchPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).LostFocus += this.PivotItem_LostFocus;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 51 "..\..\SearchPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).GotFocus += this.PivotItem_GotFocus;
                 #line default
                 #line hidden
                #line 51 "..\..\SearchPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).LostFocus += this.PivotItem_LostFocus;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 52 "..\..\SearchPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.listViewSearchUser_ItemClick;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 49 "..\..\SearchPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.listViewSearchScore_ItemClick;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 39 "..\..\SearchPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.SymbolIcon_Tapped;
                 #line default
                 #line hidden
                break;
            case 8:
                #line 59 "..\..\SearchPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.LogoutAppBarButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}



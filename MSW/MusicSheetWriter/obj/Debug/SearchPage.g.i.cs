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
    partial class SearchPage : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Pivot PivotSearchPage; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.PivotItem SearchPageScorePivotItem; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.PivotItem SearchPageUserPivotItem; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.ListView listViewSearchUser; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.ListView listViewSearchScore; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox SearchTextBox; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton LogoutAppBarButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///SearchPage.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            PivotSearchPage = (global::Windows.UI.Xaml.Controls.Pivot)this.FindName("PivotSearchPage");
            SearchPageScorePivotItem = (global::Windows.UI.Xaml.Controls.PivotItem)this.FindName("SearchPageScorePivotItem");
            SearchPageUserPivotItem = (global::Windows.UI.Xaml.Controls.PivotItem)this.FindName("SearchPageUserPivotItem");
            listViewSearchUser = (global::Windows.UI.Xaml.Controls.ListView)this.FindName("listViewSearchUser");
            listViewSearchScore = (global::Windows.UI.Xaml.Controls.ListView)this.FindName("listViewSearchScore");
            SearchTextBox = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("SearchTextBox");
            LogoutAppBarButton = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("LogoutAppBarButton");
        }
    }
}



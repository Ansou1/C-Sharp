using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MSW_10_UWA.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchView : Page
    {
        public SearchView()
        {
            this.InitializeComponent();
        }

        private void SearchPageScorePivotItem_GotFocus(object sender, RoutedEventArgs e)
        {
            var s = sender as PivotItem;
            s.Foreground = Application.Current.Resources["mswTextIconsColor"] as Brush;
        }

        private void SearchPageScorePivotItem_LostFocus(object sender, RoutedEventArgs e)
        {
            var s = sender as PivotItem;
            s.Foreground = Application.Current.Resources["mswPrimaryLightColor"] as Brush;
        }
    }
}

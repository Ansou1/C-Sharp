using MSW_10_UWA.Common;
using Music_Sheet_Writer.Models;
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
    public sealed partial class MyMuseView : Page
    {
        public MyMuseView()
        {
            this.InitializeComponent();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as CheckBox;
            System.Diagnostics.Debug.WriteLine("Checkbox => " + s.IsChecked);
            var user = s.DataContext as User;
            System.Diagnostics.Debug.WriteLine("Affichage donnée user muse => " + user.Id + " " + user.UserName);

            AddDeleteFavourites elem = new AddDeleteFavourites();
            if (s.IsChecked == true)
                elem.addNouvelAbonnement(user.Id);
            else
                elem.deleteAbonnement(user.Id);
        }
    }
}

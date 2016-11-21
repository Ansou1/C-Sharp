using System;
using System.Collections.Generic;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MusicSheetWriter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login_Page : Page
    {
        private Boolean seSouvenirDeMoi = false;

        public Login_Page()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            IReadOnlyList<PasswordCredential> data = RetriveCredential();
            if (data != null)
            {
                foreach (var item in data)
                {
                    item.RetrievePassword();
                    UserName.Text = item.UserName;
                    PassWord.Password = item.Password;
                }
            }
        }

        private void doConnection(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            string response = connect.Response;
            System.Diagnostics.Debug.WriteLine(response);
            if (connect.getValue() == true)
            {
                if (seSouvenirDeMoi == true)
                    saveCredential();
             
                //ici j'envoie la response à la hubpage
                Frame.Navigate(typeof(PivotPage), response);
            }
        }

        void saveCredential()
        {
            PasswordVault vault = new PasswordVault();
            PasswordCredential cred = new PasswordCredential("MSW", UserName.Text, PassWord.Password);
            vault.Add(cred);
        }

        IReadOnlyList<PasswordCredential> RetriveCredential()
        {
            PasswordVault vault = new PasswordVault();
            try
            {
                return vault.FindAllByResource("MSW");
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doConnection);
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/login", "{ \"login\":\"" + UserName.Text + "\",\n\"password\":\"" + PassWord.Password + "\" }");
        }

        public void ForgottenPass_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Forgotten_Password));
        }

        public void SignUp_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Sign_Up));
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as CheckBox;
            if (s.IsChecked == true)
                seSouvenirDeMoi = true;
        }
    }
}

using MusicSheetWriter.Common;
using System;
using System.Text.RegularExpressions;
using Windows.Data.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MusicSheetWriter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Sign_Up : Page
    {
        private readonly NavigationHelper navigationHelper;

        public Sign_Up()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>.
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache. Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/>.</param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
        }

        private void doConnection(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine(connect.getValue());
            if (connect.getValue() == true)
            {
                Frame.Navigate(typeof(Login_Page));
            }
            System.Diagnostics.Debug.WriteLine(connect.getValue());
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            //UserName Validation  
            if (TxtUserName.Text == "")
            {
                MessageDialog test = new MessageDialog("Identifiant invalide");
                await test.ShowAsync();
            }

            //Password length Validation  
            else if (TxtPwd.Password.Length < 6)
            {
                MessageDialog test = new MessageDialog("La taille du mot de passe doit au moins avoir 6 charactères");
                await test.ShowAsync();
            }

            //EmailID validation  
            else if (!Regex.IsMatch(TxtEmail.Text.Trim(), @"^([a-zA-Z_])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$"))
            {
                MessageDialog test = new MessageDialog("Email non valide");
                await test.ShowAsync();
            }

            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doConnection);
            JsonObject json = new JsonObject();
            json.SetNamedValue("username", JsonValue.CreateStringValue(TxtUserName.Text));
            json.SetNamedValue("email", JsonValue.CreateStringValue(TxtEmail.Text));
            json.SetNamedValue("password", JsonValue.CreateStringValue(TxtPwd.Password));
            //"{ \"username\":\"" + TxtUserName.Text + "\",\n\"email\":\"" + TxtEmail.Text + "\",\n\"password\":\"" + TxtPwd.Password + "\" }"
            System.Diagnostics.Debug.WriteLine(json.Stringify());
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/users", json.Stringify());
        }
    }
}
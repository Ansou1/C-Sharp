using MusicSheetWriter.Common;
using System;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace MusicSheetWriter
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AccountPage : Page
    {
        private readonly NavigationHelper navigationHelper;

        private string loginResponse;
        private User user;
        private string userId;

        public AccountPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            this.loginResponse = e.Parameter as string;

            System.Diagnostics.Debug.WriteLine("Je suis ici !!!!!!!!!!!!!");
            JsonObject json = new JsonObject();
            if (JsonObject.TryParse(this.loginResponse, out json) == true)
            {
                IJsonValue jsonValue = null;
                System.Diagnostics.Debug.WriteLine("Je suis là !!!!!!!!!!!!!");
                if (json.TryGetValue("id", out jsonValue) == true)
                {
                    System.Diagnostics.Debug.WriteLine("Je suis passer par là regarde...");
                    this.userId = jsonValue.GetString();
                    this.requestGetUserInformation();
                }
                else
                {
                    //throw new MSWException("Error with the json returned from the login request.");
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
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

        private void requestGetUserInformation()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.populateView);
            connect.HttpGetRequest("https://musicsheetwriter.tk/api/users/" + this.userId);
        }

        private void populateView(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine(connect.Response);

            this.user = this.getUserFromJson(connect.Response);

            this.UsernameTextBlock.Text = this.user.PersonalData.Username;
            this.FirstnameTextBlock.Text = this.user.PersonalData.FirstName;
            this.LastnameTextBlock.Text = this.user.PersonalData.Lastname;
            this.UserImage.ImageSource = new BitmapImage(new Uri(this.user.PersonalData.PictureURL, UriKind.Absolute));
            this.MessageTextBlock.Text = this.user.PersonalData.Message;
            System.Diagnostics.Debug.WriteLine(this.user);
        }

        private User getUserFromJson(string jsonString)
        {
            JsonObject jsonObject = new JsonObject();
            User user = new User();
            if (JsonObject.TryParse(jsonString, out jsonObject))
            {
                var personal_data = jsonObject.GetNamedObject("personal_data");
                IJsonValue value = null;
                try
                {
                    user.PersonalData.Id = (personal_data.TryGetValue("id", out value)) ? (int.Parse(value.GetString())) : (0);
                    user.PersonalData.Username = (personal_data.TryGetValue("username", out value)) ? (value.GetString()) : ("");
                    user.PersonalData.FirstName = (personal_data.TryGetValue("firstname", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.PersonalData.Lastname = (personal_data.TryGetValue("lastname", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.PersonalData.Message = (personal_data.TryGetValue("message", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.PersonalData.Email = (personal_data.TryGetValue("email", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.PersonalData.PictureURL = (personal_data.TryGetValue("photo", out value)) ? (value.GetString()) : ("");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    throw;
                }
            }
            return user;
        }

        private void LogoutAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doLogout);
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/logout", "");
        }

        private void doLogout(object sender, EventArgs e)
        {
            Frame.Navigate(typeof(Login_Page));
        }

        private void SearchAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage), userId);
        }

        private void SettingsAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage), this.user);
        }
    }
}

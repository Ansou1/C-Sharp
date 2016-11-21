using MusicSheetWriter.Common;
using System;
using System.Collections.ObjectModel;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MusicSheetWriter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FanPage : Page
    {
        private readonly NavigationHelper navigationHelper;

        private string loginResponse;
        private string userId;

        private ObservableCollection<User> UserList = new ObservableCollection<User>();

        public FanPage()
        {
            this.InitializeComponent();

            UserList.Clear();

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
            this.navigationHelper.OnNavigatedTo(e);
            this.loginResponse = e.Parameter as string;

            JsonObject json = new JsonObject();
            if (JsonObject.TryParse(this.loginResponse, out json) == true)
            {
                IJsonValue jsonValue = null;

                if (json.TryGetValue("id", out jsonValue) == true)
                {
                    this.userId = jsonValue.GetString();
                }
                else
                {
                    //throw new MSWException("Error with the json returned from the login request.");
                }
            }
            getListAbonne();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
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

        private void getListAbo(object sender, EventArgs e)
        {
            UserList.Clear();
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Liste des abonne à user #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);

            JsonArray jsonArray;
            if (JsonArray.TryParse(connect.Response, out jsonArray))
            {
                foreach (var item in jsonArray)
                {
                    UserList.Add(getUserFromJson(item.Stringify()));
                }
                listViewFan.ItemsSource = UserList;
                foreach (var item in UserList)
                {
                    System.Diagnostics.Debug.WriteLine(item);
                }
            }
        }

        private void getListAbonne()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.getListAbo);
            System.Diagnostics.Debug.WriteLine("https://musicsheetwriter.tk/api/users/" + this.userId + "/subscribers");
            connect.HttpGetRequest("https://musicsheetwriter.tk/api/users/" + this.userId + "/subscribers");
        }

        private void doLogout(object sender, EventArgs e)
        {
            Frame.Navigate(typeof(Login_Page));
        }

        private void LogoutAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doLogout);
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/logout", "");
        }

        private void SearchAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage), this.loginResponse);
        }

        private User getUserFromJson(string jsonString)
        {
            JsonObject jsonObject = new JsonObject();
            User user = new User();
            if (JsonObject.TryParse(jsonString, out jsonObject))
            {
                IJsonValue value = null;
                try
                {
                    user.PersonalData.Id = (jsonObject.TryGetValue("id", out value)) ? (int.Parse(value.GetString())) : (0);
                    user.PersonalData.Username = (jsonObject.TryGetValue("username", out value)) ? (value.GetString()) : ("");
                    user.PersonalData.FirstName = (jsonObject.TryGetValue("firstname", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.PersonalData.Lastname = (jsonObject.TryGetValue("lastname", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.PersonalData.Message = (jsonObject.TryGetValue("message", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.PersonalData.Email = (jsonObject.TryGetValue("email", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.PersonalData.PictureURL = (jsonObject.TryGetValue("photo", out value)) ? (value.GetString()) : ("");
                    user.isASubscription = (jsonObject.TryGetValue("is_subscription", out value)) ? (value.GetBoolean()) : (false);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    throw;
                }
            }
            return user;
        }

        private void listViewFan_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as User;
            System.Diagnostics.Debug.WriteLine("Affichage donnée user clicker => " + clickedItem.PersonalData.Id +  " " + clickedItem.PersonalData.Username);
            Frame.Navigate(typeof(AccountViewerView), clickedItem);
        }

        private void doDeleteAbonnement(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  delete abonnement  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
        }

        private void deleteAbonnement(String deleteId)
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doDeleteAbonnement);
            connect.HttpDeleteRequest("https://musicsheetwriter.tk/api/users/" + userId + "/subscriptions/" + deleteId);
        }

        private void doAddNouvelAbonnement(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Add abonnement  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
        }

        private void addNouvelAbonnement(String addId)
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doAddNouvelAbonnement);
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/users/" + userId + "/subscriptions", "{ \"id\": " + addId + " }");
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as CheckBox;
            System.Diagnostics.Debug.WriteLine(s.IsChecked);

            var user = s.DataContext as User;
            System.Diagnostics.Debug.WriteLine("Affichage donnée user fan => " + user.PersonalData.Id + " " + user.PersonalData.Username);
            if (s.IsChecked == true)
                addNouvelAbonnement(user.PersonalData.Id.ToString());
            else
                deleteAbonnement(user.PersonalData.Id.ToString());
        }
    }
}

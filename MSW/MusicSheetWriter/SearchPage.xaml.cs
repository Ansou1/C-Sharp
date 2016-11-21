using MusicSheetWriter.Common;
using System;
using System.Collections.ObjectModel;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MusicSheetWriter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        private readonly NavigationHelper navigationHelper;

        private string userId;

        private ObservableCollection<User> UserList = new ObservableCollection<User>();
        private ObservableCollection<Score> ScoreList = new ObservableCollection<Score>();

        private string loginResponse;

        public SearchPage()
        {
            this.InitializeComponent();

            UserList.Clear();
            ScoreList.Clear();

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

        private void dogetListUser(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Get all Users  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);

            JsonArray jsonArray;
            if (JsonArray.TryParse(connect.Response, out jsonArray))
            {
                foreach (var item in jsonArray)
                {
                    UserList.Add(getUserFromJson(item.Stringify()));
                }
                listViewSearchUser.ItemsSource = UserList;
                foreach (var item in UserList)
                {
                    System.Diagnostics.Debug.WriteLine(item);
                }
            }
        }

        private void getListUser()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.dogetListUser);
            connect.HttpGetRequest("https://musicsheetwriter.tk/api/users?uname=" + SearchTextBox.Text);
        }

        private void dogetListScore(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Get all Scores  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);

            JsonArray jsonArray;
            if (JsonArray.TryParse(connect.Response, out jsonArray))
            {
                foreach (var item in jsonArray)
                {
                    ScoreList.Add(getScoreFromJson(item.Stringify()));
                }
                listViewSearchScore.ItemsSource = ScoreList;
                foreach (var item in ScoreList)
                {
                    System.Diagnostics.Debug.WriteLine(item);
                }
            }
        }

        private void getListScore()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.dogetListScore);
            connect.HttpGetRequest("https://musicsheetwriter.tk/api/scores?title=" + SearchTextBox.Text);
        }

        private Score getScoreFromJson(string jsonString)
        {
            JsonObject jsonObject = new JsonObject();
            Score score = new Score();
            if (JsonObject.TryParse(jsonString, out jsonObject))
            {
                var by = jsonObject.GetNamedObject("by");
                IJsonValue value = null;
                try
                {
                    score.Id = (jsonObject.TryGetValue("id", out value)) ? (int.Parse(value.GetNumber().ToString())) : (0);
                    score.Title = (jsonObject.TryGetValue("title", out value)) ? (value.GetString()) : ("");
                    score.LocationProject = (jsonObject.TryGetValue("location_project", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    score.LocationPreview = (jsonObject.TryGetValue("location_preview", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    score.Is_Favourite = (jsonObject.TryGetValue("is_favourite", out value)) ? (value.GetBoolean()) : (false);

                    score.Compositor.PersonalData.Id = (by.TryGetValue("id", out value)) ? (int.Parse(value.GetString())) : (0);
                    score.Compositor.PersonalData.Username = (by.TryGetValue("username", out value)) ? (value.GetString()) : ("");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    throw;
                }
            }
            return score;
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
                    user.PersonalData.PictureURL = (jsonObject.TryGetValue("photo", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("http//163.5.84.253/bundles/msw/images/default-avatar.png")) : ("http//163.5.84.253/bundles/msw/images/default-avatar.png");
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

        private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Search button");
            UserList.Clear();
            ScoreList.Clear();
            getListUser();
            getListScore();
        }

        private void PivotItem_GotFocus(object sender, RoutedEventArgs e)
        {
            var s = sender as PivotItem;
            s.Foreground = Application.Current.Resources["mswTextIconsColor"] as Brush;
        }

        private void PivotItem_LostFocus(object sender, RoutedEventArgs e)
        {
            var s = sender as PivotItem;
            s.Foreground = Application.Current.Resources["mswPrimaryLightColor"] as Brush;
        }

        private void listViewSearchUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as User;
            System.Diagnostics.Debug.WriteLine("Affichage donnée user clicker => " + clickedItem.PersonalData.Id + " " + clickedItem.PersonalData.Username);
            Frame.Navigate(typeof(AccountViewerView), clickedItem);
        }

        private void listViewSearchScore_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as Score;
            System.Diagnostics.Debug.WriteLine("Affichage donnée score clicker => " + clickedItem.Id + " " + clickedItem.Title);
            Frame.Navigate(typeof(PreviewImageScore), clickedItem);
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

        private void doDeleteFavourites(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  delete favourite  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
        }

        private void deleteFavourites(String delId)
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doDeleteFavourites);
            connect.HttpDeleteRequest("https://musicsheetwriter.tk/api/users/" + userId + "/scores/favourites/" + delId);
        }

        private void doAddNouveauFavourites(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Add favourtie  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
        }

        private void addNouveauFavourites(String addId)
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doAddNouveauFavourites);
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/users/" + userId + "/scores/favourites", "{ \"id\": " + addId + " }");
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as CheckBox;
            System.Diagnostics.Debug.WriteLine(s.IsChecked);

            var user = s.DataContext as User;
            System.Diagnostics.Debug.WriteLine("Affichage donnée user search => " + user.PersonalData.Id + " " + user.PersonalData.Username);
            if (s.IsChecked == true)
                addNouvelAbonnement(user.PersonalData.Id.ToString());
            else
                deleteAbonnement(user.PersonalData.Id.ToString());
        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            var s = sender as CheckBox;
            System.Diagnostics.Debug.WriteLine(s.IsChecked);

            var score = s.DataContext as Score;
            System.Diagnostics.Debug.WriteLine("Affichage donnée score search => " + score.Id + " " + score.Title);
            if (s.IsChecked == true)
                addNouveauFavourites(score.Id.ToString());
            else
                deleteFavourites(score.Id.ToString());
        }
    }
}

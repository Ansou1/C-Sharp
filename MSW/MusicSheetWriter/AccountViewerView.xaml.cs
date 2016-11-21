using MusicSheetWriter.Common;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Data.Json;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MusicSheetWriter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountViewerView : Page
    {
        private readonly NavigationHelper navigationHelper;
        private User my_user;
        private ObservableCollection<Score> ScoreList = new ObservableCollection<Score>();
        private ObservableCollection<User> UserList = new ObservableCollection<User>();

        public AccountViewerView()
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

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            my_user = new User();
            my_user = e.Parameter as User;
            //System.Diagnostics.Debug.WriteLine(my_user);

            this.UsernameTextBlock.Text = my_user.PersonalData.Username;
            this.FirstnameTextBlock.Text = my_user.PersonalData.FirstName;
            this.LastnameTextBlock.Text = my_user.PersonalData.Lastname;
            this.MessageTextBlock.Text = my_user.PersonalData.Message;
            this.UserImage.ImageSource = new BitmapImage(new Uri(this.my_user.PersonalData.PictureURL, UriKind.Absolute));
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

        /*
        ** My Score
        */

        private void doGetListScoreOfUser(object sender, EventArgs e)
        {
            ScoreList.Clear();
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Get all scores of user #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
            JsonArray jsonArray;
            if (JsonArray.TryParse(connect.Response, out jsonArray))
            {
                foreach (var item in jsonArray)
                {
                    ScoreList.Add(getScoreFromJson(item.Stringify()));
                }
                //listViewScore.ItemsSource = ScoreList;
                foreach (var item in ScoreList)
                {
                    System.Diagnostics.Debug.WriteLine(item);
                }
            }
        }

        private void getListScoreOfUser()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doGetListScoreOfUser);
            connect.HttpGetRequest("https://musicsheetwriter.tk/api/users/" + my_user.PersonalData.Id + "/scores/own");
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

        private void button_Click(object sender, RoutedEventArgs e) // My scores
        {
            getListScoreOfUser();
            Frame.Navigate(typeof(DisplayScore), ScoreList);
        }


        /* My favourites */
        private void doGetListFavouriteScore(object sender, EventArgs e)
        {
            ScoreList.Clear();
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Get favourite scores of user #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
            JsonArray jsonArray;
            if (JsonArray.TryParse(connect.Response, out jsonArray))
            {
                foreach (var item in jsonArray)
                {
                    ScoreList.Add(getScoreFromJson(item.Stringify()));
                }
                //listViewFavorite.ItemsSource = ScoreList;
                foreach (var item in ScoreList)
                {
                    System.Diagnostics.Debug.WriteLine(item);
                }
            }
        }

        private void getListFavouriteScore()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doGetListFavouriteScore);
            connect.HttpGetRequest("https://musicsheetwriter.tk/api/users/" + my_user.PersonalData.Id + "/scores/favourites");
        }

        private void button1_Click(object sender, RoutedEventArgs e) // My favourites
        {
            getListFavouriteScore();
            Frame.Navigate(typeof(DisplayScore), ScoreList);
        }

        /* Muses */
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
                //listViewMuse.ItemsSource = UserList;
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
            connect.HttpGetRequest("https://musicsheetwriter.tk/api/users/" + my_user.PersonalData.Id + "/subscriptions");
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

        private void button2_Click(object sender, RoutedEventArgs e) // muses
        {
            getListAbonne();
            Frame.Navigate(typeof(DisplayUser), UserList);
        }

        /* fans */
        private void getListAbon(object sender, EventArgs e)
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
                //listViewFan.ItemsSource = UserList;
                foreach (var item in UserList)
                {
                    System.Diagnostics.Debug.WriteLine(item);
                }
            }
        }

        private void getListAbonnet()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.getListAbon);
            System.Diagnostics.Debug.WriteLine("https://musicsheetwriter.tk/api/users/" + my_user.PersonalData.Id + "/subscribers");
            connect.HttpGetRequest("https://musicsheetwriter.tk/api/users/" + my_user.PersonalData.Id + "/subscribers");
        }

        private void button3_Click(object sender, RoutedEventArgs e) // fans
        {
            getListAbonnet();
            Frame.Navigate(typeof(DisplayUser), UserList);
        }
    }
}

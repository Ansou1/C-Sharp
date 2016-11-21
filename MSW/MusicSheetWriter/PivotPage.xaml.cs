using MusicSheetWriter.Common;
using System;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Pivot Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace MusicSheetWriter
{
    public sealed partial class PivotPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        //private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();

        //private User myuser;
        private string loginResponse;

        public static PivotPage Current;
        private string userId;

        public PivotPage()
        {
            this.InitializeComponent();

            Current = this;

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        #region NavigationHelper

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

        #endregion

        ///// <summary>
        ///// Gets the view model for this <see cref="Page"/>.
        ///// This can be changed to a strongly typed view model.
        ///// </summary>
        //public ObservableDictionary DefaultViewModel
        //{
        //    get { return this.defaultViewModel; }
        //}

        ///// <summary>
        ///// Adds an item to the list when the app bar button is clicked.
        ///// </summary>
        //private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string groupName = this.pivot.SelectedIndex == 0 ? FirstGroupName : SecondGroupName;
        //    var group = this.DefaultViewModel[groupName] as SampleDataGroup;
        //    var nextItemId = group.Items.Count + 1;
        //    var newItem = new SampleDataItem(
        //        string.Format(CultureInfo.InvariantCulture, "Group-{0}-Item-{1}", this.pivot.SelectedIndex + 1, nextItemId),
        //        string.Format(CultureInfo.CurrentCulture, this.resourceLoader.GetString("NewItemTitle"), nextItemId),
        //        string.Empty,
        //        string.Empty,
        //        this.resourceLoader.GetString("NewItemDescription"),
        //        string.Empty);

        //    group.Items.Add(newItem);

        //    // Scroll the new item into view.
        //    var container = this.pivot.ContainerFromIndex(this.pivot.SelectedIndex) as ContentControl;
        //    var listView = container.ContentTemplateRoot as ListView;
        //    listView.ScrollIntoView(newItem, ScrollIntoViewAlignment.Leading);
        //}

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            this.loginResponse = e.Parameter as string;
            //this.get_first_user = JsonConvert.DeserializeObject<DataModel.connectionData>((string)e.Parameter);

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
            //this.testUser();
        }


        private void dotestuser(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  USER #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
        }

        private void testUser()
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.dotestuser);
            System.Diagnostics.Debug.WriteLine("https://musicsheetwriter.tk/api/users/" + this.userId);
            connect.HttpGetRequest("https://musicsheetwriter.tk/api/users/" + this.userId);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        #region Méthodes à déplacer dans d'autres pages

        //private void getListScoreID2(object sender, EventArgs e)
        //{
        //    HTTPRequest connect = (HTTPRequest)sender;
        //    System.Diagnostics.Debug.WriteLine("##################  Get all scores  #######################");
        //    System.Diagnostics.Debug.WriteLine(connect.Response);
        //}

        //private void getListScoreID()
        //{
        //    HTTPRequest connect = new HTTPRequest();
        //    connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.getListScoreID2);
        //    connect.GetaString("https://musicsheetwriter.tk/api/scores/[id]");
        //}

        #endregion
        
       

        private void AccountAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountPage), this.loginResponse);
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

        private void SettingsAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Pas implémenter à voir ce qu'on en fait :)");
        }

        private void SearchAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage), this.loginResponse);
        }

        //Followers
        private void MusesTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MusePage), this.loginResponse);
        }

        //Subscribers
        private void FansTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FanPage), this.loginResponse);
        }

        //My Partitions
        private void MyScoresTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyScorePage), this.loginResponse);
        }

        //Partitions favorites
        private void MyFavoritesTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyFavoritePage), this.loginResponse);
        }
    }
}

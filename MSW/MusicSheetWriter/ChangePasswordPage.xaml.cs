using MusicSheetWriter.Common;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MusicSheetWriter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChangePasswordPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        User my_user;

        public ChangePasswordPage()
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
            my_user = new User();
            my_user = e.Parameter as User;
            //System.Diagnostics.Debug.WriteLine("pray god of computer");
            //System.Diagnostics.Debug.WriteLine(my_user.PersonalData.Username);
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

        private void CancelAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPasswordTextBlock.Text = "";
            this.NewPasswordTextBlock.Text = "";
        }

        private void doChangePassword(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("wait de l'élement en envoie :)");
        }

        private void ValidateAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doChangePassword);
            connect.HttpPutRequest("https://musicsheetwriter.tk/api/users/" + my_user.PersonalData.Id + "/password", "{ \"current_password\":\"" + this.CurrentPasswordTextBlock.Text + "\",\n\"new_password\":\"" + this.NewPasswordTextBlock.Text + "\" }");
        }
    }
}

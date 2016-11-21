using MusicSheetWriter.Common;
using System;
using System.IO;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Graphics.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MusicSheetWriter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page, IFileOpenPickerContinuable
    {
        private readonly NavigationHelper navigationHelper;
        User my_user;

        private FileOpenPickerContinuationEventArgs _continuationArgs = null;
        private string contentType;
        byte[] byteArray;

        public FileOpenPickerContinuationEventArgs ContinuationArgs
        {
            get { return _continuationArgs; }
            set
            {
                _continuationArgs = value;
                ContinueFileOpenPicker(_continuationArgs);
            }
        }

        public SettingsPage()
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
            //System.Diagnostics.Debug.WriteLine(my_user);

            this.UsernameTextBlock.Text = my_user.PersonalData.Username;
            this.FirstnameTextBlock.Text = my_user.PersonalData.FirstName;
            this.LastnameTextBlock.Text = my_user.PersonalData.Lastname;
            this.MessageTextBlock.Text = my_user.PersonalData.Message;
            this.UserImage.Source = new BitmapImage(new Uri(my_user.PersonalData.PictureURL, UriKind.Absolute));
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

        private void doEditProfile(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("wait de l'élement en envoie :)");
        }

        private void doEditProfile2(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("wait de l'élement en envoie :)");
        }

        private void ValidateAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doEditProfile);
            connect.HttpPutRequest("https://musicsheetwriter.tk/api/users/" + my_user.PersonalData.Id + "/personal_data", "{ \"firstname\":\"" + this.FirstnameTextBlock.Text + "\",\n\"lastname\":\"" + this.LastnameTextBlock.Text + "\",\n\"email\":\"" + my_user.PersonalData.Email + "\",\n\"message\":\"" + this.MessageTextBlock.Text + "\" }");
            
            HTTPRequest connect_sec = new HTTPRequest();
            connect_sec.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doEditProfile2);
            connect_sec.HttpPutRequestUpload("https://musicsheetwriter.tk/api/users/" + my_user.PersonalData.Id + "/photo", byteArray, contentType);

            Frame.Navigate(typeof(AccountPage), "{\"id\":\"" + my_user.PersonalData.Id + "\",\"username\":\"" + my_user.PersonalData.Username + "\",\"last_activity_date\":null}");
            System.Diagnostics.Debug.WriteLine("{\"id\":\"" + my_user.PersonalData.Id + "\",\"username\":\"" + my_user.PersonalData.Username + "\",\"last_activity_date\":null}");
        }

        private void CancelAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.FirstnameTextBlock.Text = my_user.PersonalData.FirstName;
            this.LastnameTextBlock.Text = my_user.PersonalData.Lastname;
            this.MessageTextBlock.Text = my_user.PersonalData.Message;
            this.UserImage.Source = new BitmapImage(new Uri(my_user.PersonalData.PictureURL, UriKind.Absolute));
        }

        private void ChangePasswordAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangePasswordPage), my_user);
        }

        private void UserImage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".gif");
            openPicker.FileTypeFilter.Add(".ico");

            openPicker.PickSingleFileAndContinue();
        }

        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if (args.Files.Count > 0)
            {
                contentType = args.Files[0].ContentType;
                Stream requestStream = await args.Files[0].OpenStreamForReadAsync();
                byteArray = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    await requestStream.CopyToAsync(ms);
                    byteArray = ms.ToArray();
                }
                StorageFile storageFile = args.Files[0];
                var stream = await storageFile.OpenAsync(FileAccessMode.Read);
                var bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(stream);

                var decodeur = await BitmapDecoder.CreateAsync(stream);
                this.UserImage.Source = bitmapImage;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Operation cancelled.");
            }
        }
    }
}

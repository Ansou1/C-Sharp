using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MSW_10_UWA.Common;
using Music_Sheet_Writer.Models;
using MusicSheetWriter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Activation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;

namespace MSW_10_UWA.ViewModel
{
    class AccountUpdateViewModel : INotifyPropertyChanged
    {
        private string _loginResponse;
        private string _userID;
        private User _user;

        public ICommand AcceptCommandAccountUpdateViewModel { get; private set; }
        public ICommand CancelCommandAccountUpdateViewModel { get; private set; }
        public ICommand ChangePasswordCommandAccountUpdateViewModel { get; private set; }
        public ICommand ImageProfileCommandAccountUpdateViewModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private string contentType;
        byte[] byteArray;

        private String username;
        public String UserName
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("UserName");
            }
        }

        private String firstname;
        public String FirstName
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged("FirstName");
            }
        }

        private String lastname;
        public String LastName
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged("LastName");
            }
        }

        private BitmapImage pictureurl;
        public BitmapImage PictureURL
        {
            get { return pictureurl; }
            set
            {
                pictureurl = value;
                OnPropertyChanged("PictureURL");
            }
        }

        private String message;
        public String Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private String email;
        public String Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public AccountUpdateViewModel()
        {
            Messenger.Default.Register<String>(this, loginResponse =>
            {
                _loginResponse = loginResponse;
                System.Diagnostics.Debug.WriteLine("###### recieved AccountUpdateViewModel ######");
                System.Diagnostics.Debug.WriteLine(_loginResponse);
                JsonTools test = new JsonTools();
                _userID = test.JsonGetIdUser(_loginResponse);
                byteArray = null;
                contentType = null;
                AccountInformationDisplay();
            });

            AcceptCommandAccountUpdateViewModel = new RelayCommand(() =>
            {
                Globals test_global = null;
                test_global = Globals.getInstance();
                HTTPRequest connect = new HTTPRequest();
                connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doEditProfile);
                connect.HttpPutRequest(test_global.IpAPI + "api/users/" + _user.Id + "/personal_data", "{ \"firstname\":\"" + FirstName + "\",\n\"lastname\":\"" + LastName + "\",\n\"email\":\"" + _user.Email + "\",\n\"message\":\"" + Message + "\" }");

                System.Diagnostics.Debug.WriteLine("byteArray => " + byteArray);
                System.Diagnostics.Debug.WriteLine("contentType => " + contentType);

                if (contentType != null && byteArray != null)
                {
                    HTTPRequest connect_sec = new HTTPRequest();
                    connect_sec.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doEditProfile2);
                    connect_sec.HttpPutRequestUpload(test_global.IpAPI + "api/users/" + _user.Id + "/photo", byteArray, contentType);
                }

                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                Messenger.Default.Send(_loginResponse);
                navigationService.GoBack();

            });

            CancelCommandAccountUpdateViewModel = new RelayCommand(() => 
            {
                UserName = _user.UserName;
                FirstName = _user.FirstName;
                LastName = _user.LastName;
                Email = _user.Email;
                //this.UserImage.ImageSource = new BitmapImage(new Uri(_user.PictureURL, UriKind.Absolute));
                PictureURL = new BitmapImage(new Uri(_user.PictureURL, UriKind.Absolute));
                Message = _user.Message;
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                navigationService.GoBack();
            });

            ChangePasswordCommandAccountUpdateViewModel = new RelayCommand(() => 
            {
                System.Diagnostics.Debug.WriteLine("AccountUpdateCommandAccountPage click :)");
                INavigationService navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
                Messenger.Default.Send(_loginResponse);
                navigationService.NavigateTo(ViewModelLocator.NavigationChangePasswordView);
            });

            ImageProfileCommandAccountUpdateViewModel = new RelayCommand(async () =>
            {
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                openPicker.FileTypeFilter.Add(".jpg");
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".png");
                openPicker.FileTypeFilter.Add(".gif");
                openPicker.FileTypeFilter.Add(".ico");

                StorageFile file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                    System.Diagnostics.Debug.WriteLine("Picked photo: " + file.Name);
                    contentType = file.ContentType;
                    
                    Stream requestStream = await file.OpenStreamForReadAsync();
                    byteArray = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await requestStream.CopyToAsync(ms);
                        byteArray = ms.ToArray();
                    }
                    StorageFile storageFile = file;
                    var stream = await storageFile.OpenAsync(FileAccessMode.Read);
                    var bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(stream);

                    var decodeur = await BitmapDecoder.CreateAsync(stream);
                    PictureURL = bitmapImage;
                    
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Operation cancelled.");
                }
            });
        }

        private void doEditProfile(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("wait de l'élement en envoie :)");
        }

        private void doEditProfile2(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("wait de l'élement en envoie :)");
        }

        private void AccountInformationDisplay()
        {
            Globals test_global = null;
            test_global = Globals.getInstance();
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(AccountInformationDisplay2);
            connect.HttpGetRequest(test_global.IpAPI + "api/users/" + _userID + "/personal_data");
        }

        private void AccountInformationDisplay2(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            JsonTools tmp = new JsonTools();
            System.Diagnostics.Debug.WriteLine(connect.Response);

            this._user = tmp.getUserFromJson(connect.Response);

            UserName = _user.UserName;
            FirstName = _user.FirstName;
            LastName = _user.LastName;
            Email = _user.Email;

            Globals test_global = null;
            test_global = Globals.getInstance();
            if (_user.PictureURL.Equals(""))
                _user.PictureURL = test_global.IpAPI + "bundles/msw/images/default_avatar.png";
                //_user.PictureURL = "https://musicsheetwriter.tk/bundles/msw/images/default_avatar.png";
            PictureURL = new BitmapImage(new Uri(_user.PictureURL, UriKind.Absolute));
            Message = _user.Message;
        }
    }
}

using MSW_10_UWA.Common;
using MusicSheetWriter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Sheet_Writer.Models
{
    public class User : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; } 
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

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

        private String pictureurl;
        public String PictureURL
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

        private Boolean isASubscription;
        public Boolean IsASubscription
        {
            get { return isASubscription; }
            set
            { 
                isASubscription = value;

                OnPropertyChanged("IsASubscription");
            }
        }
  
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

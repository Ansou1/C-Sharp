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
                if (isASubscription == true)
                {
                    System.Diagnostics.Debug.WriteLine("IsASubscription click checked event!!!");
                    addNouvelAbonnement(Id);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("IsASubscription click unchecked event!!!");
                    deleteAbonnement(Id);
                }
            }
        }
  
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void doDeleteAbonnement(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  delete abonnement  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
        }

        private void deleteAbonnement(int deleteId)
        {
            Globals value_user = null;
            value_user = Globals.getInstance();
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doDeleteAbonnement);
            connect.HttpDeleteRequest(value_user.IpAPI + "api/users/" + value_user.UserConnected + "/subscriptions/" + deleteId.ToString());
        }

        private void doAddNouvelAbonnement(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Add abonnement  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
        }

        private void addNouvelAbonnement(int addId)
        {
            Globals value_user = null;
            value_user = Globals.getInstance();
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doAddNouvelAbonnement);
            connect.HttpPostRequest(value_user.IpAPI + "api/users/" + value_user.UserConnected + "/subscriptions", "{ \"id\": " + addId.ToString() + " }");
        }
    }
}

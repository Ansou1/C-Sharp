using System;
using System.Collections.Generic;

namespace MusicSheetWriter
{
    public class User
    {
        #region API parameters
        public PersonalDatas PersonalData { get; set; }
        public String Status { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime CreationDate { get; set; }
        public Boolean isASubscription { get; set; }
        public List<User> Subscription { get; set; }
        public int nb_subscribers { get; set; }
        public int nb_scores { get; set; }

        public User()
        {
            this.PersonalData = new PersonalDatas();
        }

        public override string ToString()
        {
            return "ID: " + PersonalData.Id.ToString() +
                "\nUsername: " + PersonalData.Username +
                "\nFirstname: " + PersonalData.FirstName +
                "\nLastname: " + PersonalData.Lastname +
                "\nEmail: " + PersonalData.Email +
                "\nPictureURL: " + PersonalData.PictureURL +
                "\nMessage: " + PersonalData.Message +
                "\nStatus: " + Status +
                "\nLastActivityDate: " + LastActivityDate +
                "\nCreationDate: " + CreationDate +
                "\nisASubscription: " + isASubscription +
                "\nSubscription: " + Subscription;
        }

        #endregion API parameters
    }

    public class PersonalDatas
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String FirstName { get; set; }
        public String Lastname { get; set; }
        public String Email { get; set; }
        public String PictureURL { get; set; }
        public String Message { get; set; }

        public override string ToString()
        {
            return "ID: " + Id.ToString() +
                "\nUsername: " + Username +
                "\nFirstname: " + FirstName +
                "\nLastname: " + Lastname +
                "\nEmail: " + Email +
                "\nPictureURL: " + PictureURL +
                "\nMessage: " + Message;
        }
    }
}

using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MSW_10_UWA.Models;
using MSW_10_UWA.ViewModel;
using Music_Sheet_Writer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace MSW_10_UWA.Common
{
    public class JsonTools
    {
        private String _userID;

        public string JsonGetIdUser(string _loginResponse)
        {
            JsonObject json = new JsonObject();
            if (JsonObject.TryParse(_loginResponse, out json) == true)
            {
                IJsonValue jsonValue = null;
                if (json.TryGetValue("id", out jsonValue) == true)
                {
                    _userID = jsonValue.GetString();
                    System.Diagnostics.Debug.WriteLine(_userID);
                }
                else
                {
                    //throw new MSWException("Error with the json returned from the login request.");
                }
            }
            return _userID;
        }

        public Score getScoreFromJson(string jsonString)
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

                    score.Compositor.Id = (by.TryGetValue("id", out value)) ? (int.Parse(value.GetString())) : (0);
                    score.Compositor.UserName = (by.TryGetValue("username", out value)) ? (value.GetString()) : ("");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    throw;
                }
            }
            return score;
        }

        public User getUserFromJson(string jsonString)
        {
            JsonObject jsonObject = new JsonObject();
            User user = new User();
            if (JsonObject.TryParse(jsonString, out jsonObject))
            {
                IJsonValue value = null;
                try
                {
                    user.Id = (jsonObject.TryGetValue("id", out value)) ? (int.Parse(value.GetString())) : (0);
                    user.UserName = (jsonObject.TryGetValue("username", out value)) ? (value.GetString()) : ("");
                    user.FirstName = (jsonObject.TryGetValue("firstname", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.LastName = (jsonObject.TryGetValue("lastname", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.Message = (jsonObject.TryGetValue("message", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.Email = (jsonObject.TryGetValue("email", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                    user.PictureURL = (jsonObject.TryGetValue("photo", out value)) ? (value.GetString()) : ("");
                    user.IsASubscription = (jsonObject.TryGetValue("is_subscription", out value)) ? (value.GetBoolean()) : (false);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    throw;
                }
            }
            return user;
        }

    }
}

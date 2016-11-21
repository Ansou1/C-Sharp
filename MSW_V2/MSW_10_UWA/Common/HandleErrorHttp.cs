using System;
using Windows.Data.Json;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace MusicSheetWriter
{
    class HandleErrorHttp
    {
        private String shortCode;
        private String message;

        public async void popError(HttpStatusCode StatusCode, String response)
        {
            JsonObject json = new JsonObject();
            if (JsonObject.TryParse(response, out json) == true)
            {
                IJsonValue value = null;

                shortCode = (json.TryGetValue("shortcode", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
                message = (json.TryGetValue("message", out value)) ? ((value.ValueType != JsonValueType.Null) ? (value.GetString()) : ("")) : ("");
            }

            if ((shortCode[0] != 'G' && shortCode[1] != 'L' && shortCode[2] != 'O') || shortCode == "GLO-BADFIELD")
            {
                MessageDialog test = new MessageDialog(message);
                await test.ShowAsync();
            }
        }
    }
}

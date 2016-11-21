using System;
using System.IO;
using Windows.Web.Http;

namespace MusicSheetWriter
{
    public class HTTPRequest
    {
        private bool value = false;
        public bool getValue()
        {
            return value;
        }

        private string _response;
        public string Response
        {
            get { return _response; }
            private set { }
        }

        private HttpStatusCode _statusCode;
        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
            private set { }
        }
        
        public delegate void RequestFinishedEventHandler(object sender, EventArgs e);
        public event RequestFinishedEventHandler RequestFinished;

        
        public async void HttpDeleteRequest(String URL)
        {
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ delete request (arguments)=>" + URL);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(new Uri(URL));
            this._statusCode = response.StatusCode;
            this._response = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ delete request (response)=>" + this._response);
            if (this._statusCode == HttpStatusCode.Ok || this._statusCode == HttpStatusCode.NoContent)
            {
                value = true;
            }
            else
            {
                HandleErrorHttp err = new HandleErrorHttp();
                err.popError(this._statusCode, this._response);
                value = false;
            }
            OnRequestFinished(EventArgs.Empty);
        }

        public async void HttpPostRequest(String URL, String data)
        {
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ post request (arguments)=>" + URL + data);
                HttpStringContent stringContent = new HttpStringContent(data, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
                HttpClient client = new HttpClient();
                System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ post request (stringContent)=> " + stringContent);
                HttpResponseMessage response = await client.PostAsync(new Uri(URL), stringContent);
                this._statusCode = response.StatusCode;
                this._response = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ post request (response)=>" + this._response);

                if (this._statusCode == HttpStatusCode.Ok || this._statusCode == HttpStatusCode.NoContent)
                {
                    value = true;
                }
                else
                {
                    HandleErrorHttp err = new HandleErrorHttp();
                    err.popError(this._statusCode, this._response);
                    value = false;
                }
                OnRequestFinished(EventArgs.Empty);
        }

        public async void HttpGetRequest(String URL)
        {
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ get request (arguments)=>" + URL);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri(URL));
            this._statusCode = response.StatusCode;
            this._response = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ get request (response)=>" + this._response);
            if (this._statusCode == HttpStatusCode.Ok || this._statusCode == HttpStatusCode.NoContent)
            {
                value = true;
            }
            else
            {
                HandleErrorHttp err = new HandleErrorHttp();
                err.popError(this._statusCode, this._response);
                value = false;
            }
            OnRequestFinished(EventArgs.Empty);
        }

        public async void HttpPutRequestUpload(String URL, byte[] data, String contentType)
        {
            Stream stream = new MemoryStream(data);
            HttpStreamContent streamContent = new HttpStreamContent(stream.AsInputStream());
            streamContent.Headers.TryAppendWithoutValidation("Content-Type", contentType);
            Uri resourceAddress = null;
            Uri.TryCreate(URL.Trim(), UriKind.Absolute, out resourceAddress);
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ putImage request (address)=>" + resourceAddress);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, resourceAddress);
            request.Content = streamContent;
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.SendRequestAsync(request);
            this._statusCode = response.StatusCode;
            this._response = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ putImage request (response)=>" + response);
            if (this._statusCode == HttpStatusCode.Ok || this._statusCode == HttpStatusCode.NoContent)
            {
                value = true;
            }
            else
            {
                HandleErrorHttp err = new HandleErrorHttp();
                err.popError(this._statusCode, this._response);
                value = false;
            }
            OnRequestFinished(EventArgs.Empty);
        }

        public async void HttpPutRequest(String URL, String data)
        {
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ put request (arguments)=>" + URL + data);
            HttpStringContent stringContent = new HttpStringContent(data, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
            HttpClient client = new HttpClient();
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ put request (stringContent)=> " + stringContent);
            HttpResponseMessage response = await client.PutAsync(new Uri(URL), stringContent);
            this._statusCode = response.StatusCode;
            this._response = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@ put request (response)=>" + this._response);
            if (this._statusCode == HttpStatusCode.Ok || this._statusCode == HttpStatusCode.NoContent)
            {
                value = true;
            }
            else
            {
                HandleErrorHttp err = new HandleErrorHttp();
                err.popError(this._statusCode, this._response);
                value = false;
            }
            OnRequestFinished(EventArgs.Empty);
        }

        protected virtual void OnRequestFinished(EventArgs e)
        {
            RequestFinished(this, e);
        }
    }
}
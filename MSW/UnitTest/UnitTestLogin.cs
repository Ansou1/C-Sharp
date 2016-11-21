using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MusicSheetWriter;
using Windows.Web.Http;

namespace UnitTest
{
    [TestClass]
    public class UnitTestLogin
    {
        HTTPRequest connect = new HTTPRequest();
        [TestMethod]
        public void test_ok()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/login", "{ \"login\":\"bob\",\n\"password\":\"qwerty123\" }");
            Assert.AreEqual(HttpStatusCode.Ok, connect.StatusCode);
        }

        [TestMethod]
        public void test_ko_bad_user_mail()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/login", "{ \"login\":\"bobdezdz\",\n\"password\":\"qwerty123\" }");
            Assert.AreEqual(HttpStatusCode.Unauthorized, connect.StatusCode);
        }

        [TestMethod]
        public void test_ko_bad_user_mail2()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/login", "{ \"login\":\"bob\",\n\"password\":\"qwdezedzerty123\" }");
            Assert.AreEqual(HttpStatusCode.Unauthorized, connect.StatusCode);
        }

        //need real one
        [TestMethod]
        public void test_ko_account_not_activate()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/login", "{ \"login\":\"bob\",\n\"password\":\"qwerty123\" }");
            Assert.AreEqual(HttpStatusCode.Ok, connect.StatusCode);
        }

        //need real one
        [TestMethod]
        public void test_ko_account_closed()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/login", "{ \"login\":\"bob\",\n\"password\":\"qwerty123\" }");
            Assert.AreEqual(HttpStatusCode.Ok, connect.StatusCode);
        }
    }
}

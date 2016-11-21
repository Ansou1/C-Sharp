using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MusicSheetWriter;
using Windows.Web.Http;

namespace UnitTest
{
    [TestClass]
    class UnitTestCreation
    {
        HTTPRequest connect = new HTTPRequest();

        [TestMethod]
        public void test_ko_username_not_valid()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/users", "{ \"username\":\"!§\",\n\"firstname\":\"simon\",\n\"lastname\":\"Daguenet\",\n\"email\":\"simondaguenet81@yahoo.fr\",\n\"password\":\"azerty123\" }");
            Assert.AreEqual(HttpStatusCode.BadRequest, connect.StatusCode);
        }

        [TestMethod]
        public void test_ko_email_not_valid()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/users", "{ \"username\":\"zefzefzfzvze\",\n\"firstname\":\"simon\",\n\"lastname\":\"Daguenet\",\n\"email\":\"k@a.fr\",\n\"password\":\"azerty123\" }");
            Assert.AreEqual(HttpStatusCode.BadRequest, connect.StatusCode);
        }

        [TestMethod]
        public void test_ko_username_used()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/users", "{ \"username\":\"simons\",\n\"firstname\":\"simon\",\n\"lastname\":\"Daguenet\",\n\"email\":\"simondaguenet81@yahoo.fr\",\n\"password\":\"azerty123\" }");
            Assert.AreEqual(HttpStatusCode.Conflict, connect.StatusCode);
        }

        [TestMethod]
        public void test_ko_email_used()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/users", "{ \"username\":\"zefzefzfzvzefze\",\n\"firstname\":\"simon\",\n\"lastname\":\"Daguenet\",\n\"email\":\"simondaguenet81@gmail.com\",\n\"password\":\"azerty123\" }");
            Assert.AreEqual(HttpStatusCode.Conflict, connect.StatusCode);
        }
    }
}

using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MusicSheetWriter;
using Windows.Web.Http;

namespace UnitTest
{
    [TestClass]
    class UnitTestForgetPass
    {
        HTTPRequest connect = new HTTPRequest();
        [TestMethod]
        public void test_ok()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/forgotten_password", "{ \"email\":\"simondaguenet81@gmail.com\" }");
            Assert.AreEqual(HttpStatusCode.Ok, connect.StatusCode);
        }

        [TestMethod]
        public void test_ko()
        {
            connect.HttpPostRequest("https://musicsheetwriter.tk/api/forgotten_password", "{ \"email\":\"toto@lol.fr\" }");
            Assert.AreEqual(HttpStatusCode.NotFound, connect.StatusCode);
        }
    }
}

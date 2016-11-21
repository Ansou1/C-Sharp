using MusicSheetWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSW_10_UWA.Common
{
    class AddDeleteFavourites
    {
        public AddDeleteFavourites()
        {
        }

        // USER
        private void doDeleteAbonnement(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  delete abonnement  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
        }

        public void deleteAbonnement(int deleteId)
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

        public void addNouvelAbonnement(int addId)
        {
            Globals value_user = null;
            value_user = Globals.getInstance();
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doAddNouvelAbonnement);
            connect.HttpPostRequest(value_user.IpAPI + "api/users/" + value_user.UserConnected + "/subscriptions", "{ \"id\": " + addId.ToString() + " }");
        }

        //SCORES
        private void doDeleteFavourites(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  delete favourite  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
        }

        public void deleteFavourites(int delId)
        {
            Globals value_user = null;
            value_user = Globals.getInstance();
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doDeleteFavourites);
            connect.HttpDeleteRequest(value_user.IpAPI + "api/users/" + value_user.UserConnected + "/scores/favourites/" + delId);
        }

        private void doAddNouveauFavourites(object sender, EventArgs e)
        {
            HTTPRequest connect = (HTTPRequest)sender;
            System.Diagnostics.Debug.WriteLine("##################  Add favourtie  #######################");
            System.Diagnostics.Debug.WriteLine(connect.Response);
        }

        public void addNouveauFavourites(int addId)
        {
            Globals value_user = null;
            value_user = Globals.getInstance();
            HTTPRequest connect = new HTTPRequest();
            connect.RequestFinished += new HTTPRequest.RequestFinishedEventHandler(this.doAddNouveauFavourites);
            connect.HttpPostRequest(value_user.IpAPI + "api/users/" + value_user.UserConnected + "/scores/favourites", "{ \"id\": " + addId + " }");
         }
    }
}

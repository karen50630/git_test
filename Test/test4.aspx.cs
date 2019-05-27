using Google.Apis.Auth.OAuth2;
using Google.Apis.Dialogflow.v2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test
{
    public partial class test4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string targetAddress = "https://dialogflow.googleapis.com/v2/projects/test2-52e24/agent/intents";
            var credentials = GoogleCredential.FromFile(@"D:/test2-87b545c5f3de.json");
            var scopedCredentials = credentials.CreateScoped(DialogflowService.Scope.CloudPlatform);
            var _oAuthToken = scopedCredentials.UnderlyingCredential.GetAccessTokenForRequestAsync().Result;
            //WebClient webclient = new WebClient();
            //WebRequest request = WebRequest.Create(targetAddress);
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(targetAddress);
            //request.Method = "GET";
            //request.ContentType = "application/json";
            //request.Headers.Add("Authorization",  "Bearer" +_oAuthToken);
            WebClient request = new WebClient();
            request.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {_oAuthToken}");
            //string result = "";
            //var result = request.DownloadString(targetAddress);
            byte[] bResult = request.DownloadData(targetAddress);
            string result = Encoding.UTF8.GetString(bResult);

            Response.Write(result);

        }
    }
}
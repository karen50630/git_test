using Google.Apis.Auth.OAuth2;
using Google.Apis.Dialogflow.v2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test
{
    public partial class test5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string targetAddress = "https://dialogflow.googleapis.com/v2/projects/test5-f89ef/agent/intents";
            var credentials = GoogleCredential.FromFile(@"D:\test5-f89ef-72412da22b93.json");
            var scopedCredentials = credentials.CreateScoped(DialogflowService.Scope.CloudPlatform);
            var _oAuthToken = scopedCredentials.UnderlyingCredential.GetAccessTokenForRequestAsync().Result;

          

            WebRequest request = WebRequest.Create(targetAddress);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", $"Bearer {_oAuthToken}");
            string result = "";
            using (var response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }
            Response.Write(result);
            //string result = "";
            //var result = request.DownloadString(targetAddress);
        }
    }
}
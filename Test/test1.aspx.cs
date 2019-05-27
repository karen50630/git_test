using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Dialogflow.v2;
using Google.Apis.Dialogflow.v2.Data;
using Google.Apis.Services;
using System.Threading.Tasks;
using System.IO;

namespace Test
{
    public partial class test1 : System.Web.UI.Page
    {
        private static readonly string PROJECT_ID = "test5-f89ef";
        private static readonly string CREDENTIAL_PATH = @"D:\test5-f89ef-72412da22b93.json";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            var sessionId = Guid.NewGuid().ToString();
            var message = "cc";
            var creds = GoogleCredential.FromFile(CREDENTIAL_PATH);
            var scopedCreds = creds.CreateScoped(DialogflowService.Scope.CloudPlatform);
            var response = new DialogflowService(new BaseClientService.Initializer
            {
                HttpClientInitializer = scopedCreds,
                ApplicationName = PROJECT_ID
            }).Projects.Agent.Sessions.DetectIntent(
                        new GoogleCloudDialogflowV2DetectIntentRequest
                        {
                            QueryInput = new GoogleCloudDialogflowV2QueryInput
                            {
                                Text = new GoogleCloudDialogflowV2TextInput
                                {
                                    Text = message,
                                    LanguageCode = "en-US"
                                }
                            }
                        },
                        $"projects/{PROJECT_ID}/agent/sessions/{sessionId}")
                        .Execute();


            var queryResult = response.QueryResult;
            Console.WriteLine(queryResult.FulfillmentText);


        }


    




    }
}
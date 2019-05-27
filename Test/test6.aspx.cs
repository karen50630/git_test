using Google.Apis.Auth.OAuth2;
using Google.Apis.Dialogflow.v2;
using Google.Apis.Dialogflow.v2.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test
{
    public partial class test6 : System.Web.UI.Page
    {
        private static readonly string PROJECT_ID = "test5-f89ef";
        private static readonly string CREDENTIAL_PATH = @"D:\test5-f89ef-72412da22b93.json";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string targetAddress = "https://dialogflow.googleapis.com/v2/projects/test5-f89ef/agent/intents";
            var credentials = GoogleCredential.FromFile(@"D:\test5-f89ef-72412da22b93.json");
            var scopedCredentials = credentials.CreateScoped(DialogflowService.Scope.CloudPlatform);
            var _oAuthToken = scopedCredentials.UnderlyingCredential.GetAccessTokenForRequestAsync().Result;
            var sessionId = Guid.NewGuid().ToString();
            var message = "cc";
            var response = ResponseAsync(sessionId, message).Result;



        }
        private static async Task<GoogleCloudDialogflowV2DetectIntentResponse> ResponseAsync(string sessionId, string message)
        {


            ServiceAccountCredential credential;
            using (var stream = new FileStream(CREDENTIAL_PATH, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                                             .CreateScoped(DialogflowService.Scope.CloudPlatform)
                                             .UnderlyingCredential as ServiceAccountCredential;
            }

            var service = new DialogflowService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = PROJECT_ID
            });

            var response = service.Projects.Agent.Sessions.DetectIntent(new GoogleCloudDialogflowV2DetectIntentRequest
            {
                QueryInput = new GoogleCloudDialogflowV2QueryInput
                {
                    Text = new GoogleCloudDialogflowV2TextInput
                    {
                        Text = message,
                        LanguageCode = "en-US",
                    },
                },
            }, $"projects/{PROJECT_ID}/agent/sessions/{sessionId}");


            return await response.ExecuteAsync();





        }
       

    }
}
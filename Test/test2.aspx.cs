using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Dialogflow.v2;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Web.Script.Serialization;
using Google.Cloud.Dialogflow.V2;

namespace Test
{
    public partial class test2 : System.Web.UI.Page
    {
        private static readonly string PROJECT_ID = "test5-f89ef";
        private static readonly string CREDENTIAL_PATH = @"D:\test5-f89ef-72412da22b93.json";
        protected void Page_Load(object sender, EventArgs e)
        {

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", CREDENTIAL_PATH);
            var session_Id = Guid.NewGuid().ToString();
            string[] text = { "cc" };
            Response.Write(DetectIntentFromTexts(PROJECT_ID, session_Id, text, "en-US"));
        }
        public static int DetectIntentFromTexts(string projectId, string sessionId, string[] texts, string languageCode)
        {

            var client = SessionsClient.Create();

            foreach (var text in texts)
            {
                var response = client.DetectIntent(
                    session: new SessionName(projectId, sessionId),
                    queryInput: new QueryInput()
                    {
                        Text = new TextInput()
                        {
                            Text = text,
                            LanguageCode = languageCode
                        }
                    }
                );

                var queryResult = response.QueryResult;

                Console.WriteLine($"Query text: {queryResult.QueryText}");
                if (queryResult.Intent != null)
                {
                    Console.WriteLine($"Intent detected: {queryResult.Intent.DisplayName}");
                }
                Console.WriteLine($"Intent confidence: {queryResult.IntentDetectionConfidence}");
                Console.WriteLine($"Fulfillment text: {queryResult.FulfillmentText}");
                Console.WriteLine();

            }

            return 0;
        }
    }
}
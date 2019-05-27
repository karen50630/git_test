using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;
using Grpc.Auth;

namespace Test
{
    public partial class test3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = new QueryInput
            {
                Text = new TextInput
                {
                    Text = "cc",
                    LanguageCode = "en-us"
                }
            };

            var sessionId = "SomeUniqueId";
            var agent = "test5";
            var creds = GoogleCredential.FromJson(@"D:/test5.json");
            var channel = new Grpc.Core.Channel(SessionsClient.DefaultEndpoint.Host,
                          creds.ToChannelCredentials());

            var client = SessionsClient.Create(channel);

            var dialogFlow = client.DetectIntent(
                new SessionName(agent, sessionId),
                query
            );
            channel.ShutdownAsync();
        }
    }
}
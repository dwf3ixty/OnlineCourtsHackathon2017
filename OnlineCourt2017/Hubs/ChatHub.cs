using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace OnlineCourt2017.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            GetQnAMakerResponse(name, message);
        }

        private void GetQnAMakerResponse(string name, string question)
        {
            string responseString = string.Empty;

            // var query = “hi”; //User Query
            var knowledgebaseId = ""; // Use knowledge base id created.
            var qnamakerSubscriptionKey = ""; //Use subscription key assigned to you.

            //Build the URI
            Uri qnamakerUriBase = new Uri("https://westus.api.cognitive.microsoft.com/qnamaker/v1.0");
            var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{knowledgebaseId}/generateAnswer");

            //Add the question as part of the body
            var postBody = $"{{\"question\": \"{question}\"}}";

            //Send the POST request
            using (WebClient client = new WebClient())
            {
                //Set the encoding to UTF8
                client.Encoding = System.Text.Encoding.UTF8;

                //Add the subscription key header
                client.Headers.Add("Ocp-Apim-Subscription-Key", qnamakerSubscriptionKey);
                client.Headers.Add("Content-Type", "application/json");
                responseString = client.UploadString(builder.Uri, postBody);

                //De-serialize the response
                QnAMakerResult response;
                try
                {
                    response = JsonConvert.DeserializeObject<QnAMakerResult>(responseString);
                }
                catch
                {
                    throw new Exception("Unable to deserialize QnA Maker response string.");
                }

                if(response.Score == 0)
                {
                    Clients.All.addNewMessageToPage("Case Officer", "I'm sorry, the answer to your question isn't in my knowledge base. Your query has been forwarded onto the assigned case officer.");
                }
                else
                {
                    Clients.All.addNewMessageToPage("Case Officer", response.Answer);
                }
                
            }
        }

        private class QnAMakerResult
        {
            /// <summary>
            /// The top answer found in the QnA Service.
            /// </summary>
            [JsonProperty(PropertyName = "answer")]
            public string Answer { get; set; }

            /// <summary>
            /// The score in range [0, 100] corresponding to the top answer found in the QnA    Service.
            /// </summary>
            [JsonProperty(PropertyName = "score")]
            public double Score { get; set; }
        }
    }
}
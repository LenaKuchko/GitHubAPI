using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubAPI.Models
{
    public class Repo
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Stargazers_Count { get; set; }

        public static List<Repo> GetStarredRepoes()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/search/repositories?q=user:LenaKuchko&sort=stars", Method.GET);

            client.Authenticator = new HttpBasicAuthenticator(EnvironmentVariables.UserName, EnvironmentVariables.Token);
            request.AddHeader("User-Agent", "GithubAPI");
            
            var response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var repoList = JsonConvert.DeserializeObject<List<Repo>>(jsonResponse["items"].ToString());
            return repoList;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

    }
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Github.BusinessLayer.Entities;
using Github.BusinessLayer.Extensions;
using Newtonsoft;
using Newtonsoft.Json;

namespace Github.BusinessLayer;

public class GithubBusinessLayer : IGithubBusinessLayer
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GithubBusinessLayer()
    {

    }

    public GithubBusinessLayer(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<GithubUser>> GetUsers(List<string> usernames)
    {
        usernames = usernames.RemoveDuplicates();

        var result = new List<GithubUser>();

        using (HttpClient httpClient = _httpClientFactory.CreateClient())
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Github code test");
            // In a real app the user would probably have to log in, or this would be stored in settings if we used a token that did not expire or had a reasonable life.
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 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");

            foreach (var username in usernames)
            {
                try
                {
                    var data = await httpClient.GetAsync($"https://api.github.com/users/{username.Trim()}");

                    if (data.IsSuccessStatusCode)
                    {
                        var body = await data.Content.ReadAsStringAsync();

                        var item = JsonConvert.DeserializeObject<GithubUser>(body);

                        if (item != null)
                        {
                            if (!string.IsNullOrWhiteSpace(item.Followers_Url))
                            {
                                var followersCall = await httpClient.GetAsync(item.Followers_Url);

                                if(followersCall.IsSuccessStatusCode)
                                {
                                    var followers = await followersCall.Content.ReadAsStringAsync();

                                    var items = JsonConvert.DeserializeObject<List<GithubUser>>(followers);

                                    if(items != null)
                                    {
                                        item.NumberOfFollowers = items.Count;
                                    }
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(item.Repos_Url))
                            {
                                var reposCall = await httpClient.GetAsync(item.Repos_Url);

                                if (reposCall.IsSuccessStatusCode)
                                {
                                    var repos = await reposCall.Content.ReadAsStringAsync();

                                    var items = JsonConvert.DeserializeObject<List<GithubRepo>>(repos);

                                    if (items != null)
                                    {
                                        item.NumberOfRepositories = items.Count;
                                    }
                                }
                            }

                            result.Add(item);
                        }
                    }
                }
                catch (Exception)
                {
                    //TODO: log error
                }
            }
        }

        return result;
    }
}

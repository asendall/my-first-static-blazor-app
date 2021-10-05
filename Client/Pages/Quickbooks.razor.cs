using BlazorApp.Client.Extensions;
using BlazorApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages
{
    public partial class Quickbooks
    {
        private string url;
        private string accessToken;
        private string refreshToken;

        private AuthorizationServerResponse authorizationServerResponse;

        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public HttpClient Http { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
                var queryStrings = QueryHelpers.ParseQuery(uri.Query);

                authorizationServerResponse = new AuthorizationServerResponse()
                {
                    State = NavManager.ExtractQueryStringByKey<string>("state"),
                    Code = NavManager.ExtractQueryStringByKey<string>("code"),
                    RealmId = NavManager.ExtractQueryStringByKey<string>("realmId")
                };


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task GetAuthorizationURL()
        {
            url = await Http.GetStringAsync("/api/GetAuthorizationURL");
        }

        private void Connect()
        {
            NavManager.NavigateTo(url);
        }

        private async Task GetAuthTokensAsync()
        {
            accessToken = "Loading...";

            var response = await Http.PostAsJsonAsync("/api/GetAuthTokensAsync", authorizationServerResponse);
            var tokenResponse = await response.Content.ReadFromJsonAsync<MyTokenResponse>();

            accessToken = tokenResponse.AccessToken;
            refreshToken = tokenResponse.RefreshToken;

        }

        private async Task RefreshToken()
        {
            accessToken = "Loading...";
            refreshToken = "";

            var response = await Http.PostAsJsonAsync("/api/RefreshTokenAsync", authorizationServerResponse);
            var tokenResponse = await response.Content.ReadFromJsonAsync<MyTokenResponse>();

            accessToken = tokenResponse.AccessToken;
            refreshToken = tokenResponse.RefreshToken;
        }

        private async Task CreateCustomer()
        {
            var response = await Http.PostAsJsonAsync("/api/CreateCustomerAsync", new AppCustomer(Guid.NewGuid(),"Test","test@mycharity.org"));
            var customer = await response.Content.ReadFromJsonAsync<AppCustomer>();
        }
    }
}

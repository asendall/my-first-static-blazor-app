﻿namespace BlazorApp.Api
{
    public class OAuth2Keys
    {
        public OAuth2Keys()
        {

        }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RealmId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
        public string QBOBaseUrl { get; set; }
        public string Environment { get; set; }
        public static string CSRFToken { get; set; }
        public static string AuthURL { get; set; }
    }
}

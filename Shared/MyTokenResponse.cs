using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
    public class MyTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

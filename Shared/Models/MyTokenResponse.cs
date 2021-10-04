using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.Models
{
    public class MyTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

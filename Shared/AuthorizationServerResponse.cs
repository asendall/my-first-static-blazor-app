namespace BlazorApp.Shared
{
    public class  AuthorizationServerResponse
    {
        public string Code { get; set; }
        public string RealmId { get; set; }
        public string State { get; set; }
    }
}

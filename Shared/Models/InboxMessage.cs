using GAR.Functions.IntegrationEvents;
using System;
namespace BlazorApp.Shared.Models
{
    public class InboxMessage
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public IntegrationEvent Event { get; set; }        
    }
}

using System;

namespace GAR.Functions.IntegrationEvents
{
    public class IntegrationEvent
    {
        public string TenantId { get; set; }
        public string EventId { get; set; } 
        public DateTime OccurredAt { get; set; }
    }
}

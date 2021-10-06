using BlazorApp.Shared.Models;
using GAR.Functions.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace GAR.API.Library.Data.Configurations
{
    public class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
    {
        public void Configure(EntityTypeBuilder<InboxMessage> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Event)
                .HasConversion(
                    e => JsonConvert.SerializeObject(e, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    }),
                    e => JsonConvert.DeserializeObject<IntegrationEvent>(e, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    })
                );
        }
    }
}

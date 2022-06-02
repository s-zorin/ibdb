using Ibdb.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ibdb.Shared.Infrastructure.Ef
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder
                .ToTable("event_store");

            builder
                .HasIndex(x => x.EntityVersion);

            builder
                .Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("Event Id.")
                .IsRequired();

            builder
                .Property(e => e.Timestamp)
                .HasColumnName("timestamp")
                .HasComment("Event timestamp.")
                .HasDefaultValueSql("NOW()")
                .IsRequired();

            builder
                .Property(e => e.Name)
                .HasColumnName("name")
                .HasComment("Event name.")
                .IsRequired();

            builder
                .Property(e => e.EntityId)
                .HasColumnName("entity_id")
                .HasComment("An Id of an entity which this event related to.")
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property(e => e.EntityVersion)
                .HasColumnName("entity_version")
                .HasComment("A would be entity version if this event was applied to it.")
                .IsRequired();

            builder
                .Property(e => e.DataVersion)
                .HasColumnName("data_version")
                .HasComment("Event data version.")
                .IsRequired();

            builder
                .Property(e => e.Data)
                .HasColumnName("data")
                .HasComment("Event data.")
                .IsRequired();
        }
    }
}

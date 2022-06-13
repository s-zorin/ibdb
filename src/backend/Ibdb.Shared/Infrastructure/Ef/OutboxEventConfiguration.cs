using Ibdb.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ibdb.Shared.Infrastructure.Ef
{
    internal class OutboxEventConfiguration : IEntityTypeConfiguration<OutboxEvent>
    {
        public void Configure(EntityTypeBuilder<OutboxEvent> builder)
        {
            builder
                .ToTable("outbox");

            builder
                .HasIndex(x => x.Timestamp);

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

﻿// <auto-generated />
using System;
using Ibdb.Shared.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ibdb.Shared.Migrations.Outbox
{
    [DbContext(typeof(OutboxContext))]
    [Migration("20220608112809_OutboxInitial")]
    partial class OutboxInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ibdb.Shared.Domain.OutboxEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Event Id.");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("data")
                        .HasComment("Event data.");

                    b.Property<int>("DataVersion")
                        .HasColumnType("integer")
                        .HasColumnName("data_version")
                        .HasComment("Event data version.");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid")
                        .HasColumnName("entity_id")
                        .HasComment("An Id of an entity which this event related to.");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasComment("Event name.");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp")
                        .HasDefaultValueSql("NOW()")
                        .HasComment("Event timestamp.");

                    b.HasKey("Id");

                    b.HasIndex("Timestamp");

                    b.ToTable("outbox", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}

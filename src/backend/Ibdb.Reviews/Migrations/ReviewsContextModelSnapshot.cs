﻿// <auto-generated />
using System;
using Ibdb.Reviews.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ibdb.Reviews.Migrations
{
    [DbContext(typeof(ReviewsContext))]
    partial class ReviewsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ibdb.Reviews.Domain.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Review Id.");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id")
                        .HasComment("Book Id.");

                    b.Property<float>("Score")
                        .HasColumnType("real")
                        .HasColumnName("score")
                        .HasComment("Score. 0.0 - lowest possible score, 1.0 highest possible score.");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text")
                        .HasComment("Review text.");

                    b.HasKey("Id");

                    b.ToTable("reviews", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
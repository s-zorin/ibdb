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

            modelBuilder.Entity("Ibdb.Reviews.Domain.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Book Id.");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description")
                        .HasComment("Book description.");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted")
                        .HasComment("If true the book is considered to be deleted.");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title")
                        .HasComment("Book title.");

                    b.HasKey("Id");

                    b.ToTable("books", (string)null);
                });

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

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("book_title")
                        .HasComment("Book title.");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<float>("Score")
                        .HasColumnType("real")
                        .HasColumnName("score")
                        .HasComment("Score. 0.0 - lowest possible score, 1.0 - highest possible score.");

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

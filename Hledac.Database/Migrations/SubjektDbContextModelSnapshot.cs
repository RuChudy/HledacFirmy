﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Hledac.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hledac.Database.Migrations
{
    [DbContext(typeof(SubjektDbContext))]
    partial class SubjektDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hledac.Database.Context.RssCacheFeed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Copyright")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiteUri")
                        .IsRequired()
                        .HasMaxLength(900)
                        .HasColumnType("nvarchar(900)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SiteUri")
                        .IsUnique();

                    b.ToTable("RssCacheFeeds");
                });

            modelBuilder.Entity("Hledac.Database.Context.RssCacheFeedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<int>("FeedId")
                        .HasColumnType("int");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.ComplexProperty<Dictionary<string, object>>("Categories", "Hledac.Database.Context.RssCacheFeedItem.Categories#RssCacheCategories", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Categories")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("FeedId");

                    b.ToTable("RssCacheFeedItems");
                });

            modelBuilder.Entity("Hledac.Database.Context.Subjekt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CisloDomovni")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CisloOrientacni")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumAktualizace")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumVzniku")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatumZaniku")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dic")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("DorucovaciAdresa1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DorucovaciAdresa2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DorucovaciAdresa3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ico")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Kraj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObchJmeno")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Obec")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Obvod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Okres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Psc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ulice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Ico");

                    b.ToTable("Subjekty");
                });

            modelBuilder.Entity("Hledac.Database.Context.RssCacheFeedItem", b =>
                {
                    b.HasOne("Hledac.Database.Context.RssCacheFeed", "Feed")
                        .WithMany("FeedItems")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feed");
                });

            modelBuilder.Entity("Hledac.Database.Context.RssCacheFeed", b =>
                {
                    b.Navigation("FeedItems");
                });
#pragma warning restore 612, 618
        }
    }
}

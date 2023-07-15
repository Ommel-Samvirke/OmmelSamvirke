﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OmmelSamvirke.Persistence.DatabaseContext;

#nullable disable

namespace OmmelSamvirke.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230715134458_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Admins.Models.Admin", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Communities.Models.Community", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Communities", (string)null);
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.HeadlineBlockData", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("ContentBlockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Headline")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentBlockId");

                    b.HasIndex("PageId");

                    b.ToTable("HeadlineBlockData", (string)null);
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.ImageBlockData", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("ContentBlockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentBlockId");

                    b.HasIndex("PageId");

                    b.ToTable("ImageBlockData", (string)null);
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.PdfBlockData", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("ContentBlockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentBlockId");

                    b.HasIndex("PageId");

                    b.ToTable("PdfBlockData", (string)null);
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.SlideshowBlockData", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("ContentBlockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrls")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentBlockId");

                    b.HasIndex("PageId");

                    b.ToTable("SlideshowBlockData", (string)null);
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.TextBlockData", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("ContentBlockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContentBlockId");

                    b.HasIndex("PageId");

                    b.ToTable("TextBlockData", (string)null);
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.VideoBlockData", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("ContentBlockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentBlockId");

                    b.HasIndex("PageId");

                    b.ToTable("VideoBlockData", (string)null);
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockLayoutConfiguration", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.Property<int>("XPosition")
                        .HasColumnType("int");

                    b.Property<int>("YPosition")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ContentBlockLayoutConfigurations", (string)null);
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.ContentBlock", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("DesktopConfigurationId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOptional")
                        .HasColumnType("bit");

                    b.Property<int>("MobileConfigurationId")
                        .HasColumnType("int");

                    b.Property<int>("PageTemplateId")
                        .HasColumnType("int");

                    b.Property<int>("TabletConfigurationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DesktopConfigurationId");

                    b.HasIndex("MobileConfigurationId");

                    b.HasIndex("PageTemplateId");

                    b.HasIndex("TabletConfigurationId");

                    b.ToTable("ContentBlocks", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("ContentBlock");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.Page", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int?>("CommunityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.HasIndex("TemplateId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.PageTemplate", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PageTemplates", (string)null);
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.HeadlineBlock", b =>
                {
                    b.HasBaseType("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.ContentBlock");

                    b.HasDiscriminator().HasValue("HeadlineBlock");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.ImageBlock", b =>
                {
                    b.HasBaseType("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.ContentBlock");

                    b.HasDiscriminator().HasValue("ImageBlock");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.PdfBlock", b =>
                {
                    b.HasBaseType("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.ContentBlock");

                    b.HasDiscriminator().HasValue("PdfBlock");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.SlideshowBlock", b =>
                {
                    b.HasBaseType("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.ContentBlock");

                    b.HasDiscriminator().HasValue("SlideshowBlock");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.TextBlock", b =>
                {
                    b.HasBaseType("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.ContentBlock");

                    b.HasDiscriminator().HasValue("TextBlock");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.VideoBlock", b =>
                {
                    b.HasBaseType("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.ContentBlock");

                    b.HasDiscriminator().HasValue("VideoBlock");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.HeadlineBlockData", b =>
                {
                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.HeadlineBlock", "ContentBlock")
                        .WithMany()
                        .HasForeignKey("ContentBlockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContentBlock");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.ImageBlockData", b =>
                {
                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.ImageBlock", "ContentBlock")
                        .WithMany()
                        .HasForeignKey("ContentBlockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("OmmelSamvirke.Domain.ValueObjects.Url", "ImageUrl", b1 =>
                        {
                            b1.Property<int>("ImageBlockDataId")
                                .HasColumnType("int");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(4000)
                                .HasColumnType("nvarchar(4000)");

                            b1.HasKey("ImageBlockDataId");

                            b1.ToTable("ImageBlockData");

                            b1.WithOwner()
                                .HasForeignKey("ImageBlockDataId");
                        });

                    b.Navigation("ContentBlock");

                    b.Navigation("ImageUrl")
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.PdfBlockData", b =>
                {
                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.PdfBlock", "ContentBlock")
                        .WithMany()
                        .HasForeignKey("ContentBlockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("OmmelSamvirke.Domain.ValueObjects.Url", "PdfUrl", b1 =>
                        {
                            b1.Property<int>("PdfBlockDataId")
                                .HasColumnType("int");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(4000)
                                .HasColumnType("nvarchar(4000)");

                            b1.HasKey("PdfBlockDataId");

                            b1.ToTable("PdfBlockData");

                            b1.WithOwner()
                                .HasForeignKey("PdfBlockDataId");
                        });

                    b.Navigation("ContentBlock");

                    b.Navigation("Page");

                    b.Navigation("PdfUrl")
                        .IsRequired();
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.SlideshowBlockData", b =>
                {
                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.SlideshowBlock", "ContentBlock")
                        .WithMany()
                        .HasForeignKey("ContentBlockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContentBlock");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.TextBlockData", b =>
                {
                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.TextBlock", "ContentBlock")
                        .WithMany()
                        .HasForeignKey("ContentBlockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContentBlock");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData.VideoBlockData", b =>
                {
                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.VideoBlock", "ContentBlock")
                        .WithMany()
                        .HasForeignKey("ContentBlockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("OmmelSamvirke.Domain.ValueObjects.Url", "VideoUrl", b1 =>
                        {
                            b1.Property<int>("VideoBlockDataId")
                                .HasColumnType("int");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(4000)
                                .HasColumnType("nvarchar(4000)");

                            b1.HasKey("VideoBlockDataId");

                            b1.ToTable("VideoBlockData");

                            b1.WithOwner()
                                .HasForeignKey("VideoBlockDataId");
                        });

                    b.Navigation("ContentBlock");

                    b.Navigation("Page");

                    b.Navigation("VideoUrl")
                        .IsRequired();
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks.ContentBlock", b =>
                {
                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockLayoutConfiguration", "DesktopConfiguration")
                        .WithMany()
                        .HasForeignKey("DesktopConfigurationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockLayoutConfiguration", "MobileConfiguration")
                        .WithMany()
                        .HasForeignKey("MobileConfigurationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.PageTemplate", "PageTemplate")
                        .WithMany("ContentBlocks")
                        .HasForeignKey("PageTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockLayoutConfiguration", "TabletConfiguration")
                        .WithMany()
                        .HasForeignKey("TabletConfigurationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DesktopConfiguration");

                    b.Navigation("MobileConfiguration");

                    b.Navigation("PageTemplate");

                    b.Navigation("TabletConfiguration");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.Page", b =>
                {
                    b.HasOne("OmmelSamvirke.Domain.Features.Communities.Models.Community", null)
                        .WithMany("Pages")
                        .HasForeignKey("CommunityId");

                    b.HasOne("OmmelSamvirke.Domain.Features.Pages.Models.PageTemplate", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Communities.Models.Community", b =>
                {
                    b.Navigation("Pages");
                });

            modelBuilder.Entity("OmmelSamvirke.Domain.Features.Pages.Models.PageTemplate", b =>
                {
                    b.Navigation("ContentBlocks");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmmelSamvirke.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LayoutConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XPosition = table.Column<int>(type: "int", nullable: false),
                    YPosition = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    LayoutConfigurationId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentBlocks_LayoutConfigurations_LayoutConfigurationId",
                        column: x => x.LayoutConfigurationId,
                        principalTable: "LayoutConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DesktopConfigurationId = table.Column<int>(type: "int", nullable: false),
                    TabletConfigurationId = table.Column<int>(type: "int", nullable: false),
                    MobileConfigurationId = table.Column<int>(type: "int", nullable: false),
                    CommunityId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pages_LayoutConfigurations_DesktopConfigurationId",
                        column: x => x.DesktopConfigurationId,
                        principalTable: "LayoutConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pages_LayoutConfigurations_MobileConfigurationId",
                        column: x => x.MobileConfigurationId,
                        principalTable: "LayoutConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pages_LayoutConfigurations_TabletConfigurationId",
                        column: x => x.TabletConfigurationId,
                        principalTable: "LayoutConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HeadlineBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Headline = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContentBlockType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadlineBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeadlineBlocks_ContentBlocks_Id",
                        column: x => x.Id,
                        principalTable: "ContentBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ContentBlockType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageBlocks_ContentBlocks_Id",
                        column: x => x.Id,
                        principalTable: "ContentBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PdfBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PdfUrl = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ContentBlockType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdfBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PdfBlocks_ContentBlocks_Id",
                        column: x => x.Id,
                        principalTable: "ContentBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlideshowBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentBlockType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideshowBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlideshowBlocks_ContentBlocks_Id",
                        column: x => x.Id,
                        principalTable: "ContentBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentBlockType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextBlocks_ContentBlocks_Id",
                        column: x => x.Id,
                        principalTable: "ContentBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ContentBlockType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoBlocks_ContentBlocks_Id",
                        column: x => x.Id,
                        principalTable: "ContentBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_LayoutConfigurationId",
                table: "ContentBlocks",
                column: "LayoutConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_CommunityId",
                table: "Pages",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_DesktopConfigurationId",
                table: "Pages",
                column: "DesktopConfigurationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_MobileConfigurationId",
                table: "Pages",
                column: "MobileConfigurationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_TabletConfigurationId",
                table: "Pages",
                column: "TabletConfigurationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "HeadlineBlocks");

            migrationBuilder.DropTable(
                name: "ImageBlocks");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "PdfBlocks");

            migrationBuilder.DropTable(
                name: "SlideshowBlocks");

            migrationBuilder.DropTable(
                name: "TextBlocks");

            migrationBuilder.DropTable(
                name: "VideoBlocks");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropTable(
                name: "ContentBlocks");

            migrationBuilder.DropTable(
                name: "LayoutConfigurations");
        }
    }
}

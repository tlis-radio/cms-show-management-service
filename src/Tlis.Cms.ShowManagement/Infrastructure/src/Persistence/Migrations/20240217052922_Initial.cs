using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tlis.Cms.ShowManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cms_show_management");

            migrationBuilder.CreateTable(
                name: "show",
                schema: "cms_show_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    moderator_ids = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    created_date = table.Column<DateOnly>(type: "date", nullable: false),
                    profile_image_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_show", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_show_name",
                schema: "cms_show_management",
                table: "show",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "show",
                schema: "cms_show_management");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tlis.Cms.ShowManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedProfilePicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profile_image_url",
                schema: "cms_show_management",
                table: "show",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_image_url",
                schema: "cms_show_management",
                table: "show");
        }
    }
}

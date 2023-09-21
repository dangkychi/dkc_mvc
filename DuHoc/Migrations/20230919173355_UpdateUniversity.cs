using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuHoc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUniversity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Decription",
                table: "University",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decription",
                table: "University");
        }
    }
}

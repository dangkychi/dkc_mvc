using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuHoc.Migrations
{
    /// <inheritdoc />
    public partial class CreateComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParentComment",
                columns: table => new
                {
                    ParentComment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    NewsPostNews_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentComment", x => x.ParentComment_Id);
                    table.ForeignKey(
                        name: "FK_ParentComment_NewsPost_NewsPostNews_Id",
                        column: x => x.NewsPostNews_Id,
                        principalTable: "NewsPost",
                        principalColumn: "News_Id");
                    table.ForeignKey(
                        name: "FK_ParentComment_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChildComment",
                columns: table => new
                {
                    Comment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    ParentComment_Id = table.Column<int>(type: "int", nullable: false),
                    ParentComment_Id1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildComment", x => x.Comment_Id);
                    table.ForeignKey(
                        name: "FK_ChildComment_ParentComment_ParentComment_Id1",
                        column: x => x.ParentComment_Id1,
                        principalTable: "ParentComment",
                        principalColumn: "ParentComment_Id");
                    table.ForeignKey(
                        name: "FK_ChildComment_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildComment_ParentComment_Id1",
                table: "ChildComment",
                column: "ParentComment_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_ChildComment_user_id",
                table: "ChildComment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ParentComment_NewsPostNews_Id",
                table: "ParentComment",
                column: "NewsPostNews_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ParentComment_user_id",
                table: "ParentComment",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildComment");

            migrationBuilder.DropTable(
                name: "ParentComment");
        }
    }
}

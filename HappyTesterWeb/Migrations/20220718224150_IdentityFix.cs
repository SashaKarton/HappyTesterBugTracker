using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyTesterWeb.Migrations
{
    public partial class IdentityFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AppUserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AppUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AppUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AppUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "AppUserProject",
                columns: table => new
                {
                    AppUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserProject", x => new { x.AppUsersId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_AppUserProject_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTicket",
                columns: table => new
                {
                    AppUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTicket", x => new { x.AppUsersId, x.TicketsId });
                    table.ForeignKey(
                        name: "FK_AppUserTicket_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserTicket_Tickets_TicketsId",
                        column: x => x.TicketsId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserProject_ProjectsId",
                table: "AppUserProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTicket_TicketsId",
                table: "AppUserTicket",
                column: "TicketsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserProject");

            migrationBuilder.DropTable(
                name: "AppUserTicket");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AppUserId",
                table: "Tickets",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AppUserId",
                table: "Projects",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AppUserId",
                table: "Projects",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AppUserId",
                table: "Tickets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

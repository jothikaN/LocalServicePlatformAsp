using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalServicePlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatetasker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {




            migrationBuilder.CreateTable(
            name: "TaskersUpdate",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Location = table.Column<int>(type: "int", nullable: false),
                ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PhoneNumber = table.Column<float>(type: "real", nullable: false),
                ServiceImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TaskerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TaskersUpdate", x => x.Id);
                table.ForeignKey(
                    name: "FK_TaskersUpdate_AspNetUsers_TaskerId",
                    column: x => x.TaskerId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_TaskersUpdate_Services_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Services",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

            migrationBuilder.CreateIndex(
                name: "IX_TaskersUpdate_ServiceId",
                table: "TaskersUpdate",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskersUpdate_TaskerId",
                table: "TaskersUpdate",
                column: "TaskerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "TaskersUpdate");

            migrationBuilder.DropColumn(name: "PhoneNumber", table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "Taskers",
                columns: table => new
                {
                    TaskId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SelectedServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskerImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taskers", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Taskers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Taskers_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(name: "IX_Taskers_Id", table: "Taskers", column: "Id");
            migrationBuilder.CreateIndex(name: "IX_Taskers_ServiceId", table: "Taskers", column: "ServiceId");
        }
    }
}
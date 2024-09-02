using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalServicePlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class taskerservices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskersService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskersUpdateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskersService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskersService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskersService_TaskersUpdate_TaskersUpdateId",
                        column: x => x.TaskersUpdateId,
                        principalTable: "TaskersUpdate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskersService_ServicesId",
                table: "TaskersService",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskersService_TaskersUpdateId",
                table: "TaskersService",
                column: "TaskersUpdateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskersService");
        }
    }
}

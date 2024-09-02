using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalServicePlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class taskeruppdateedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskersService",
                table: "TaskersService");

            migrationBuilder.DropIndex(
                name: "IX_TaskersService_TaskersUpdateId",
                table: "TaskersService");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskersService",
                table: "TaskersService",
                columns: new[] { "TaskersUpdateId", "ServiceId" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskersService_ServiceId",
                table: "TaskersService",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
     name: "FK_TaskersService_Services_ServiceId",
     table: "TaskersService",
     column: "ServiceId",
     principalTable: "Services",
     principalColumn: "Id",
     onDelete: ReferentialAction.NoAction); // Change to NoAction or SetNull

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskersService_Services_ServiceId",
                table: "TaskersService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskersService",
                table: "TaskersService");

            migrationBuilder.DropIndex(
                name: "IX_TaskersService_ServiceId",
                table: "TaskersService");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskersService",
                table: "TaskersService",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskersService_TaskersUpdateId",
                table: "TaskersService",
                column: "TaskersUpdateId");
        }
    }
}

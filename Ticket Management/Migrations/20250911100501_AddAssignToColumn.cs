using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignToColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignTo",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignTo",
                table: "Tickets");
        }
    }
}

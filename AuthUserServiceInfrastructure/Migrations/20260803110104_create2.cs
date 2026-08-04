using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthUserServiceInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalID",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "NationalID",
                table: "Clients");
        }
    }
}

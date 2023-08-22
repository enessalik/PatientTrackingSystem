using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientTrackingSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doctor_Name",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "Patient_Name",
                table: "Visits");

            migrationBuilder.AddColumn<string>(
                name: "Complaint",
                table: "Visits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Doctor",
                table: "Visits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Treatment",
                table: "Visits",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complaint",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "Doctor",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "Treatment",
                table: "Visits");

            migrationBuilder.AddColumn<string>(
                name: "Doctor_Name",
                table: "Visits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patient_Name",
                table: "Visits",
                type: "text",
                nullable: true);
        }
    }
}

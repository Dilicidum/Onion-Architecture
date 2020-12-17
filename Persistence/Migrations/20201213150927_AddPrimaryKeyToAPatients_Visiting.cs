using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddPrimaryKeyToAPatients_Visiting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient_Visitings",
                table: "Patient_Visitings");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "Patient_Visitings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient_Visitings",
                table: "Patient_Visitings",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Visitings_DoctorId",
                table: "Patient_Visitings",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient_Visitings",
                table: "Patient_Visitings");

            migrationBuilder.DropIndex(
                name: "IX_Patient_Visitings_DoctorId",
                table: "Patient_Visitings");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Patient_Visitings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient_Visitings",
                table: "Patient_Visitings",
                columns: new[] { "DoctorId", "PatientId" });
        }
    }
}

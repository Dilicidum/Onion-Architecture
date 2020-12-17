using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class changePKDoctor_Schedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Visitings_Patient_Visitings_Patient_VisitingDoctorId_Patient_VisitingPatientId",
                table: "Patient_Visitings");

            migrationBuilder.DropIndex(
                name: "IX_Patient_Visitings_Patient_VisitingDoctorId_Patient_VisitingPatientId",
                table: "Patient_Visitings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor_Schedules",
                table: "Doctor_Schedules");

            migrationBuilder.DropColumn(
                name: "Patient_VisitingDoctorId",
                table: "Patient_Visitings");

            migrationBuilder.DropColumn(
                name: "Patient_VisitingPatientId",
                table: "Patient_Visitings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Doctor_Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor_Schedules",
                table: "Doctor_Schedules",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Schedules_DoctorId",
                table: "Doctor_Schedules",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor_Schedules",
                table: "Doctor_Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_Schedules_DoctorId",
                table: "Doctor_Schedules");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Doctor_Schedules");

            migrationBuilder.AddColumn<int>(
                name: "Patient_VisitingDoctorId",
                table: "Patient_Visitings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Patient_VisitingPatientId",
                table: "Patient_Visitings",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor_Schedules",
                table: "Doctor_Schedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Visitings_Patient_VisitingDoctorId_Patient_VisitingPatientId",
                table: "Patient_Visitings",
                columns: new[] { "Patient_VisitingDoctorId", "Patient_VisitingPatientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Visitings_Patient_Visitings_Patient_VisitingDoctorId_Patient_VisitingPatientId",
                table: "Patient_Visitings",
                columns: new[] { "Patient_VisitingDoctorId", "Patient_VisitingPatientId" },
                principalTable: "Patient_Visitings",
                principalColumns: new[] { "DoctorId", "PatientId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}

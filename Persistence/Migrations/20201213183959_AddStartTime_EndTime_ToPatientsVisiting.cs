using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddStartTime_EndTime_ToPatientsVisiting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "End_TimeVisit",
                table: "Patient_Visitings",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Start_TimeVisit",
                table: "Patient_Visitings",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End_TimeVisit",
                table: "Patient_Visitings");

            migrationBuilder.DropColumn(
                name: "Start_TimeVisit",
                table: "Patient_Visitings");
        }
    }
}

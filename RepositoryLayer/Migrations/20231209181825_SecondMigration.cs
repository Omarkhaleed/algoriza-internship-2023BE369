using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookingAppointments_bookingDays_DayId",
                table: "bookingAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingRequests_bookingAppointments_AppointmentId",
                table: "bookingRequests");

            migrationBuilder.DropIndex(
                name: "IX_bookingAppointments_DayId",
                table: "bookingAppointments");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "bookingAppointments");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "bookingAppointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "bookingRequests",
                newName: "BookingAppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_bookingRequests_AppointmentId",
                table: "bookingRequests",
                newName: "IX_bookingRequests_BookingAppointmentId");

            migrationBuilder.RenameColumn(
                name: "DayName",
                table: "bookingDays",
                newName: "Day");

            migrationBuilder.AddColumn<int>(
                name: "BookingAppointmentId",
                table: "bookingDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingDayId = table.Column<int>(type: "int", nullable: false),
                    BookingDaysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_bookingDays_BookingDaysId",
                        column: x => x.BookingDaysId,
                        principalTable: "bookingDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookingDays_BookingAppointmentId",
                table: "bookingDays",
                column: "BookingAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_BookingDaysId",
                table: "TimeSlots",
                column: "BookingDaysId");

            migrationBuilder.AddForeignKey(
                name: "FK_bookingDays_bookingAppointments_BookingAppointmentId",
                table: "bookingDays",
                column: "BookingAppointmentId",
                principalTable: "bookingAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookingRequests_bookingAppointments_BookingAppointmentId",
                table: "bookingRequests",
                column: "BookingAppointmentId",
                principalTable: "bookingAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookingDays_bookingAppointments_BookingAppointmentId",
                table: "bookingDays");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingRequests_bookingAppointments_BookingAppointmentId",
                table: "bookingRequests");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_bookingDays_BookingAppointmentId",
                table: "bookingDays");

            migrationBuilder.DropColumn(
                name: "BookingAppointmentId",
                table: "bookingDays");

            migrationBuilder.RenameColumn(
                name: "BookingAppointmentId",
                table: "bookingRequests",
                newName: "AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_bookingRequests_BookingAppointmentId",
                table: "bookingRequests",
                newName: "IX_bookingRequests_AppointmentId");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "bookingDays",
                newName: "DayName");

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "bookingAppointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "bookingAppointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_bookingAppointments_DayId",
                table: "bookingAppointments",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_bookingAppointments_bookingDays_DayId",
                table: "bookingAppointments",
                column: "DayId",
                principalTable: "bookingDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookingRequests_bookingAppointments_AppointmentId",
                table: "bookingRequests",
                column: "AppointmentId",
                principalTable: "bookingAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

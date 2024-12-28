using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KuaforYonetim.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkingHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SelectedTimeSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvailableSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableSlots_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "Name", "Phone", "Position" },
                values: new object[,]
                {
                    { 1, "mehmet@example.com", "Mehmet Topal(Serdivan)", "5369874521", "Saç Kesimi" },
                    { 2, "mustafa@example.com", "Mustafa Aslan(Serdivan)", "5427895686", "Saç Kesimi" },
                    { 3, "mustafa@example.com", "Cüneyt Akif(Arifiye)", "5239687542", "Saç Kesimi" },
                    { 4, "mustafa@example.com", "Samet Akpınar(Arifiye)", "5648569575", "Saç Kesimi" }
                });

            migrationBuilder.InsertData(
                table: "Salons",
                columns: new[] { "Id", "Address", "Image", "Name", "Phone", "WorkingHours" },
                values: new object[,]
                {
                    { 1, "Serdivan, Sincan Mahallesi", "serdivan4.jpg", "Serdivan Salon", "0312-123-4567", "09:00 - 21:00" },
                    { 2, "Arifiye, Arifbey Mahallesi", "arifiye.jpg", "Arifiye Salon", "0216-987-6543", "10:00 - 20:00" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "CustomerName", "Date", "EmployeeId", "Phone", "SelectedTimeSlot", "Service", "Status" },
                values: new object[,]
                {
                    { 1, "Mehmet", new DateTime(2024, 12, 25, 20, 40, 59, 727, DateTimeKind.Local).AddTicks(5389), 1, "5551234567", "09:00 - 10:00", "Saç Kesimi", "Confirmed" },
                    { 2, "Zeynep", new DateTime(2024, 12, 26, 20, 40, 59, 729, DateTimeKind.Local).AddTicks(305), 2, "5559876543", "10:00 - 11:00", "Makyaj", "Pending" }
                });

            migrationBuilder.InsertData(
                table: "AvailableSlots",
                columns: new[] { "Id", "Day", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, 1, new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) },
                    { 2, 1, 1, new TimeSpan(0, 11, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) },
                    { 6, 1, 2, new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) },
                    { 9, 1, 3, new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) },
                    { 12, 1, 4, new TimeSpan(0, 11, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId",
                table: "Appointments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailableSlots_EmployeeId",
                table: "AvailableSlots",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AvailableSlots");

            migrationBuilder.DropTable(
                name: "Salons");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}

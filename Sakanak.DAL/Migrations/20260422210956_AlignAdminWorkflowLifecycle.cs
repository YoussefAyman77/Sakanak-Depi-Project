using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sakanak.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AlignAdminWorkflowLifecycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE Bookings
                SET Status = 'Accepted'
                WHERE Status = 'Confirmed';
                """);

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Apartments_ApartmentId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Contracts_ContractId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Landlords_LandlordId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Students_StudentId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ApartmentId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ContractId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_StudentId_Status",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_Type_Status",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_BookingId",
                table: "Contracts");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Contracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE c
                SET c.StudentId = COALESCE(cs.StudentsUserId, b.StudentId),
                    c.SubmittedAt = COALESCE(b.BookingDate, c.StartDate)
                FROM Contracts c
                LEFT JOIN ContractStudents cs ON cs.ContractsContractId = c.ContractId
                LEFT JOIN Bookings b ON b.BookingId = c.BookingId;
                """);

            migrationBuilder.Sql("""
                UPDATE c
                SET c.ReviewedAt = CASE
                    WHEN c.VerifiedByAdminId IS NOT NULL OR c.Status IN ('Approved', 'Active', 'Rejected', 'Expired', 'Terminated')
                        THEN c.StartDate
                    ELSE NULL
                END
                FROM Contracts c;
                """);

            migrationBuilder.Sql("""
                IF EXISTS (SELECT 1 FROM Contracts WHERE StudentId IS NULL OR SubmittedAt IS NULL)
                    THROW 50000, 'Unable to backfill contract student or submission date for all existing rows.', 1;
                """);

            migrationBuilder.DropTable(
                name: "ContractStudents");

            migrationBuilder.Sql("""
                DELETE FROM Requests
                WHERE ContractId IS NOT NULL
                   OR Type = 'Contract'
                   OR LandlordId IS NULL
                   OR ApartmentId IS NULL;
                """);

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Requests",
                newName: "ReviewedByAdminId");

            migrationBuilder.Sql("""
                UPDATE Requests
                SET ReviewedByAdminId = NULL;
                """);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Requests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "LandlordId",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentId",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.Sql("""
                UPDATE p
                SET p.ContractId = c.ContractId
                FROM Payments p
                CROSS APPLY (
                    SELECT TOP 1 c.ContractId
                    FROM Contracts c
                    WHERE c.StudentId = p.StudentId
                      AND c.LandlordId = p.LandlordId
                      AND c.ApartmentId = p.ApartmentId
                    ORDER BY
                        CASE WHEN c.Status = 'Active' THEN 0
                             WHEN c.Status = 'Approved' THEN 1
                             ELSE 2 END,
                        c.StartDate DESC
                ) c
                WHERE p.ContractId IS NULL;
                """);

            migrationBuilder.Sql("""
                IF EXISTS (SELECT 1 FROM Payments WHERE ContractId IS NULL)
                    THROW 50001, 'Unable to backfill contract id for all existing payments.', 1;
                """);

            migrationBuilder.AlterColumn<int>(
                name: "ContractId",
                table: "Payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Contracts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedAt",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApartmentId_Status",
                table: "Requests",
                columns: new[] { "ApartmentId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ReviewedByAdminId",
                table: "Requests",
                column: "ReviewedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BookingId",
                table: "Contracts",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_StudentId_Status",
                table: "Contracts",
                columns: new[] { "StudentId", "Status" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Students_StudentId",
                table: "Contracts",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Admins_ReviewedByAdminId",
                table: "Requests",
                column: "ReviewedByAdminId",
                principalTable: "Admins",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Apartments_ApartmentId",
                table: "Requests",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "ApartmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Landlords_LandlordId",
                table: "Requests",
                column: "LandlordId",
                principalTable: "Landlords",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE Bookings
                SET Status = 'Confirmed'
                WHERE Status = 'Accepted';
                """);

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Students_StudentId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Admins_ReviewedByAdminId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Apartments_ApartmentId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Landlords_LandlordId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ApartmentId_Status",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ReviewedByAdminId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_BookingId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_StudentId_Status",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "Contracts");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Contracts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedAt",
                table: "Contracts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "ReviewedByAdminId",
                table: "Requests",
                newName: "StudentId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Requests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Pending");

            migrationBuilder.AlterColumn<int>(
                name: "LandlordId",
                table: "Requests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentId",
                table: "Requests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Requests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ContractId",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ContractStudents",
                columns: table => new
                {
                    ContractsContractId = table.Column<int>(type: "int", nullable: false),
                    StudentsUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStudents", x => new { x.ContractsContractId, x.StudentsUserId });
                    table.ForeignKey(
                        name: "FK_ContractStudents_Contracts_ContractsContractId",
                        column: x => x.ContractsContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractStudents_Students_StudentsUserId",
                        column: x => x.StudentsUserId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApartmentId",
                table: "Requests",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ContractId",
                table: "Requests",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_StudentId_Status",
                table: "Requests",
                columns: new[] { "StudentId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_Type_Status",
                table: "Requests",
                columns: new[] { "Type", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BookingId",
                table: "Contracts",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractStudents_StudentsUserId",
                table: "ContractStudents",
                column: "StudentsUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Apartments_ApartmentId",
                table: "Requests",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "ApartmentId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Contracts_ContractId",
                table: "Requests",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Landlords_LandlordId",
                table: "Requests",
                column: "LandlordId",
                principalTable: "Landlords",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Students_StudentId",
                table: "Requests",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

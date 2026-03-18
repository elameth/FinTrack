using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTrackPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecurringBudgets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodEndDate",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "SpentAmount",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "PeriodStartDate",
                table: "Budgets",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "SpentCurrency",
                table: "Budgets",
                newName: "Period");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Budgets",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Budgets",
                newName: "PeriodStartDate");

            migrationBuilder.RenameColumn(
                name: "Period",
                table: "Budgets",
                newName: "SpentCurrency");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodStartDate",
                table: "Budgets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodEndDate",
                table: "Budgets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "SpentAmount",
                table: "Budgets",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}

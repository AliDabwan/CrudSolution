using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddnewProp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PN",
                table: "Persons",
                newName: "PassportNumber");

            migrationBuilder.AlterColumn<string>(
                name: "PassportNumber",
                table: "Persons",
                type: "nvarchar(9)",
                nullable: false,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("06d15bad-52f4-498e-b478-acad847abfaa"),
                column: "PassportNumber",
                value: "None");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("3c12d8e8-3c1c-4f57-b6a4-c8caac893d7a"),
                column: "PassportNumber",
                value: "None");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("6717c42d-16ec-4f15-80d8-4c7413e250cb"),
                column: "PassportNumber",
                value: "None");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("6e789c86-c8a6-4f18-821c-2abdb2e95982"),
                column: "PassportNumber",
                value: "None");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("7b75097b-bff2-459f-8ea8-63742bbd7afb"),
                column: "PassportNumber",
                value: "None");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("8082ed0c-396d-4162-ad1d-29a13f929824"),
                column: "PassportNumber",
                value: "None");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("89452edb-bf8c-4283-9ba4-8259fd4a7a76"),
                column: "PassportNumber",
                value: "None");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("a795e22d-faed-42f0-b134-f3b89b8683e5"),
                column: "PassportNumber",
                value: "None");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("d3ea677a-0f5b-41ea-8fef-ea2fc41900fd"),
                column: "PassportNumber",
                value: "None");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("f5bd5979-1dc1-432c-b1f1-db5bccb0e56d"),
                column: "PassportNumber",
                value: "None");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassportNumber",
                table: "Persons",
                newName: "PN");

            migrationBuilder.AlterColumn<string>(
                name: "PN",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchr(9)",
                oldDefaultValue: "None");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("06d15bad-52f4-498e-b478-acad847abfaa"),
                column: "PN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("3c12d8e8-3c1c-4f57-b6a4-c8caac893d7a"),
                column: "PN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("6717c42d-16ec-4f15-80d8-4c7413e250cb"),
                column: "PN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("6e789c86-c8a6-4f18-821c-2abdb2e95982"),
                column: "PN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("7b75097b-bff2-459f-8ea8-63742bbd7afb"),
                column: "PN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("8082ed0c-396d-4162-ad1d-29a13f929824"),
                column: "PN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("89452edb-bf8c-4283-9ba4-8259fd4a7a76"),
                column: "PN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("a795e22d-faed-42f0-b134-f3b89b8683e5"),
                column: "PN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("d3ea677a-0f5b-41ea-8fef-ea2fc41900fd"),
                column: "PN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("f5bd5979-1dc1-432c-b1f1-db5bccb0e56d"),
                column: "PN",
                value: null);
        }
    }
}

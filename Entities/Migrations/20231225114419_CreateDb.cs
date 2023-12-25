﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiveEmails = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("000c76eb-62e9-4465-96d1-2c41fdb64c3b"), "Yemen" },
                    { new Guid("15889048-af93-412c-b8f3-22103e943a6d"), "Syria" },
                    { new Guid("32da506b-3eba-48a4-bd86-5f93a2e19e3f"), "Palestine" },
                    { new Guid("80df255c-efe7-49e5-a7f9-c35d7c701cab"), "Libya" },
                    { new Guid("df7c89ce-3341-4246-84ae-e01ab7ba476e"), "Iraq" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "CountryId", "DateOfBirth", "Email", "Gender", "Name", "ReceiveEmails" },
                values: new object[,]
                {
                    { new Guid("06d15bad-52f4-498e-b478-acad847abfaa"), new Guid("32da506b-3eba-48a4-bd86-5f93a2e19e3f"), new DateTime(1991, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "amany@email.com", "Female", "Amany Muhammad", true },
                    { new Guid("3c12d8e8-3c1c-4f57-b6a4-c8caac893d7a"), new Guid("80df255c-efe7-49e5-a7f9-c35d7c701cab"), new DateTime(1996, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "amir@email.com", "Male", "Amir Muhammad", true },
                    { new Guid("6717c42d-16ec-4f15-80d8-4c7413e250cb"), new Guid("80df255c-efe7-49e5-a7f9-c35d7c701cab"), new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "saad@email.com", "Male", "Saad Muhammad", false },
                    { new Guid("6e789c86-c8a6-4f18-821c-2abdb2e95982"), new Guid("000c76eb-62e9-4465-96d1-2c41fdb64c3b"), new DateTime(1996, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "fareed@email.com", "Male", "Fareed Said", false },
                    { new Guid("7b75097b-bff2-459f-8ea8-63742bbd7afb"), new Guid("000c76eb-62e9-4465-96d1-2c41fdb64c3b"), new DateTime(1982, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "basel@email.com", "Male", "Basel Mahmoud", false },
                    { new Guid("8082ed0c-396d-4162-ad1d-29a13f929824"), new Guid("000c76eb-62e9-4465-96d1-2c41fdb64c3b"), new DateTime(1981, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "mo@email.com", "Male", "Muhammad Awadallah", true },
                    { new Guid("89452edb-bf8c-4283-9ba4-8259fd4a7a76"), new Guid("df7c89ce-3341-4246-84ae-e01ab7ba476e"), new DateTime(1985, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "galal@email.com", "Male", "Galal Ali", true },
                    { new Guid("a795e22d-faed-42f0-b134-f3b89b8683e5"), new Guid("15889048-af93-412c-b8f3-22103e943a6d"), new DateTime(1993, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "batool@email.com", "Female", "Batool Ali", false },
                    { new Guid("d3ea677a-0f5b-41ea-8fef-ea2fc41900fd"), new Guid("32da506b-3eba-48a4-bd86-5f93a2e19e3f"), new DateTime(1993, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "kha@email.com", "Male", "Khaled Jaber", false },
                    { new Guid("f5bd5979-1dc1-432c-b1f1-db5bccb0e56d"), new Guid("df7c89ce-3341-4246-84ae-e01ab7ba476e"), new DateTime(1996, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "fatima@email.com", "Female", "Fatima Ahmad", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CountryId",
                table: "Persons",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}

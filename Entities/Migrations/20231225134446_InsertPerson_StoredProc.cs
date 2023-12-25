using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InsertPerson_StoredProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_InsertPerson = @"
                CREATE PROCEDURE InsertPerson
                (@Id uniqueidentifier,
                @Name nvarchar(40),
                @Email nvarchar(40),
                @DateOfBirth datetime2(7), 
                @Gender varchar(6),
                @CountryId uniqueidentifier,
                @ReceiveEmails bit
                )
                AS BEGIN
                  INSERT INTO Persons
VALUES (@Id, @Name, @Email, @DateOfBirth, @Gender, @CountryId, @ReceiveEmails)
                END
              ";
            migrationBuilder.Sql(sp_InsertPerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_InsertPerson = @"
                DROP PROCEDURE InsertPerson
                
              ";
            migrationBuilder.Sql(sp_InsertPerson);

        }
    }
}

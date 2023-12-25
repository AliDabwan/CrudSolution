using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetPersons_StoredProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllPersons = @"
            CREATE PROCEDURE GetAllPersons
            AS BEGIN
            SELECT * FROM PERSONS 
            END
            ";
            migrationBuilder.Sql(sp_GetAllPersons);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllPersons = @"
            DROP PROCEDURE GetAllPersons";
            migrationBuilder.Sql(sp_GetAllPersons);
        }

    
    }
}

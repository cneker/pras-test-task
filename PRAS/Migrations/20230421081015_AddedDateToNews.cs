using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRAS.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateToNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_News_PublicationDate",
                table: "News",
                column: "PublicationDate",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_News_PublicationDate",
                table: "News");

            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "News");
        }
    }
}

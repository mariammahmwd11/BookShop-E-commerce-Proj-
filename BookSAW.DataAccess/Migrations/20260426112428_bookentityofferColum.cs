using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSAW.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class bookentityofferColum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOffered",
                table: "Books");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                table: "Books",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsOffered",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

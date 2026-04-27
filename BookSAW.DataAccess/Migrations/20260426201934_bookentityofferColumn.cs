using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSAW.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class bookentityofferColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "Books");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercent",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Books");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                table: "Books",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}

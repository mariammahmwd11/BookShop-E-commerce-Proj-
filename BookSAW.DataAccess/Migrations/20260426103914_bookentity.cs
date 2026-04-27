using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSAW.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class bookentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAvailable",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "isOffered",
                table: "Books",
                newName: "IsOffered");

            migrationBuilder.RenameColumn(
                name: "isFeatured",
                table: "Books",
                newName: "IsFeatured");

            migrationBuilder.RenameColumn(
                name: "ImageURl",
                table: "Books",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "NumberOfAvaBooks",
                table: "Books",
                newName: "Stock");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOffered",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFeatured",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOffered",
                table: "Books",
                newName: "isOffered");

            migrationBuilder.RenameColumn(
                name: "IsFeatured",
                table: "Books",
                newName: "isFeatured");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Books",
                newName: "ImageURl");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Books",
                newName: "NumberOfAvaBooks");

            migrationBuilder.AlterColumn<bool>(
                name: "isOffered",
                table: "Books",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "isFeatured",
                table: "Books",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "isAvailable",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

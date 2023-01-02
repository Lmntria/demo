using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuarterApp.Migrations
{
    public partial class Address2SetasNullableinOrderAndDiscountPercentageNameChangedInOrderitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountPercent",
                table: "OrderItems",
                newName: "DiscountPercantage");

            migrationBuilder.AlterColumn<string>(
                name: "Address2",
                table: "Orders",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountPercantage",
                table: "OrderItems",
                newName: "DiscountPercent");

            migrationBuilder.AlterColumn<string>(
                name: "Address2",
                table: "Orders",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}

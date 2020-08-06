using Microsoft.EntityFrameworkCore.Migrations;

namespace AhmedTrading.Data.Migrations
{
    public partial class AddCustomerOpeningDue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OpeningDue",
                table: "Customer",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AlterColumn<double>(
                name: "Due",
                table: "Customer",
                nullable: false,
                computedColumnSql: "(([OpeningDue]+[TotalAmount]+[ReturnAmount])-([TotalDiscount]+[Paid]))",
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "(([TotalAmount]+[ReturnAmount])-([TotalDiscount]+[Paid]))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningDue",
                table: "Customer");

            migrationBuilder.AlterColumn<double>(
                name: "Due",
                table: "Customer",
                type: "float",
                nullable: false,
                computedColumnSql: "(([TotalAmount]+[ReturnAmount])-([TotalDiscount]+[Paid]))",
                oldClrType: typeof(double),
                oldComputedColumnSql: "(([OpeningDue]+[TotalAmount]+[ReturnAmount])-([TotalDiscount]+[Paid]))");
        }
    }
}

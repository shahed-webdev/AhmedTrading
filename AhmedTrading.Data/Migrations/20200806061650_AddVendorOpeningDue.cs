using Microsoft.EntityFrameworkCore.Migrations;

namespace AhmedTrading.Data.Migrations
{
    public partial class AddVendorOpeningDue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OpeningDue",
                table: "Vendor",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "Vendor",
                nullable: false,
                computedColumnSql: "(((([Paid]+[Advance])+[Commission])+[TotalDiscount])-([TotalAmount]+[ReturnAmount]+[OpeningDue]))",
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "(((([Paid]+[Advance])+[Commission])+[TotalDiscount])-([TotalAmount]+[ReturnAmount]))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningDue",
                table: "Vendor");

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "Vendor",
                type: "float",
                nullable: false,
                computedColumnSql: "(((([Paid]+[Advance])+[Commission])+[TotalDiscount])-([TotalAmount]+[ReturnAmount]))",
                oldClrType: typeof(double),
                oldComputedColumnSql: "(((([Paid]+[Advance])+[Commission])+[TotalDiscount])-([TotalAmount]+[ReturnAmount]+[OpeningDue]))");
        }
    }
}

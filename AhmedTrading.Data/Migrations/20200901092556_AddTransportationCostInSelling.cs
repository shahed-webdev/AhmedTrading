using Microsoft.EntityFrameworkCore.Migrations;

namespace AhmedTrading.Data.Migrations
{
    public partial class AddTransportationCostInSelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TransportationCost",
                table: "Selling",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "SellingPaymentStatus",
                table: "Selling",
                unicode: false,
                maxLength: 4,
                nullable: false,
                computedColumnSql: "(case when (([SellingTotalPrice]+[SellingReturnAmount]+[TransportationCost])-([SellingDiscountAmount]+[SellingPaidAmount]))<=(0) then 'Paid' else 'Due' end)",
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldUnicode: false,
                oldMaxLength: 4,
                oldComputedColumnSql: "(case when (([SellingTotalPrice]+[SellingReturnAmount])-([SellingDiscountAmount]+[SellingPaidAmount]))<=(0) then 'Paid' else 'Due' end)");

            migrationBuilder.AlterColumn<double>(
                name: "SellingDueAmount",
                table: "Selling",
                nullable: false,
                computedColumnSql: "(round(([SellingTotalPrice]+[SellingReturnAmount]+[TransportationCost])-([SellingDiscountAmount]+[SellingPaidAmount]),(2)))",
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "(round(([SellingTotalPrice]+[SellingReturnAmount])-([SellingDiscountAmount]+[SellingPaidAmount]),(2)))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransportationCost",
                table: "Selling");

            migrationBuilder.AlterColumn<string>(
                name: "SellingPaymentStatus",
                table: "Selling",
                type: "varchar(4)",
                unicode: false,
                maxLength: 4,
                nullable: false,
                computedColumnSql: "(case when (([SellingTotalPrice]+[SellingReturnAmount])-([SellingDiscountAmount]+[SellingPaidAmount]))<=(0) then 'Paid' else 'Due' end)",
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 4,
                oldComputedColumnSql: "(case when (([SellingTotalPrice]+[SellingReturnAmount]+[TransportationCost])-([SellingDiscountAmount]+[SellingPaidAmount]))<=(0) then 'Paid' else 'Due' end)");

            migrationBuilder.AlterColumn<double>(
                name: "SellingDueAmount",
                table: "Selling",
                type: "float",
                nullable: false,
                computedColumnSql: "(round(([SellingTotalPrice]+[SellingReturnAmount])-([SellingDiscountAmount]+[SellingPaidAmount]),(2)))",
                oldClrType: typeof(double),
                oldComputedColumnSql: "(round(([SellingTotalPrice]+[SellingReturnAmount]+[TransportationCost])-([SellingDiscountAmount]+[SellingPaidAmount]),(2)))");
        }
    }
}

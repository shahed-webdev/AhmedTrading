using Microsoft.EntityFrameworkCore.Migrations;

namespace AhmedTrading.Data.Migrations
{
    public partial class AddBankNameAndAccountNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseList_Purchase",
                table: "PurchaseList");

            migrationBuilder.DropForeignKey(
                name: "FK_SellingList_Selling",
                table: "SellingList");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "BankAccount",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "BankAccount",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseList_Purchase",
                table: "PurchaseList",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellingList_Selling",
                table: "SellingList",
                column: "SellingId",
                principalTable: "Selling",
                principalColumn: "SellingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseList_Purchase",
                table: "PurchaseList");

            migrationBuilder.DropForeignKey(
                name: "FK_SellingList_Selling",
                table: "SellingList");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "BankAccount");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseList_Purchase",
                table: "PurchaseList",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellingList_Selling",
                table: "SellingList",
                column: "SellingId",
                principalTable: "Selling",
                principalColumn: "SellingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

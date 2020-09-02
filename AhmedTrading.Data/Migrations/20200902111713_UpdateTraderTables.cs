using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AhmedTrading.Data.Migrations
{
    public partial class UpdateTraderTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ShareDate",
                table: "TraderSharing",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "GivenAmount",
                table: "Trader",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GivenProductPrice",
                table: "Trader",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TakenAmount",
                table: "Trader",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TakenProductPrice",
                table: "Trader",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetAmount",
                table: "Trader",
                nullable: false,
                computedColumnSql: "(([TakenAmount]+[TakenProductPrice])-([GivenAmount]+[GivenProductPrice]))");

            migrationBuilder.CreateTable(
                name: "TraderSharingPayment",
                columns: table => new
                {
                    TraderSharingPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsGiven = table.Column<bool>(nullable: false),
                    TraderId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    PaymentMethod = table.Column<string>(maxLength: 50, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "date", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraderSharingPayment", x => x.TraderSharingPaymentId);
                    table.ForeignKey(
                        name: "FK_TraderSharingPayment_Trader",
                        column: x => x.TraderId,
                        principalTable: "Trader",
                        principalColumn: "TraderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TraderSharingPayment_TraderId",
                table: "TraderSharingPayment",
                column: "TraderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraderSharingPayment");

            migrationBuilder.DropColumn(
                name: "ShareDate",
                table: "TraderSharing");

            migrationBuilder.DropColumn(
                name: "GivenAmount",
                table: "Trader");

            migrationBuilder.DropColumn(
                name: "GivenProductPrice",
                table: "Trader");

            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "Trader");

            migrationBuilder.DropColumn(
                name: "TakenAmount",
                table: "Trader");

            migrationBuilder.DropColumn(
                name: "TakenProductPrice",
                table: "Trader");
        }
    }
}

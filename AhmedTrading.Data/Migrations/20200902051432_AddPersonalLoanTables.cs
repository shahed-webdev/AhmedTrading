using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AhmedTrading.Data.Migrations
{
    public partial class AddPersonalLoanTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "PersonalLoan",
                columns: table => new
                {
                    PersonalLoanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    RegistrationId = table.Column<int>(nullable: false),
                    LoanName = table.Column<string>(maxLength: 255, nullable: false),
                    LoanAmount = table.Column<double>(nullable: false),
                    ReturnAmount = table.Column<double>(nullable: false),
                    RemainingAmount = table.Column<double>(nullable: false, computedColumnSql: "([LoanAmount]-[ReturnAmount])"),
                    LoanDate = table.Column<DateTime>(type: "date", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalLoan", x => x.PersonalLoanId);
                    table.ForeignKey(
                        name: "FK_PersonalLoan_Person",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalLoan_Registration",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalLoanReturn",
                columns: table => new
                {
                    PersonalLoanReturnId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalLoanId = table.Column<int>(nullable: false),
                    ReturnAmount = table.Column<double>(nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "date", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalLoanReturn", x => x.PersonalLoanReturnId);
                    table.ForeignKey(
                        name: "FK_PersonalLoanReturn_PersonalLoan",
                        column: x => x.PersonalLoanId,
                        principalTable: "PersonalLoan",
                        principalColumn: "PersonalLoanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalLoan_PersonId",
                table: "PersonalLoan",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalLoan_RegistrationId",
                table: "PersonalLoan",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalLoanReturn_PersonalLoanId",
                table: "PersonalLoanReturn",
                column: "PersonalLoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalLoanReturn");

            migrationBuilder.DropTable(
                name: "PersonalLoan");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}

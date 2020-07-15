using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AhmedTrading.Data.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advance",
                columns: table => new
                {
                    AdvanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvanceName = table.Column<string>(maxLength: 128, nullable: false),
                    AdvanceFor = table.Column<string>(maxLength: 500, nullable: false),
                    AdvanceAmount = table.Column<double>(nullable: false),
                    TimePeriod = table.Column<string>(maxLength: 50, nullable: true),
                    AdvanceDate = table.Column<DateTime>(type: "date", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advance", x => x.AdvanceId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    BankAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(maxLength: 128, nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.BankAccountId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(maxLength: 128, nullable: false),
                    CustomerAddress = table.Column<string>(maxLength: 500, nullable: true),
                    TotalAmount = table.Column<double>(nullable: false),
                    TotalDiscount = table.Column<double>(nullable: false),
                    ReturnAmount = table.Column<double>(nullable: false),
                    Paid = table.Column<double>(nullable: false),
                    Due = table.Column<double>(nullable: false, computedColumnSql: "(([TotalAmount]+[ReturnAmount])-([TotalDiscount]+[Paid]))"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategory",
                columns: table => new
                {
                    ExpenseCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(maxLength: 128, nullable: false),
                    TotalExpense = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategory", x => x.ExpenseCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Institution",
                columns: table => new
                {
                    InstitutionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionName = table.Column<string>(maxLength: 500, nullable: false),
                    DialogTitle = table.Column<string>(maxLength: 256, nullable: true),
                    Established = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    City = table.Column<string>(maxLength: 128, nullable: true),
                    State = table.Column<string>(maxLength: 128, nullable: true),
                    LocalArea = table.Column<string>(maxLength: 128, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Website = table.Column<string>(maxLength: 50, nullable: true),
                    InstitutionLogo = table.Column<byte[]>(nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.InstitutionId);
                });

            migrationBuilder.CreateTable(
                name: "PageLinkCategory",
                columns: table => new
                {
                    LinkCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(maxLength: 128, nullable: false),
                    IconClass = table.Column<string>(maxLength: 128, nullable: true),
                    SN = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageLinkCategory", x => x.LinkCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ProductBrand",
                columns: table => new
                {
                    ProductBrandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(maxLength: 128, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrand", x => x.ProductBrandId);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Validation = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    FatherName = table.Column<string>(maxLength: 128, nullable: true),
                    Designation = table.Column<string>(maxLength: 128, nullable: true),
                    DateofBirth = table.Column<string>(maxLength: 50, nullable: true),
                    NationalID = table.Column<string>(maxLength: 128, nullable: true),
                    Address = table.Column<string>(maxLength: 1000, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    PS = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.RegistrationId);
                });

            migrationBuilder.CreateTable(
                name: "Trader",
                columns: table => new
                {
                    TraderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraderName = table.Column<string>(maxLength: 128, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trader", x => x.TraderId);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorCompanyName = table.Column<string>(maxLength: 128, nullable: false),
                    VendorName = table.Column<string>(maxLength: 128, nullable: true),
                    VendorAddress = table.Column<string>(maxLength: 500, nullable: true),
                    VendorPhone = table.Column<string>(maxLength: 50, nullable: true),
                    TotalAmount = table.Column<double>(nullable: false),
                    TotalDiscount = table.Column<double>(nullable: false),
                    ReturnAmount = table.Column<double>(nullable: false),
                    Paid = table.Column<double>(nullable: false),
                    Advance = table.Column<double>(nullable: false),
                    Commission = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false, computedColumnSql: "(((([Paid]+[Advance])+[Commission])+[TotalDiscount])-([TotalAmount]+[ReturnAmount]))"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankDeposit",
                columns: table => new
                {
                    BankDepositId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Details = table.Column<string>(maxLength: 1000, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ActivityDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDeposit", x => x.BankDepositId);
                    table.ForeignKey(
                        name: "FK_BankDeposit_BankAccount",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "BankAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankLoan",
                columns: table => new
                {
                    BankLoanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountId = table.Column<int>(nullable: false),
                    LoanName = table.Column<string>(maxLength: 128, nullable: false),
                    LoanAmount = table.Column<double>(nullable: false),
                    LoanDate = table.Column<DateTime>(type: "date", nullable: false),
                    ReturnAmount = table.Column<double>(nullable: false),
                    RemainingAmount = table.Column<double>(nullable: false, computedColumnSql: "([LoanAmount]-[ReturnAmount])"),
                    InterestPercentage = table.Column<double>(nullable: false),
                    ReturnPeriod = table.Column<string>(maxLength: 128, nullable: true),
                    LoanDetails = table.Column<string>(maxLength: 1000, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankLoan", x => x.BankLoanId);
                    table.ForeignKey(
                        name: "FK_BankLoan_BankAccount",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "BankAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankWithdrew",
                columns: table => new
                {
                    BankWithdrewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Details = table.Column<string>(maxLength: 1000, nullable: true),
                    ActivityDate = table.Column<DateTime>(type: "date", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankWithdrew", x => x.BankWithdrewId);
                    table.ForeignKey(
                        name: "FK_BankWithdrew_BankAccount",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "BankAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPhone",
                columns: table => new
                {
                    CustomerPhoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    IsPrimary = table.Column<bool>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPhone", x => x.CustomerPhoneId);
                    table.ForeignKey(
                        name: "FK_CustomerPhone_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageLink",
                columns: table => new
                {
                    LinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkCategoryId = table.Column<int>(nullable: false),
                    RoleId = table.Column<string>(maxLength: 128, nullable: true),
                    Controller = table.Column<string>(maxLength: 128, nullable: false),
                    Action = table.Column<string>(maxLength: 128, nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    IconClass = table.Column<string>(maxLength: 128, nullable: true),
                    SN = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageLink", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_PageLink_PageLinkCategory",
                        column: x => x.LinkCategoryId,
                        principalTable: "PageLinkCategory",
                        principalColumn: "LinkCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBrandId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 128, nullable: false),
                    SellingUnitPrice = table.Column<double>(nullable: false),
                    UnitType = table.Column<string>(maxLength: 50, nullable: false),
                    Stock = table.Column<double>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_ProductBrand",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrand",
                        principalColumn: "ProductBrandId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(nullable: false),
                    ExpenseCategoryId = table.Column<int>(nullable: false),
                    ExpenseAmount = table.Column<double>(nullable: false),
                    ExpenseFor = table.Column<string>(maxLength: 256, nullable: true),
                    ExpensePaymentMethod = table.Column<string>(maxLength: 50, nullable: true),
                    ExpenseDate = table.Column<DateTime>(type: "date", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expense_ExpenseCategory",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategory",
                        principalColumn: "ExpenseCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expense_Registration",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Selling",
                columns: table => new
                {
                    SellingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    SellingSN = table.Column<int>(nullable: false),
                    SellingTotalPrice = table.Column<double>(nullable: false),
                    SellingDiscountAmount = table.Column<double>(nullable: false),
                    SellingDiscountPercentage = table.Column<double>(nullable: false, computedColumnSql: "(case when [SellingTotalPrice]=(0) then (0) else round(([SellingDiscountAmount]*(100))/[SellingTotalPrice],(2)) end)"),
                    SellingPaidAmount = table.Column<double>(nullable: false),
                    SellingReturnAmount = table.Column<double>(nullable: false),
                    SellingDueAmount = table.Column<double>(nullable: false, computedColumnSql: "(round(([SellingTotalPrice]+[SellingReturnAmount])-([SellingDiscountAmount]+[SellingPaidAmount]),(2)))"),
                    SellingPaymentStatus = table.Column<string>(unicode: false, maxLength: 4, nullable: false, computedColumnSql: "(case when (([SellingTotalPrice]+[SellingReturnAmount])-([SellingDiscountAmount]+[SellingPaidAmount]))<=(0) then 'Paid' else 'Due' end)"),
                    SellingDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selling", x => x.SellingId);
                    table.ForeignKey(
                        name: "FK_Selling_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Selling_Registration",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellingPayment",
                columns: table => new
                {
                    SellingPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    ReceiptSN = table.Column<int>(nullable: false),
                    PaidAmount = table.Column<double>(nullable: false),
                    PaymentMethod = table.Column<string>(maxLength: 50, nullable: true),
                    PaidDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellingPayment", x => x.SellingPaymentId);
                    table.ForeignKey(
                        name: "FK_SellingPayment_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SellingPayment_Registration",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(nullable: false),
                    VendorId = table.Column<int>(nullable: false),
                    PurchaseSN = table.Column<int>(nullable: false),
                    PurchaseTotalPrice = table.Column<double>(nullable: false),
                    PurchaseDiscountAmount = table.Column<double>(nullable: false),
                    PurchaseDiscountPercentage = table.Column<double>(nullable: false, computedColumnSql: "(case when [PurchaseTotalPrice]=(0) then (0) else round(([PurchaseDiscountAmount]*(100))/[PurchaseTotalPrice],(2)) end)"),
                    PurchasePaidAmount = table.Column<double>(nullable: false),
                    PurchaseReturnAmount = table.Column<double>(nullable: false),
                    PurchaseDueAmount = table.Column<double>(nullable: false, computedColumnSql: "(round(([PurchaseTotalPrice]+[PurchaseReturnAmount])-([PurchaseDiscountAmount]+[PurchasePaidAmount]),(2)))"),
                    PurchasePaymentStatus = table.Column<string>(unicode: false, maxLength: 4, nullable: false, computedColumnSql: "(case when (([PurchaseTotalPrice]+[PurchaseReturnAmount])-([PurchaseDiscountAmount]+[PurchasePaidAmount]))<=(0) then 'Paid' else 'Due' end)"),
                    PurchaseDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    MemoNumber = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchase_Registration",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchasePayment",
                columns: table => new
                {
                    PurchasePaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(nullable: false),
                    VendorId = table.Column<int>(nullable: false),
                    ReceiptSN = table.Column<int>(nullable: false),
                    PaidAmount = table.Column<double>(nullable: false),
                    PaymentMethod = table.Column<string>(maxLength: 50, nullable: true),
                    PaidDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasePayment", x => x.PurchasePaymentId);
                    table.ForeignKey(
                        name: "FK_PurchasePayment_Registration",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePayment_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorAdvance",
                columns: table => new
                {
                    VendorAdvanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    Advance = table.Column<double>(nullable: false),
                    AdvanceDate = table.Column<DateTime>(type: "date", nullable: false),
                    PaymentMethod = table.Column<string>(maxLength: 50, nullable: true),
                    AdvanceDetails = table.Column<string>(maxLength: 1000, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorAdvance", x => x.VendorAdvanceId);
                    table.ForeignKey(
                        name: "FK_VendorAdvance_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankLoanReturn",
                columns: table => new
                {
                    BankLoanReturnId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankLoanId = table.Column<int>(nullable: false),
                    ReturnAmount = table.Column<double>(nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "date", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankLoanReturn", x => x.BankLoanReturnId);
                    table.ForeignKey(
                        name: "FK_BankLoanReturn_BankLoan",
                        column: x => x.BankLoanId,
                        principalTable: "BankLoan",
                        principalColumn: "BankLoanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PageLinkAssign",
                columns: table => new
                {
                    LinkAssignId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(nullable: false),
                    LinkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageLinkAssign", x => x.LinkAssignId);
                    table.ForeignKey(
                        name: "FK_PageLinkAssign_PageLink",
                        column: x => x.LinkId,
                        principalTable: "PageLink",
                        principalColumn: "LinkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageLinkAssign_Registration",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TraderSharing",
                columns: table => new
                {
                    TraderSharingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    SharePrice = table.Column<double>(nullable: false, computedColumnSql: "([Quantity]*[UnitPrice])"),
                    IsGiven = table.Column<bool>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraderSharing", x => x.TraderSharingId);
                    table.ForeignKey(
                        name: "FK_TraderSharing_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraderSharing_Trader",
                        column: x => x.TraderId,
                        principalTable: "Trader",
                        principalColumn: "TraderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorCommission",
                columns: table => new
                {
                    VendorCommissionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Commission = table.Column<double>(nullable: false),
                    MonthDate = table.Column<DateTime>(type: "date", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCommission", x => x.VendorCommissionId);
                    table.ForeignKey(
                        name: "FK_VendorCommission_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorCommission_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellingList",
                columns: table => new
                {
                    SellingListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellingId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SellingQuantity = table.Column<double>(nullable: false),
                    SellingUnitPrice = table.Column<double>(nullable: false),
                    SellingPrice = table.Column<double>(nullable: false, computedColumnSql: "([SellingQuantity]*[SellingUnitPrice])")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellingList", x => x.SellingListId);
                    table.ForeignKey(
                        name: "FK_SellingList_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellingList_Selling",
                        column: x => x.SellingId,
                        principalTable: "Selling",
                        principalColumn: "SellingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellingPaymentList",
                columns: table => new
                {
                    SellingPaymentListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellingPaymentId = table.Column<int>(nullable: false),
                    SellingId = table.Column<int>(nullable: false),
                    SellingPaidAmount = table.Column<double>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellingPaymentList", x => x.SellingPaymentListId);
                    table.ForeignKey(
                        name: "FK_SellingPaymentList_Selling",
                        column: x => x.SellingId,
                        principalTable: "Selling",
                        principalColumn: "SellingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SellingPaymentList_SellingPayment",
                        column: x => x.SellingPaymentId,
                        principalTable: "SellingPayment",
                        principalColumn: "SellingPaymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseList",
                columns: table => new
                {
                    PurchaseListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    PurchaseQuantity = table.Column<double>(nullable: false),
                    PurchaseUnitPrice = table.Column<double>(nullable: false),
                    SellingUnitPrice = table.Column<double>(nullable: false),
                    PurchasePrice = table.Column<double>(nullable: false, computedColumnSql: "([PurchaseQuantity]*[PurchaseUnitPrice])")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseList", x => x.PurchaseListId);
                    table.ForeignKey(
                        name: "FK_PurchaseList_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseList_Purchase",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchasePaymentList",
                columns: table => new
                {
                    PurchasePaymentListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchasePaymentId = table.Column<int>(nullable: false),
                    PurchaseId = table.Column<int>(nullable: false),
                    PurchasePaidAmount = table.Column<double>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasePaymentList", x => x.PurchasePaymentListId);
                    table.ForeignKey(
                        name: "FK_PurchasePaymentList_Purchase",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePaymentList_PurchasePayment",
                        column: x => x.PurchasePaymentId,
                        principalTable: "PurchasePayment",
                        principalColumn: "PurchasePaymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5A71C6C4-9488-4BCC-A680-445A34C6E721", "5A71C6C4-9488-4BCC-A680-445A34C6E721", "admin", "ADMIN" },
                    { "F73A5277-2535-48A4-A371-300508ADDD2F", "F73A5277-2535-48A4-A371-300508ADDD2F", "sub-admin", "SUB-ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "A0456563-F978-4135-B563-97F23EA02FDA", 0, "A0456563-F978-4135-B563-97F23EA02FDA", "admin@gmail.com", true, true, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEDch3arYEB9dCAudNdsYEpVX7ryywa8f3ZIJSVUmEThAI50pLh9RyEu7NjGJccpOog==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Institution",
                columns: new[] { "InstitutionId", "Address", "City", "DialogTitle", "Email", "Established", "InstitutionLogo", "InstitutionName", "LocalArea", "Phone", "PostalCode", "State", "Website" },
                values: new object[] { 1, null, null, null, null, null, null, "Institution", null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Registration",
                columns: new[] { "RegistrationId", "Address", "DateofBirth", "Designation", "Email", "FatherName", "Image", "Name", "NationalID", "Phone", "PS", "Type", "UserName" },
                values: new object[] { 1, null, null, null, null, null, null, "Admin", null, null, "Admin_121", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "A0456563-F978-4135-B563-97F23EA02FDA", "5A71C6C4-9488-4BCC-A680-445A34C6E721" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BankDeposit_BankAccountId",
                table: "BankDeposit",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankLoan_BankAccountId",
                table: "BankLoan",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankLoanReturn_BankLoanId",
                table: "BankLoanReturn",
                column: "BankLoanId");

            migrationBuilder.CreateIndex(
                name: "IX_BankWithdrew_BankAccountId",
                table: "BankWithdrew",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhone_CustomerId",
                table: "CustomerPhone",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ExpenseCategoryId",
                table: "Expense",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_RegistrationId",
                table: "Expense",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_PageLink_LinkCategoryId",
                table: "PageLink",
                column: "LinkCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PageLinkAssign_LinkId",
                table: "PageLinkAssign",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_PageLinkAssign_RegistrationId",
                table: "PageLinkAssign",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductBrandId",
                table: "Product",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_RegistrationId",
                table: "Purchase",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_VendorId",
                table: "Purchase",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseList_ProductId",
                table: "PurchaseList",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseList_PurchaseId",
                table: "PurchaseList",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePayment_RegistrationId",
                table: "PurchasePayment",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePayment_VendorId",
                table: "PurchasePayment",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePaymentList_PurchaseId",
                table: "PurchasePaymentList",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePaymentList_PurchasePaymentId",
                table: "PurchasePaymentList",
                column: "PurchasePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Selling_CustomerId",
                table: "Selling",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Selling_RegistrationId",
                table: "Selling",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_SellingList_ProductId",
                table: "SellingList",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SellingList_SellingId",
                table: "SellingList",
                column: "SellingId");

            migrationBuilder.CreateIndex(
                name: "IX_SellingPayment_CustomerId",
                table: "SellingPayment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SellingPayment_RegistrationId",
                table: "SellingPayment",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_SellingPaymentList_SellingId",
                table: "SellingPaymentList",
                column: "SellingId");

            migrationBuilder.CreateIndex(
                name: "IX_SellingPaymentList_SellingPaymentId",
                table: "SellingPaymentList",
                column: "SellingPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_TraderSharing_ProductId",
                table: "TraderSharing",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TraderSharing_TraderId",
                table: "TraderSharing",
                column: "TraderId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorAdvance_VendorId",
                table: "VendorAdvance",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCommission_ProductId",
                table: "VendorCommission",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCommission_VendorId",
                table: "VendorCommission",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advance");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BankDeposit");

            migrationBuilder.DropTable(
                name: "BankLoanReturn");

            migrationBuilder.DropTable(
                name: "BankWithdrew");

            migrationBuilder.DropTable(
                name: "CustomerPhone");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "Institution");

            migrationBuilder.DropTable(
                name: "PageLinkAssign");

            migrationBuilder.DropTable(
                name: "PurchaseList");

            migrationBuilder.DropTable(
                name: "PurchasePaymentList");

            migrationBuilder.DropTable(
                name: "SellingList");

            migrationBuilder.DropTable(
                name: "SellingPaymentList");

            migrationBuilder.DropTable(
                name: "TraderSharing");

            migrationBuilder.DropTable(
                name: "VendorAdvance");

            migrationBuilder.DropTable(
                name: "VendorCommission");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BankLoan");

            migrationBuilder.DropTable(
                name: "ExpenseCategory");

            migrationBuilder.DropTable(
                name: "PageLink");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "PurchasePayment");

            migrationBuilder.DropTable(
                name: "Selling");

            migrationBuilder.DropTable(
                name: "SellingPayment");

            migrationBuilder.DropTable(
                name: "Trader");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "PageLinkCategory");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "ProductBrand");
        }
    }
}

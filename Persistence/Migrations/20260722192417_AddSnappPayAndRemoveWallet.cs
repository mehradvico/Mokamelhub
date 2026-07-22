using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSnappPayAndRemoveWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropColumn(
                name: "WalletPrice",
                table: "ProductOrders");

            migrationBuilder.AlterColumn<string>(
                name: "GatewayStatus",
                table: "Payments",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GatewayAmountRial",
                table: "Payments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GatewayCanceledAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GatewayLastError",
                table: "Payments",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GatewaySettledAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GatewayTransactionId",
                table: "Payments",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GatewayUpdatedAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GatewayVerifiedAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_GatewayTransactionId",
                table: "Payments",
                column: "GatewayTransactionId",
                unique: true,
                filter: "[GatewayTransactionId] IS NOT NULL");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM [Banks] WHERE [Id] = 5 AND ISNULL([Label], '') <> 'snapppay')
    THROW 51000, 'Bank Id 5 is already used by another payment provider.', 1;

IF NOT EXISTS (SELECT 1 FROM [Banks] WHERE [Id] = 5)
BEGIN
    SET IDENTITY_INSERT [Banks] ON;
    INSERT INTO [Banks] ([Id], [PictureId], [Label], [PaymentUrl], [VerficationUrl], [Verfication2Url], [Active], [Name])
    VALUES (5, NULL, 'snapppay', NULL, NULL, NULL, 1, N'اسنپ‌پی');
    SET IDENTITY_INSERT [Banks] OFF;
END;

IF NOT EXISTS (SELECT 1 FROM [Merchants] WHERE [BankId] = 5)
    INSERT INTO [Merchants] ([BankId], [Username], [Password], [PrivateKey], [TerminalKey], [MerchantNo], [Active])
    VALUES (5, NULL, NULL, NULL, NULL, NULL, 1);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_GatewayTransactionId",
                table: "Payments");

            migrationBuilder.Sql(@"
DELETE FROM [Merchants]
WHERE [BankId] = 5
  AND NOT EXISTS (SELECT 1 FROM [Payments] WHERE [Payments].[MerchantId] = [Merchants].[Id]);

DELETE FROM [Banks]
WHERE [Id] = 5
  AND [Label] = 'snapppay'
  AND NOT EXISTS (SELECT 1 FROM [Merchants] WHERE [Merchants].[BankId] = [Banks].[Id]);");

            migrationBuilder.DropColumn(
                name: "GatewayAmountRial",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayCanceledAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayLastError",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewaySettledAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayTransactionId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayUpdatedAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayVerifiedAt",
                table: "Payments");

            migrationBuilder.AlterColumn<string>(
                name: "GatewayStatus",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WalletPrice",
                table: "ProductOrders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<long>(type: "bigint", nullable: true),
                    ProductOrderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    IsIncrease = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Painding = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Wallets_ProductOrders_ProductOrderId",
                        column: x => x.ProductOrderId,
                        principalTable: "ProductOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_PaymentId",
                table: "Wallets",
                column: "PaymentId",
                unique: true,
                filter: "[PaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_ProductOrderId",
                table: "Wallets",
                column: "ProductOrderId",
                unique: true,
                filter: "[ProductOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");
        }
    }
}

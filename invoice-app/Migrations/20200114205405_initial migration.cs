using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace invoice_app.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    creation_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    modified_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    modification_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    active_status = table.Column<bool>(type: "bit", nullable: false),
                    title = table.Column<string>(type: "nVarChar(25)", nullable: true),
                    firstname = table.Column<string>(type: "nVarChar(50)", nullable: true),
                    lastname = table.Column<string>(type: "nVarChar(50)", nullable: true),
                    Address1 = table.Column<string>(type: "nVarChar(50)", nullable: true),
                    Address2 = table.Column<string>(type: "nVarChar(50)", nullable: true),
                    Address3 = table.Column<string>(type: "nVarChar(50)", nullable: true),
                    city = table.Column<string>(type: "nVarChar(50)", nullable: true),
                    country = table.Column<string>(type: "nVarChar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetail",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    creation_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    modified_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    modification_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    active_status = table.Column<bool>(type: "bit", nullable: false),
                    CardOwnerName = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    CardNumber = table.Column<string>(type: "VarChar(16)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    CVV = table.Column<string>(type: "VarChar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    creation_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    modified_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    modification_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    active_status = table.Column<bool>(type: "bit", nullable: false),
                    _number = table.Column<string>(type: "nVarChar(25)", nullable: false),
                    Name = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    Description = table.Column<string>(type: "nVarChar(Max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingData",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    creation_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    modified_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    modification_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    active_status = table.Column<bool>(type: "bit", nullable: false),
                    ship_number = table.Column<string>(type: "nVarChar(25)", nullable: true),
                    sales_rep = table.Column<string>(type: "nVarChar(50)", nullable: true),
                    ship_date = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ship_via = table.Column<string>(type: "nVarChar(50)", nullable: true),
                    terms = table.Column<string>(type: "nVarChar(100)", nullable: true),
                    due_date = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    creation_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    modified_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    modification_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    active_status = table.Column<bool>(type: "bit", nullable: false),
                    invoice_date = table.Column<DateTime>(type: "DateTime", nullable: false),
                    invoice_number = table.Column<string>(type: "nVarChar(25)", nullable: false),
                    ship_to_id = table.Column<int>(nullable: true),
                    bill_to_id = table.Column<int>(nullable: true),
                    end_notes = table.Column<string>(type: "nVarChar(Max)", nullable: true),
                    payment_details = table.Column<string>(type: "nVarChar(Max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.id);
                    table.ForeignKey(
                        name: "FK_Invoice_Address_bill_to_id",
                        column: x => x.bill_to_id,
                        principalTable: "Address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Address_ship_to_id",
                        column: x => x.ship_to_id,
                        principalTable: "Address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLine",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    creation_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    modified_by_user = table.Column<string>(type: "nVarChar(100)", nullable: false),
                    modification_time = table.Column<DateTime>(type: "DateTime", nullable: false),
                    active_status = table.Column<bool>(type: "bit", nullable: false),
                    invoice_id = table.Column<int>(nullable: true),
                    product_id = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "numeric(16, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Invoice_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "Invoice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Product_product_id",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_bill_to_id",
                table: "Invoice",
                column: "bill_to_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ship_to_id",
                table: "Invoice",
                column: "ship_to_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_invoice_id",
                table: "InvoiceLine",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_product_id",
                table: "InvoiceLine",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceLine");

            migrationBuilder.DropTable(
                name: "PaymentDetail");

            migrationBuilder.DropTable(
                name: "ShippingData");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}

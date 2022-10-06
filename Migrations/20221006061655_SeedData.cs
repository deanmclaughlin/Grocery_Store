using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassroomStart.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int(10)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    HomeAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    PhoneNumber = table.Column<long>(type: "bigint(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.CustomerID);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "inventory",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int(10)", nullable: false),
                    ProductName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    QuantityOnHand = table.Column<int>(type: "int(6)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsDiscontinued = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.ProductID);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<int>(type: "int(10)", nullable: false),
                    product_id = table.Column<int>(type: "int(10)", nullable: false),
                    QuantitySold = table.Column<int>(type: "int(6)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_orders_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_inventory_product_id",
                        column: x => x.product_id,
                        principalTable: "inventory",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.InsertData(
                table: "inventory",
                columns: new[] { "ProductID", "IsDiscontinued", "ProductName", "QuantityOnHand", "SalePrice" },
                values: new object[,]
                {
                    { 1, false, "Apples", 150, 1.50m },
                    { 2, false, "Oranges", 50, 1.25m },
                    { 3, false, "Bananas", 95, 0.99m },
                    { 4, false, "Bread", 85, 3.50m },
                    { 5, false, "Milk", 99, 6.50m },
                    { 6, false, "Eggs", 250, 5.35m },
                    { 7, false, "Bacon", 100, 8.99m },
                    { 8, false, "Coffee", 88, 12.50m },
                    { 9, false, "Ground Beef", 75, 17.55m },
                    { 10, false, "Steak", 75, 20.25m },
                    { 11, false, "Sausage", 110, 7.25m },
                    { 12, false, "Lettuce", 70, 1.99m },
                    { 13, false, "Tomato", 91, 0.89m },
                    { 14, false, "Potato", 88, 0.55m },
                    { 15, false, "Spinach", 45, 1.05m }
                });

            migrationBuilder.CreateIndex(
                name: "customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "product_id",
                table: "orders",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "inventory");
        }
    }
}

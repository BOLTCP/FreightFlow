using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreightFlow.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDofDelivery = table.Column<int>(type: "int", nullable: false),
                    NameofDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionofDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoadCapacity = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    CurrentLoad = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    LoadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "FreightInventory",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "FreightInventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FreightInventory_CustomerId",
                table: "FreightInventory",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FreightInventory_Customers_CustomerId",
                table: "FreightInventory",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

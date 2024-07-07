using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OIB = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_HouseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address_Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OibName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TechnicalContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TechnicalContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TechnicalContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Segment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KAM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TAM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EndpointId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdapterId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Assignee = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssignedToDepartment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CrmRootName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CrmTariffName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderClassification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsOnHold = table.Column<bool>(type: "bit", nullable: false),
                    Technology = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServiceRequestId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OnHoldReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CrmOrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}

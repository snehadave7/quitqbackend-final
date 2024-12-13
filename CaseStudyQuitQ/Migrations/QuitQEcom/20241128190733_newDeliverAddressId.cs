using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseStudyQuitQ.Migrations.QuitQEcom {
    public partial class newDeliverAddressId : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<int>(
            name: "AddressId",
            table: "Orders",
            nullable: true);

            migrationBuilder.AddForeignKey(
            name: "FK_Orders_DeliveryAddress_AddressId",
            table: "Orders",
            column: "AddressId",
            principalTable: "DeliveryAddress",
            principalColumn: "Id");



            //migrationBuilder.CreateTable(
            //    name: "ProductCategory",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductCategory", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FirstName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        LastName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        UserName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        ContactNumber = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Role = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SubCategory",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        CategoryId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SubCategory", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__SubCatego__Categ__3F466844",
            //            column: x => x.CategoryId,
            //            principalTable: "ProductCategory",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Cart",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Cart", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Cart__UserId__46E78A0C",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DeliveryAddress",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<int>(type: "int", nullable: true),
            //        Address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        City = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Pincode = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Phone = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Notes = table.Column<string>(type: "text", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DeliveryAddress", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__DeliveryA__UserI__5FB337D6",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Product",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
            //        Stock = table.Column<int>(type: "int", nullable: true),
            //        ImageUrl = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        SellerId = table.Column<int>(type: "int", nullable: true),
            //        CategoryId = table.Column<int>(type: "int", nullable: true),
            //        SubCategoryId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Product", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Product__Categor__4316F928",
            //            column: x => x.CategoryId,
            //            principalTable: "ProductCategory",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK__Product__SellerI__4222D4EF",
            //            column: x => x.SellerId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK__Product__SubCate__440B1D61",
            //            column: x => x.SubCategoryId,
            //            principalTable: "SubCategory",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CartItem",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CartId = table.Column<int>(type: "int", nullable: true),
            //        ProductId = table.Column<int>(type: "int", nullable: true),
            //        Quantity = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CartItem", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__CartItem__CartId__66603565",
            //            column: x => x.CartId,
            //            principalTable: "Cart",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK__CartItem__Produc__6754599E",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Orders",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<int>(type: "int", nullable: true),
            //        ProductId = table.Column<int>(type: "int", nullable: true),
            //        OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        Quantity = table.Column<int>(type: "int", nullable: true),
            //        OrderStatus = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        AddressId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Orders", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Orders__ProductI__6FE99F9F",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK__Orders__UserId__6EF57B66",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Orders_DeliveryAddress_AddressId",
            //            column: x => x.AddressId,
            //            principalTable: "DeliveryAddress",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Reviews",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<int>(type: "int", nullable: true),
            //        ProductId = table.Column<int>(type: "int", nullable: true),
            //        Rating = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
            //        Comment = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        ReviewDate = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Reviews", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Reviews__Product__787EE5A0",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK__Reviews__UserId__778AC167",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Payment",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OrderId = table.Column<int>(type: "int", nullable: true),
            //        Status = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Method = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Payment", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Payment__OrderId__74AE54BC",
            //            column: x => x.OrderId,
            //            principalTable: "Orders",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Cart_UserId",
            //    table: "Cart",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CartItem_CartId",
            //    table: "CartItem",
            //    column: "CartId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CartItem_ProductId",
            //    table: "CartItem",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DeliveryAddress_UserId",
            //    table: "DeliveryAddress",
            //    column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Orders_ProductId",
            //        table: "Orders",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Orders_UserId",
            //        table: "Orders",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Payment_OrderId",
            //        table: "Payment",
            //        column: "OrderId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Product_CategoryId",
            //        table: "Product",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Product_SellerId",
            //        table: "Product",
            //        column: "SellerId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Product_SubCategoryId",
            //        table: "Product",
            //        column: "SubCategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Reviews_ProductId",
            //        table: "Reviews",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Reviews_UserId",
            //        table: "Reviews",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_SubCategory_CategoryId",
            //        table: "SubCategory",
            //        column: "CategoryId");
            //}

            ////protected override void Down(MigrationBuilder migrationBuilder)
            //{
            //    migrationBuilder.DropTable(
            //        name: "CartItem");

            //    migrationBuilder.DropTable(
            //        name: "Payment");

            //    migrationBuilder.DropTable(
            //        name: "Reviews");

            //    migrationBuilder.DropTable(
            //        name: "Cart");

            //    migrationBuilder.DropTable(
            //        name: "Orders");

            //    migrationBuilder.DropTable(
            //        name: "Product");

            //    migrationBuilder.DropTable(
            //        name: "DeliveryAddress");

            //    migrationBuilder.DropTable(
            //        name: "SubCategory");

            //    migrationBuilder.DropTable(
            //        name: "Users");

            //    migrationBuilder.DropTable(
            //        name: "ProductCategory");
            //}
        }
    }
}

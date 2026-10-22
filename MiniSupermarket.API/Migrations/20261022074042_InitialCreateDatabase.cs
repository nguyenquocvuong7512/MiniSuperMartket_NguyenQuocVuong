using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RewardPoints = table.Column<int>(type: "int", nullable: false),
                    MembershipRank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountReceived = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Rau lá hữu cơ Farm", "Rau ăn lá canh tác hữu cơ theo mùa, thu hoạch trực tiếp tại vườn mỗi sớm" },
                    { 2, "Củ quả tươi sạch", "Củ quả sạch tự nhiên, không tồn dư thuốc BVTV và phân bón hóa học" },
                    { 3, "Trái cây vườn sinh thái", "Trái cây chín tự nhiên, không chất kích thích tăng trưởng và hóa chất bảo quản" },
                    { 4, "Nấm tươi & Nấm ăn hữu cơ", "Các loại nấm tươi trồng trên giá thể mùn cưa và rơm sạch tiêu chuẩn Organic" },
                    { 5, "Thịt sạch chăn thả tự nhiên", "Thịt heo, gà, bò thả đồi/thảo mộc, ăn thức ăn tự nhiên, không chất tạo nạc" },
                    { 6, "Thủy hải sản sinh thái", "Cá, tôm sinh thái nuôi đầm tự nhiên hoặc đánh bắt bền vững" },
                    { 7, "Trứng gia cầm thả vườn", "Trứng gà ta, trứng vịt cỏ thả vườn tự nhiên, giàu đạm và không kháng sinh" },
                    { 8, "Sữa tươi thanh trùng & Sữa hạt", "Sữa bò tươi nguyên chất từ trang trại và các loại sữa hạt nấu thủ công" },
                    { 9, "Gạo sạch & Nông sản ngũ cốc", "Gạo lứt, lúa rươi, nếp nương và các loại đậu hạt nguyên bản không tẩy trắng" },
                    { 10, "Mật ong rừng & Dược liệu vườn", "Mật ong tự nhiên, thảo mộc sấy lạnh, nghệ, gừng sẻ từ vùng đệm sinh thái" },
                    { 11, "Gia vị & Nước mắm thủ công", "Nước mắm ủ chượp truyền thống, muối hầm, tiêu rừng, tương ủ tự nhiên" },
                    { 12, "Dầu ép lạnh nguyên chất", "Dầu mè, dầu phộng, dầu sacha inchi ép cơ học không tinh luyện" },
                    { 13, "Rau củ sấy & Trà thảo mộc Farm", "Rau củ quả sấy thăng hoa giữ trọn khoáng chất, trà hoa thảo mộc sấy mộc" },
                    { 14, "Thực phẩm lên men tự nhiên", "Dưa cải chua men tự nhiên, kim chi hữu cơ, giấm táo thủ công không phụ gia" },
                    { 15, "Combo Nông Sản Tuần", "Gói nông sản tiện lợi theo mùa được tuyển chọn tươi mới từ trang trại" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 1, "123 Lê Lợi, Phường Bến Thành, Quận 1, Hồ Chí Minh", "Nguyễn Văn An", "Kim Cương", "0901122334", 1250 },
                    { 2, "45 Nguyễn Chí Thanh, Quận Đống Đa, Hà Nội", "Trần Thị Bình", "Vàng", "0918877665", 680 },
                    { 3, "78 Hùng Vương, Quận Hải Châu, Đà Nẵng", "Lê Văn Cường", "Bạc", "0983344556", 240 },
                    { 4, "12 Nguyễn Văn Cừ, Quận Ninh Kiều, Cần Thơ", "Phạm Thị Dung", "Thân Thiết", "0935566778", 85 },
                    { 5, "56 Trần Hưng Đạo, Thành phố Nha Trang, Khánh Hòa", "Hoàng Minh Đức", "Mới", "0977889900", 20 },
                    { 6, "89 Nguyễn Huệ, Phường Bến Nghé, Quận 1, Hồ Chí Minh", "Võ Thị Hoa", "Vàng", "0901234567", 720 },
                    { 7, "34 Trần Phú, Quận Hải Châu, Đà Nẵng", "Đặng Văn Hùng", "Bạc", "0912345678", 315 },
                    { 8, "67 Hai Bà Trưng, Quận Hoàn Kiếm, Hà Nội", "Bùi Thị Lan", "Thân Thiết", "0987654321", 150 },
                    { 9, "25 Lê Duẩn, Thành phố Huế, Thừa Thiên Huế", "Đỗ Minh Quân", "Mới", "0931234567", 45 },
                    { 10, "112 Phan Chu Trinh, Thành phố Hội An, Quảng Nam", "Ngô Thị Mai", "Bạc", "0971234568", 280 },
                    { 11, "43 Điện Biên Phủ, Quận Thanh Khê, Đà Nẵng", "Phan Văn Nam", "Vàng", "0908765432", 590 },
                    { 12, "78 Lạc Long Quân, Quận Tây Hồ, Hà Nội", "Lý Thị Ngọc", "Thân Thiết", "0913456789", 110 },
                    { 13, "156 Võ Văn Tần, Quận 3, Hồ Chí Minh", "Trương Minh Phúc", "Kim Cương", "0984567890", 1480 },
                    { 14, "29 Nguyễn Trãi, Quận 5, Hồ Chí Minh", "Mai Thị Thảo", "Bạc", "0936789012", 360 },
                    { 15, "91 Trần Hưng Đạo, Thành phố Vũng Tàu, Bà Rịa - Vũng Tàu", "Đinh Văn Tùng", "Vàng", "0978901234", 830 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "RoleName" },
                values: new object[,]
                {
                    { 1, "Toàn quyền toàn bộ hệ thống", "Admin" },
                    { 2, "Màn hình POS, Khách hàng thành viên", "Cashier" },
                    { 3, "Sản phẩm, Nhóm hàng, Nhập xuất kho", "Warehouse" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity", "Unit" },
                values: new object[,]
                {
                    { 101, "8930000001010", 1, 35000m, "Cải bó xôi hữu cơ Farm", 80, "Bó 500g" },
                    { 201, "8930000002017", 2, 38000m, "Cà rốt giống củ bản địa", 100, "Kg" },
                    { 301, "8930000003014", 3, 45000m, "Ổi lê hữu cơ chín cây", 60, "Kg" },
                    { 401, "8930000004011", 4, 42000m, "Nấm đùi gà tươi Organic", 50, "Gói 250g" },
                    { 501, "8930000005018", 5, 165000m, "Thịt ba chỉ heo thảo mộc", 40, "Khay 500g" },
                    { 601, "8930000006015", 6, 195000m, "Cá lăng sinh thái sông Đà", 30, "Kg" },
                    { 701, "8930000007012", 7, 48000m, "Trứng gà ta thảo mộc tự nhiên", 120, "Hộp 10 quả" },
                    { 801, "8930000008019", 8, 52000m, "Sữa bò tươi thanh trùng Organic", 45, "Chai 900ml" },
                    { 901, "8930000009016", 9, 125000m, "Gạo ST25 hữu cơ ruộng rươi", 70, "Túi 2kg" },
                    { 1001, "8930000010013", 10, 220000m, "Mật ong hoa rừng nguyên chất", 35, "Hũ 500ml" },
                    { 1101, "8930000011010", 11, 95000m, "Nước mắm truyền thống cá cơm 40N", 50, "Chai 500ml" },
                    { 1201, "8930000012017", 12, 110000m, "Dầu mè vàng ép lạnh thủ công", 40, "Chai 250ml" },
                    { 1301, "8930000013014", 13, 85000m, "Trà hoa cúc sấy lạnh tự nhiên", 55, "Hũ 80g" },
                    { 1401, "8930000014011", 14, 32000m, "Dưa cải củ lên men tự nhiên", 60, "Hũ 450g" },
                    { 1501, "8930000015018", 15, 250000m, "Combo Rau Củ Xanh Dinh Dưỡng Tuần", 20, "Set 5kg" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "Nguyễn Quản Trị", "123456", 1, "admin01" },
                    { 2, "Trần Giám Đốc", "123456", 1, "admin02" },
                    { 3, "Lê Thu Ngân", "123456", 2, "cashier01" },
                    { 4, "Phạm Bán Hàng", "123456", 2, "cashier02" },
                    { 5, "Hoàng Thu Ngân", "123456", 2, "cashier03" },
                    { 6, "Vũ Thị Quầy", "123456", 2, "cashier04" },
                    { 7, "Đỗ Bán Lẻ", "123456", 2, "cashier05" },
                    { 8, "Ngô Quản Kho", "123456", 3, "ware01" },
                    { 9, "Bùi Kiểm Kê", "123456", 3, "ware02" },
                    { 10, "Dương Thủ Kho", "123456", 3, "ware03" },
                    { 11, "Lý Nhập Hàng", "123456", 3, "ware04" },
                    { 12, "Đặng Hỗ Trợ", "123456", 1, "admin_backup" },
                    { 13, "Hồ Ca Chiều", "123456", 2, "cashier06" },
                    { 14, "Trương Vận Chuyển", "123456", 3, "ware05" },
                    { 15, "Mai Giám Sát", "123456", 1, "supervisor" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "AmountReceived", "ChangeAmount", "OrderDate", "PaymentMethod", "Status", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, 300000m, 35000m, new DateTime(2026, 10, 1, 8, 30, 0, 0, DateTimeKind.Unspecified), "Cash", "Completed", 265000m, 3 },
                    { 2, 326000m, 0m, new DateTime(2026, 10, 1, 9, 15, 0, 0, DateTimeKind.Unspecified), "Transfer", "Completed", 326000m, 4 },
                    { 3, 500000m, 80000m, new DateTime(2026, 10, 2, 10, 20, 0, 0, DateTimeKind.Unspecified), "Cash", "Completed", 420000m, 5 },
                    { 4, 150000m, 18000m, new DateTime(2026, 10, 2, 11, 45, 0, 0, DateTimeKind.Unspecified), "Cash", "Completed", 132000m, 6 },
                    { 5, 181000m, 0m, new DateTime(2026, 10, 3, 13, 10, 0, 0, DateTimeKind.Unspecified), "Transfer", "Completed", 181000m, 7 },
                    { 6, 150000m, 8000m, new DateTime(2026, 10, 3, 14, 25, 0, 0, DateTimeKind.Unspecified), "Cash", "Completed", 142000m, 3 },
                    { 7, 475000m, 0m, new DateTime(2026, 10, 4, 8, 40, 0, 0, DateTimeKind.Unspecified), "Transfer", "Completed", 475000m, 4 },
                    { 8, 300000m, 49000m, new DateTime(2026, 10, 4, 15, 30, 0, 0, DateTimeKind.Unspecified), "Cash", "Completed", 251000m, 5 },
                    { 9, 334000m, 0m, new DateTime(2026, 10, 5, 9, 5, 0, 0, DateTimeKind.Unspecified), "Transfer", "Completed", 334000m, 6 },
                    { 10, 350000m, 45000m, new DateTime(2026, 10, 5, 10, 50, 0, 0, DateTimeKind.Unspecified), "Cash", "Completed", 305000m, 7 },
                    { 11, 685000m, 0m, new DateTime(2026, 10, 6, 12, 15, 0, 0, DateTimeKind.Unspecified), "Transfer", "Completed", 685000m, 3 },
                    { 12, 300000m, 18000m, new DateTime(2026, 10, 7, 16, 20, 0, 0, DateTimeKind.Unspecified), "Cash", "Completed", 282000m, 4 },
                    { 13, 296000m, 0m, new DateTime(2026, 10, 8, 9, 35, 0, 0, DateTimeKind.Unspecified), "Transfer", "Completed", 296000m, 5 },
                    { 14, 100000m, 13000m, new DateTime(2026, 10, 9, 14, 45, 0, 0, DateTimeKind.Unspecified), "Cash", "Completed", 87000m, 6 },
                    { 15, 0m, 0m, new DateTime(2026, 10, 10, 17, 10, 0, 0, DateTimeKind.Unspecified), "Cash", "Cancelled", 340000m, 7 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "OrderId", "ProductId", "Quantity", "SubTotal", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 101, 2, 70000m, 35000m },
                    { 2, 1, 601, 1, 195000m, 195000m },
                    { 3, 2, 201, 2, 76000m, 38000m },
                    { 4, 2, 901, 2, 250000m, 125000m },
                    { 5, 3, 301, 2, 90000m, 45000m },
                    { 6, 3, 501, 2, 330000m, 165000m },
                    { 7, 4, 401, 2, 84000m, 42000m },
                    { 8, 4, 701, 1, 48000m, 48000m },
                    { 9, 5, 701, 2, 96000m, 48000m },
                    { 10, 5, 1301, 1, 85000m, 85000m },
                    { 11, 6, 1201, 1, 110000m, 110000m },
                    { 12, 6, 1401, 1, 32000m, 32000m },
                    { 13, 7, 1001, 2, 440000m, 220000m },
                    { 14, 7, 101, 1, 35000m, 35000m },
                    { 15, 8, 801, 3, 156000m, 52000m },
                    { 16, 8, 1101, 1, 95000m, 95000m },
                    { 17, 9, 401, 2, 84000m, 42000m },
                    { 18, 9, 1501, 1, 250000m, 250000m },
                    { 19, 10, 1301, 1, 85000m, 85000m },
                    { 20, 10, 1001, 1, 220000m, 220000m },
                    { 21, 11, 501, 3, 495000m, 165000m },
                    { 22, 11, 1101, 2, 190000m, 95000m },
                    { 23, 12, 1401, 1, 32000m, 32000m },
                    { 24, 12, 1501, 1, 250000m, 250000m },
                    { 25, 13, 201, 2, 76000m, 38000m },
                    { 26, 13, 1201, 2, 220000m, 110000m },
                    { 27, 14, 101, 1, 35000m, 35000m },
                    { 28, 14, 801, 1, 52000m, 52000m },
                    { 29, 15, 301, 2, 90000m, 45000m },
                    { 30, 15, 901, 2, 250000m, 125000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

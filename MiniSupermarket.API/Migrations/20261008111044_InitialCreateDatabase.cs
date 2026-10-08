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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Rau củ quả hữu cơ", "Rau xanh, củ quả sạch đạt chuẩn Organic, thu hoạch trong ngày" },
                    { 2, "Trái cây tươi", "Trái cây nội địa và nhập khẩu tươi ngon, không hóa chất bảo quản" },
                    { 3, "Thịt & Thủy hải sản", "Thịt heo sạch, bò thả rông, cá tươi sống đánh bắt tự nhiên" },
                    { 4, "Thực phẩm khô & Hạt", "Các loại hạt dinh dưỡng, ngũ cốc nguyên cám, gạo lứt" },
                    { 5, "Sữa & Sản phẩm từ sữa", "Sữa tươi thanh trùng, sữa chua hữu cơ, bơ phô mai tự nhiên" },
                    { 6, "Đồ uống & Trà thảo mộc", "Trà hoa cúc, mật ong rừng nguyên chất, nước ép trái cây tươi" },
                    { 7, "Gia vị & Dầu ăn hữu cơ", "Dầu ô liu, nước mắm truyền thống, hạt nêm hữu cơ, muối thảo dược" },
                    { 8, "Thực phẩm chay & Dinh dưỡng", "Thực phẩm chay sạch, đạm thực vật, bột dinh dưỡng các loại" },
                    { 9, "Thực phẩm chế biến sẵn sạch", "Đồ ăn vặt hữu cơ, bánh kẹo healthy, thực phẩm đóng hộp không phụ gia" },
                    { 10, "Mẹ và Bé (Sản phẩm hữu cơ)", "Thực phẩm ăn dặm, sữa hạt, sản phẩm chăm sóc an toàn cho bé" },
                    { 11, "Thảo dược & Đông dược", "Các loại sâm, nấm linh chi, thảo mộc bồi bổ sức khỏe tự nhiên" },
                    { 12, "Trứng gia cầm sạch", "Trứng gà ta, trứng gà ác, trứng vịt thả vườn không chất tăng trọng" },
                    { 13, "Bánh mì & Ngũ cốc tươi", "Bánh mì nguyên cám, bánh ngọt healthy làm từ bột mì hữu cơ" },
                    { 14, "Sản phẩm chăm sóc cá nhân Organic", "Mỹ phẩm thiên nhiên, dầu gội, sữa tắm hữu cơ an toàn cho da" },
                    { 15, "Sản phẩm gia dụng thân thiện môi trường", "Túi vải sinh học, ống hút tre, chất tẩy rửa hữu cơ lành tính" }
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
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity", "Unit" },
                values: new object[,]
                {
                    { 104, "8930000001041", 1, 42000m, "Bông cải xanh hữu cơ", 75, "Kg" },
                    { 204, "8930000002048", 2, 120000m, "Dâu tây Đà Lạt sạch", 40, "Hộp 500g" },
                    { 304, "8930000003045", 3, 290000m, "Tôm sú sinh thái tự nhiên", 25, "Kg" },
                    { 403, "8930000004039", 4, 160000m, "Hạt óc chó Mỹ tách vỏ", 60, "Túi 300g" },
                    { 503, "8930000005036", 5, 85000m, "Phô mai tự nhiên hữu cơ", 35, "Hộp 200g" },
                    { 603, "8930000006033", 6, 45000m, "Nước ép táo nguyên chất không đường", 50, "Chai 330ml" },
                    { 701, "8930000007016", 7, 210000m, "Dầu ô liu nguyên chất Extra Virgin", 30, "Chai 500ml" },
                    { 801, "8930000008013", 8, 55000m, "Sườn non thực vật hữu cơ", 80, "Gói 200g" },
                    { 901, "8930000009010", 9, 65000m, "Bánh quy yến mạch nguyên cám", 90, "Hộp 150g" },
                    { 1001, "8930000010014", 10, 48000m, "Bánh ăn dặm hữu cơ vị gạo lứt", 65, "Hộp 50g" },
                    { 1101, "8930000011011", 11, 115000m, "Nấm đông cô khô hữu cơ", 45, "Túi 100g" },
                    { 1201, "8930000012018", 12, 45000m, "Trứng gà ta thả vườn hữu cơ", 100, "Vỉ 10 quả" },
                    { 1301, "8930000013015", 13, 38000m, "Bánh mì sandwich nguyên cám", 40, "Túi 350g" },
                    { 1401, "8930000014012", 14, 180000m, "Dầu gội bồ kết thảo dược thiên nhiên", 20, "Chai 400ml" },
                    { 1501, "8930000015019", 15, 25000m, "Túi lưới đựng rau củ tái sử dụng", 150, "Set 3 cái" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

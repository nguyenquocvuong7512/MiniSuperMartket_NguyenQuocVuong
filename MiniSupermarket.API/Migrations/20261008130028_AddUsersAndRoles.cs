using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

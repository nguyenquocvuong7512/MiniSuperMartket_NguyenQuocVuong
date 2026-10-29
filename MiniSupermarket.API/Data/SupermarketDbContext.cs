using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Quan hệ User - Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ Order - OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ Product - OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // === 15 DANH MỤC (ORGANIC FARM) ===
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Rau lá hữu cơ Farm", Description = "Rau ăn lá canh tác hữu cơ theo mùa, thu hoạch trực tiếp tại vườn mỗi sớm" },
                new Category { CategoryId = 2, CategoryName = "Củ quả tươi sạch", Description = "Củ quả sạch tự nhiên, không tồn dư thuốc BVTV và phân bón hóa học" },
                new Category { CategoryId = 3, CategoryName = "Trái cây vườn sinh thái", Description = "Trái cây chín tự nhiên, không chất kích thích tăng trưởng và hóa chất bảo quản" },
                new Category { CategoryId = 4, CategoryName = "Nấm tươi & Nấm ăn hữu cơ", Description = "Các loại nấm tươi trồng trên giá thể mùn cưa và rơm sạch tiêu chuẩn Organic" },
                new Category { CategoryId = 5, CategoryName = "Thịt sạch chăn thả tự nhiên", Description = "Thịt heo, gà, bò thả đồi/thảo mộc, ăn thức ăn tự nhiên, không chất tạo nạc" },
                new Category { CategoryId = 6, CategoryName = "Thủy hải sản sinh thái", Description = "Cá, tôm sinh thái nuôi đầm tự nhiên hoặc đánh bắt bền vững" },
                new Category { CategoryId = 7, CategoryName = "Trứng gia cầm thả vườn", Description = "Trứng gà ta, trứng vịt cỏ thả vườn tự nhiên, giàu đạm và không kháng sinh" },
                new Category { CategoryId = 8, CategoryName = "Sữa tươi thanh trùng & Sữa hạt", Description = "Sữa bò tươi nguyên chất từ trang trại và các loại sữa hạt nấu thủ công" },
                new Category { CategoryId = 9, CategoryName = "Gạo sạch & Nông sản ngũ cốc", Description = "Gạo lứt, lúa rươi, nếp nương và các loại đậu hạt nguyên bản không tẩy trắng" },
                new Category { CategoryId = 10, CategoryName = "Mật ong rừng & Dược liệu vườn", Description = "Mật ong tự nhiên, thảo mộc sấy lạnh, nghệ, gừng sẻ từ vùng đệm sinh thái" },
                new Category { CategoryId = 11, CategoryName = "Gia vị & Nước mắm thủ công", Description = "Nước mắm ủ chượp truyền thống, muối hầm, tiêu rừng, tương ủ tự nhiên" },
                new Category { CategoryId = 12, CategoryName = "Dầu ép lạnh nguyên chất", Description = "Dầu mè, dầu phộng, dầu sacha inchi ép cơ học không tinh luyện" },
                new Category { CategoryId = 13, CategoryName = "Rau củ sấy & Trà thảo mộc Farm", Description = "Rau củ quả sấy thăng hoa giữ trọn khoáng chất, trà hoa thảo mộc sấy mộc" },
                new Category { CategoryId = 14, CategoryName = "Thực phẩm lên men tự nhiên", Description = "Dưa cải chua men tự nhiên, kim chi hữu cơ, giấm táo thủ công không phụ gia" },
                new Category { CategoryId = 15, CategoryName = "Combo Nông Sản Tuần", Description = "Gói nông sản tiện lợi theo mùa được tuyển chọn tươi mới từ trang trại" }
            );

            // === 15 SẢN PHẨM ĐẠI DIỆN TƯƠNG ỨNG TỪNG DANH MỤC ===
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Barcode = "8930000001010", ProductName = "Cải bó xôi hữu cơ Farm", CategoryId = 1, Price = 35000, StockQuantity = 80, Unit = "Bó 500g" },
                new Product { ProductId = 2, Barcode = "8930000002017", ProductName = "Cà rốt giống củ bản địa", CategoryId = 2, Price = 38000, StockQuantity = 100, Unit = "Kg" },
                new Product { ProductId = 3, Barcode = "8930000003014", ProductName = "Ổi lê hữu cơ chín cây", CategoryId = 3, Price = 45000, StockQuantity = 60, Unit = "Kg" },
                new Product { ProductId = 4, Barcode = "8930000004011", ProductName = "Nấm đùi gà tươi Organic", CategoryId = 4, Price = 42000, StockQuantity = 50, Unit = "Gói 250g" },
                new Product { ProductId = 5, Barcode = "8930000005018", ProductName = "Thịt ba chỉ heo thảo mộc", CategoryId = 5, Price = 165000, StockQuantity = 40, Unit = "Khay 500g" },
                new Product { ProductId = 6, Barcode = "8930000006015", ProductName = "Cá lăng sinh thái sông Đà", CategoryId = 6, Price = 195000, StockQuantity = 6, Unit = "Kg" },
                new Product { ProductId = 7, Barcode = "8930000007012", ProductName = "Trứng gà ta thảo mộc tự nhiên", CategoryId = 7, Price = 48000, StockQuantity = 120, Unit = "Hộp 10 quả" },
                new Product { ProductId = 8, Barcode = "8930000008019", ProductName = "Sữa bò tươi thanh trùng Organic", CategoryId = 8, Price = 52000, StockQuantity = 45, Unit = "Chai 900ml" },
                new Product { ProductId = 9, Barcode = "8930000009016", ProductName = "Gạo ST25 hữu cơ ruộng rươi", CategoryId = 9, Price = 125000, StockQuantity = 70, Unit = "Túi 2kg" },
                new Product { ProductId = 10, Barcode = "8930000010013", ProductName = "Mật ong hoa rừng nguyên chất", CategoryId = 10, Price = 220000, StockQuantity = 35, Unit = "Hũ 500ml" },
                new Product { ProductId = 11, Barcode = "8930000011010", ProductName = "Nước mắm truyền thống cá cơm 40N", CategoryId = 11, Price = 95000, StockQuantity = 50, Unit = "Chai 500ml" },
                new Product { ProductId = 12, Barcode = "8930000012017", ProductName = "Dầu mè vàng ép lạnh thủ công", CategoryId = 12, Price = 110000, StockQuantity = 40, Unit = "Chai 250ml" },
                new Product { ProductId = 13, Barcode = "8930000013014", ProductName = "Trà hoa cúc sấy lạnh tự nhiên", CategoryId = 13, Price = 85000, StockQuantity = 55, Unit = "Hũ 80g" },
                new Product { ProductId = 14, Barcode = "8930000014011", ProductName = "Dưa cải củ lên men tự nhiên", CategoryId = 14, Price = 32000, StockQuantity = 60, Unit = "Hũ 450g" },
                new Product { ProductId = 15, Barcode = "8930000015018", ProductName = "Combo Rau Củ Xanh Dinh Dưỡng Tuần", CategoryId = 15, Price = 250000, StockQuantity = 8, Unit = "Set 5kg" }
            );

            // === 15 KHÁCH HÀNG ===
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Nguyễn Văn An", PhoneNumber = "0901122334", Address = "123 Lê Lợi, Phường Bến Thành, Quận 1, Hồ Chí Minh", MembershipRank = "Kim Cương", RewardPoints = 1250 },
                new Customer { CustomerId = 2, CustomerName = "Trần Thị Bình", PhoneNumber = "0918877665", Address = "45 Nguyễn Chí Thanh, Quận Đống Đa, Hà Nội", MembershipRank = "Vàng", RewardPoints = 680 },
                new Customer { CustomerId = 3, CustomerName = "Lê Văn Cường", PhoneNumber = "0983344556", Address = "78 Hùng Vương, Quận Hải Châu, Đà Nẵng", MembershipRank = "Bạc", RewardPoints = 240 },
                new Customer { CustomerId = 4, CustomerName = "Phạm Thị Dung", PhoneNumber = "0935566778", Address = "12 Nguyễn Văn Cừ, Quận Ninh Kiều, Cần Thơ", MembershipRank = "Thân Thiết", RewardPoints = 85 },
                new Customer { CustomerId = 5, CustomerName = "Hoàng Minh Đức", PhoneNumber = "0977889900", Address = "56 Trần Hưng Đạo, Thành phố Nha Trang, Khánh Hòa", MembershipRank = "Mới", RewardPoints = 20 },
                new Customer { CustomerId = 6, CustomerName = "Võ Thị Hoa", PhoneNumber = "0901234567", Address = "89 Nguyễn Huệ, Phường Bến Nghé, Quận 1, Hồ Chí Minh", MembershipRank = "Vàng", RewardPoints = 720 },
                new Customer { CustomerId = 7, CustomerName = "Đặng Văn Hùng", PhoneNumber = "0912345678", Address = "34 Trần Phú, Quận Hải Châu, Đà Nẵng", MembershipRank = "Bạc", RewardPoints = 315 },
                new Customer { CustomerId = 8, CustomerName = "Bùi Thị Lan", PhoneNumber = "0987654321", Address = "67 Hai Bà Trưng, Quận Hoàn Kiếm, Hà Nội", MembershipRank = "Thân Thiết", RewardPoints = 150 },
                new Customer { CustomerId = 9, CustomerName = "Đỗ Minh Quân", PhoneNumber = "0931234567", Address = "25 Lê Duẩn, Thành phố Huế, Thừa Thiên Huế", MembershipRank = "Mới", RewardPoints = 45 },
                new Customer { CustomerId = 10, CustomerName = "Ngô Thị Mai", PhoneNumber = "0971234568", Address = "112 Phan Chu Trinh, Thành phố Hội An, Quảng Nam", MembershipRank = "Bạc", RewardPoints = 280 },
                new Customer { CustomerId = 11, CustomerName = "Phan Văn Nam", PhoneNumber = "0908765432", Address = "43 Điện Biên Phủ, Quận Thanh Khê, Đà Nẵng", MembershipRank = "Vàng", RewardPoints = 590 },
                new Customer { CustomerId = 12, CustomerName = "Lý Thị Ngọc", PhoneNumber = "0913456789", Address = "78 Lạc Long Quân, Quận Tây Hồ, Hà Nội", MembershipRank = "Thân Thiết", RewardPoints = 110 },
                new Customer { CustomerId = 13, CustomerName = "Trương Minh Phúc", PhoneNumber = "0984567890", Address = "156 Võ Văn Tần, Quận 3, Hồ Chí Minh", MembershipRank = "Kim Cương", RewardPoints = 1480 },
                new Customer { CustomerId = 14, CustomerName = "Mai Thị Thảo", PhoneNumber = "0936789012", Address = "29 Nguyễn Trãi, Quận 5, Hồ Chí Minh", MembershipRank = "Bạc", RewardPoints = 360 },
                new Customer { CustomerId = 15, CustomerName = "Đinh Văn Tùng", PhoneNumber = "0978901234", Address = "91 Trần Hưng Đạo, Thành phố Vũng Tàu, Bà Rịa - Vũng Tàu", MembershipRank = "Vàng", RewardPoints = 830 }
            );
            // === 3 VAI TRÒ ===
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleName = "Admin",
                    Description = "Toàn quyền toàn bộ hệ thống"
                },
                new Role
                {
                    Id = 2,
                    RoleName = "Cashier",
                    Description = "Màn hình POS, Khách hàng thành viên"
                },
                new Role
                {
                    Id = 3,
                    RoleName = "Warehouse",
                    Description = "Sản phẩm, Nhóm hàng, Nhập xuất kho"
                }
            );
            // === 15 TÀI KHOẢN NHÂN VIÊN ===
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin01",
                    Password = "123456",
                    FullName = "Nguyễn Quản Trị",
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    Username = "admin02",
                    Password = "123456",
                    FullName = "Trần Giám Đốc",
                    RoleId = 1
                },

                new User
                {
                    Id = 3,
                    Username = "cashier01",
                    Password = "123456",
                    FullName = "Lê Thu Ngân",
                    RoleId = 2
                },
                new User
                {
                    Id = 4,
                    Username = "cashier02",
                    Password = "123456",
                    FullName = "Phạm Bán Hàng",
                    RoleId = 2
                },
                new User
                {
                    Id = 5,
                    Username = "cashier03",
                    Password = "123456",
                    FullName = "Hoàng Thu Ngân",
                    RoleId = 2
                },
                new User
                {
                    Id = 6,
                    Username = "cashier04",
                    Password = "123456",
                    FullName = "Vũ Thị Quầy",
                    RoleId = 2
                },
                new User
                {
                    Id = 7,
                    Username = "cashier05",
                    Password = "123456",
                    FullName = "Đỗ Bán Lẻ",
                    RoleId = 2
                },

                new User
                {
                    Id = 8,
                    Username = "ware01",
                    Password = "123456",
                    FullName = "Ngô Quản Kho",
                    RoleId = 3
                },
                new User
                {
                    Id = 9,
                    Username = "ware02",
                    Password = "123456",
                    FullName = "Bùi Kiểm Kê",
                    RoleId = 3
                },
                new User
                {
                    Id = 10,
                    Username = "ware03",
                    Password = "123456",
                    FullName = "Dương Thủ Kho",
                    RoleId = 3
                },
                new User
                {
                    Id = 11,
                    Username = "ware04",
                    Password = "123456",
                    FullName = "Lý Nhập Hàng",
                    RoleId = 3
                },

                new User
                {
                    Id = 12,
                    Username = "admin_backup",
                    Password = "123456",
                    FullName = "Đặng Hỗ Trợ",
                    RoleId = 1
                },

                new User
                {
                    Id = 13,
                    Username = "cashier06",
                    Password = "123456",
                    FullName = "Hồ Ca Chiều",
                    RoleId = 2
                },

                new User
                {
                    Id = 14,
                    Username = "ware05",
                    Password = "123456",
                    FullName = "Trương Vận Chuyển",
                    RoleId = 3
                },

                new User
                {
                    Id = 15,
                    Username = "supervisor",
                    Password = "123456",
                    FullName = "Mai Giám Sát",
                    RoleId = 1
                }
            );
            // === 15 ĐƠN HÀNG ===
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, UserId = 3, OrderDate = new DateTime(2026, 10, 1, 8, 30, 0), TotalAmount = 265000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 300000, ChangeAmount = 35000 },
                new Order { OrderId = 2, UserId = 4, OrderDate = new DateTime(2026, 10, 1, 9, 15, 0), TotalAmount = 326000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 326000, ChangeAmount = 0 },
                new Order { OrderId = 3, UserId = 5, OrderDate = new DateTime(2026, 10, 2, 10, 20, 0), TotalAmount = 420000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 500000, ChangeAmount = 80000 },
                new Order { OrderId = 4, UserId = 6, OrderDate = new DateTime(2026, 10, 2, 11, 45, 0), TotalAmount = 132000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 150000, ChangeAmount = 18000 },
                new Order { OrderId = 5, UserId = 7, OrderDate = new DateTime(2026, 10, 3, 13, 10, 0), TotalAmount = 181000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 181000, ChangeAmount = 0 },
                new Order { OrderId = 6, UserId = 3, OrderDate = new DateTime(2026, 10, 3, 14, 25, 0), TotalAmount = 142000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 150000, ChangeAmount = 8000 },
                new Order { OrderId = 7, UserId = 4, OrderDate = new DateTime(2026, 10, 4, 8, 40, 0), TotalAmount = 475000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 475000, ChangeAmount = 0 },
                new Order { OrderId = 8, UserId = 5, OrderDate = new DateTime(2026, 10, 4, 15, 30, 0), TotalAmount = 251000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 300000, ChangeAmount = 49000 },
                new Order { OrderId = 9, UserId = 6, OrderDate = new DateTime(2026, 10, 5, 9, 5, 0), TotalAmount = 334000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 334000, ChangeAmount = 0 },
                new Order { OrderId = 10, UserId = 7, OrderDate = new DateTime(2026, 10, 5, 10, 50, 0), TotalAmount = 305000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 350000, ChangeAmount = 45000 },
                new Order { OrderId = 11, UserId = 3, OrderDate = new DateTime(2026, 10, 6, 12, 15, 0), TotalAmount = 685000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 685000, ChangeAmount = 0 },
                new Order { OrderId = 12, UserId = 4, OrderDate = new DateTime(2026, 10, 7, 16, 20, 0), TotalAmount = 282000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 300000, ChangeAmount = 18000 },
                new Order { OrderId = 13, UserId = 5, OrderDate = new DateTime(2026, 10, 8, 9, 35, 0), TotalAmount = 296000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 296000, ChangeAmount = 0 },
                new Order { OrderId = 14, UserId = 6, OrderDate = new DateTime(2026, 10, 9, 14, 45, 0), TotalAmount = 87000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 100000, ChangeAmount = 13000 },
                new Order { OrderId = 15, UserId = 7, OrderDate = new DateTime(2026, 10, 10, 17, 10, 0), TotalAmount = 340000, Status = "Cancelled", PaymentMethod = "Cash", AmountReceived = 0, ChangeAmount = 0 }
            );
            // === 30 CHI TIẾT ĐƠN HÀNG ===
            modelBuilder.Entity<OrderDetail>().HasData(
                // Đơn 1: Cải bó xôi (35k x 2) + Cá lăng (195k x 1) = 265k
                new OrderDetail { OrderDetailId = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 35000, SubTotal = 70000 },
                new OrderDetail { OrderDetailId = 2, OrderId = 1, ProductId = 6, Quantity = 1, UnitPrice = 195000, SubTotal = 195000 },

                // Đơn 2: Cà rốt (38k x 2) + Gạo ST25 (125k x 2) = 326k
                new OrderDetail { OrderDetailId = 3, OrderId = 2, ProductId = 2, Quantity = 2, UnitPrice = 38000, SubTotal = 76000 },
                new OrderDetail { OrderDetailId = 4, OrderId = 2, ProductId = 9, Quantity = 2, UnitPrice = 125000, SubTotal = 250000 },

                // Đơn 3: Ổi lê (45k x 2) + Thịt ba chỉ (165k x 2) = 420k
                new OrderDetail { OrderDetailId = 5, OrderId = 3, ProductId = 3, Quantity = 2, UnitPrice = 45000, SubTotal = 90000 },
                new OrderDetail { OrderDetailId = 6, OrderId = 3, ProductId = 5, Quantity = 2, UnitPrice = 165000, SubTotal = 330000 },

                // Đơn 4: Nấm đùi gà (42k x 2) + Trứng gà (48k x 1) = 132k
                new OrderDetail { OrderDetailId = 7, OrderId = 4, ProductId = 4, Quantity = 2, UnitPrice = 42000, SubTotal = 84000 },
                new OrderDetail { OrderDetailId = 8, OrderId = 4, ProductId = 7, Quantity = 1, UnitPrice = 48000, SubTotal = 48000 },

                // Đơn 5: Trứng gà (48k x 2) + Trà hoa cúc (85k x 1) = 181k
                new OrderDetail { OrderDetailId = 9, OrderId = 5, ProductId = 7, Quantity = 2, UnitPrice = 48000, SubTotal = 96000 },
                new OrderDetail { OrderDetailId = 10, OrderId = 5, ProductId = 13, Quantity = 1, UnitPrice = 85000, SubTotal = 85000 },

                // Đơn 6: Dầu mè (110k x 1) + Dưa cải củ (32k x 1) = 142k
                new OrderDetail { OrderDetailId = 11, OrderId = 6, ProductId = 12, Quantity = 1, UnitPrice = 110000, SubTotal = 110000 },
                new OrderDetail { OrderDetailId = 12, OrderId = 6, ProductId = 14, Quantity = 1, UnitPrice = 32000, SubTotal = 32000 },

                // Đơn 7: Mật ong hoa rừng (220k x 2) + Cải bó xôi (35k x 1) = 475k
                new OrderDetail { OrderDetailId = 13, OrderId = 7, ProductId = 10, Quantity = 2, UnitPrice = 220000, SubTotal = 440000 },
                new OrderDetail { OrderDetailId = 14, OrderId = 7, ProductId = 1, Quantity = 1, UnitPrice = 35000, SubTotal = 35000 },

                // Đơn 8: Sữa tươi (52k x 3) + Nước mắm (95k x 1) = 251k
                new OrderDetail { OrderDetailId = 15, OrderId = 8, ProductId = 8, Quantity = 3, UnitPrice = 52000, SubTotal = 156000 },
                new OrderDetail { OrderDetailId = 16, OrderId = 8, ProductId = 11, Quantity = 1, UnitPrice = 95000, SubTotal = 95000 },

                // Đơn 9: Nấm đùi gà (42k x 2) + Combo rau củ (250k x 1) = 334k
                new OrderDetail { OrderDetailId = 17, OrderId = 9, ProductId = 4, Quantity = 2, UnitPrice = 42000, SubTotal = 84000 },
                new OrderDetail { OrderDetailId = 18, OrderId = 9, ProductId = 15, Quantity = 1, UnitPrice = 250000, SubTotal = 250000 },

                // Đơn 10: Trà hoa cúc (85k x 1) + Mật ong hoa rừng (220k x 1) = 305k
                new OrderDetail { OrderDetailId = 19, OrderId = 10, ProductId = 13, Quantity = 1, UnitPrice = 85000, SubTotal = 85000 },
                new OrderDetail { OrderDetailId = 20, OrderId = 10, ProductId = 10, Quantity = 1, UnitPrice = 220000, SubTotal = 220000 },

                // Đơn 11: Thịt ba chỉ (165k x 3) + Nước mắm (95k x 2) = 685k
                new OrderDetail { OrderDetailId = 21, OrderId = 11, ProductId = 5, Quantity = 3, UnitPrice = 165000, SubTotal = 495000 },
                new OrderDetail { OrderDetailId = 22, OrderId = 11, ProductId = 11, Quantity = 2, UnitPrice = 95000, SubTotal = 190000 },

                // Đơn 12: Dưa cải củ (32k x 1) + Combo rau củ (250k x 1) = 282k
                new OrderDetail { OrderDetailId = 23, OrderId = 12, ProductId = 14, Quantity = 1, UnitPrice = 32000, SubTotal = 32000 },
                new OrderDetail { OrderDetailId = 24, OrderId = 12, ProductId = 15, Quantity = 1, UnitPrice = 250000, SubTotal = 250000 },

                // Đơn 13: Cà rốt (38k x 2) + Dầu mè (110k x 2) = 296k
                new OrderDetail { OrderDetailId = 25, OrderId = 13, ProductId = 2, Quantity = 2, UnitPrice = 38000, SubTotal = 76000 },
                new OrderDetail { OrderDetailId = 26, OrderId = 13, ProductId = 12, Quantity = 2, UnitPrice = 110000, SubTotal = 220000 },

                // Đơn 14: Cải bó xôi (35k x 1) + Sữa tươi (52k x 1) = 87k
                new OrderDetail { OrderDetailId = 27, OrderId = 14, ProductId = 1, Quantity = 1, UnitPrice = 35000, SubTotal = 35000 },
                new OrderDetail { OrderDetailId = 28, OrderId = 14, ProductId = 8, Quantity = 1, UnitPrice = 52000, SubTotal = 52000 },

                // Đơn 15 (Đơn hủy): Ổi lê (45k x 2) + Gạo ST25 (125k x 2) = 340k
                new OrderDetail { OrderDetailId = 29, OrderId = 15, ProductId = 3, Quantity = 2, UnitPrice = 45000, SubTotal = 90000 },
                new OrderDetail { OrderDetailId = 30, OrderId = 15, ProductId = 9, Quantity = 2, UnitPrice = 125000, SubTotal = 250000 }
            );
        }
    }
}
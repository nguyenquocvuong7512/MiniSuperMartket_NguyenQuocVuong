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

            // === 15 DANH MỤC ===
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Rau củ quả hữu cơ", Description = "Rau xanh, củ quả sạch đạt chuẩn Organic, thu hoạch trong ngày" },
                new Category { CategoryId = 2, CategoryName = "Trái cây tươi", Description = "Trái cây nội địa và nhập khẩu tươi ngon, không hóa chất bảo quản" },
                new Category { CategoryId = 3, CategoryName = "Thịt & Thủy hải sản", Description = "Thịt heo sạch, bò thả rông, cá tươi sống đánh bắt tự nhiên" },
                new Category { CategoryId = 4, CategoryName = "Thực phẩm khô & Hạt", Description = "Các loại hạt dinh dưỡng, ngũ cốc nguyên cám, gạo lứt" },
                new Category { CategoryId = 5, CategoryName = "Sữa & Sản phẩm từ sữa", Description = "Sữa tươi thanh trùng, sữa chua hữu cơ, bơ phô mai tự nhiên" },
                new Category { CategoryId = 6, CategoryName = "Đồ uống & Trà thảo mộc", Description = "Trà hoa cúc, mật ong rừng nguyên chất, nước ép trái cây tươi" },
                new Category { CategoryId = 7, CategoryName = "Gia vị & Dầu ăn hữu cơ", Description = "Dầu ô liu, nước mắm truyền thống, hạt nêm hữu cơ, muối thảo dược" },
                new Category { CategoryId = 8, CategoryName = "Thực phẩm chay & Dinh dưỡng", Description = "Thực phẩm chay sạch, đạm thực vật, bột dinh dưỡng các loại" },
                new Category { CategoryId = 9, CategoryName = "Thực phẩm chế biến sẵn sạch", Description = "Đồ ăn vặt hữu cơ, bánh kẹo healthy, thực phẩm đóng hộp không phụ gia" },
                new Category { CategoryId = 10, CategoryName = "Mẹ và Bé (Sản phẩm hữu cơ)", Description = "Thực phẩm ăn dặm, sữa hạt, sản phẩm chăm sóc an toàn cho bé" },
                new Category { CategoryId = 11, CategoryName = "Thảo dược & Đông dược", Description = "Các loại sâm, nấm linh chi, thảo mộc bồi bổ sức khỏe tự nhiên" },
                new Category { CategoryId = 12, CategoryName = "Trứng gia cầm sạch", Description = "Trứng gà ta, trứng gà ác, trứng vịt thả vườn không chất tăng trọng" },
                new Category { CategoryId = 13, CategoryName = "Bánh mì & Ngũ cốc tươi", Description = "Bánh mì nguyên cám, bánh ngọt healthy làm từ bột mì hữu cơ" },
                new Category { CategoryId = 14, CategoryName = "Sản phẩm chăm sóc cá nhân Organic", Description = "Mỹ phẩm thiên nhiên, dầu gội, sữa tắm hữu cơ an toàn cho da" },
                new Category { CategoryId = 15, CategoryName = "Sản phẩm gia dụng thân thiện môi trường", Description = "Túi vải sinh học, ống hút tre, chất tẩy rửa hữu cơ lành tính" }
            );

            // === 15 SẢN PHẨM ===
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 104, Barcode = "8930000001041", ProductName = "Bông cải xanh hữu cơ", CategoryId = 1, Price = 42000, StockQuantity = 75, Unit = "Kg" },
                new Product { ProductId = 204, Barcode = "8930000002048", ProductName = "Dâu tây Đà Lạt sạch", CategoryId = 2, Price = 120000, StockQuantity = 40, Unit = "Hộp 500g" },
                new Product { ProductId = 304, Barcode = "8930000003045", ProductName = "Tôm sú sinh thái tự nhiên", CategoryId = 3, Price = 290000, StockQuantity = 25, Unit = "Kg" },
                new Product { ProductId = 403, Barcode = "8930000004039", ProductName = "Hạt óc chó Mỹ tách vỏ", CategoryId = 4, Price = 160000, StockQuantity = 60, Unit = "Túi 300g" },
                new Product { ProductId = 503, Barcode = "8930000005036", ProductName = "Phô mai tự nhiên hữu cơ", CategoryId = 5, Price = 85000, StockQuantity = 35, Unit = "Hộp 200g" },
                new Product { ProductId = 603, Barcode = "8930000006033", ProductName = "Nước ép táo nguyên chất không đường", CategoryId = 6, Price = 45000, StockQuantity = 50, Unit = "Chai 330ml" },
                new Product { ProductId = 701, Barcode = "8930000007016", ProductName = "Dầu ô liu nguyên chất Extra Virgin", CategoryId = 7, Price = 210000, StockQuantity = 30, Unit = "Chai 500ml" },
                new Product { ProductId = 801, Barcode = "8930000008013", ProductName = "Sườn non thực vật hữu cơ", CategoryId = 8, Price = 55000, StockQuantity = 80, Unit = "Gói 200g" },
                new Product { ProductId = 901, Barcode = "8930000009010", ProductName = "Bánh quy yến mạch nguyên cám", CategoryId = 9, Price = 65000, StockQuantity = 90, Unit = "Hộp 150g" },
                new Product { ProductId = 1001, Barcode = "8930000010014", ProductName = "Bánh ăn dặm hữu cơ vị gạo lứt", CategoryId = 10, Price = 48000, StockQuantity = 65, Unit = "Hộp 50g" },
                new Product { ProductId = 1101, Barcode = "8930000011011", ProductName = "Nấm đông cô khô hữu cơ", CategoryId = 11, Price = 115000, StockQuantity = 45, Unit = "Túi 100g" },
                new Product { ProductId = 1201, Barcode = "8930000012018", ProductName = "Trứng gà ta thả vườn hữu cơ", CategoryId = 12, Price = 45000, StockQuantity = 100, Unit = "Vỉ 10 quả" },
                new Product { ProductId = 1301, Barcode = "8930000013015", ProductName = "Bánh mì sandwich nguyên cám", CategoryId = 13, Price = 38000, StockQuantity = 40, Unit = "Túi 350g" },
                new Product { ProductId = 1401, Barcode = "8930000014012", ProductName = "Dầu gội bồ kết thảo dược thiên nhiên", CategoryId = 14, Price = 180000, StockQuantity = 20, Unit = "Chai 400ml" },
                new Product { ProductId = 1501, Barcode = "8930000015019", ProductName = "Túi lưới đựng rau củ tái sử dụng", CategoryId = 15, Price = 25000, StockQuantity = 150, Unit = "Set 3 cái" }
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
                new Order { OrderId = 1, UserId = 3, OrderDate = new DateTime(2026, 10, 1, 8, 30, 0), TotalAmount = 204000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 250000, ChangeAmount = 46000 },
                new Order { OrderId = 2, UserId = 4, OrderDate = new DateTime(2026, 10, 1, 9, 15, 0), TotalAmount = 360000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 360000, ChangeAmount = 0 },
                new Order { OrderId = 3, UserId = 5, OrderDate = new DateTime(2026, 10, 2, 10, 20, 0), TotalAmount = 580000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 600000, ChangeAmount = 20000 },
                new Order { OrderId = 4, UserId = 6, OrderDate = new DateTime(2026, 10, 2, 11, 45, 0), TotalAmount = 160000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 200000, ChangeAmount = 40000 },
                new Order { OrderId = 5, UserId = 7, OrderDate = new DateTime(2026, 10, 3, 13, 10, 0), TotalAmount = 425000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 425000, ChangeAmount = 0 },
                new Order { OrderId = 6, UserId = 3, OrderDate = new DateTime(2026, 10, 3, 14, 25, 0), TotalAmount = 90000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 100000, ChangeAmount = 10000 },
                new Order { OrderId = 7, UserId = 4, OrderDate = new DateTime(2026, 10, 4, 8, 40, 0), TotalAmount = 630000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 630000, ChangeAmount = 0 },
                new Order { OrderId = 8, UserId = 5, OrderDate = new DateTime(2026, 10, 4, 15, 30, 0), TotalAmount = 245000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 300000, ChangeAmount = 55000 },
                new Order { OrderId = 9, UserId = 6, OrderDate = new DateTime(2026, 10, 5, 9, 5, 0), TotalAmount = 320000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 320000, ChangeAmount = 0 },
                new Order { OrderId = 10, UserId = 7, OrderDate = new DateTime(2026, 10, 5, 10, 50, 0), TotalAmount = 138000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 150000, ChangeAmount = 12000 },
                new Order { OrderId = 11, UserId = 3, OrderDate = new DateTime(2026, 10, 6, 12, 15, 0), TotalAmount = 870000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 870000, ChangeAmount = 0 },
                new Order { OrderId = 12, UserId = 4, OrderDate = new DateTime(2026, 10, 7, 16, 20, 0), TotalAmount = 250000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 300000, ChangeAmount = 50000 },
                new Order { OrderId = 13, UserId = 5, OrderDate = new DateTime(2026, 10, 8, 9, 35, 0), TotalAmount = 465000, Status = "Completed", PaymentMethod = "Transfer", AmountReceived = 465000, ChangeAmount = 0 },
                new Order { OrderId = 14, UserId = 6, OrderDate = new DateTime(2026, 10, 9, 14, 45, 0), TotalAmount = 113000, Status = "Completed", PaymentMethod = "Cash", AmountReceived = 150000, ChangeAmount = 37000 },
                new Order { OrderId = 15, UserId = 7, OrderDate = new DateTime(2026, 10, 10, 17, 10, 0), TotalAmount = 500000, Status = "Cancelled", PaymentMethod = "Cash", AmountReceived = 0, ChangeAmount = 0 }
            );
            // === 30 CHI TIẾT ĐƠN HÀNG ===
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { OrderDetailId = 1, OrderId = 1, ProductId = 104, Quantity = 2, UnitPrice = 42000, SubTotal = 84000 },
                new OrderDetail { OrderDetailId = 2, OrderId = 1, ProductId = 603, Quantity = 1, UnitPrice = 45000, SubTotal = 45000 },

                new OrderDetail { OrderDetailId = 3, OrderId = 2, ProductId = 204, Quantity = 2, UnitPrice = 120000, SubTotal = 240000 },
                new OrderDetail { OrderDetailId = 4, OrderId = 2, ProductId = 901, Quantity = 2, UnitPrice = 65000, SubTotal = 130000 },

                new OrderDetail { OrderDetailId = 5, OrderId = 3, ProductId = 304, Quantity = 1, UnitPrice = 290000, SubTotal = 290000 },
                new OrderDetail { OrderDetailId = 6, OrderId = 3, ProductId = 503, Quantity = 2, UnitPrice = 85000, SubTotal = 170000 },

                new OrderDetail { OrderDetailId = 7, OrderId = 4, ProductId = 403, Quantity = 1, UnitPrice = 160000, SubTotal = 160000 },
                new OrderDetail { OrderDetailId = 8, OrderId = 4, ProductId = 603, Quantity = 0, UnitPrice = 45000, SubTotal = 0 },

                new OrderDetail { OrderDetailId = 9, OrderId = 5, ProductId = 701, Quantity = 2, UnitPrice = 210000, SubTotal = 420000 },
                new OrderDetail { OrderDetailId = 10, OrderId = 5, ProductId = 1301, Quantity = 1, UnitPrice = 38000, SubTotal = 38000 },

                new OrderDetail { OrderDetailId = 11, OrderId = 6, ProductId = 1201, Quantity = 2, UnitPrice = 45000, SubTotal = 90000 },
                new OrderDetail { OrderDetailId = 12, OrderId = 6, ProductId = 1501, Quantity = 0, UnitPrice = 25000, SubTotal = 0 },

                new OrderDetail { OrderDetailId = 13, OrderId = 7, ProductId = 304, Quantity = 2, UnitPrice = 290000, SubTotal = 580000 },
                new OrderDetail { OrderDetailId = 14, OrderId = 7, ProductId = 104, Quantity = 1, UnitPrice = 42000, SubTotal = 42000 },

                new OrderDetail { OrderDetailId = 15, OrderId = 8, ProductId = 801, Quantity = 3, UnitPrice = 55000, SubTotal = 165000 },
                new OrderDetail { OrderDetailId = 16, OrderId = 8, ProductId = 603, Quantity = 1, UnitPrice = 45000, SubTotal = 45000 },

                new OrderDetail { OrderDetailId = 17, OrderId = 9, ProductId = 403, Quantity = 2, UnitPrice = 160000, SubTotal = 320000 },
                new OrderDetail { OrderDetailId = 18, OrderId = 9, ProductId = 1501, Quantity = 0, UnitPrice = 25000, SubTotal = 0 },

                new OrderDetail { OrderDetailId = 19, OrderId = 10, ProductId = 1301, Quantity = 1, UnitPrice = 38000, SubTotal = 38000 },
                new OrderDetail { OrderDetailId = 20, OrderId = 10, ProductId = 1001, Quantity = 2, UnitPrice = 48000, SubTotal = 96000 },

                new OrderDetail { OrderDetailId = 21, OrderId = 11, ProductId = 701, Quantity = 3, UnitPrice = 210000, SubTotal = 630000 },
                new OrderDetail { OrderDetailId = 22, OrderId = 11, ProductId = 1101, Quantity = 2, UnitPrice = 115000, SubTotal = 230000 },

                new OrderDetail { OrderDetailId = 23, OrderId = 12, ProductId = 1401, Quantity = 1, UnitPrice = 180000, SubTotal = 180000 },
                new OrderDetail { OrderDetailId = 24, OrderId = 12, ProductId = 1501, Quantity = 2, UnitPrice = 25000, SubTotal = 50000 },

                new OrderDetail { OrderDetailId = 25, OrderId = 13, ProductId = 204, Quantity = 2, UnitPrice = 120000, SubTotal = 240000 },
                new OrderDetail { OrderDetailId = 26, OrderId = 13, ProductId = 1101, Quantity = 2, UnitPrice = 115000, SubTotal = 230000 },

                new OrderDetail { OrderDetailId = 27, OrderId = 14, ProductId = 104, Quantity = 1, UnitPrice = 42000, SubTotal = 42000 },
                new OrderDetail { OrderDetailId = 28, OrderId = 14, ProductId = 603, Quantity = 1, UnitPrice = 45000, SubTotal = 45000 },

                new OrderDetail { OrderDetailId = 29, OrderId = 15, ProductId = 503, Quantity = 2, UnitPrice = 85000, SubTotal = 170000 },
                new OrderDetail { OrderDetailId = 30, OrderId = 15, ProductId = 403, Quantity = 1, UnitPrice = 160000, SubTotal = 160000 }
            );
        }
    }
}